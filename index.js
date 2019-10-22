const express = require("express");
const crypto = require("crypto");
const dotenv = require("dotenv");
const cookieParser = require("cookie-parser");

dotenv.config();

const mongoClient = require("./clients/mongo");
const sentryClient = require("./clients/sentry");
const spotifyClient = require("./clients/spotify");

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

  const profile = await spotifyClient.getUserProfile({ code });
  await mongoClient.saveUserProfile(profile);

  return res.sendStatus(200);
});

if (process.env.ENV !== "dev") {
  app.use(sentryClient.errorHandler());
}

app.listen(process.env.PORT);
