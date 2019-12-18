
using System;
using System.Collections.Generic;
using StudentExercise5.Models;

namespace StudentExercise5
{
    class Program
    {
        static void Main(string[] args)
        {

            ////part 1---------query database for all exercises

            Repository repository = new Repository();

            //My list of exercises from SQL stored in exercises
            List<Exercise> exercises = repository.GetExercises();
            //just writing it in the console
            Console.WriteLine("All Exercises");
            //this says go into my list and get everything
            foreach (Exercise e in exercises)
            {
                Console.WriteLine($"{e.ExerciseName}: {e.ProgrammingLanguage}");
            };
            Console.ReadLine();

            //part 2----------find all the exercises in the database where the language is JavaScript

            List<Exercise> jsExercises = repository.GetJavascriptExercises("Javascript");
            Console.WriteLine("Javascript Exercises");
            foreach (Exercise e in jsExercises)
            {
                Console.WriteLine($"{e.ExerciseName}: {e.ProgrammingLanguage}");

            };
            Console.ReadLine();

            //part 3----------- insert new exercise into database
            Exercise AddNewExercise = new Exercise
            {
                ExerciseName = "Trestlebridge",
                ProgrammingLanguage = "C sharp"
            };

            List<Exercise> newExercises = repository.GetExercises();
            Console.WriteLine("Add a new exercise"); 
            foreach (Exercise e in newExercises)
            {
                Console.WriteLine($"{e.ExerciseName}: {e.ProgrammingLanguage}");
            }
            Console.ReadLine();

            // part 4-------------------- find all instructors in the database. Include each instructor's cohort

            List<Instructor> instructors = repository.GetInstructors();
            Console.WriteLine("All Instructors");
            foreach (Instructor i in instructors)
            {
                Console.WriteLine($"{i.CohortId}: {i.FirstName} {i.LastName}, {i.Specialty}");
            }
            Console.ReadLine();

            //part 5---------------insert a new instructor into the database. Assign the instructor to an existing cohort

            Instructor NewInstructor = new Instructor
            {
                FirstName = "Jim",
                LastName = "Jimington",
                SlackHandle = "JimJimington",
                Specialty = "Jiming",
                CohortId = 2
            };

            List<Instructor> updatedInstructors = repository.GetInstructors();
            Console.WriteLine("Add a new Instructor");
            foreach (Instructor i in updatedInstructors)
            {
                Console.WriteLine($"{i.FirstName} {i.LastName} {i.SlackHandle} {i.Specialty} {i.CohortId}");
            }
            Console.ReadLine();

            //part 6--------------------Assign an existing exercise to an existing student
        }
    }
}
