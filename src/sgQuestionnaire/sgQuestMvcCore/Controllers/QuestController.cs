using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sgQuestMvcCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace sgQuestMvcCore.Controllers
{
    public class QuestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["horoscopes"] = new SelectList( Quest.horoscopes, "Id", "Text" );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("name,join,horoscope,memo")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                // 実際は保存処理が入る
                // _context.Add(quest);
                // await _context.SaveChangesAsync();
                return View("Result", quest);
                // return RedirectToAction("Post");
            }
            return View(quest);
        }
    }
}
