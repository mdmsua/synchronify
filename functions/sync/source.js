async function fetch(resource, http, token, offset) {
  const { statusCode, body } = await http.get({
    url: `https://api.spotify.com/v1/me/${resource}?offset=${offset}`,
    headers: {
      'Authorization': [`Bearer ${token}`]
    }
  });

  if (statusCode === 200) {
    return JSON.parse(body.text());
  }

  console.error(statusCode, body.text());
}

async function sync(resource, collection, http, token) {
  await collection.deleteMany({});
  let offset = 0;
  let sync = true;
  while (sync) {
    const { items } = await fetch(resource, http, token, offset);
    if (items.length === 0) {
      sync = false;
      continue;
    }
    await collection.insertMany(items);
    offset += items.length;
  }
  return offset;
}

exports = async function ({ fullDocument }) {
  const { _id: id, token } = fullDocument;
  const db = context.services.get('mongodb-atlas').db(id);
  const tracksCollection = db.collection('tracks');
  const albumsCollection = db.collection('albums');
  const playlistsCollection = db.collection('playlists');
  const logsCollection = db.collection('logs');
  const timestamp = new Date();
  const [albums, tracks, playlists] = await Promise.all([
    sync('albums', albumsCollection, context.http, token),
    sync('tracks', tracksCollection, context.http, token),
    sync('playlists', playlistsCollection, context.http, token)
  ]);
  await logsCollection.insertOne({ started: timestamp, finished: new Date(), duration: new Date() - timestamp, tracks, albums, playlists });
}
