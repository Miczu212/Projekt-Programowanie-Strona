using System.ComponentModel.DataAnnotations;

namespace tysjyfgjkhfghjetsrstr.Models
{
    public class polaczenia
    {
        [Key] 
        public int Id { get; set; }
        public string nazwa { get; set; }
        public string stacja1 { get; set; }
        public string stacja2 { get; set; }
    }
}
