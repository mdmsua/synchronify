exports = async function (details) {
  const authorizationToken = context.values.get('authorizationToken');
  const total = { albums: 0, tracks: 0, playlists: 0, duration: 0 };
  details.forEach(({ albums, tracks, playlists, duration }) => {
    total.albums += albums;
    total.tracks += tracks;
    total.playlists += playlists;
    total.duration += duration;
  });
  const body = {
    from: {
      email: 'syncify@mdmsua.com'
    },
    personalizations: [
      {
        to: [
          {
            email: 'mdmsua@gmail.com'
          }
        ],
        dynamic_template_data: {
          total,
          details
        }
      }
    ],
    template_id: 'd-d72ce9f74a5f465e9f3a6d6502a2b35e'
  }
  await context.http.post({
    url: 'https://api.sendgrid.com/v3/mail/send',
    body: JSON.stringify(body),
    headers: {
      'Content-Type': ['application/json'],
      'Authorization': [`Bearer ${authorizationToken}`]
    }
  });
};
