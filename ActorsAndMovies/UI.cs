using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorsAndMovies
{
    internal class UI
    {
        public void MainMenu(DB database)
        {
            Console.Clear();
            Console.WriteLine("WELCOME TO THE SAKILA ACTOR SEARCH ENGINE.");
            Console.WriteLine("ENTER THE NAME OF AN ACTOR, AND WE WILL PRESENT POSSIBLE MATCHES.");
            string inputActorName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputActorName))
            {
                Console.Clear();
                Console.WriteLine("A BLANK ENTRY IS NOT ACCEPTABLE. TRY AGAIN.");
                PressKeyMenu();
                this.MainMenu(database);
            }
            else
            {
                SqlDataReader readActorName = database.FindActorByName(inputActorName);
                bool namesFound = this.PrintActorNames(database, readActorName);
                if (namesFound == true)
                {
                    this.Submenu(database);
                }
                else
                {
                    PressKeyMenu();
                    this.MainMenu(database);
                }
            }
        }

        public void Submenu(DB database)
        {
            Console.WriteLine("BROWSE AND ENTER THE ID NUMBER OF YOUR DESIRED ACTOR.");
            Console.WriteLine("WE WILL THEN PRESENT A LIST OF MOVIES ASSOCIATED WITH THEM.");
            try
            {
                int inputActorId = Convert.ToInt32(Console.ReadLine());
                SqlDataReader readMovies = database.FindMoviesByActorId(inputActorId);
                bool moviesFound = this.PrintActorMovies(database, readMovies);
                if (moviesFound == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("THANK YOU FOR USING SAKILA ACTOR SEACH ENGINE.");
                    Console.WriteLine("PRESS ANY KEY TO EXIT. HAVE A NICE DAY!");
                    Console.ReadKey();
                }
                else
                {
                    PressKeyMenu();
                    this.MainMenu(database);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("INVALID ENTRY. ONLY NUMBERS ACCEPTABLE AS INPUT.");
                this.PressKeyMenu();
                this.MainMenu(database);
            }
        }

        public bool PrintActorNames(DB database, SqlDataReader readActorNames)
        {
            bool namesFound;
            Console.Clear();
            if (readActorNames.HasRows)
            {
                while (readActorNames.Read())
                {
                    Console.WriteLine($"ID: {readActorNames[0]} - {readActorNames[1]} {readActorNames[2]}");
                }
                Console.WriteLine();
                readActorNames.Close();
                return namesFound = true;
            }
            else
            {
                Console.WriteLine("NO MATCHES WERE FOUND. TRY A DIFFERENT NAME.");
                readActorNames.Close();
                return namesFound = false;
            }
        }

        public bool PrintActorMovies(DB database, SqlDataReader readActorMovies)
        {
            bool moviesFound;
            Console.Clear();
            if (readActorMovies.HasRows)
            {
                readActorMovies.Read();
                Console.WriteLine($"ID: {readActorMovies[0]} - {readActorMovies[1]} {readActorMovies[2]}");
                Console.WriteLine();
                while (readActorMovies.Read())
                {
                    Console.Write($"{readActorMovies[3]}, ");
                }
                Console.WriteLine();
                readActorMovies.Close();
                return moviesFound = true;
            }
            else
            {
                Console.WriteLine("THERE ARE NO MOVIES ASSOCIATED WITH THAT ACTOR.");
                readActorMovies.Close();
                return moviesFound = false;
            }
        }
        public void PressKeyMenu()
        {
            Console.WriteLine();
            Console.WriteLine("PRESSING ANY KEY WILL RETURN YOU TO THE MENU.");
            Console.ReadKey();
        }

    }
}
