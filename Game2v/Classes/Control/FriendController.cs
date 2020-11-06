using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authorization;

using System.Linq;
using Game2v.Model;

using System.Threading.Tasks;

namespace Game2v.Control
{
    [Route("/[controller]/[action]/")]
    [Controller]
    public class FriendController : Controller
    {
        private DataContext db = null;
        public FriendController(DataContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult List()
        {
            List<Friend> model = (from e in db.Friends
                                orderby e.FriendName
                                select e).ToList();
            return View(model);
        }
        [HttpPost]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Insert(Friend model)
        {
            if (ModelState.IsValid)
            {
                await db.AddFriendAsync(model);

                TempData["HasMessage"] = "1";                
                TempData["Message"] = "Amigo salvo com sucesso";
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Update(int id)
        {
            Friend model = db.Friends.Find(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Update(Friend model)
        {
            if (ModelState.IsValid)
            {
                db.Friends.Update(model);
                db.SaveChanges();
                ViewBag.Message = "Amigo atualizado";
            }
            return View(model);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                Friend model = db.Friends.Find(id);
                await db.DeleteFriendAsync(model.FriendId);
                TempData["Message"] = "Amigo removido";
                TempData["HasMessage"] = "1";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Falha ao remover. Verifique se empresou algum game pra este amigo.";
                TempData["HasMessage"] = "1";
            }
            return RedirectToAction("List");

        }

    }

}