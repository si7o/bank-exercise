<template>
    <div class="register">
        <h1>Register</h1>
        <form name="register" @submit.prevent="handleSubmitRegister" :class="{hasErrors}">
            <label for="email">Email:</label>
            <input type="text" id="email" v-model="email" required>

            <label for="password">Password:</label>
            <input type="password" id="password" v-model="password" required>

            <button type="submit">Create Account</button>
        </form>

        <RouterLink to="/login">Already have an account? Login here</RouterLink>

        <ErrorToast v-if="hasErrors" message="There was an error registering your account" />
    </div>
</template>

<script setup>
import { ref } from 'vue';
import useBankStore from '@/stores/bank';
import ErrorToast from '@/components/ErrorToast.vue';
import { useRouter } from 'vue-router';

const bankStore = useBankStore();
const router = useRouter();

const email = ref('');
const password = ref('');
const hasErrors = ref(false);

const handleSubmitRegister = async () => {
    try {
        await bankStore.register(email.value, password.value);

        router.replace('/login');
    } catch (error) {
        hasErrors.value = true;
        console.error(error);
    }
};
</script>