<template>
  <div>
      
      <div id="mySidepanel" class="sidepanel">
         <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">×</a>
         <a href="index.html">Home </a>
         <a href="#about">About</a>
         <a href="#service">Services  </a>
         <a href="#vehicles">Our Vehicles</a>
         <a href="#testimonial">Testimonial</a>
         <a href="#contact">Contact</a>
      </div>
      
      <header>
         <div class="header">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-md-4 col-sm-4">
                     <div class="logo">
                        <h1><b style="color: #0f98f8; cursor: pointer;">NotaInt</b></h1>
                     </div>
                  </div>
                  <div class="col-md-8 col-sm-8">
                     <div class="right_bottun">
                        <ul class="conat_info d_none ">
                           <li v-if="registro" b-tooltip.hover title="Iniciar sesión" @click.prevent="cambiarForm()"><a href="#"><i class="fa fa-sign-in" aria-hidden="true"></i></a></li>
                           <li v-else b-tooltip.hover title="Registrarse" @click.prevent="cambiarForm()"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a></li>
                        </ul>
                        <!--<button class="openbtn" onclick="openNav()"><img src="images/menu_icon.png" alt="#"/> </button>-->
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </header>

      <section class="banner_main">
         <div id="banner1" class="carousel slide banner_slide" data-ride="carousel">
            <div class="carousel-inner">
               <div class="carousel-item active">
                  <div class="container-fluid">
                     <div class="carousel-caption">
                        <div class="row">
                           <div class="col-md-7 col-lg-5">
                              <div class="text-bg">
                                 <h1>Su gestor de Notas de confianza</h1>
                                 <span>Crea y administra tus notas de texto como nunca antes lo has hecho</span>
                              </div>
                           </div>
                           <div class="col-md-12 col-lg-7">
                              <div class="row">
                                 <div class="col-md-6">
                                    <div class="ban_track">
                                       <figure><img src="images/chat.png" alt="#"/></figure>
                                    </div>
                                 </div>
                                 <div class="col-md-6">
                                    <form class="transfot" v-if="registro">
                                       <div class="col-md-12 fondoCeleste">
                                          <h3 class="textoFondo">Regístrese aquí</h3>
                                       </div>
                                       <div class="col-md-12">
                                          <input class="transfot_form inputForm" maxlength="10" placeholder="Usuario" type="text" v-model.trim="usuarioRegistro.Usu_Login">
                                       </div>
                                       <div class="col-md-12">
                                          <input class="transfot_form inputForm" maxlength="50" placeholder="Nombre" type="text" v-model.trim="usuarioRegistro.Usu_Nombre">
                                       </div>
                                       <div class="col-md-12">
                                          <input class="transfot_form inputForm" maxlength="50" placeholder="Apellido" type="text" v-model.trim="usuarioRegistro.Usu_Apellido">
                                       </div>
                                       <div class="col-md-12">
                                          <div class="inputPass">
                                             <span class="passEye" id="ojoR" style="font-size: 20px" @click="mostrarContrasenaRegistro(1)"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechadoR"></i></span>
                                             <input maxlength="20" class="transfot_form" type="password" placeholder="Contraseña" v-model.trim="usuarioRegistro.Usu_Password">
                                          </div>
                                       </div>
                                       <div class="col-md-12">
                                          <div class="inputPass">
                                             <span class="passEye" id="ojoC" style="font-size: 20px" @click="mostrarContrasenaRegistro(2)"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechadoC"></i></span>
                                             <input maxlength="20" class="transfot_form" type="password" placeholder="Confirmar contraseña" v-model.trim="confirmacionRegistro">
                                          </div>
                                       </div>
                                       <div class="col-md-12" style="margin-top: 15px;">
                                          <button class="get_now" @click.prevent="Registrarse">Registrarse</button>
                                       </div>
                                    </form>
                                    <form class="transfot" v-else>
                                       <div class="col-md-12 fondoCeleste">
                                          <h3 class="textoFondo">Iniciar Sesión</h3>
                                       </div>
                                       <div class="col-md-12">
                                          <input class="transfot_form inputForm" maxlength="10" placeholder="Usuario" type="text" v-model.trim="usuarioInicio.Usu_Login">
                                       </div>
                                       <div class="col-md-12">
                                          <div class="inputPass">
                                             <span class="passEye" id="ojo" style="font-size: 20px" @click="mostrarContrasena"><i style="color: black;" class="fa fa-eye-slash" id="ojoTechado"></i></span>
                                             <input maxlength="20" class="transfot_form" type="password" placeholder="Contraseña" v-model.trim="usuarioInicio.Usu_Password">
                                          </div>
                                       </div>
                                       <div class="col-md-12" style="margin-top: 15px">
                                          <button class="get_now" @click.prevent="IniciarSesion">Ingresar</button>
                                       </div>
                                    </form>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </section>

  </div>
</template>

<script>
import { useToast } from "vue-toastification";
import ApiReconocimiento from '@/api/ApiReconocimiento.js';
import Router from '@/router/index.js'

export default {

   setup() {
      const toast = useToast();
      return { toast }
   },

   components: {
      
   },

   data() {
      return {
         usuarioRegistro: {
            Usu_Login: "",
            Usu_Nombre: "",
            Usu_Apellido: "",
            Usu_Password: "",
         },
         usuarioInicio: {
            Usu_Login: "",
            Usu_Password: "",
         },
         confirmacionRegistro: "",
         registro: true,
         usuarioConcatenado: ""
      }
   },

   methods: {

      mostrarContrasena: function () {
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
      },

      mostrarContrasenaRegistro: function (valor) {
         if (valor == 1) {
            var ojo = document.getElementById("ojoR")
            var next = ojo.nextSibling
            var ojoTechado = document.getElementById("ojoTechadoR")
            if (next.type === 'password') {
               next.type = 'text';
               ojoTechado.classList.remove("fa-eye-slash");
               ojoTechado.classList.add("fa-eye");
            } else if (next.type === 'text') {
               next.type = 'password'
               ojoTechado.classList.remove("fa-eye");
               ojoTechado.classList.add("fa-eye-slash");
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

      cambiarForm: function() {
         this.registro = !this.registro;
      },

      Registrarse: async function() {
         if (this.usuarioRegistro.Usu_Login == "" || this.usuarioRegistro.Usu_Nombre == ""  || this.usuarioRegistro.Usu_Apellido == "" || this.usuarioRegistro.Usu_Password == "" || this.confirmacionRegistro == "") {
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

         if (this.usuarioRegistro.Usu_Password !== this.confirmacionRegistro) {
            this.toast("Las contraseñas no coinciden, por favor verifique", {
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

         await ApiReconocimiento.registrarUsuario(this.usuarioRegistro)
         .then(async response => {
            if (response.data > 0) {
               this.toast("Usuario registrado exitosamente", {
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
               await this.limpiarRegistro()
               this.registro = false;
               return
            } else if (response.data == -1) {
               this.toast("Ocurrió un error al tratar de registrar el usuario", {
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
            } else if (response.data == -2) {
               this.toast("El usuario que está tratando de registrar ya existe", {
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
         })
      },

      IniciarSesion: async function() {
         if (this.usuarioInicio.Usu_Login == "" || this.usuarioInicio.Usu_Password == "") {
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

         await ApiReconocimiento.IniciarSesion(this.usuarioInicio)
         .then(async response => {
            if (response.data > 0) {
               localStorage.setItem('usuarioRec', this.usuarioInicio.Usu_Login)
               const response = await fetch('https://localhost:44387/api/Usuario/RecuperarDatosUsuario?usuario=' + this.usuarioInicio.Usu_Login)

               if(!response.ok) {
                  throw new Error('Error al cargar los datos del usuario');
               }

               const data = await response.json()
               this.usuarioConcatenado = data.Usu_Nombre + " " + data.Usu_Apellido

               await this.darBienvenida()
               Router.push({name: 'Inicio'})
               return
            } else if (response.data == -1) {
               this.toast("Ocurrió un error al tratar de ejecutar la consulta", {
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
            } else if (response.data == 0) {
               this.toast("Usuario o contraseña incorrectos", {
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
         })
      },

      limpiarRegistro: function() {
         this.usuarioRegistro.Usu_Login = ""
         this.usuarioRegistro.Usu_Login = "",
         this.usuarioRegistro.Usu_Nombre = "",
         this.usuarioRegistro.Usu_Apellido = "",
         this.usuarioRegistro.Usu_Password = "",
         this.confirmacionRegistro = ""
      },

      darBienvenida() {
            var mensaje = new SpeechSynthesisUtterance();
            var saludo = "Bienvenido " + this.usuarioConcatenado;
            mensaje.text = saludo;
            mensaje.voice = speechSynthesis.getVoices()[9];
            speechSynthesis.speak(mensaje);
        },

        validarLogin: function() {
            let usuario = localStorage.getItem('usuarioRec')
            if (usuario != null && usuario != undefined && usuario != ""){
                Router.push({ name: 'Inicio' })
                return
            }
        },
      
   },

   mounted: async function() {
    let loader = this.$loading.show({
        opacity: 0.8
    })
    await this.validarLogin()
    setTimeout(function() {
        loader.hide();
    }, 500)
}

}
</script>

<style>

.inputPass {
   position: relative;
   width: 55%;
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

.inputForm {
   width: 55%;
}

</style>