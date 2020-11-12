//REFERENCE: Code used is from Learningc# for webdevelopment part 10,11 and part 12, Accessed NOV 10 2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// The following defines what a Teacher is, so that when referenced in another file
/// the information in the teacher class can be easily accessed from the MySql database.
/// </summary>
namespace TeacherAppAssign3.Models
    
{
    public class Teacher
    {
        //the following properties define a teacher
        public int Teacherid;
        public string Teacherfname;
        public string Teacherlname;
        public string Employeenumber;
        public DateTime Hiredate;
        public decimal Salary;
    }
}
//Result:"Teacher" is now easily refernced in all other files.