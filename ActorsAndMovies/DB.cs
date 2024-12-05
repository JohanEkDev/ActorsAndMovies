using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorsAndMovies
{
    internal class DB
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        public SqlDataReader FindActorByName(string inputActorName)
        {
            SqlCommand commandFindNames = new SqlCommand($"SELECT * FROM Actor " +
                        $"WHERE first_name='{inputActorName}' " +
                        $"OR last_name='{inputActorName}'", connection);
            SqlDataReader readActorNames = commandFindNames.ExecuteReader();
            return readActorNames;
        }

        public SqlDataReader FindMoviesByActorId(int inputActorId)
        {
            SqlCommand commandFindMovies = new SqlCommand($"SELECT actor.actor_id, actor.first_name, actor.last_name, film.title FROM film " +
                            $"inner join film_actor ON film.film_id=film_actor.film_id " +
                            $"inner join actor ON film_actor.actor_id=actor.actor_id " +
                            $"WHERE actor.actor_id={inputActorId};", connection);
            SqlDataReader readActorMovies = commandFindMovies.ExecuteReader();
            return readActorMovies;
        }
        public void connectionOpen()
        {
            connection.Open();
        }
        public void connectionClose()
        {
            connection.Close();
        }
    }
}
