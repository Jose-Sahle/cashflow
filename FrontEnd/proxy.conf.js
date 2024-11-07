const { env } = require('process');

const target = 'http://banktransactionservice-api'

const PROXY_CONFIG = [
  {
    context: [
      "/api/BankTransaction",
   ],
    proxyTimeout: 10000,
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
