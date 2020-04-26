import Vue from 'vue';
import moment from 'moment';

Vue.filter('formatDate', (value) => {
  if (value) {
    return moment(String(value)).format('DD/MM/YYYY HH:mm');
  }
  return null;
});

Vue.filter('calcEndDate', (value, start) => {
  if (value && start) {
    return moment(String(start)).add(value, 'hours');
  }
  return null;
});
