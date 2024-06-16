<template>
    <div class="login-view">
        <h1>Login</h1>
        <form name="login" @submit.prevent="handleSubmitLogin">
            <label for="email">Email:</label>
            <input type="text" id="email" v-model="email" required>

            <label for="password">Password:</label>
            <input type="password" id="password" v-model="password" required>

            <button type="submit">Login</button>
        </form>

        <RouterLink to="/">Don't have an account? Register here</RouterLink>

        <ErrorToast v-if="hasErrors" message="There was an error logging in" />
    </div>
</template>

<script setup>
import { ref } from 'vue';
import useBankStore from '@/stores/bank';
import { useRouter } from 'vue-router';
import ErrorToast from '@/components/ErrorToast.vue';

const bankStore = useBankStore()
const router = useRouter();

const email = ref('');
const password = ref('');
const hasErrors = ref(false);

const handleSubmitLogin = async () => {
    try {
        await bankStore.login(email.value, password.value);
        await router.replace('/balance');
    } catch (error) {
        hasErrors.value = true;
        console.error(error);
    }
};
</script>