import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios';

const API_URL = 'http://localhost:5045';
const bankApi = axios.create({
  baseURL: API_URL,
});

const useBankStore = defineStore('bank', () => {
  const userEmail = ref(undefined)
  const jwt = ref(undefined)
  const movements = ref([])

  const isLoggedIn = computed(() => !!jwt.value?.accessToken);
  const balance = computed(() => movements.value?.reduce((acc, movement) => acc + movement.amount, 0).toFixed(2));

  async function register(email, password) {
    try {
      const response = await bankApi.post('/register', { email, password });

      return response.data;
    } catch (error) {
      throw new Error(error.response.data.message);
    }
  }

  async function login(email, password) {
    try {
      const response = await bankApi.post('/login', { email, password });
      userEmail.value = email;
      jwt.value = response.data;
      bankApi.defaults.headers.common.Authorization = `Bearer ${jwt.value?.accessToken}`;

      return response.data;
    } catch (error) {
      throw new Error(error.response.data.message);
    }
  }

  async function getMovements() {
    try {
      const response = await bankApi.get('/account-movements');
      movements.value = response.data;
    } catch (error) {
      throw new Error(error.response.data.message);
    }
  }

  async function depositMoney(amount, description) {
    try {
      const response = await bankApi.post('/deposit', { amount, description });

      return response.data;
    } catch (error) {
      throw new Error(error.response.data.message);
    }
  }

  async function withdrawMoney(amount, description) {
    try {
      const response = await bankApi.post('/withdraw', { amount, description });
      return response.data;
    } catch (error) {
      throw new Error(error.response.data.message);
    }
  }

  async function logout() {
    jwt.value.accessToken = '';
    movements.value = [];
    delete axios.defaults.headers.common['Authorization'];
  }

  return {
    userEmail,
    jwt,
    isLoggedIn,
    balance,
    movements,
    register,
    login,
    logout,
    getMovements,
    depositMoney,
    withdrawMoney,
  };
})

export default useBankStore;
