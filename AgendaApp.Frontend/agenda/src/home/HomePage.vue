<template>
  <div class="home col-md-12">
    <h1>Employees</h1>
      <b-table primary-key="" striped hover :items="clients" :fields="fields" @row-clicked="rowClickHandler">
        <template v-slot:cell(index)="data">
          {{ data.index + 1 }}
        </template>
        <template slot="actions" scope="client">
          <b-btn size="sm" @click="navigate(client.id)">Agenda</b-btn>
        </template>
      </b-table>
  </div>
</template>

<script>

export default {
  name: 'HomePage',
  created() {
    this.$store.dispatch('getClients');
  },
  data() {
    return {
      fields: [
        'index',
        {
          key: 'name',
          label: 'Name',
          sortable: true,
        },
        {
          key: 'appointmentTodayCount',
          label: 'Appointments today',
          sortable: true,
        },
      ],
    };
  },
  methods: {
    rowClickHandler(record) {
      this.navigate(record.id);
    },
    navigate(clientId) {
      this.$router.push({ name: 'ClientAppointments', params: { id: clientId } });
    },
  },
  computed: {
    clients() {
      return this.$store.state.clients;
    },
  },
};
</script>

<style scoped>

</style>
