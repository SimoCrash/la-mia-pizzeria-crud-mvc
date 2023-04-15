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
            using var ctx = new PizzeriaContext();

            var formModel = new PizzaFormModel
            {
                Categorie = ctx.Categorie.ToArray(),
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel form)
        {
            using var ctx = new PizzeriaContext();

            if (!ModelState.IsValid)
            {
                form.Categorie = ctx.Categorie.ToArray();
                return View(form);
            }
            

            ctx.Pizzas.Add(form.Pizza);
            ctx.SaveChanges(); //Attenzione dà problemi dopo UpdatesPizzaInModel e database update se non inserisci l'img

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }

            var formModel = new PizzaFormModel
            {
                Pizza = pizza,
                Categorie = ctx.Categorie.ToArray(),
            };

            return View(formModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel form)
        {

            if(!ModelState.IsValid)
            { 
                return View(form); 
            }

            using var ctx = new PizzeriaContext();
            var pizzaToUpdate = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaToUpdate == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }

            pizzaToUpdate.Nome = form.Pizza.Nome;
            pizzaToUpdate.Descrizione = form.Pizza.Descrizione;
            pizzaToUpdate.Foto = form.Pizza.Foto;    
            pizzaToUpdate.Prezzo = form.Pizza.Prezzo;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizzaToDelete = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizzaToDelete == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }
            
            ctx.Pizzas.Remove(pizzaToDelete);
            ctx.SaveChanges();

            return RedirectToAction("Index");
           
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
