/* eslint-disable no-unused-vars */
import Vue from 'vue';
import Vuex from 'vuex';
import Axios from 'axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    clients: [],
    appointments: [],
  },
  mutations: {
    addAppointment(state, appointment) {
      state.appointments.push(appointment);
    },
    updateAppointment(state, [appointment, appointmentId]) {
      const newAppointments = state.appointments.filter((o) => o.id !== appointmentId);
      newAppointments.push(appointment);
      state.appointments = newAppointments;
      console.log(state.appointments.length);
    },
    addAppointments(state, appointments) {
      state.appointments = appointments;
    },
    removeAppointment(state, appointmentId) {
      const newAppointments = state.appointments.filter((o) => o.id !== appointmentId);
      state.appointments = newAppointments;
    },
    addClients(state, client) {
      state.clients = client;
    },
  },
  actions: {
    getClients({ commit }) {
      Axios.get('/api/clients')
        .then((result) => commit('addClients', result.data))
        .catch(console.error);
    },
    addAppointment({ commit }, [clientId, toUpdateAppointment]) {
      const config = { headers: { 'Content-Type': 'application/json' } };
      Axios.post(`/api/clients/${clientId}/appointments/`, toUpdateAppointment, config)
        .then((result) => commit('addAppointment', result.data))
        .catch(console.error);
    },
    getAppointments({ commit }, clientId) {
      Axios.get(`/api/clients/${clientId}/appointments`)
        .then((result) => commit('addAppointments', result.data))
        .catch(console.error);
    },
    updateAppointment({ commit }, [clientId, appointmentId, toUpdateAppointment]) {
      const config = { headers: { 'Content-Type': 'application/json' } };
      Axios.put(`/api/clients/${clientId}/appointments/${appointmentId}`, toUpdateAppointment, config)
        .then(() => commit('updateAppointment', [toUpdateAppointment, appointmentId]))
        .catch(console.error);
    },
    deleteAppointment({ commit }, [clientId, appointmentId]) {
      const config = { headers: { 'Content-Type': 'application/json' } };
      Axios.delete(`/api/clients/${clientId}/appointments/${appointmentId}`, config)
        .then(() => commit('removeAppointment', appointmentId))
        .catch(console.error);
    },
  },
});
