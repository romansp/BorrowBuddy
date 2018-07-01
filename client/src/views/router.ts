import Vue from "vue";
import Router from "vue-router";
import Admin from "./views/Admin.vue";
import AdminCurrencies from "./views/AdminCurrencies.vue";
import AdminCurrenciesAdd from "./views/AdminCurrenciesAdd.vue";
import AdminCurrenciesEdit from "./views/AdminCurrenciesEdit.vue";
import AdminParticipants from "./views/AdminParticipants.vue";
import AdminParticipantsAdd from "./views/AdminParticipantsAdd.vue";
import AdminParticipantsEdit from "./views/AdminParticipantsEdit.vue";
import Home from "./views/Home.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/admin",
      name: "admin",
      component: Admin,
      redirect: { name: "admin.participant" },
      children: [
        {
          path: "currencies",
          name: "admin.currency",
          component: AdminCurrencies,
          children: [
            {
              path: "add",
              name: "admin.currency.add",
              component: AdminParticipantsAdd
            },
            {
              path: ":id/edit",
              name: "admin.currency.edit",
              component: AdminParticipantsEdit,
              props: true
            }
          ]
        },
        {
          path: "participants",
          name: "admin.participant",
          component: AdminParticipants,
          children: [
            {
              path: "add",
              name: "admin.participant.add",
              component: AdminParticipantsAdd
            },
            {
              path: ":id/edit",
              name: "admin.participant.edit",
              component: AdminParticipantsEdit,
              props: true
            }
          ]
        }
      ]
    }
  ]
});
