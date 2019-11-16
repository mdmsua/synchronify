async function getAccessToken(context, token) {
  const authorizationCode = context.values.get('authorizationCode');
  const { statusCode, body } = await context.http.post({
    url: 'https://accounts.spotify.com/api/token',
    body: [
      `refresh_token=${token}`,
      'grant_type=refresh_token'
    ].join('&'),
    headers: {
      'Content-Type': ['application/x-www-form-urlencoded'],
      'Authorization': [`Basic ${authorizationCode}`]
    }
  });

  if (statusCode === 200) {
    return JSON.parse(body.text());
  }

  console.error(statusCode, body.text());
}

exports = async function ({ documentKey }) {
  const id = documentKey._id;
  const db = context.services.get('mongodb-atlas').db('spotify');
  const { token } = await db.collection('refresh_tokens').findOne({ _id: id });
  const { access_token: accessToken } = await getAccessToken(context, token);
  await db.collection('access_tokens').insertOne({ _id: id, token: accessToken, timestamp: new Date() });
}
