//REFERENCE: Code used is from Learningc# for webdevelopment part 10,11 and part 12, Accessed NOV 10 2020
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TeacherAppAssign3.Models;

namespace TeacherAppAssign3.Controllers
{
    /// <summary>
    /// The following is a data controller which oppens the connectio
    /// n to MySql, accesses the database, removes the information, converts it into a string and then closes the connection.
    /// </summary>
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext teacherdatabase = new SchoolDbContext();
        //api/TeacherData/ListTeachers
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]//The question mark makes it an optional perameter
        public IEnumerable <Teacher> ListTeachers (string Searchkey=null)//string search key allows us to modify sql for search, null means it can not be provided
        {
            //The following connects to the database
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            //the following opens the connection
            conn.Open();
            //The following is what allows you to create SQL commands
            MySqlCommand cmd = conn.CreateCommand();
            //The Sql command itself
            cmd.CommandText = "select * from teachers where lower(teacherfname) like lower(@key) " +
                "or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key","%" + Searchkey + "%"); //using the @ key and this comand we are sanitizing our sql to prevent injection attacks
            cmd.Prepare();

           //The following allows information to actualy be read by the browser 
           //from the sql table 
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            //The following indicates we are returning a list of teachers
            List<Teacher> Teachers = new List<Teacher> { };
           //A loop that will "read" off all of the information in the MySql Database  and convert it to strings
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string AuthorFname = (string)ResultSet["teacherfname"];
                string AuthorLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                //The following indicates what information from the database is in the Teachers model
                //Teacher model is sort of like a manikin and the following is sort of like the clothing
                //The model emplies something exists, and the following is what is implied 
                Teacher NewTeacher = new Teacher();
                NewTeacher.Teacherid = TeacherId;
                NewTeacher.Teacherfname = AuthorFname;
                NewTeacher.Teacherlname = AuthorLname;
                NewTeacher.Employeenumber = EmployeeNumber;
                NewTeacher.Hiredate = HireDate;
                NewTeacher.Salary = Salary;
               
                Teachers.Add(NewTeacher);
            }
            //The following closes the Connection
            conn.Close();
            //The following returns all teachers and their information.
            return Teachers;
        }
        /// <summary>
        /// The following uses a specific teacher ID in the URl to return all the information about one teacher
        /// //It is effectively the same as the previous method with a few small differences 
        /// </summary>
        /// <param name="id">this is the teacherid which is used to find information about a specific teacher</param>
        /// <returns></returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            //the following indicates were creating a new instance of Teacher
            Teacher NewTeacher = new Teacher();
            //The following connects to the database
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            //the following opens the connection
            conn.Open();
            //The following creates the ability to issue as MySql Command
            MySqlCommand cmd = conn.CreateCommand();
            //The following is the new MySql command NOTE: we are indicating a SPECIFIC teacher ID
            cmd.CommandText = "select * from teachers where teacherid =" +id;
           //The following "reads: it
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
                //This functions exactly the same as the previous while loop accept because of the
                //different MySql command it will return the information for 1 teacher indicated by the 
                //teacher ID
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.Teacherid = TeacherId;
                NewTeacher.Teacherfname = TeacherFname;
                NewTeacher.Teacherlname = TeacherLname;
                NewTeacher.Employeenumber = EmployeeNumber;
                NewTeacher.Hiredate = HireDate;
                NewTeacher.Salary = Salary;

            }
            //The following returns the information of ONE specific teacher
            return NewTeacher;
            //Results: both methods function correctly and allow for the collection of data from all teachers
            //and all individual teachers
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// ////<example>Post:/api/TeacherData/DeleteAuthor/3</example>
        /// 

        //<summary>
        /// The following is used to delete a teacher
        /// //It is effectively the same as the previous method with a few small differences 
        /// </summary>
        /// <returns> Doent return anything but effectively removes data</returns>
        [HttpPost]
        ///
        public void DeleteTeacher(int id)
        {
            
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// Adds a new teacher, functions the same as delete more or less, with the primary difference being a diferent sql query
        /// </summary>
        /// <param name="NewTeacher"></param>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) " +
                "values (@TeacherFname,@TeacherLname,@Employeenumber,@Hiredate,@Salary)";
            //NOTE: I am not using CURRENT_DATE for hire date, as employees are rarely entered into the system on the actual date they are hired
            //The following subsstantiates all the values in the AddTeacher method
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.Teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.Teacherlname);
            cmd.Parameters.AddWithValue("@Employeenumber", NewTeacher.Employeenumber);
            cmd.Parameters.AddWithValue("@Hiredate", NewTeacher.Hiredate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //<Results> Successfully adds new teacher info
    }
    
}
