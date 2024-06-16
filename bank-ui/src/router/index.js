import { createRouter, createWebHistory } from 'vue-router';
import RegisterView from '@/views/RegisterView/RegisterView.vue';
import LoginView from '@/views/LoginView/LoginView.vue';
import BalanceView from '@/views/BalanceView/BalanceView.vue';
import AboutView from '@/views/AboutView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/balance',
      name: 'balance',
      component: BalanceView,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/about',
      name: 'about',
      component: AboutView
    }
  ]
});



export default router;
