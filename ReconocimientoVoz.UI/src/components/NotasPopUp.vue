<template>
  <div>
    <div class="notaPopUp row">
        <a type="button" style="color: #000; cursor: pointer; font-size: large; text-align: end;"><span class="fa fa-close" @click.prevent="cerrarNota()"></span></a>
        <div class="notaPopUp-titulo">Hola {{ usuarioConcatenado }}, ¿En qué estás pensando el día de hoy?</div>
        <div class="notaPopUp-mensajes">
            <div class="gridTitulo">
                <input maxlength="130" class="notaPopUp-mensajes-text" type="text" v-model.trim="nota.Titulo" placeholder="Escribe aquí el título de la nota">
                <span style="text-align: center;"><i class="botonGrabarTitulo fa fa-microphone" b-tooltip.hover title="Grabar título" @click.prevent="grabarNota(1)"></i></span>
            </div>
            <textarea class="notaPopUp-mensajes-textarea" v-model.trim="nota.Contenido" placeholder="Escribe aquí el contenido de la nota, o... ¡habla!"></textarea>
        </div>
        <div class="notaPopUp-botones">
            <div class="row">
                <div class="col-6">
                    <select class="form-select notaPopUp-select" v-model="nota.Prioridad" style="cursor: pointer;">
                        <option value="0">Prioridad</option>
                        <option value="1">Alta</option>
                        <option value="2">Media</option>
                        <option value="3">Baja</option>
                    </select>
                </div>
                <div class="col-6" style="margin-top: 8px;">
                    <span class="botonEnviar fa fa-send" @click.prevent="guardarNota()" b-tooltip.hover title="Guardar nota"></span>
                    <span class="botonGrabar fa fa-microphone" b-tooltip.hover title="Grabar contenido" @click.prevent="grabarNota(2)"></span>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import ApiReconocimiento from '@/api/ApiReconocimiento.js';
import { useToast } from "vue-toastification";

export default {

    setup() {
        const toast = useToast();
        return { toast }
    },
    
    data() {
        return {
            usuarioConcatenado: "",
            nota: {
                NotaId: 0,
                Usu_Login: "",
                Titulo: "",
                Contenido: "",
                Fecha: "",
                Prioridad: 0,
            },
        }
    },

    methods: {

        grabarNota: async function (valor) {
            const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
            if (SpeechRecognition) {
                var resultado = "";
                const recognition = new SpeechRecognition();

                recognition.lang = 'es';

                recognition.onresult = event => {
                    const result = event.results[0][0].transcript;
                    if (valor == 1) {
                        resultado = result;
                        this.nota.Titulo = this.capitalizarPrimeraLetra(resultado);
                    } else if (valor == 2) {
                        resultado = result
                        this.nota.Contenido = this.capitalizarPrimeraLetra(resultado);
                    }
                };
                recognition.start();
            } else {
                this.toast("El reconocimiento de voz no es compatible con este navegador", {
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
        },

        capitalizarPrimeraLetra: function (texto) {
            return texto.charAt(0).toUpperCase() + texto.slice(1);
        },

        guardarNota: async function () {
            this.nota.Usu_Login = localStorage.getItem('usuarioRec')
            if (this.nota.Usu_Login == "") {
                return
            }
            if (this.nota.Titulo == "" || this.nota.Contenido == "") {
                this.toast("Debe completar todos los campos de la nota", {
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
            if (this.nota.Prioridad == 0) {
                this.toast("Debe seleccionar la prioridad", {
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

            await ApiReconocimiento.GuardarNota(this.nota)
                .then(async response => {
                    var respuesta = response.data
                    this.toast(respuesta, {
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
                    await this.limpiarCampos()
                    await this.cerrarNota()
                    await this.$root.listarNotasERoot();
                    await this.$root.ListarNotasRoot();
                    await this.$root.cutPagesRoot();
                    return
                })
        },

        limpiarCampos() {
            this.nota.Titulo = "";
            this.nota.Contenido = "";
            this.nota.Prioridad = 0;
        },

        cerrarNota: function() {
            this.$store.state.mostrarNota = false
        },

        asignarNombreUsuario: function() {
            this.usuarioConcatenado = this.$store.state.usuarioActivo
        },

        computed: {
            ...mapState(
                ["mostrarNota, usuarioActivo"]
            )
        }
    
    },

    mounted: async function() {
        await this.asignarNombreUsuario()
    },
}
</script>

<style>

</style>