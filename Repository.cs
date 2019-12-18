using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Text;
using StudentExercise5.Models;

namespace StudentExercise5
{
    public class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        //part 1---------query database for all exercises
        public List<Exercise> GetExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //getting this data from SQL
                    cmd.CommandText = "SELECT Id, ExerciseName, ProgrammingLanguage FROM Exercise";
                    // this is executing the reader, which preps everything
                    SqlDataReader reader = cmd.ExecuteReader();
                    //this list is csharp
                    List<Exercise> exercises = new List<Exercise>();
                    //this translates from SQL to csharp
                    while (reader.Read())
                    {
                        int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                        string exerciseNameValue = reader.GetString(reader.GetOrdinal("ExerciseName"));
                        string exerciseLanguageValue = reader.GetString(reader.GetOrdinal("ProgrammingLanguage"));
                        // this is the anatomy of the csharp object
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            ExerciseName = exerciseNameValue,
                            ProgrammingLanguage = exerciseLanguageValue
                        };
                        // add this csharp object to the csharp list
                        exercises.Add(exercise);
                    }
                    //stop reading
                    reader.Close();
                    //this returns a list of csharp exercises to the getExerises method.
                    return exercises;
                }
            }
        }
        //part 2-------------find all the exercises in the database where the language is JavaScript
        // get 
        public List<Exercise> GetJavascriptExercises(string language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, ExerciseName, ProgrammingLanguage FROM Exercise WHERE ProgrammingLanguage = @language";
                    cmd.Parameters.Add(new SqlParameter("@language", language));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                        string exerciseNameValue = reader.GetString(reader.GetOrdinal("ExerciseName"));
                        string exerciseLanguageValue = reader.GetString(reader.GetOrdinal("ProgrammingLanguage"));


                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            ExerciseName = exerciseNameValue,
                            ProgrammingLanguage = exerciseLanguageValue
                        };
                        exercises.Add(exercise);
                    }
                    reader.Close();
                    return exercises;
                }
            }
        }

        //part 3-------------Insert a new exercise into the database

        public void AddNewExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Exercise (ExerciseName, Programming Language) Values (@name, @language)";
                    cmd.Parameters.Add(new SqlParameter("@name", exercise.ExerciseName));
                    cmd.Parameters.Add(new SqlParameter("@language", exercise.ProgrammingLanguage));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //part 4--------------get a list of all the instructors and their cohort
        public List<Instructor> GetInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT i.Id, i.FirstName, i.LastName, i.Specialty, i.SlackHandle i.CohortId, c.CohortName" +
                                      "FROM Instructor i INNER JOIN Cohort c ON c.id = i.CohortId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        string specialty = reader.GetString(reader.GetOrdinal("Specialty"));
                        string slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle"));
                        int cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"));
                        string cohortName = reader.GetString(reader.GetOrdinal("CohortName"));

                        Instructor instructor = new Instructor
                        {
                            Id = idValue,
                            FirstName = firstName,
                            LastName = lastName,
                            Specialty = specialty,
                            CohortId = cohortId,
                            CohortName = cohortName,
                            SlackHandle = slackHandle
                        };
                        instructors.Add(instructor);
                    }
                    reader.Close();
                    return instructors;
                }
            }

        }
        //part 5--------------------insert a new instructor into the database. Assign the instructor to an existing cohort

        public void AddNewInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Instructor (FirstName, LastName, SlackHandle, CohortId) Values (@firstName, @lastName, @slackhandle, @cohortid)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@slackhandle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortid", instructor.CohortId));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //part 6--------------------Assign an existing exercise to an existing student
        public void AddExercise(int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO StudentExercises (StudentId, ExerciseId) Values (@studentId, @exerciseId)";
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@exerciseId", exerciseId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

