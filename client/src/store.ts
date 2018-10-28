import Vue from "vue";
import Vuex from "vuex";
import { getAll as getCurrencies } from "./services/currency.service";
import { balance as getBalance, getAll as getParticipants } from "./services/participants.service";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    participants: {},
    currencies: {},
    balance: {
      from: "",
      to: "",
      code: "",
      amount: 0
    }
  },
  mutations: {
    ["PARTICIPANTS_SET"](state, participants) {
      for (const participant of participants) {
        Vue.set(state.participants, participant.id, participant);
      }
    },
    ["CURRENCIES_SET"](state, currencies) {
      for (const currency of currencies) {
        Vue.set(state.currencies, currency.code, currency);
      }
    },
    ["BALANCE_SET"](state, { from, to, amount, code }) {
      state.balance = {
        code,
        from,
        to,
        amount
      };
    }
  },
  actions: {
    async ["PARTICIPANTS_FETCH"]({ commit }) {
      commit("PARTICIPANTS_SET", await getParticipants());
    },
    async ["CURRENCIES_FETCH"]({ commit }) {
      commit("CURRENCIES_SET", await getCurrencies());
    },
    async ["BALANCE_FETCH"]({ commit }, { from, to, code }) {
      commit("BALANCE_SET", {
        from,
        to,
        amount: await getBalance(from, to, code)
      });
    }
  }
});
