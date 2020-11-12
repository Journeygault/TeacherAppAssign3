//REFERENCE: Code used is from Learningc# for webdevelopment part 10,11 and part 12, Accessed NOV 10 2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Bellow allows access to Models Folder which has the definition for a teacher
using TeacherAppAssign3.Models;
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
        public ActionResult List()
        {
            //The Following Is what Collects the information from Teacher Data Controller and sends it to theViews/Teacher/List
            TeacherDataController controller = new TeacherDataController();
            //The Following is the method we are involing from the TeacherDataController (We defind it there)
           IEnumerable<Teacher> Teachers= controller.ListTeachers();
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
    }
}//Results:This controller routs all required information to the List and Show views, allowing for 
//access of information for all teachers, and individual teachers.