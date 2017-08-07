using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TestXML.Business;
using TestXML.Business.ViewModel;
using TestXML.Models;

namespace TestXML.Controllers
{
    public class HomeController : Controller
    {
        PersonBusiness pb = new PersonBusiness();
        public ActionResult Index()
        {
            var people = pb.People();
            if (people != null)
            {
                return View(people);
            }

            return RedirectToAction("Create");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
          return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonModel person)
        {
            var pBus = new PersonBusiness();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Error Please Try Again !");                
                using (pBus)
                {
                    return View(person);
                }
            }
            if (person.FileUpload != null)
            {
                using (var ms = new MemoryStream())
                {
                    person.FileUpload.InputStream.CopyTo(ms);
                    var array = ms.GetBuffer();
                    person.Base64String = Convert.ToBase64String(array);
                }
            }
            int id = pBus.SavePerson(person);
            return RedirectToAction("Details/"+id);
        }
        // Get: Get Person using Id
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            var PersonModel = pb.Find(id);
            return View(PersonModel);
        }

        // Get: Get Person using Id
        public ActionResult Edit(int? id)
        {
            if (id == null){ return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var PersonModel = pb.Find(id);

            if(PersonModel == null) { return HttpNotFound(); }

            return View(PersonModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Try Again");
            }
            var pModel = pb.Find(personModel.Id);

            using (var pBus = new PersonBusiness()) {
                if (pModel == null) return RedirectToAction("Index");
                if (personModel.FileUpload != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        personModel.FileUpload.InputStream.CopyTo(ms);
                        var array = ms.GetBuffer();
                        personModel.Base64String = Convert.ToBase64String(array);
                    }
                }

                pModel = pBus.Update(personModel);

                return RedirectToAction("Details/"+ pModel.Id);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pModel = pb.Find(id);
            if (pModel == null)
            {
                return HttpNotFound();
            }
            return View(pModel);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pModel = pb.Find(id);

            if (pModel != null) 
                if(pb.DeletePerson(id))
                    return RedirectToAction("Index");

            return RedirectToAction("Details/" + pModel.Id);
        }
    }
}