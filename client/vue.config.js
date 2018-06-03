const fs = require('fs');

module.exports = {
  pwa: {
    name: 'Borrow Buddy'
  },
  css: {
    loaderOptions: {
      sass: {
        data: fs.readFileSync('src/scss/app/_variables.scss')
      }
    }
  }
};
