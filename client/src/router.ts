import Vue from "vue";
import Router from "vue-router";
import Admin from "./views/Admin.vue";
import AdminCurrencies from "./views/AdminCurrencies.vue";
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
      redirect: { name: "admin.participants" },
      children: [
        {
          path: "currencies",
          name: "admin.currencies",
          component: AdminCurrencies
        },
        {
          path: "participants",
          name: "admin.participants",
          component: AdminParticipants,
          children: [
            {
              path: "add",
              name: "admin.participants.add",
              component: AdminParticipantsAdd
            },
            {
              path: ":id/edit",
              name: "admin.participants.edit",
              component: AdminParticipantsEdit,
              props: true
            }
          ]
        }
      ]
    }
  ]
});
