async function report(details) {
  const authorizationToken = context.values.get('authorizationToken');
  const reportSender = context.values.get('reportSender');
  const reportReceiver = context.values.get('reportReceiver');
  const reportTemplate = context.values.get('reportTemplate');
  const total = { albums: 0, tracks: 0, playlists: 0, duration: 0 };
  details.forEach(({ albums, tracks, playlists, duration }) => {
    total.albums += albums;
    total.tracks += tracks;
    total.playlists += playlists;
    total.duration += duration;
  });
  const body = {
    from: {
      email: reportSender
    },
    personalizations: [
      {
        to: [
          {
            email: reportReceiver
          }
        ],
        dynamic_template_data: {
          total,
          details
        }
      }
    ],
    template_id: reportTemplate
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

exports = report;

if (process.env.TEST) {
  module.exports = report;
}
