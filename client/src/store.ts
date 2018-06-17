import Vue from "vue";
import Vuex from "vuex";
import { getAll } from "./services/participants.service";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    participants: []
  },
  mutations: {
    ["PARTICIPANTS_SET"](state, participants) {
      state.participants = participants;
    }
  },
  actions: {
    async ["PARTICIPANTS_FETCH"]({ commit }) {
      commit("PARTICIPANTS_SET", await getAll());
    }
  }
});
