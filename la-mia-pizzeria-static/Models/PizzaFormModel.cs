namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; } = new Pizza();
        public IEnumerable<Categoria> Categorie { get; set; } = Enumerable.Empty<Categoria>();
    }
}
