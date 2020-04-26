/* eslint-disable no-unused-vars */
    <!--
    Todo: move modals own component
    Tood: move calendar own component
    -->

<template>
  <div class="Appointments col-md-12">
    <FullCalendar
      class="agenda-app-calendar"
      ref="fullCalendar"
      locale="nl"
      defaultView="dayGridMonth"
      :header="{
        left: 'prev, next today',
        center: 'title',
        right: 'dayGridMonth, timeGridWeek, timeGridDay, listWeek',
      }"
      :editable="true"
      :selectable="true"
      :config="config"
      :plugins="calendarPlugins"
      :weekends="calendarWeekends"
      :events="calendarEvents"
      @dateClick="handleDateClick"
      @eventClick="handleEventClick"
      @eventDrop="handleDrop"
    />
    <b-modal @hide="clearForms" ref="agenda-add-modal" hide-footer title="Plan a new appointment">
      <b-form @submit="onSubmitAdd">
        <b-form-group id="input-group-1" label="Title" label-for="input-title">
          <b-form-input id="input-title" v-model="newAppointment.title" required></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-2" label="Start" label-for="input-start">
          <date-picker id="input-start" v-model="newAppointment.start" required :config="options"></date-picker>
        </b-form-group>
        <b-form-group id="input-group-3" label="End" label-for="input-end">
          <date-picker id="input-end" v-model="newAppointment.end" :disabled="newAppointment.isFullDay" :config="options"></date-picker>
        </b-form-group>
        <b-form-group id="input-group-4">
          <b-form-checkbox v-model="newAppointment.isFullDay">Is all day</b-form-checkbox>
        </b-form-group>
        <b-button type="submit" variant="primary">Submit</b-button>
      </b-form>
    </b-modal>

    <b-modal @hide="clearForms" ref="agenda-update-modal" hide-footer title="Edit appointment">
      <b-form @submit="onSubmitUpdate" @reset="onResetDelete">
        <b-form-group id="input-group-1" label="Title" label-for="input-title">
          <b-form-input id="input-title" v-model="editAppointment.title" required></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-2" label="Start" label-for="input-start">
          <date-picker id="input-start" v-model="editAppointment.start" required :config="options"></date-picker>
        </b-form-group>
        <b-form-group id="input-group-3" label="End" label-for="input-end">
          <date-picker id="input-end" v-model="editAppointment.end" :disabled="editAppointment.isFullDay" :config="options"></date-picker>
        </b-form-group>
        <b-form-group id="input-group-4">
          <b-form-checkbox v-model="editAppointment.isFullDay">Is all day</b-form-checkbox>
        </b-form-group>
        <b-button type="submit" variant="primary">Edit</b-button>
        <b-button type="reset" variant="danger">Delete</b-button>
      </b-form>
    </b-modal>
  </div>
</template>

<script>
import FullCalendar from '@fullcalendar/vue';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import moment from 'moment';

import '@fullcalendar/core/main.css';
import '@fullcalendar/daygrid/main.css';
import '@fullcalendar/timegrid/main.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css';

global.jQuery = require('jquery');

const $ = global.jQuery;
window.$ = $;

// Using font-awesome 5 icons
$.extend(true, $.fn.datetimepicker.defaults, {
  icons: {
    time: 'far fa-clock',
    date: 'far fa-calendar',
    up: 'fas fa-arrow-up',
    down: 'fas fa-arrow-down',
    previous: 'fas fa-chevron-left',
    next: 'fas fa-chevron-right',
    today: 'fas fa-calendar-check',
    clear: 'far fa-trash-alt',
    close: 'far fa-times-circle',
  },
});

export default {
  components: {
    FullCalendar,
  },
  data() {
    return {
      newAppointment: {
        title: '',
        start: null,
        end: null,
        isFullDay: false,
      },
      editAppointment: {
        id: null,
        title: '',
        start: null,
        end: null,
        isFullDay: false,
      },
      options: {
        format: 'YYYY-MM-DD HH:mm:ss',
        useCurrent: false,
      },
      calendarPlugins: [
        dayGridPlugin,
        timeGridPlugin,
        interactionPlugin,
      ],
      calendarWeekends: true,
      calendarEvents: [],
      config: {
        droppable: true,
        timeFormat: 'H(:mm)',
      },
    };
  },
  created() {
    this.$store.dispatch('getAppointments', this.$route.params.id);
  },
  watch: {
    appointments: {
      immediate: true,
      deep: false,
      handler(newValue) {
        if (newValue.length !== 0) {
          this.calendarEvents = [];
          newValue.forEach((appointment) => {
            this.getEvents(appointment);
          });
        }
      },
    },
  },
  methods: {
    getEvents(appointment) {
      this.calendarEvents.push({
        id: appointment.id,
        title: appointment.title,
        start: appointment.start,
        end: appointment.end,
        allDay: appointment.isFullDay,
        borderColor: appointment.type,
      });
    },
    gotoPast() {
      const calendarApi = this.$refs.fullCalendar.getApi();
      calendarApi.gotoDate('2000-01-01');
    },
    handleDateClick(arg) {
      this.newAppointment.start = arg.dateStr;
      this.$refs['agenda-add-modal'].toggle();
    },
    handleEventClick(arg) {
      this.editAppointment = {
        id: arg.event.id,
        title: arg.event.title,
        start: moment(arg.event.start).format('YYYY-MM-DD[T]HH:mm:ss'),
        end: arg.event.end === null ? null : moment(arg.event.end).format('YYYY-MM-DD[T]HH:mm:ss'),
        isFullDay: arg.event.IallDay,
      };
      this.$refs['agenda-update-modal'].toggle();
    },
    handleDrop(arg) {
      // eslint-disable-next-line no-unused-vars
      const appointmentId = arg.oldEvent.id;
      // eslint-disable-next-line no-unused-vars
      const updatedAppointment = {
        start: moment(arg.event.start).format('YYYY-MM-DD[T]HH:mm:ss'),
        end: arg.event.end === null ? null : moment(arg.event.end).format('YYYY-MM-DD[T]HH:mm:ss'),
        title: arg.event.title,
        isFullDay: arg.event.allDay,
      };
      this.$store.dispatch('updateAppointment', [this.$route.params.id, appointmentId, updatedAppointment]);
    },
    // Form Methodes
    onSubmitAdd(evt) {
      evt.preventDefault();
      this.newAppointment.start = moment(this.newAppointment.start).format('YYYY-MM-DD[T]HH:mm:ss');
      this.newAppointment.end = this.newAppointment.end === null ? null : moment(this.newAppointment.end).format('YYYY-MM-DD[T]HH:mm:ss');
      this.$store.dispatch('addAppointment', [this.$route.params.id, this.newAppointment]);
      this.$refs['agenda-add-modal'].toggle();
    },
    onSubmitUpdate(evt) {
      evt.preventDefault();
      this.editAppointment.start = moment(this.editAppointment.start).format('YYYY-MM-DD[T]HH:mm:ss');
      this.editAppointment.end = this.editAppointment.end === null ? null : moment(this.editAppointment.end).format('YYYY-MM-DD[T]HH:mm:ss');
      this.$store.dispatch('updateAppointment', [this.$route.params.id, this.editAppointment.id, this.editAppointment]);
      this.$refs['agenda-update-modal'].toggle();
    },
    onResetDelete() {
      this.$store.dispatch('deleteAppointment', [this.$route.params.id, this.editAppointment.id]);
      this.$ref['agenda-update-modal'].toggle();
    },
    clearForms() {
      this.newAppointment = {
        title: '',
        start: null,
        end: null,
        isFullDay: false,
      };
      this.updateAppointment = {
        title: '',
        start: null,
        end: null,
        isFullDay: false,
      };
    },
  },
  computed: {
    appointments() {
      return this.$store.state.appointments;
    },
  },
};
</script>

<style scoped>
  .demo-app {
    font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
    font-size: 14px;
  }

  .demo-app-top {
    margin: 0 0 3em;
  }

  .demo-app-calendar {
    margin: 0 auto;
    max-width: 900px;
  }
</style>
