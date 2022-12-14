using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.Domain
{
    public class Domanda
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Question { get; set; }

        [MaxLength(100)]
        public string Risposta1 { get; set; }

        [MaxLength(100)]
        public string Risposta2 { get; set; }

        [MaxLength(100)]
        public string Risposta3 { get; set; }

        [MaxLength(100)]
        public string Risposta4 { get; set; }

        [MaxLength(100)]
        public string RispostaEsatta { get; set; }

        [MaxLength(50)]
        public string? Categoria { get; set; }
    }
}
