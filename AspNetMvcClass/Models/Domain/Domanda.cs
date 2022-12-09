using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.Domain
{
    public class Domanda
    {
        public int Id { get; set; }        
        public string Question { get; set; }
        public string Risposta1 { get; set; }
        public string Risposta2 { get; set; }
        public string Risposta3 { get; set; }
        public string Risposta4 { get; set; }        
        public string RispostaEsatta { get; set; }
    }
}
