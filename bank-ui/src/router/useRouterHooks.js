import useBankStore from '@/stores/bank';
import { useRouter } from 'vue-router';

const useRouterHooks = () => {
    const router = useRouter();
    const bankStore = useBankStore();

    const setup = () => {
        router.beforeEach((to, from, next) => {
            if (to.meta.requiresAuth && !bankStore.isLoggedIn) {
                console.log('User is not logged in, redirecting to register');
                next({ name: 'register' })
                return;
            }

            next() // does not require auth, make sure to always call next()!
        })
    };

    return { setup }
}

export default useRouterHooks;