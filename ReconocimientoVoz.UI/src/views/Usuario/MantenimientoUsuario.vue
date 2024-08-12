<template>
    <div class="formMantenimientoUsuario">
        <h2 style="text-align: center;">Mantenimiento de Usuario</h2>
        <a @click="cancelar()" class="botonRegresar"><span class="fa fa-arrow-left"></span> Regresar</a>
        <div class="formMantenimientoUsuarioInner" style="margin-top: 25px">
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" maxlength="50" class="form-control" v-model.trim="usuario.Usu_Nombre">
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <input type="text" maxlength="50" class="form-control" v-model.trim="usuario.Usu_Apellido">
            </div>
            <div class="mb-3" style="padding-bottom: 20px;">
                <label class="form-label">¿Editar contraseña?</label>
                <br>
                <div style="padding-left: 25px;">
                    <input class="form-check-input" type="checkbox" id="checkPass" @click="editarPass()">
                </div>
            </div>
            <div v-if="modificarPass">
                <div class="mb-3">
                    <label class="form-label">Contraseña actual</label>
                    <div class="inputPass">
                        <span class="passEye" id="ojo" style="font-size: 20px" @click="mostrarContrasena(1)"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechado"></i></span>
                        <input maxlength="20" class="form-control" type="password" v-model.trim="usuario.Usu_PasswordActual">
                    </div>
                </div>
                <div class="mb-3">
                    <label class="form-label">Nueva contraseña</label>
                    <div class="inputPass">
                        <span class="passEye" id="ojoN" style="font-size: 20px" @click="mostrarContrasena(2)"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechadoN"></i></span>
                        <input maxlength="20" class="form-control" type="password" v-model.trim="usuario.Usu_Password">
                    </div>
                </div>
                <div class="mb-3">
                    <label class="form-label">Confirmar contraseña</label>
                    <div class="inputPass">
                        <span class="passEye" id="ojoC" style="font-size: 20px" @click="mostrarContrasena(3)"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechadoC"></i></span>
                        <input maxlength="20" class="form-control" type="password" v-model.trim="PasswordConfirm">
                    </div>
                </div>
            </div>
            <div style="margin-top: 50px;">
                <button type="button" class="btn btn-success" style="margin-right: 10px" @click="modificarUsuario()">Guardar</button>
                <button type="button" class="btn btn-danger" @click="cancelar()">Cancelar</button>
            </div>
        </div>
    </div>
</template>

<script>
import ApiReconocimiento from '@/api/ApiReconocimiento.js';
import Router from '@/router/index.js'
import { useToast } from "vue-toastification";

export default ({
    setup() {
        const toast = useToast();
        return { toast }
    },

    data() {
        return {
            usuario: {
                Usu_Login: "",
                Usu_Nombre: "",
                Usu_Apellido: "",
                Usu_Password: "",
                Usu_PasswordActual: "",
            },
            modificarPass: false,
            PasswordConfirm: ""
        }
    },

    methods: {

        mostrarContrasena: function (valor) {
            if (valor == 1) {
                var ojo = document.getElementById("ojo")
                var next = ojo.nextSibling
                var ojoTechado = document.getElementById("ojoTechado")
                if (next.type === 'password') {
                    next.type = 'text';
                    ojoTechado.classList.remove("fa-eye-slash");
                    ojoTechado.classList.add("fa-eye");
                } else if (next.type === 'text') {
                    next.type = 'password'
                    ojoTechado.classList.remove("fa-eye");
                    ojoTechado.classList.add("fa-eye-slash");
                }
            } else if (valor == 2) {
                var ojoN = document.getElementById("ojoN")
                var nextN = ojoN.nextSibling
                var ojoTechadoN = document.getElementById("ojoTechadoN")
                if (nextN.type === 'password') {
                    nextN.type = 'text';
                    ojoTechadoN.classList.remove("fa-eye-slash");
                    ojoTechadoN.classList.add("fa-eye");
                } else if (nextN.type === 'text') {
                    nextN.type = 'password'
                    ojoTechadoN.classList.remove("fa-eye");
                    ojoTechadoN.classList.add("fa-eye-slash");
                }
            } else {
                var ojoC = document.getElementById("ojoC")
                var nextC = ojoC.nextSibling
                var ojoTechadoC = document.getElementById("ojoTechadoC")
                if (nextC.type === 'password') {
                    nextC.type = 'text';
                    ojoTechadoC.classList.remove("fa-eye-slash");
                    ojoTechadoC.classList.add("fa-eye");
                } else if (nextC.type === 'text') {
                    nextC.type = 'password'
                    ojoTechadoC.classList.remove("fa-eye");
                    ojoTechadoC.classList.add("fa-eye-slash");
                }
            }
        },

        editarPass () {
            let check = document.getElementById('checkPass')
            if (check.checked == true) {
                this.modificarPass = true
            } else {
                this.modificarPass = false
            }
        },

        cargarUsuario: async function () {
            let usuario = localStorage.getItem('usuarioRec')

            const response = await fetch('https://localhost:44387/api/Usuario/RecuperarDatosUsuario?usuario=' + usuario)
            
            if (!response.ok) {
                throw new Error('Error al cargar los datos del usuario');
            }

            const data = await response.json()

            this.usuario = {
                Usu_Nombre: data.Usu_Nombre,
                Usu_Apellido: data.Usu_Apellido,
                Usu_Password: "",
                Usu_PasswordActual: "",
            }

            this.PasswordConfirm = ""

        },
        
        modificarUsuario: async function () {
            let login = localStorage.getItem('usuarioRec')
            this.usuario.Usu_Login = login

            if ((this.usuario.Usu_Nombre == "" || this.usuario.Usu_Apellido == "") || this.modificarPass == true && (this.usuario.Usu_Password == "" || this.usuario.Usu_PasswordActual == "")) {
                this.toast("Hay campos sin completar, por favor verifique", {
                    position: "top-right",
                    timeout: 4000,
                    closeOnClick: true,
                    pauseOnFocusLoss: true,
                    pauseOnHover: true,
                    draggable: true,
                    draggablePercent: 0.6,
                    showCloseButtonOnHover: false,
                    hideProgressBar: true,
                    closeButton: "button",
                    rtl: false
                });
                return
            }

            if ((this.usuario.Usu_Password != this.PasswordConfirm) && this.modificarPass == true) {
                this.toast("La nueva contraseña y la contraseña de conformación no coinciden", {
                    position: "top-right",
                    timeout: 4000,
                    closeOnClick: true,
                    pauseOnFocusLoss: true,
                    pauseOnHover: true,
                    draggable: true,
                    draggablePercent: 0.6,
                    showCloseButtonOnHover: false,
                    hideProgressBar: true,
                    closeButton: "button",
                    rtl: false
                });
                return
            }

            await ApiReconocimiento.ModificarUsuario(this.usuario)
            .then(async response => {
                    if (response.data == -2) {
                        this.toast("La contraseña actual ingresada no coincide con la registrada en la base de datos", {
                            position: "top-right",
                            timeout: 4000,
                            closeOnClick: true,
                            pauseOnFocusLoss: true,
                            pauseOnHover: true,
                            draggable: true,
                            draggablePercent: 0.6,
                            showCloseButtonOnHover: false,
                            hideProgressBar: true,
                            closeButton: "button",
                            rtl: false
                        });
                        return
                    } else if (response.data > 0) {
                        this.toast("Usuario modificado con éxito", {
                            position: "top-right",
                            timeout: 4000,
                            closeOnClick: true,
                            pauseOnFocusLoss: true,
                            pauseOnHover: true,
                            draggable: true,
                            draggablePercent: 0.6,
                            showCloseButtonOnHover: false,
                            hideProgressBar: true,
                            closeButton: "button",
                            rtl: false,
                            icon: "fa fa-check"
                        });
                        await this.cargarUsuario()
                        this.modificarPass = false
                        let check = document.getElementById('checkPass')
                        check.checked = false
                        return;
                    }
                }
            )
        },

        cancelar () {
            Router.push({
                name: 'Inicio'
            })
        },

        validarLogin: function() {
            let usuario = localStorage.getItem('usuarioRec')
            if (usuario == null || usuario == undefined || usuario == ""){
                this.toast("Aún no ha iniciado sesión", {
                    position: "top-right",
                    timeout: 4000,
                    closeOnClick: true,
                    pauseOnFocusLoss: true,
                    pauseOnHover: true,
                    draggable: true,
                    draggablePercent: 0.6,
                    showCloseButtonOnHover: false,
                    hideProgressBar: true,
                    closeButton: "button",
                    rtl: false
                });
                Router.push({ name: 'Login' })
                return
            }
        },

    },

    mounted: async function () {
        let loader = this.$loading.show({
            opacity: 0.8
        })

        await this.validarLogin()
        await this.cargarUsuario();

        setTimeout(function() {
            loader.hide();
        }, 500)
    }

})
</script>

<style scoped>

.formMantenimientoUsuario {
    padding: 50px;
}

.formMantenimientoUsuarioInner {
    padding: 50px;
    background-color: #0f98f8;
    border-radius: 25px;
}

.botonRegresar {
    cursor: pointer;
    font-size: large;
}

.botonRegresar:hover {
    color: rgb(0, 3, 192) !important;
}

label {
    color: white;
    font-size: large;
}

.inputPass {
   position: relative;
   width: 100%;
}

   .inputPass input {
      outline: none;
      width: 100%;
      padding: 10px 30px 10px 20px;
   }

.passEye {
   position: absolute;
   right: 15px;
   transform: translateY(25%);
   cursor: pointer;
   transition: 0.4s ease all;
}

   .passEye:hover {
      color: cadetblue;
   }

</style>
