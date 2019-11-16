async function getProfile(context, accessToken) {
  const { body, statusCode } = await context.http.get({
    url: 'https://api.spotify.com/v1/me',
    headers: {
      'Authorization': [`Bearer ${accessToken}`],
      'Accept': ['application/json']
    }
  });

  if (statusCode === 200) {
    return JSON.parse(body.text());
  }

  console.error(statusCode, body.getText());
}

async function getTokens(context, code) {
  const redirectUri = context.values.get('redirectUri');
  const authorizationCode = context.values.get('authorizationCode');
  const { statusCode, body } = await context.http.post({
    url: 'https://accounts.spotify.com/api/token',
    body: [
      `code=${encodeURIComponent(code)}`,
      `redirect_uri=${encodeURIComponent(redirectUri)}`,
      'grant_type=authorization_code'
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

exports = async function (payload, response) {
  const stateSecret = context.values.get('stateSecret');
  const { code, state, error } = payload.query;
  if (error) {
    response.setStatusCode(400);
    response.setBody(error);
  } else if (state !== stateSecret) {
    response.setStatusCode(400);
    response.setBody('State mismatch');
  } else {
    const {
      access_token: accessToken,
      refresh_token: refreshToken
    } = await getTokens(context, code);
    if (accessToken && refreshToken) {
      const profile = await getProfile(context, accessToken);
      if (profile) {
        const db = context.services
          .get('mongodb-atlas')
          .db('spotify');
        await db.collection('access_tokens')
          .updateOne({ _id: profile.id }, { $set: { token: accessToken, timestamp: new Date() } }, { upsert: true });
        await db.collection('refresh_tokens')
          .updateOne({ _id: profile.id }, { $set: { token: refreshToken, timestamp: new Date() } }, { upsert: true });
        response.setStatusCode(200);
        response.setHeader('Content-Type', 'text/plain');
        response.setBody(JSON.stringify(profile));
      }
    }
  }
};
