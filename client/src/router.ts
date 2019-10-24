import Vue from "vue";
import Router, { RouteConfig, RouterOptions } from "vue-router";
import FlowAdd from "@/components/FlowAdd.vue";
import Admin from "./views/Admin.vue";
import AdminCurrencies from "./views/AdminCurrencies.vue";
import AdminCurrenciesAdd from "./views/AdminCurrenciesAdd.vue";
import AdminCurrenciesEdit from "./views/AdminCurrenciesEdit.vue";
import AdminParticipants from "./views/AdminParticipants.vue";
import AdminParticipantsAdd from "./views/AdminParticipantsAdd.vue";
import AdminParticipantsEdit from "./views/AdminParticipantsEdit.vue";
import History from "./views/History.vue";
import Home from "./views/Home.vue";

Vue.use(Router);

// prettier-ignore
export type Keys =
  | "home"
    | "flow.add"
    | "history"
  | "admin"
    | "admin.currency"
      | "admin.currency.add"
      | "admin.currency.edit"
    | "admin.participant"
      | "admin.participant.add"
      | "admin.participant.edit";

export const routes: { readonly [R in Keys]: RouteConfig } = {
  home: {
    path: "/",
    name: "home",
    component: Home
  },
  "flow.add": {
    path: "",
    name: "flow.add",
    component: FlowAdd
  },
  history: {
    path: "history",
    name: "history",
    component: History
  },
  admin: {
    path: "/admin",
    name: "admin",
    component: Admin
  },
  "admin.currency": {
    path: "currencies",
    name: "admin.currency",
    component: AdminCurrencies
  },
  "admin.currency.add": {
    path: "add",
    name: "admin.currency.add",
    component: AdminCurrenciesAdd
  },
  "admin.currency.edit": {
    path: ":id/edit",
    name: "admin.currency.edit",
    component: AdminCurrenciesEdit,
    props: true
  },
  "admin.participant": {
    path: "participants",
    name: "admin.participant",
    component: AdminParticipants
  },
  "admin.participant.add": {
    path: "add",
    name: "admin.participant.add",
    component: AdminParticipantsAdd
  },
  "admin.participant.edit": {
    path: ":id/edit",
    name: "admin.participant.edit",
    component: AdminParticipantsEdit,
    props: true
  }
};

const options: RouterOptions = {
  routes: [
    {
      ...routes.home,
      redirect: routes["flow.add"],
      children: [routes.history, routes["flow.add"]]
    },
    {
      ...routes.admin,
      redirect: routes["admin.participant"],
      children: [
        {
          ...routes["admin.currency"],
          children: [routes["admin.currency.add"], routes["admin.currency.edit"]]
        },
        {
          ...routes["admin.participant"],
          children: [routes["admin.participant.add"], routes["admin.participant.edit"]]
        }
      ]
    }
  ]
};

export default new Router(options);
