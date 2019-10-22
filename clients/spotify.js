const querystring = require("querystring");
const request = require("superagent");

const clientId = process.env.CLIENT_ID;
const clientSecret = process.env.CLIENT_SECRET;
const redirectUri = process.env.REDIRECT_URI;

const scope = "user-read-private playlist-read-private user-library-read";

function getAuthorizationUrl({ state }) {
  return `https://accounts.spotify.com/authorize?${querystring.stringify({
    response_type: "code",
    client_id: clientId,
    scope,
    redirect_uri: redirectUri,
    state
  })}`;
}

async function getTokens({ code }) {
  const {
    body: {
      access_token: accessToken,
      refresh_token: refreshToken,
      expires_in: expiresIn
    }
  } = await request
    .post("https://accounts.spotify.com/api/token")
    .auth(clientId, clientSecret, { type: "basic" })
    .type("form")
    .send({
      code,
      redirect_uri: process.env.REDIRECT_URI,
      grant_type: "authorization_code"
    });
  return { accessToken, refreshToken };
}

async function getUserProfile({ code }) {
  const { accessToken } = await getTokens({ code });
  const { body } = await request
    .get("https://api.spotify.com/v1/me")
    .auth(accessToken, { type: "bearer" })
    .accept("json");
  return body;
}

module.exports = {
  getAuthorizationUrl,
  getUserProfile
};
