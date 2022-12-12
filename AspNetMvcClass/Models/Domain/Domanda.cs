using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.Domain
{
    public class Domanda
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Question { get; set; }

        [MaxLength(200)]
        public string Risposta1 { get; set; }

        [MaxLength(200)]
        public string Risposta2 { get; set; }

        [MaxLength(200)]
        public string Risposta3 { get; set; }

        [MaxLength(200)]
        public string Risposta4 { get; set; }

        [MaxLength(200)]
        public string RispostaEsatta { get; set; }
    }
}
