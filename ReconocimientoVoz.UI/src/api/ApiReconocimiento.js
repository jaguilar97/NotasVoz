import axios from 'axios'
import api from '@/configuracion.js'

const apiCliente = axios.create({
    baseURL: api.urlApi
})

export default {

    ////////////////////////////////////////////// Login //////////////////////////////////////////////

    registrarUsuario(usuario) {
        return apiCliente.post('Usuario/RegistrarUsuario', usuario, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    ModificarUsuario(usuario) {
        return apiCliente.put('Usuario/ModificarUsuario', usuario, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    IniciarSesion(usuario) {
        return apiCliente.post('Usuario/IniciarSesion', usuario, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    ////////////////////////////////////////////// Inicio //////////////////////////////////////////////

    RecuperarDatosUsuario(usuario) {
        return apiCliente.get('Usuario/RecuperarDatosUsuario?usuario=' + usuario, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },
    
    GuardarNota(nota) {
        return apiCliente.post('Nota/GuardarNota', nota, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },
    
    ActualizarNota(nota) {
        return apiCliente.put('Nota/ActualizarNota', nota, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    EliminarNota(NotaId) {
        return apiCliente.delete('Nota/EliminarNota?NotaId=' + NotaId, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    ListadoNotas(usuario) {
        return apiCliente.get('Nota/ListadoNotas?usuario=' + usuario, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

    RecuperarNota(NotaId) {
        return apiCliente.get('Nota/RecuperarNota?NotaId=' + NotaId, {
            withCredentials: false,
            headers: { 'Content-Type': 'application/json; charset=UTF-8' }
        })
    },

}