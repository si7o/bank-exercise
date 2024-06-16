<template>
    <div class="error-toast">
        <p>{{ message }}</p>
    </div>
</template>

<script setup>
const props = defineProps({
    message: {
        type: String,
        required: true
    }
});
</script>

<style scoped>
.error-toast {
    background-color: red;
    color: white;
    padding: 10px;
    border-radius: 5px;
}
</style>