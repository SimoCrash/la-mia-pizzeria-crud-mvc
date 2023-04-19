using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
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

            if (!Ingredienti.Any())
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

            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("ADMIN"),
                    new("USER"),
                };

                Roles.AddRange(seed);
            }

            if(Users.Any(u => u.Email == "admin@admin.it" || u.Email == "user@user.it") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@admin.it");
                var user = Users.First(u => u.Email == "user@user.it");

                var adminRole = Roles.First(r => r.Name == "ADMIN");
                var userRole = Roles.First(r => r.Name == "USER");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id,
                    },

                    new()
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id,
                    },
                };
                UserRoles.AddRange(seed);
            }

            SaveChanges();
        }
    }
}
