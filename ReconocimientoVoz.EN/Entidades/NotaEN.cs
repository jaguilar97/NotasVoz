using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconocimientoVoz.EN.Entidades
{
    public class NotaEN
    {
        public int NotaId { get; set; }
        public string Usu_Login { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public int Prioridad { get; set; }
    }
}
