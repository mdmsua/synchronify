function getAuthorizationUrl({ clientId, redirectUri, state }) {
  const scope = "user-read-private playlist-read-private user-library-read";
  const queryString = `response_type=code&client_id=${clientId}&scope=${encodeURIComponent(scope)}&redirect_uri=${encodeURIComponent(redirectUri)}&state=${state}`;
  return `https://accounts.spotify.com/authorize?${queryString}`;
}

exports = function(payload, response) {
  const clientId = context.values.get('clientId');
  const redirectUri = context.values.get('redirectUri');
  const stateSecret = context.values.get('stateSecret');
  const uri = getAuthorizationUrl({ clientId, redirectUri, state: stateSecret });
  response.setStatusCode(302);
  response.setHeader('Location', uri);
};
