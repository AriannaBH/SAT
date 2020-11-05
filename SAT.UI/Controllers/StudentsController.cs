using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using SAT.UI.Utilities;

namespace SAT.UI.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {

        private SATDatabaseEntities db = new SATDatabaseEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - Using the Image Service
                //no default image to be concerned with all records in the database should have a valid file name
                //AND all files in the database should be represented in the Website Content folder.
                //if there is NO FILE in the input, maintain the existing image (Front End using the HiddenFor() field)

                //if the input is NOT Null, process the image with the other updates AND remove the OLD image
                if (studentImage != null)
                {
                    //retrieve the fileName and assign it to a variable
                    string imgName = studentImage.FileName;

                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare a good list of file extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //check the variable (ToLower()) against the list and verify the content length is less than 4MB
                    if (goodExts.Contains(ext.ToLower()) && (studentImage.ContentLength <= 4194304))
                    {

                        //rename the file using a guid (see create POST for other unique naming options) - use the Covention in BOTH places
                        imgName = Guid.NewGuid() + ext.ToLower(); //ToLower() is optional, just cleans the files on the server

                        //ResizeImage Values
                        //path
                        string savePath = Server.MapPath("~/Content/StudentImages/");

                        //actual image (converted image)
                        Image convertedImage = Image.FromStream(studentImage.InputStream);

                        //maxImageSize
                        int maxImageSize = 500;

                        //maxThumbSize
                        int maxThumbSize = 100;

                        //Call the ImageService.ResizeImage()
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        //DELETE from the Image Service and delete the old image
                        //--Image Service Make sure the file is NOT noImage.png && that it exists on the server BEFORE deleting
                        //we dont need to do that check
                        //TODO: Start Here.
                        ImageService.Delete(savePath, student.PhotoUrl);

                        #region Manual Delete code if you are NOT using the service
                        //if (book.BookImage != null && book.BookImage != "noImage.png")
                        //{
                        //    System.IO.File.Delete(Server.MapPath("~/Content/imgstore/books/" + book.BookImage));
                        //}
                        #endregion

                        //save the object ONLY if all other conditions are met
                        student.PhotoUrl = imgName;

                    }//end extgood if
                }//end if !=null
                 //HiddenFor() is used here (if the file information is not valid) OR if it fails the Ext & Size check
                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - Using the Image Service
                //no default image to be concerned with all records in the database should have a valid file name
                //AND all files in the database should be represented in the Website Content folder.
                //if there is NO FILE in the input, maintain the existing image (Front End using the HiddenFor() field)

                //if the input is NOT Null, process the image with the other updates AND remove the OLD image
                if (studentImage != null)
                {
                    //retrieve the fileName and assign it to a variable
                    string imgName = studentImage.FileName;

                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare a good list of file extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //check the variable (ToLower()) against the list and verify the content length is less than 4MB
                    if (goodExts.Contains(ext.ToLower()) && (studentImage.ContentLength <= 4194304))
                    {

                        //rename the file using a guid (see create POST for other unique naming options) - use the Covention in BOTH places
                        imgName = Guid.NewGuid() + ext.ToLower(); //ToLower() is optional, just cleans the files on the server

                        //ResizeImage Values
                        //path
                        string savePath = Server.MapPath("~/Content/StudentImages/");

                        //actual image (converted image)
                        Image convertedImage = Image.FromStream(studentImage.InputStream);

                        //maxImageSize
                        int maxImageSize = 500;

                        //maxThumbSize
                        int maxThumbSize = 100;

                        //Call the ImageService.ResizeImage()
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        //DELETE from the Image Service and delete the old image
                        //--Image Service Make sure the file is NOT noImage.png && that it exists on the server BEFORE deleting
                        //we dont need to do that check
                        //TODO: Start Here.
                        ImageService.Delete(savePath, student.PhotoUrl);

                        #region Manual Delete code if you are NOT using the service
                        //if (book.BookImage != null && book.BookImage != "noImage.png")
                        //{
                        //    System.IO.File.Delete(Server.MapPath("~/Content/imgstore/books/" + book.BookImage));
                        //}
                        #endregion

                        //save the object ONLY if all other conditions are met
                        student.PhotoUrl = imgName;

                    }//end extgood if
                }//end if !=null
                 //HiddenFor() is used here (if the file information is not valid) OR if it fails the Ext & Size check
                #endregion
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
