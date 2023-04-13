using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();
            var pizzas = ctx.Pizzas.ToArray();
            return View(pizzas);
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas.SingleOrDefault(p => p.Id == id);

            if(pizza == null)
            {
                return NotFound($"Non è stato trovato l'id n° {id}");
            }

            return View(pizza);
        }

        public IActionResult Create()
        {
            var pizza = new Pizza
            {
                Foto = "https://picsum.photos/200/300",
            };
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(!ModelState.IsValid)
            {
                return View(pizza);
            }
            
            using var ctx = new PizzeriaContext();

            ctx.Pizzas.Add(pizza);
            ctx.SaveChanges(); //Attenzione dà problemi dopo UpdatesPizzaInModel e database update se non inserisci l'img

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizza == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizza)
        {

            if(!ModelState.IsValid)
            { 
                return View(pizza); 
            }

            using var ctx = new PizzeriaContext();
            var pizzaToUpdate = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaToUpdate == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }

            pizzaToUpdate.Nome = pizza.Nome;
            pizzaToUpdate.Descrizione = pizza.Descrizione;
            pizzaToUpdate.Foto = pizza.Foto;    
            pizzaToUpdate.Prezzo = pizza.Prezzo;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
