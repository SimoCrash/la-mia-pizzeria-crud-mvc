using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzeriaDB;Integrated Security=True;Encrypt=False");
        }

        public void Seed()
        {
            var pizzaSeed = new Pizza[]
            {
                    new Pizza
                    {
                        Nome = "Regina Margherita",
                        Descrizione = "Pizza margherita con mozzarella di Bufala",
                        Foto = "/img/bufala.jpg",
                        Prezzo = "12.00€"
                    },
                    new Pizza
                    {
                        Nome = "Pizza Diavola",
                        Descrizione = "Pizza margherita con salame piccante",
                        Foto = "/img/diavola.jpg",
                        Prezzo = "7.00€"
                    },
                    new Pizza
                    {
                        Nome = "Pizza Americana",
                        Descrizione = "Pizza margherita con wurstel e stick",
                        Foto = "/img/americana.jpg",
                        Prezzo = "8.00€"
                    }
            };

            if (!Pizzas.Any())
            {
                Pizzas.AddRange(pizzaSeed);
            }

            if (!Categorie.Any())
            {
                var seed = new Categoria[]
                {
                    new()
                    {
                        Title = "Pizza classica",
                        Pizzas = pizzaSeed,
                    },
                    new()
                    {
                        Title = "Pizza Rossa",
                    },
                    new()
                    {
                        Title = "Pizza Bianca",
                    },
                    new()
                    {
                        Title = "Pizza gluten-free",
                    }
                };

                Categorie.AddRange(seed);
            }

            if(!Ingredienti.Any())
            {
                var seed = new Ingrediente[]
                {
                    new()
                    {
                        Name = "Mozzarella",
                        Pizzas = pizzaSeed,
                    },
                    new()
                    {
                        Name = "Mozzarella di Bufala"
                    },
                    new()
                    {
                        Name = "Pomodoro"
                    },
                    new()
                    {
                        Name = "Salame Piccante"
                    },
                    new()
                    {
                        Name = "Patatine Fritte"
                    },
                    new()
                    {
                        Name = "Wurstel"
                    }
                };

                Ingredienti.AddRange(seed);
            }

            SaveChanges();
        }
    }
}
