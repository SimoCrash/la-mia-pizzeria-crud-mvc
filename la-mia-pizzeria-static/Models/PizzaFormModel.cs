namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; } = new Pizza { Foto = "https://picsum.photos/160/95" };
        public IEnumerable<Categoria> Categorie { get; set; } = Enumerable.Empty<Categoria>();
    }
}


