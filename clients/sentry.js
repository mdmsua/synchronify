const Sentry = require("@sentry/node");

if (process.env.ENV !== "dev") {
  Sentry.init({ dsn: process.env.SENTRY_DSN });
}

module.exports = {
  requestHandler: Sentry.Handlers.requestHandler,
  errorHandler: Sentry.Handlers.errorHandler
};
