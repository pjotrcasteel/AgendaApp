import BootstrapVue, { ModalPlugin } from 'bootstrap-vue';
import Vue from 'vue';
import DatePicker from 'vue-bootstrap-datetimepicker';
import App from './App.vue';
import store from './store';
import router from './router';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import '@fortawesome/fontawesome-free/css/all.css';

import './helpers/datetime/index';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(ModalPlugin);
Vue.use(DatePicker);

new Vue({
  render: (h) => h(App),
  store,
  router,
}).$mount('#app');
