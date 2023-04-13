
using la_mia_pizzeria_static.Attributes;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserisci il Nome della pizza che vuoi aggiungere.")]
        [StringLength(100, ErrorMessage = "Puoi inserire fino a un massimo di 100 caratteri")]
        [ParoleMultiple(5)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserisci la Descrizione della pizza che vuoi aggiungere.")]
        public string Descrizione { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserisci una foto della pizza che vuoi aggiungere.")]
        public string? Foto { get; set; }

        [Required(ErrorMessage = "Inserisci il Prezzo della pizza che vuoi aggiungere.")]
        [Range(1, 30, ErrorMessage = "Inserisci il Prezzo della pizza che vuoi aggiungere, superiore a 1.")]
        public string Prezzo { get; set; } = string.Empty;
    }
}