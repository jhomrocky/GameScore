using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameScore.Models;

namespace GameScore.Controllers
{
    public class ScoresController : Controller
    {
        private GameScoreContext db = new GameScoreContext();

        // GET: Scores
        public ActionResult Index()
        {
            return View(db.Scores.ToList());
        }

        // GET: Scores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id); //searches Scores db to print info from ID #
                                              //Score score populates score variable
            if (score == null)  //controller checks db, if there is no id, returns http not found
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // GET: Scores/Create    GET: asking for stuff
        public ActionResult Create() //shows "Create" view/page
        {
            return View();
        }

        // POST: Scores/Create   POST: sending info to controller
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //data annotation sending items to page
        [ValidateAntiForgeryToken] //security feature
        public ActionResult Create([Bind(Include = "ID,Name,Team,Points")] Score score)
                                    //takes in entity as argument
        {
            if (ModelState.IsValid) //checks to make sure database is okay before adding to it
            {
                db.Scores.Add(score); //"Scores" table/model, "score" new added value
                                      //passed in on line 50
                db.SaveChanges();     //saves db changes (duh)
                return RedirectToAction("Index"); //takes user back to Index page
            }

            return View(score);
        }

        // GET: Scores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Team,Points")] Score score)
        {
            if (ModelState.IsValid) //if we're good
            {
                db.Entry(score).State = EntityState.Modified; //checks to see if scores were modified
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(score);
        }

        // GET: Scores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Score score = db.Scores.Find(id);
            db.Scores.Remove(score);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
