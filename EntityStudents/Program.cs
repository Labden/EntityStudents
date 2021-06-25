using EntityStudents.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            //Scaffold-DbContext "Server=.\SQLExpress;Database=StudentsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

            StudentsDBContext db = new StudentsDBContext();

            Student s = new Student() { Name = "Juan", Hometown = "Detroit", FavoriteFood = "Pasta",};
            AddStudent(s, db);

            List<Student> students = db.Students.ToList();

            //foreach (Student s in students)
            //{
            //    Console.WriteLine("Id Number: " + s.Id);
            //    Console.WriteLine("Name: " + s.Name);
            //    Console.WriteLine("Hometown: " + s.Hometown);
            //    Console.WriteLine("Favorite Food: " + s.FavoriteFood);

            //}

            Student searchResult = db.Students.Find(1);
            if (searchResult != null)
            {
                Console.WriteLine(searchResult.Name);
            }
            else
            {
                Console.WriteLine("No student was found at the input index");
            }
            List<Student> searchResults = searchStudentsByName("alkjhskjhsaad", db);
        }
        //Read
        public static Student searchStudentsById(int id, StudentsDBContext db)
        {
            try
            {
                Student s = db.Students.Find(id);

                return s;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        public static List<Student> searchStudentsByName(string name, StudentsDBContext db)
        {
            //When a linq statement resturns an Ienumerable call .ToList() on it
            List<Student> results = db.Students.Where(x => x.Name == name).ToList();
            return results;
        }
        public static void AddStudent(Student newStudent, StudentsDBContext db)
        {
            //This is the same as an INSERT statement in SQL
            db.Students.Add(newStudent);

            //This makes any changes we have done on the C#/.Net side of things permanent
            //You will need to call this method any time you want to make changes
            //IE Any CRUD action other than Read
            //db.SaveChanges();
        }
        //Update
        public static void UpdateStudent(int id, StudentsDBContext db)
        {
            //Find the student by id
            Student s = db.Students.Find(id);

            //change something
            s.Name = "Frankie";

            //Pass their updates along and save our changes
            db.Students.Update(s);
            db.SaveChanges();

        }
        public static void DeleteStudent(int id, StudentsDBContext db)
        {
            Student s = db.Students.Find(id);

            db.Students.Remove(s);
        }
    }
}
