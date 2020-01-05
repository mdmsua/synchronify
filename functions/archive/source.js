async function archiveDocuments(s3, key, documents) {
  await s3.PutObject({
    'Bucket': 'snapshotify',
    'Key': key,
    'Body': EJSON.stringify(documents),
    'ContentType': 'application/json',
    'StorageClass': 'DEEP_ARCHIVE'
  });
}

async function archiveCollection(collection, id, db, s3) {
  const date = new Date().toISOString().slice(0, 10);
  const key = `${id}/${collection}/${date}`;
  const documents = await db.collection(collection).find().toArray();
  await archiveDocuments(s3, key, documents);
}

exports = async function (id) {
  const db = context.services.get('mongodb-atlas').db(id);
  const s3 = context.services.get('aws').s3('eu-central-1');
  const collections = [
    'albums',
    'tracks',
    'playlists'
  ];
  await Promise.all(collections.map(collection => archiveCollection(collection, id, db, s3)));
};
