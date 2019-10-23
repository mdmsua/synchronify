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

async function getTokens(authorizationCode) {
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
      code: authorizationCode,
      redirect_uri: process.env.REDIRECT_URI,
      grant_type: "authorization_code"
    });
  return { accessToken, refreshToken, expiresIn };
}

async function refreshTokens(refreshToken) {
  const {
    body: {
      access_token: accessToken,
      expires_in: expiresIn
    }
  } = await request
    .post("https://accounts.spotify.com/api/token")
    .auth(clientId, clientSecret, { type: "basic" })
    .type("form")
    .send({
      refresh_token: refreshToken,
      grant_type: "refresh_token"
    });
  return { accessToken, expiresIn };
}

async function getCurrentUser(accessToken) {
  const { body } = await request
    .get("https://api.spotify.com/v1/me")
    .auth(accessToken, { type: "bearer" })
    .accept("json");
  return body;
}

async function getCurrentUsersTracks(accessToken, offset) {
  const { body } = await request
    .get("https://api.spotify.com/v1/me/tracks")
    .query({ offset })
    .auth(accessToken, { type: "bearer" })
    .accept("json");
  return body.items;
}

async function getCurrentUsersAlbums(accessToken, offset) {
  const { body } = await request
    .get("https://api.spotify.com/v1/me/albums")
    .query({ offset })
    .auth(accessToken, { type: "bearer" })
    .accept("json");
  return body.items;
}


module.exports = {
  getAuthorizationUrl,
  getCurrentUser,
  getCurrentUsersTracks,
  getCurrentUsersAlbums,
  refreshTokens,
  getTokens
};
