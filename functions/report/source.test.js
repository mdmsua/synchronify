const sut = require('./source');

const contextValuesGetMock = jest.fn().mockImplementation(arg => {
  switch (arg) {
    case 'authorizationToken': return 'auth';
    case 'reportSender': return 'from@email.com';
    case 'reportReceiver': return 'to@email.com';
    case 'reportTemplate': return 'default';
    default: return null;
  }
});

const contextHttpPostMock =jest.fn();

global.context = {
  values: {
    get: contextValuesGetMock
  },
  http: {
    post: contextHttpPostMock
  }
}

test('creates total object', async () => {
  const details = [
    { albums: 32, tracks: 2048, playlists: 16, duration: 4096 },
    { albums: 64, tracks: 1024, playlists: 32, duration: 2048 }];
  const actual = await sut(details);
  expect(contextHttpPostMock).toBeCalledWith({
    url: 'https://api.sendgrid.com/v3/mail/send',
    body: JSON.stringify({
      from: {
        email: 'from@email.com'
      },
      personalizations: [
        {
          to: [
            {
              email: 'to@email.com'
            }
          ],
          dynamic_template_data: {
            total: { albums: 96, tracks: 3072, playlists: 48, duration: 6144 },
            details
          }
        }
      ],
      template_id: 'default'
    }),
    headers: {
      'Content-Type': ['application/json'],
      'Authorization': ['Bearer auth']
    }
  });
});
