import { createWebHistory, createRouter } from "vue-router";

import Login from "@/views/Login/route.js";
import Inicio from "@/views/Inicio/route.js";
import MantenimientoUsuario from "@/views/Usuario/route.js";

let routes = [
    { path: '/:catchAll(.*)', redirect:'/404' }
];

routes = routes.concat(
    Login,
    Inicio,
    MantenimientoUsuario
)

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
    scrollBehavior() {
        return { top: 0 }
    },
})

export default router