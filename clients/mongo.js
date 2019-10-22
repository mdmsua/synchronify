const { MongoClient } = require("mongodb");

const client = new MongoClient(process.env.MONGO_CONNECTION_STRING, {
  useNewUrlParser: true,
  useUnifiedTopology: true,
  useRecoveryToken: true,
  retryReads: true,
  retryWrites: true,
  w: "majority"
});

async function saveUserProfile(profile) {
  await client.connect();
  const collection = client.db("spotify").collection("profiles");
  await collection.replaceOne({ _id: profile.id }, profile, { upsert: true });
  await client.close();
}

module.exports = { saveUserProfile };
