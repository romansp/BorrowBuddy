module.exports = {
  root: true,
  env: {
    node: true
  },
  plugins: ["prettier"],
  extends: ["plugin:vue/recommended", "@vue/airbnb", "@vue/typescript", "prettier"],
  rules: {
    quotes: [2, "double"],
    "quote-props": [1, "as-needed"],
    "nonblock-statement-body-position": [2, "beside"],
    "no-console": process.env.NODE_ENV === "production" ? "error" : "off",
    "no-debugger": process.env.NODE_ENV === "production" ? "error" : "off",
    "no-use-before-define": "off",
    "no-restricted-syntax": ["error", "LabeledStatement", "WithStatement"],
    "import/prefer-default-export": "off",
    "import/no-cycle": "off",

    "@typescript-eslint/no-use-before-define": "off"
  },

  settings: {
    "import/parsers": {
      "@typescript-eslint/parser": [".ts", ".tsx"]
    },
    "import/resolver": {
      // use <root>/tsconfig.json
      typescript: {}
    }
  },
  parserOptions: {
    project: process.env.NODE_ENV === "production" ? "./tsconfig.json" : undefined,
    parser: "@typescript-eslint/parser"
  }
};
