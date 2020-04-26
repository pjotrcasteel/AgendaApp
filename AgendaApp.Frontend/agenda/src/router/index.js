import Vue from 'vue';
import Router from 'vue-router';
import HomePage from '../home/HomePage.vue';
import AppointmentPage from '../appointment/AppointmentPage.vue';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      redirect: {
        name: 'HomePage',
      },
    },
    {
      path: '/HomePage',
      name: 'HomePage',
      component: HomePage,
    },
    {
      path: '/client/:id/appointments',
      name: 'ClientAppointments',
      component: AppointmentPage,
    },
  ],
});
