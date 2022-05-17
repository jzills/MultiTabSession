const createProxyMiddleware = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:37700';

const context =  [
  "/session",
  "/applicationstate",
  "/hubs"
];

module.exports = function(app) {
  console.log(target)
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    ws: true
    // headers: {
    //   Connection: 'Keep-Alive'
    // }
  });

  app.use(appProxy);
};
