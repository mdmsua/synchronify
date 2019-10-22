const express = require('express');
const querystring = require('querystring');
const crypto = require('crypto');
const request = require('superagent');
const dotenv = require('dotenv');
const cookieParser = require('cookie-parser');
const { CronJob } = require('cron');
const { MongoClient } = require('mongodb');

dotenv.config();

const app = express();

const client = new MongoClient(process.env.MONGO_CONNECTION_STRING, { useNewUrlParser: true, useUnifiedTopology: true });

const port = process.env.PORT || 8080;
const clientId = process.env.CLIENT_ID;
const clientSecret = process.env.CLIENT_SECRET;

const stateKey = 'spotify_auth_state';

app.get('/', (req, res) => {
  const state = crypto.randomBytes(32).toString('hex');
  res.cookie(stateKey, state, { httpOnly: true });
  const scope = 'user-read-private playlist-read-private user-library-read';
  const url = `https://accounts.spotify.com/authorize?${querystring.stringify({ response_type: 'code', client_id: clientId, scope, redirect_uri: process.env.REDIRECT_URI, state })}`;
  res.redirect(url);
});

app.get('/callback', cookieParser(), async (req, res) => {
  const { code, state, error } = req.query;
  if (error) {
    return res.status(400).send(error);
  }

  if (req.cookies[stateKey] !== state) {
    return res.status(400).send('States do not match');
  }

  res.clearCookie(stateKey);
  const { body: { access_token: accessToken, refresh_token: refreshToken, expires_in: expiresIn } } = await request
    .post('https://accounts.spotify.com/api/token')
    .auth(clientId, clientSecret, { type: 'basic' })
    .type('form')
    .send({ code, redirect_uri: process.env.REDIRECT_URI, grant_type: 'authorization_code' });

  try {
    const { body: { id } } = await request.get('https://api.spotify.com/v1/me').auth(accessToken, { type: 'bearer' }).accept('json');
    console.log(id);
  } catch (error) {
    console.error(error);
  }
  return res.sendStatus(200);
});

app.listen(port);

if (process.env.ENV !== 'dev') {
  new CronJob('0 * * * * *', async () => {
    await client.connect();
    const db = client.db('test');
    await db.collection('timestamps').insertOne({ timestamp: new Date() });
    await client.close();
  }, null, true, 'Europe/Berlin');
}
