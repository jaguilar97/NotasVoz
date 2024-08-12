using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconocimientoVoz.EN
{
    public class Reply<T>
    {
        public Reply()
        {
            this.ok = true;
            this.msg = string.Empty;
            this.total = 0;
        }

        public void toError(bool appActiva, bool ok, int total, string msg, T obj)
        {
            this.obj = obj;
            this.ok = false;
            this.msg = msg;
            this.total = total;
        }

        public T obj { get; set; }
        public bool ok { get; set; }
        public string msg { get; set; }
        public int total { get; set; }

    }
}
