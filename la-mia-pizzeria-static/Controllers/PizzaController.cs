using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();
            var pizzas = ctx.Pizzas.Include(p => p.Categoria).ToArray();
            return View(pizzas);
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas.Include(p => p.Categoria).Include(i => i.Ingredienti).SingleOrDefault(p => p.Id == id);

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
                Ingredienti = ctx.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray(),
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
                form.Ingredienti = ctx.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
                return View(form);
            }


            form.Pizza.Ingredienti = form.SelectedIngredienti.Select(si => ctx.Ingredienti.First(i => i.Id == Convert.ToInt32(si))).ToList();
            //foreach (var ingrediente in form.SelectedIngredienti)
            //{
            //    var ingredienteId = Convert.ToInt32(ingrediente.Id);
            //    var ingredienteDB = ctx.Ingredienti.FirstOrDefault(i => i.Id == ingredienteId);
            //    form.Pizza.Ingredienti.Add(Ingrediente);
            //}

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
                Ingredienti = ctx.Ingredienti.ToArray().Select(i => new SelectListItem(i.Name, i.Id.ToString(), pizza.Ingredienti!.Any(_i => _i.Id == i.Id))).ToArray(), //PROBLEMA QUI
            };

            formModel.SelectedIngredienti = formModel.Ingredienti.Where(i => i.Selected).Select(i => i.Value).ToList();

            return View(formModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel form)
        {
            using var ctx = new PizzeriaContext();

            if (!ModelState.IsValid)
            {
                //form.Categorie = ctx.Categorie.ToArray();                             =============> dà problemi
                form.Ingredienti = ctx.Ingredienti.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
                return View(form); 
            }

            var pizzaToUpdate = ctx.Pizzas.Include(p => p.Ingredienti).FirstOrDefault(p => p.Id == id);

            if(pizzaToUpdate == null)
            {
                return View($"Non è stato trovato l'id n° {id}");
            }


            pizzaToUpdate.Nome = form.Pizza.Nome;
            pizzaToUpdate.Descrizione = form.Pizza.Descrizione;
            pizzaToUpdate.Foto = form.Pizza.Foto;    
            pizzaToUpdate.Prezzo = form.Pizza.Prezzo;
            pizzaToUpdate.CategoriaId = form.Pizza.CategoriaId;
            pizzaToUpdate.Categoria = form.Pizza.Categoria;
            pizzaToUpdate.Ingredienti = form.SelectedIngredienti.Select(si => ctx.Ingredienti.First(i => i.Id == Convert.ToInt32(si))).ToList();


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
