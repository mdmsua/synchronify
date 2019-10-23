const { CronJob } = require("cron");
const mongoClient = require("./clients/mongo");
const spotifyClient = require("./clients/spotify");

async function onTick() {
  const refreshTokens = await mongoClient.getRefreshTokens();
  for (const { _id: id, token: refreshToken } of refreshTokens) {
    let accessToken = await mongoClient.getAccessToken(id);
    if (!accessToken) {
      const { accessToken: token, expiresIn } = await spotifyClient.refreshTokens(refreshToken);
      await mongoClient.saveAccessToken(id, accessToken, expiresIn);
      accessToken = token;
    }
    const timestamp = new Date();
    const [tracks, albums] = await Promise.all([syncTracks(id, accessToken), syncAlbums(id, accessToken)]);
    await mongoClient.saveLog(id, timestamp, new Date(), { tracks, albums });
    console.log(`${id}: ${tracks} tracks, ${albums} albums`);
  }
}

async function syncTracks(id, accessToken) {
  await mongoClient.clearTracks(id);
  let offset = 0;
  let sync = true;
  while (sync) {
    const tracks = await spotifyClient.getCurrentUsersTracks(accessToken, offset);
    if (tracks.length === 0) {
      sync = false;
      continue;
    }
    await mongoClient.saveTracks(id, tracks);
    offset += tracks.length;
  }
  return offset;
}

async function syncAlbums(id, accessToken) {
  await mongoClient.clearAlbums(id);
  let offset = 0;
  let sync = true;
  while (sync) {
    const albums = await spotifyClient.getCurrentUsersAlbums(accessToken, offset);
    if (albums.length === 0) {
      sync = false;
      continue;
    }
    await mongoClient.saveAlbums(id, albums);
    offset += albums.length;
  }
  return offset;
}

const job = new CronJob(process.env.CRON_TIME, onTick, null, false, "Europe/Berlin");

module.exports = job;
