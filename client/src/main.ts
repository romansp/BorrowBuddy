import Vue from "vue";
import App from "./App.vue";
import AppInsights from "./applicationInsights";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
import "./vendor";

Vue.config.productionTip = false;

if (process.env.NODE_ENV === "production") {
  Vue.use(AppInsights, {
    id: "6b071885-0406-4b17-a5b1-7afd31af4f51",
    router
  });
}

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
