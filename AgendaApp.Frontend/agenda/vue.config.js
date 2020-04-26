module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:51044',
        changeOrigin: true,
        secure: false,
        pathRewrite: { '^/api': '/api' },
        logLevel: 'debug',
      },
    },
  },
};
