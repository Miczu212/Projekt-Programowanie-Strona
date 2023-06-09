using System.ComponentModel.DataAnnotations;

namespace tysjyfgjkhfghjetsrstr.Models
{
    public class listapolaczenia
    {   public listapolaczenia(List<polaczenia> a)
        {
            this.lista = a;
        }
        public List<polaczenia> lista { get; set; }
        public bool czyprzesiadka=false;
}
}
