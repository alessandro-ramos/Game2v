using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authorization;

using System.Linq;
using Game2v.Model;

namespace Game2v.Control
{
    [Route("/[controller]/[action]/")]
    [Controller]
    public class GameController : Controller
    {
        private DataContext db = null;
        public GameController(DataContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult List()
        {
            List<Game> model = (from e in db.Games
                                orderby e.GameTitle
                                select e).ToList();
            return View(model);
        }
        [HttpPost]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Insert(Game model)
        {
            FillFriends();
            if (ModelState.IsValid)
            {
                db.Games.Add(model);
                db.SaveChanges();
                TempData["HasMessage"] = "1";                
                TempData["Message"] = "Jogo salvo com sucesso";
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Update(int id)
        {
            FillFriends();
            Game model = db.Games.Find(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Update(Game model)
        {
            FillFriends();
            if (ModelState.IsValid)
            {
                db.Games.Update(model);
                db.SaveChanges();
                ViewBag.Message = "Jogo atualizado";
            }
            return View(model);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            Game model = db.Games.Find(id);
            System.Console.WriteLine("parametro = " + id.ToString());
            System.Console.WriteLine("id objeto = " + model.GameId.ToString());
            db.Games.Remove(model);
            db.SaveChanges();
            TempData["Message"] = "Jogo removido";
            TempData["HasMessage"] = "1";
            return RedirectToAction("List");
        }

        private void FillFriends()
        {
            List<SelectListItem> friends =
            (from f in db.Friends
             orderby f.FriendName ascending
             select new SelectListItem()
             {
                 Text = f.FriendName,
                 Value = f.FriendId.ToString()
             }).ToList();
            ViewBag.Friends = friends;
        }
    }

}