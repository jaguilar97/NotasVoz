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

    public class NotaController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GuardarNota([FromBody] NotaEN nota)
        {

            string respuesta = NotaDAO.instance().GuardarNota(nota);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpPut]
        public HttpResponseMessage ActualizarNota([FromBody] NotaEN nota)
        {

            string respuesta = NotaDAO.instance().ActualizarNota(nota);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpDelete]
        public HttpResponseMessage EliminarNota(int NotaId)
        {

            string respuesta = NotaDAO.instance().EliminarNota(NotaId);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);

        }

        [HttpGet]
        public HttpResponseMessage ListadoNotas(string usuario)
        {

            object respuesta = NotaDAO.instance().ListadoNotas(usuario);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);
        }

        [HttpGet]
        public HttpResponseMessage RecuperarNota(int NotaId)
        {

            object respuesta = NotaDAO.instance().RecuperarNota(NotaId);

            return Request.CreateResponse(HttpStatusCode.Created, respuesta);
        }

    }
}
