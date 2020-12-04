//REFERENCE: Code used is from Learningc# for webdevelopment part 10,11 and part 12, Accessed NOV 10 2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Bellow allows access to Models Folder which has the definition for a teacher
using TeacherAppAssign3.Models;
using System.Diagnostics;
///
namespace TeacherAppAssign3.Controllers
{
    /// <summary>
    /// The following takes information from the teacherDataController and sends it to the index file 
    /// (which is more or less blank),the List View and the Show View. It behaves like a police officer
    /// directing traffic to new locations.
    /// </summary>
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Authors/List (when we send a reqeust to that URL It will link it to List View)
        public ActionResult List(string Searchkey=null)//This is our bind method and allows searches
        {
            //The Following Is what Collects the information from Teacher Data Controller and sends it to theViews/Teacher/List
            TeacherDataController controller = new TeacherDataController();
            //The Following is the method we are involing from the TeacherDataController (We defind it there)
           IEnumerable<Teacher> Teachers= controller.ListTeachers(Searchkey);//Searchkey allows for a nullable search of authors
            return View(Teachers);//The Teachers is an argument to the view method, this specifies the data being returned
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            //The Following Is what Collects the information from Teacher Data Controller and sends it to theViews/Teacher/Show
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //Delete confirmation page
        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //POST : /Author/Delete{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET  :Author/New
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TeacherFname">The teacherfname from the SQL database</param>
        /// <param name="TeacherLname">The Teacherlname from the SQL database</param>
        /// <param name="Employeenumber">The Employeenumber from the SQL database</param>
        /// <param name="Hiredate"> The Hiredate from the SQL database</param>
        /// <param name="Salary">The salary from the SQL database</param>
        /// <returns> Does not return anuthing butt alows the addition of new teachers</returns>
        //POST : /Author/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname,string Employeenumber, DateTime Hiredate,decimal Salary)
        {
            //The following is to check the output,allthough its nots nessesary uts very 
            //useful for making sure the methods hooked up and running

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Employeenumber);
            Debug.WriteLine(Hiredate);
            Debug.WriteLine(Salary);

            //The following basicaly takes the SQL names assigned in the data controller
            //and makes their values accessible to the rest of the document

            Teacher NewTeacher = new Teacher();
            NewTeacher.Teacherfname = TeacherFname;
            NewTeacher.Teacherlname = TeacherLname;
            NewTeacher.Employeenumber = Employeenumber;
            NewTeacher.Hiredate = Hiredate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");

            
        }
    }

}//Results:This controller routs all required information to the List and Show views, allowing for 
//access of information for all teachers, and individual teachers.