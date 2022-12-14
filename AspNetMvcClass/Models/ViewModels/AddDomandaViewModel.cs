using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models
{
    public class AddDomandaViewModel
    {
        [Display(Name = "Domanda")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Question { get; set; }
        [Display(Name = "Risposta1")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Risposta1 { get; set; }
        [Display(Name = "Risposta2")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Risposta2 { get; set; }
        [Display(Name = "Risposta3")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Risposta3 { get; set; }
        [Display(Name = "Risposta4")]
        [Required(ErrorMessage = "Campo necessario")]
        public string Risposta4 { get; set; }
        [Display(Name = "Risposta Esatta")]
        [Required(ErrorMessage = "Campo necessario")]
        public string RispostaEsatta { get; set; }
        [Display(Name = "Categoria")]
        public string? Categoria { get; set; }
    }
}
