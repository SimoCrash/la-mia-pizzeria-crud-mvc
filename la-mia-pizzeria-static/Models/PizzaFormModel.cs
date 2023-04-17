using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; } = new Pizza { Foto = "https://picsum.photos/95/160" };
        public IEnumerable<Categoria> Categorie { get; set; } = Enumerable.Empty<Categoria>();
        public IEnumerable<SelectListItem> Ingredienti { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<string>? SelectedIngredienti { get; set; } = new();

    }
}


