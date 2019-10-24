module.exports = {
  pwa: {
    name: "Borrow Buddy"
  },
  css: {
    loaderOptions: {
      sass: {
        prependData: `
          @import '@/styles/app/_variables.scss';
        `,
      },
    },
  },
};
