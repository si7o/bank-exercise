<template>
    <div class="balance">
        <h1>Balance</h1>
        <h2>Current Balance: {{ balance }}€</h2>
        <table name="movements">
            <tr>
                <th>Amount</th>
                <th>Description</th>
                <th>Date</th>
            </tr>
            <tr v-for="item in movements" :key="item.id">
                <td>{{ item.amount }}</td>
                <td>{{ item.description }}</td>
                <td>{{ formatDate(item.date) }}</td>
            </tr>
        </table>

        <div class="balance__deposit">
            <h3>Deposit Money</h3>
            <form name="deposit" @submit.prevent="handleSubmitDeposit">
                <label for="depositAmount">Amount:</label>                
                <input type="number" id="depositAmount" v-model="depositAmount" required step=".01">

                <label for="depositDescription">Description:</label>
                <input type="text" id="depositDescription" v-model="depositDescription" required>
                <button type="submit">Deposit</button>
            </form>
        </div>

        <div class="balance__withdraw">
            <h3>Withdraw Money</h3>
            <form name="withdraw" @submit.prevent="handleSubmitWithdraw">
                <label for="withdrawAmount">Amount:</label>
                <input type="number" id="withdrawAmount" v-model="withdrawAmount" required step=".01">

                <label for="withdrawDescription">Description:</label>
                <input type="text" id="withdrawDescription" v-model="withdrawDescription" required>
                <button type="submit">Withdraw</button>
            </form>
        </div>
    </div>

    <ErrorToast v-if="hasErrors" message="There was an error" />
</template>

<script setup>
import { computed, onMounted, ref } from 'vue';
import ErrorToast from '@/components/ErrorToast.vue';
import useBankStore from '@/stores/bank';

const bankStore = useBankStore()

const hasErrors = ref(false);
const depositAmount = ref(0);
const depositDescription = ref('');
const withdrawAmount = ref(0);
const withdrawDescription = ref('');

const movements = computed( () => bankStore.movements );
const balance = computed( () => bankStore.balance );

const formatDate = (date) => {
    return `${new Date(date).toLocaleDateString()} ${new Date(date).toLocaleTimeString()}`;
};

onMounted(async () => {
    try {
        await bankStore.getMovements();
    } catch (error) {
        hasErrors.value = true;
        console.error(error);
    }
});

const handleSubmitDeposit = async () => {
    try {
        await bankStore.depositMoney(depositAmount.value, depositDescription.value);
        await bankStore.getMovements();

        depositAmount.value = 0;
        depositDescription.value = '';
        hasErrors.value = false;
    } catch (error) {
        hasErrors.value = true;
        console.error(error);
    }
};

const handleSubmitWithdraw = async () => {
    try {
        await bankStore.withdrawMoney(withdrawAmount.value, withdrawDescription.value);
        await bankStore.getMovements();

        withdrawAmount.value = 0;
        withdrawDescription.value = '';
        hasErrors.value = false;
    } catch (error) {
        hasErrors.value = true;
        console.error(error);
    }
};

</script>