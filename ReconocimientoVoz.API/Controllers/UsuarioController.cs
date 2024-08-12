using ReconocimientoVoz.DAO.ClasesDAO;
using ReconocimientoVoz.EN.Entidades;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReconocimientoVoz.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class UsuarioController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage RegistrarUsuario([FromBody] UsuarioEN usuario)
        {

            int respuesta = UsuarioDAO.instance().RegistrarUsuario(usuario);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpPut]
        public HttpResponseMessage ModificarUsuario([FromBody] UsuarioEN usuario)
        {

            int respuesta = UsuarioDAO.instance().ModificarUsuario(usuario);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpPost]
        public HttpResponseMessage IniciarSesion([FromBody] UsuarioEN usuario)
        {

            int respuesta = UsuarioDAO.instance().IniciarSesion(usuario);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpGet]
        public HttpResponseMessage RecuperarDatosUsuario(string usuario)
        {

            object respuesta = UsuarioDAO.instance().RecuperarDatosUsuario(usuario);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }
    }
}
