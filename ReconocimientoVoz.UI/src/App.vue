<template>
    <div id="app">
        <router-view  />
        <footerPrincipal />
    </div>
</template>

<script>
import footerPrincipal from '@/components/Footer.vue'

    export default {
        name: 'app',
        components: {
            footerPrincipal
        }
    };
    
</script>

<style>
</style>

