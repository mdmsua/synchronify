const express = require("express");
const crypto = require("crypto");
const dotenv = require("dotenv");
const cookieParser = require("cookie-parser");

dotenv.config();

const mongoClient = require("./clients/mongo");
const sentryClient = require("./clients/sentry");
const spotifyClient = require("./clients/spotify");

const job = require('./job');

const app = express();

const stateKey = "spotify_auth_state";

if (process.env.ENV !== "dev") {
  app.use(sentryClient.requestHandler());
}

app.get("/", (req, res) => {
  const state = crypto.randomBytes(32).toString("hex");
  res.cookie(stateKey, state, { httpOnly: true });
  const url = spotifyClient.getAuthorizationUrl({ state });
  return res.redirect(url);
});

app.get("/callback", cookieParser(), async (req, res) => {
  const { code, state, error } = req.query;
  if (error) {
    return res.status(400).send(error);
  }

  if (req.cookies[stateKey] !== state) {
    return res.status(400).send("States do not match");
  }

  res.clearCookie(stateKey);

  const {
    accessToken,
    refreshToken,
    expiresIn
  } = await spotifyClient.getTokens(code);
  const user = await spotifyClient.getCurrentUser(accessToken);
  await Promise.all([
    mongoClient.saveUser(user),
    mongoClient.saveAccessToken(user.id, accessToken, expiresIn),
    mongoClient.saveRefreshToken(user.id, refreshToken)
  ]);
  return res.sendStatus(200);
});

if (process.env.ENV !== "dev") {
  app.use(sentryClient.errorHandler());
  job.start();
}

app.listen(process.env.PORT, async () => await mongoClient.connect());
