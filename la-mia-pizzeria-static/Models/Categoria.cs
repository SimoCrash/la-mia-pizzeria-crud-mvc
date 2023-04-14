using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Inserisci un titolo alla categoria.")]
        [StringLength(100, ErrorMessage = "Inserisci un titolo di massimo 100 caratteri.")]
        public string Title { get; set; } = string.Empty;
        public IEnumerable<Pizza> Pizzas { get; set; } = Enumerable.Empty<Pizza>();
    }
}
