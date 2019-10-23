const { MongoClient } = require("mongodb");

const client = new MongoClient(process.env.MONGO_CONNECTION_STRING, {
  useNewUrlParser: true,
  useUnifiedTopology: true,
  useRecoveryToken: true,
  retryReads: true,
  retryWrites: true,
  w: "majority"
});

function getDatabase(name) {
  return client.db(name);
}

async function getAccessTokensCollection(expiration = 3600) {
  const collection = getDatabase("spotify").collection("access_tokens");
  await collection.createIndexes([
    {
      key: { timestamp: -1 },
      background: true,
      expireAfterSeconds: expiration
    },
    { key: { token: 1 }, background: true }
  ]);
  return collection;
}

async function getRefreshTokensCollection() {
  const collection = getDatabase("spotify").collection("refresh_tokens");
  await collection.createIndexes([
    { key: { timestamp: -1 }, background: true },
    { key: { token: 1 }, background: true }
  ]);
  return collection;
}

async function saveUser(user) {
  const collection = getDatabase("spotify").collection("users");
  await collection.replaceOne({ _id: user.id }, user, { upsert: true });
}

async function saveAccessToken(id, token, expiration) {
  const collection = await getAccessTokensCollection(expiration);
  await collection.replaceOne(
    { _id: id },
    Object.assign({}, { token, timestamp: new Date() }),
    { upsert: true }
  );
}

async function saveRefreshToken(id, token) {
  const collection = await getRefreshTokensCollection();
  await collection.replaceOne(
    { _id: id },
    Object.assign({}, { token, timestamp: new Date() }),
    { upsert: true }
  );
}

async function getAccessTokens() {
  const collection = await getAccessTokensCollection();
  return await collection.find().toArray();
}

async function getRefreshTokens() {
  const collection = await getRefreshTokensCollection();
  return await collection.find().toArray();
}

async function getAccessToken(id) {
  const collection = await getAccessTokensCollection();
  const document = await collection.findOne(
    { _id: id },
    { projection: { token: 1 } }
  );
  if (document) {
    return document.token;
  }
  return null;
}

async function getTracksCollection(id) {
  const collection = getDatabase(id).collection("tracks");
  await collection.createIndexes([
    { key: { "track.name": "text" }, background: true },
    { key: { "track.artists.name": 1 }, background: true },
    { key: { "track.album.name": 1 }, background: true },
    { key: { "track.album.artists.name": 1 }, background: true }
  ]);
  return collection;
}

async function getAlbumsCollection(id) {
  const collection = getDatabase(id).collection("albums");
  await collection.createIndexes([
    { key: { "album.name": "text" }, background: true },
    { key: { "album.artists.name": 1 }, background: true },
    { key: { "album.genres": 1 }, background: true }
  ]);
  return collection;
}

async function getLogsCollection(id) {
  const collection = getDatabase(id).collection("logs");
  await collection.createIndex(
    { started_at: -1, finished_at: -1 },
    { background: true }
  );
  return collection;
}

async function clearTracks(id) {
  const collection = await getTracksCollection(id);
  await collection.drop();
}

async function clearAlbums(id) {
  const collection = await getAlbumsCollection(id);
  await collection.drop();
}

async function saveTracks(id, tracks) {
  const collection = await getTracksCollection(id);
  await collection.insertMany(tracks, { ordered: false });
}

async function saveAlbums(id, albums) {
  const collection = await getAlbumsCollection(id);
  await collection.insertMany(albums, { ordered: false });
}

async function saveLog(
  id,
  started,
  finished,
  { tracks, albums } = { tracks: 0, albums: 0 }
) {
  const collection = await getLogsCollection(id);
  await collection.insertOne({
    started_at: started,
    finished_at: finished,
    synced_tracks: tracks,
    synced_albums: albums
  });
}

module.exports = {
  connect: client.connect.bind(client),
  close: client.close.bind(client),
  saveUser,
  saveAccessToken,
  saveRefreshToken,
  getAccessTokens,
  getRefreshTokens,
  getAccessToken,
  clearAlbums,
  clearTracks,
  saveAlbums,
  saveTracks,
  saveLog
};
