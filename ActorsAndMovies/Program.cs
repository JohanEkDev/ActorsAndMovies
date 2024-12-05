using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
namespace ActorsAndMovies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI userInterface = new UI();
            DB database = new DB();
            database.connectionOpen();
            userInterface.MainMenu(database);
            database.connectionClose();
        }
    }
}//V.48 Databasutveckling, ADO.Net Koppling mot Sakila - Johan Ek
