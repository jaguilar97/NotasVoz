<template>
    <div>
        <div id="mySidepanel" class="sidepanel">
            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">×</a>
            <a href="#" @click.prevent="abrirNota()" onclick="closeNav()">Nueva nota</a>
            <a href="#" @click.prevent="verNotas()" onclick="closeNav()">Ver mis notas</a>
            <a href="#" @click.prevent="routePerfil()" onclick="closeNav()">Perfil</a>
        </div>
      
        <header @click.prevent="ocultarNota()">
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
                                <div class="fondoCeleste" style="padding-right: 15px;">
                                    <h3 style="color: white; margin-top: 15px; padding-left: 15px; cursor: pointer;"> {{ usuarioConcatenado }}</h3>
                                </div>
                                <button class="openbtn" onclick="openNav()"><img src="images/menu_icon.png" alt="#"/> </button>
                                <ul class="conat_info d_none ">
                                <li b-tooltip.hover title="Cerrar sesión" @click.prevent="cerrarSesion()"><a href="#"><i class="fa fa-sign-out" aria-hidden="true"></i></a></li>
                                </ul>
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
                                            <h1 style="cursor: default">Bienvenido {{ usuarioActual.Usu_Nombre }}</h1>
                                            <span style="cursor: default">Organízate mejor con NotaInt, la aplicación que te permite tener en un solo lugar todas tus notas</span>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-7">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="ban_track">
                                                <figure><img src="images/chat.png" alt="#"/></figure>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="cursor: default; font-size: large;">
                                                Hola {{ usuarioConcatenado }}, <br /> presione <a href="#" class="iniciarNota" @click.prevent="abrirNota()"><b>aquí</b></a> para crear una nota
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

        <NotasPopUp v-show="$store.state.mostrarNota" />

        <div class="row" style="padding: 20px;" ref="targetDiv">

            <div style="text-align: end;" v-if="paginateData.length > 0">
                <span style="text-align: center;"><i class="botonGrabarFiltro fa fa-microphone" b-tooltip.hover title="Filtrar por voz" @click.prevent="grabarNota(3)"></i></span>
                <input autocomplete="off" @keyup="aplyFilter()" v-model="palabraFiltro" class="small-input filtro-text" type="search" placeholder="Buscar por palabra..." maxlength="50" />
            </div>

            <div v-for="nota in paginateData" v-bind:key="nota.NotaId" class="col-sm-6" style="padding: 10px;">
                <div class="card cardNota">
                    <div class="card-body">
                        <div style="min-height: 125px; max-height: 125px; overflow-x: auto">
                            <h5 class="card-title" :class="{'cardNotaA': nota.Prioridad == 1, 'cardNotaM': nota.Prioridad == 2, 'cardNotaB': nota.Prioridad == 3}">
                            <span class="fa fa-exclamation-circle"></span> {{ $filters.CortarTexto(nota.Titulo, 50) }}
                            </h5>
                            <p class="card-text">{{ $filters.CortarTexto(nota.Contenido, 150) }}</p>
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <a href="#" class="btn btn-primary" @click.prevent="verNota(nota.NotaId)">Ver nota</a>
                            </div>
                            <div class="col-6" style="text-align: end;">
                                <button b-tooltip.hover title="Eliminar" class="fa fa-trash iconosCards" @click.prevent="eliminarNota(nota.NotaId)"></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <nav v-if="paginate" aria-label="Page navigation example" style="margin-top: 10px;">
                <ul class="pagination cursorPaginados">
                    <li class="page-item"><a class="page-link" @click.prevent="goBack()">Anterior</a></li>
                    <li v-for="pagina in pageNumeration" v-bind:key="pagina" class="page-item">
                        <a class="page-link" @click.prevent="changePage(pagina)"
                            v-bind:class="{ active: (pagina == actualPage) }">{{ pagina }}</a>
                    </li>
                    <li class="page-item"><a class="page-link" @click.prevent="goNext()">Siguiente</a></li>
                </ul>
            </nav>
        </div>

        <!--Editar Nota-->

        <div class="notaPopUp row" v-if="modalVerNota">
            <a type="button" style="color: #000; cursor: pointer; font-size: large; text-align: end;"><span class="fa fa-close" @click.prevent="cerrarNota()"></span></a>
            <p hidden>{{ nota.NotaId }}</p>
            <div class="notaPopUp-titulo">{{ usuarioConcatenado }}, En esto estabas pensando el <span>{{ $filters.FormatearFechaHora(nota.Fecha) }}</span></div>
            <div class="notaPopUp-mensajes">
                <div class="gridTitulo">
                    <input maxlength="130" class="notaPopUp-mensajes-text" :class="{ 'input-disabled': !editando }" :disabled="!editando" type="text" v-model.trim="nota.Titulo" placeholder="Escribe aquí el título de la nota">
                    <span v-if="editando" style="text-align: center;"><i class="botonGrabarTitulo fa fa-microphone" b-tooltip.hover title="Grabar título" @click.prevent="grabarNota(1)"></i></span>
                </div>
                <textarea class="notaPopUp-mensajes-textarea" :class="{ 'input-disabled': !editando }" :disabled="!editando" type="text" v-model.trim="nota.Contenido" placeholder="Escribe aquí el contenido de la nota, o... ¡habla!"></textarea>
            </div>
            <div class="notaPopUp-botones">
                <div class="row">
                    <div class="col-6">
                        <select class="form-select notaPopUp-select" :class="{ 'input-disabled': !editando }" :disabled="!editando" type="text" v-model="nota.Prioridad" style="cursor: pointer;">
                            <option value="0">Prioridad</option>
                            <option value="1">Alta</option>
                            <option value="2">Media</option>
                            <option value="3">Baja</option>
                        </select>
                    </div>
                    <div class="col-6" style="margin-top: 8px;">
                        <span class="botonEditar fa fa-pencil" @click.prevent="editarNota()" b-tooltip.hover title="Editar nota"></span>
                        <span class="botonEnviar fa fa-send" :class="{ 'icon-disabled': !editando }" v-if="editando" @click.prevent="guardarNotaEdit()" b-tooltip.hover title="Guardar nota"></span>
                        <span class="botonGrabar fa fa-microphone" :class="{ 'icon-disabled': !editando }" v-if="editando" b-tooltip.hover title="Grabar contenido" @click.prevent="grabarNota(2)"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { useToast } from "vue-toastification";
import ApiReconocimiento from '@/api/ApiReconocimiento.js';
import Router from '@/router/index.js'
import NotasPopUp from '@/components/NotasPopUp.vue'
import { mapState } from 'vuex';

export default {

    setup() {
        const toast = useToast();
        return { toast }
    },

    components: {
        NotasPopUp
    },

    data() {
        return {
            usuarioActual: {
                Usu_Login: "",
                Usu_Nombre: "",
                Usu_Apellido: "",
            },
            usuarioInicio: {
                Usu_Login: "",
                Usu_Password: "",
            },
            confirmacionRegistro: "",
            registro: true,
            usuarioConcatenado: "",
            ListaNotas: [],
            modalVerNota: false,
            nota: {
                NotaId: 0,
                Usu_Login: "",
                Titulo: "",
                Contenido: "",
                Fecha: "",
                Prioridad: 0,
            },
            editando: false,

            pageElements: 6,
            actualPage: 1,
            pageNumeration: [],
            paginate: true,
            paginateData: [],
            FileList: [],

            palabraFiltro: "",

        }
    },

    methods: {

        routePerfil: function() {
            Router.push({
                name: 'MantenimientoUsuario'
            })
        },

        abrirNota: function() {
            if (this.$store.state.mostrarNota == true) {
                return
            }
            this.$store.state.mostrarNota = true
        },

        ocultarNota: function() {
            this.$store.state.mostrarNota = false
        },

        verNotas() {
            const targetDiv = this.$refs.targetDiv;
            targetDiv.scrollIntoView({ behavior: 'smooth' });
        },

        verNota: async function (NotaId) {
            await ApiReconocimiento.RecuperarNota(NotaId)
                .then(response => {
                    if (response != null) {
                        response.data.obj.forEach(element => {
                            this.nota.NotaId = element.NotaId
                            this.nota.Usu_Login = element.Usu_Login,
                            this.nota.Titulo = element.Titulo,
                            this.nota.Contenido = element.Contenido,
                            this.nota.Fecha = element.Fecha,
                            this.nota.Prioridad = element.Prioridad
                        })
                    }
                })
            this.modalVerNota = true;
        },

        editarNota() {
            this.editando = !this.editando
        },

        guardarNotaEdit: async function () {
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

            await ApiReconocimiento.ActualizarNota(this.nota)
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
                    await this.cerrarNota();
                    await this.listarNotasE();
                    await this.listarNotas();
                    await this.cutPages();
                    await this.verNotas();
                    this.editando = false;
                    return
                })
        },

        cerrarNota: function() {
            this.modalVerNota = false
        },

        recuperarDatosUsuario: async function() {
            let usuario = localStorage.getItem('usuarioRec')
            await ApiReconocimiento.RecuperarDatosUsuario(usuario)
                .then(async response => {
                    if(response.data != null){
                        this.usuarioActual.Usu_Nombre = response.data.Usu_Nombre,
                        this.usuarioActual.Usu_Apellido = response.data.Usu_Apellido
                    }
                })

            this.usuarioActual.Usu_Login = usuario
            this.usuarioConcatenado = this.usuarioActual.Usu_Nombre + " " + this.usuarioActual.Usu_Apellido
            this.$store.state.usuarioActivo = this.usuarioConcatenado
        },

        grabarNota: async function (valor) {
            const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
            if (SpeechRecognition) {
                var resultado = "";
                const recognition = new SpeechRecognition();

                recognition.lang = 'es';

                recognition.onresult = async event => {
                    const result = event.results[0][0].transcript;
                    if (valor == 1) {
                        resultado = result;
                        this.nota.Titulo = this.capitalizarPrimeraLetra(resultado);
                    } else if (valor == 2) {
                        resultado = result
                        this.nota.Contenido = this.capitalizarPrimeraLetra(resultado);
                    } else if (valor == 3) {
                        resultado = result
                        this.palabraFiltro = resultado
                        await this.aplyFilter()
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

        cerrarSesion: function() {
                localStorage.removeItem('usuarioRec')
                Router.push({ name: 'Login' })
        },

        listarNotas: async function () {

            this.paginateData = [];
            this.actualPage = 1

            if (this.ListaNotas.length == 0) {

                let usuario = localStorage.getItem('usuarioRec')
                await ApiReconocimiento.ListadoNotas(usuario)
                    .then(response => {
                        response.data.obj.forEach(element => {
                            this.ListaNotas.push(element)
                        });
                    })

                if (this.ListaNotas.length < this.pageElements) {
                    for (let index = 0; index < this.ListaNotas.length; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                } else {
                    for (let index = 0; index < this.pageElements; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                }

            } else {
                if (this.ListaNotas.length < this.pageElements) {
                    for (let index = 0; index < this.ListaNotas.length; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                } else {
                    for (let index = 0; index < this.pageElements; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                }

            }

        },

        listarNotasE: async function () {
            this.ListaNotas = [];
            let usuario = localStorage.getItem('usuarioRec')
            await ApiReconocimiento.ListadoNotas(usuario)
                    .then(response => {
                        response.data.obj.forEach(element => {
                            this.ListaNotas.push(element)
                        });
                    })
        },

        ////////////////////////////////////////Paginado y Filtros////////////////////////////////////////

        aplyFilter: async function () {
            let usuario = localStorage.getItem('usuarioRec')
            const response = await ApiReconocimiento.ListadoNotas(usuario);
            const NotasList = response.data.obj;
            this.ListaNotas = NotasList;

            const filteredNotes = [];
            let success = false;

            for (const project of this.ListaNotas) {
                const matchesNombre = (!this.palabraFiltro || project.Titulo.toLowerCase().includes(this.palabraFiltro.toLowerCase()) || project.Contenido.toLowerCase().includes(this.palabraFiltro.toLowerCase()));

                if (matchesNombre) {
                    filteredNotes.push(project);
                    success = true;
                }
            }

            if ((!success && (this.palabraFiltro))) {
                this.paginateData = []
                this.paginate = false
                return
            }

            this.ListaNotas = filteredNotes;
            await this.listarNotas();
            await this.cutPages();
            this.actualPage = 1;
        },

        totalPages: function () {
            return Math.ceil(this.ListaNotas.length / this.pageElements)
        },

        changePage: async function (pageNum) {
            if (pageNum != "...") {
                this.paginateData = []
                if (pageNum == undefined) {
                    pageNum = 1
                }
                this.actualPage = pageNum
                let ini = (pageNum * this.pageElements) - this.pageElements;
                let end = (pageNum * this.pageElements);
                let total = this.ListaNotas.length;
                if (end < total) {
                    for (let index = ini; index < end; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                } else {
                    for (let index = ini; index < total; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                }
                await this.cutPages();
            }
        },

        goBack: async function () {
            if (this.actualPage > 1) {
                this.paginateData = []
                let paginaAnt = this.actualPage - 1
                this.actualPage = paginaAnt
                let ini = (paginaAnt * this.pageElements) - this.pageElements;
                let end = (paginaAnt * this.pageElements);
                let total = this.ListaNotas.length;
                if (end < total) {
                    for (let index = ini; index < end; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                } else {
                    for (let index = ini; index < total; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                }
                await this.cutPages();
            }
        },

        goNext: async function () {
            if (this.actualPage == this.pageNumeration.length) {
                return
            } else {
                this.paginateData = []
                let paginaAnt = this.actualPage + 1
                this.actualPage = paginaAnt
                let ini = (paginaAnt * this.pageElements) - this.pageElements;
                let end = (paginaAnt * this.pageElements);
                let total = this.ListaNotas.length;
                if (end < total) {
                    for (let index = ini; index < end; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                } else {
                    for (let index = ini; index < total; index++) {
                        this.paginateData.push(this.ListaNotas[index]);
                    }
                }
                await this.cutPages();
            }
        },

        cutPages: async function () {
            let pages = []
            let numberOfPages = Math.ceil(this.ListaNotas.length / this.pageElements)
            let actualPage = this.actualPage
            let numeration = 2
            let numerationSide = Math.floor(numeration / 2)
            let initialPage = 1
            let finalPage = numberOfPages

            if (numberOfPages > numeration) {

                if (actualPage > numerationSide) {

                    initialPage = actualPage - numerationSide

                    finalPage = actualPage + numerationSide

                } else {

                    initialPage = 1

                    finalPage = actualPage + numerationSide

                    finalPage += (numerationSide - (actualPage - 1))

                }

                if (finalPage > numberOfPages) {

                    finalPage = numberOfPages

                    initialPage = numberOfPages - numeration + 1

                }

            }

            for (let i = initialPage; i <= finalPage; i++) {

                pages.push(i)

            }

            if (actualPage > (numerationSide + 2)) { pages.unshift("...") }

            if (actualPage > (numerationSide + 1)) { pages.unshift(1) }

            if (

                (actualPage < (numberOfPages - numerationSide - 1)) &&

                numberOfPages != finalPage

            ) { pages.push("...") }

            if (

                (actualPage < (numberOfPages - numerationSide)) &&

                numberOfPages != finalPage

            ) { pages.push(numberOfPages) }

            this.pageNumeration = pages

            await this.validatePaginate();

        },

        validatePaginate: function () {
            let quantity = this.ListaNotas.length
            if (quantity < 7) {
                this.paginate = false
            } else {
                this.paginate = true
            }
        },

        ////////////////////////////////////////Paginado y Filtros////////////////////////////////////////

        eliminarNota: async function (id) {
            await ApiReconocimiento.EliminarNota(id)
                .then(async response => {
                    if (response != null){
                        var mensaje = response.data
                        this.toast(mensaje, {
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
                        await this.listarNotasE();
                        await this.listarNotas()
                        await this.cutPages()
                        return
                    }
                })
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

        computed: {
            ...mapState(
                ["mostrarNota, usuarioActivo"]
            )
        }
    
    },

    mounted: async function() {
        let loader = this.$loading.show({
            opacity: 0.8
        })
        await this.validarLogin();
        await this.recuperarDatosUsuario();
        //await this.darBienvenida()
        await this.listarNotas();
        await this.cutPages();
        this.$root.ListarNotasRoot = this.listarNotas;
        this.$root.cutPagesRoot = this.cutPages;
        this.$root.listarNotasERoot = this.listarNotasE;
        setTimeout(function() {
            loader.hide();
        }, 500)
    },

}
</script>

<style scoped>

.iniciarNota{
    color: white;
    cursor: pointer;
    font-size: large;
}

.iniciarNota:hover{
    color: rgb(1, 1, 56);
}

.iconosCards{
    font-size: 25px;
    color: #0f98f8;
    margin-top: 5px;
    background-color: transparent;
}

.iconosCards:hover{
    color: #085891;
}

.cardNotaA {
    color: red;
    font-size: large;
}

.cardNotaM {
    color: orange;
    font-size: large;
}

.cardNotaB {
    color: green;
    font-size: large;
}

.cardNota {
    border-radius: 10px;
    min-height: 200px;
    max-height: 200px;
}

.cardNota:hover {
    background-color: #dff2ff;
    box-shadow: 5px 5px 20px rgba(0,0,0,0.1);
    transform: translate(-0.5%);
    cursor: pointer;
}

.input-disabled {
  background-color: #f0f0f0;
  color: #665;
}

.icon-disabled {
  color: #f0f0f0;
  color: #665;
}

</style>