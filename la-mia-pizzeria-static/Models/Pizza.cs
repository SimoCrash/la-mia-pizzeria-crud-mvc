
using la_mia_pizzeria_static.Attributes;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserisci il Nome della pizza che vuoi aggiungere.")]
        [StringLength(100, ErrorMessage = "Puoi inserire fino a un massimo di 100 caratteri")]
        //[ParoleMultiple(5)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserisci la Descrizione della pizza che vuoi aggiungere.")]
        public string Descrizione { get; set; } = string.Empty;

        public string? Foto { get; set; }

        //public byte[]? Immagine { get; set; }

        //public string ImgSrc => Immagine is null ? Foto : $"data:image/png;base64, {Convert.ToBase64String(Immagine)}";

        [Required(ErrorMessage = "Inserisci il Prezzo della pizza che vuoi aggiungere.")]
        //[Range(1, 30, ErrorMessage = "Inserisci il Prezzo della pizza che vuoi aggiungere, superiore a 1.")]
        public string Prezzo { get; set; } = string.Empty;

        [NoZeroOption(ErrorMessage = "Devi inserire almeno un valore.")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public List<Ingrediente>? Ingredienti { get; set; }
    }
}