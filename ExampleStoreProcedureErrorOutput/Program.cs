using System;
using System.Data.SqlClient;

Console.WriteLine("Ein Beispiel wie man eine Error Meldung aus einer Store Procedure ausgegeben werden kann");

Console.WriteLine("Store Procedure wird ausgeführt ohne Ausnahmefehler!");
RunStoreProcedure(false);

Console.WriteLine("\r\nStore Procedure wird ausgeführt mit Ausnahmefehler!");
RunStoreProcedure(true);

Console.WriteLine("\r\nPress enter to close application");
Console.ReadLine();

static void RunStoreProcedure(bool throwError)
{
    var connection = new SqlConnection(
        @"server=127.0.0.1,1433;User Id=CustomUser;Password=passwort;Database=example-data;");
    connection.Open();

    var query = "DECLARE @StatusOutput AS NVARCHAR(20), @ErrorOutput AS NVARCHAR(100);" +
                $"EXEC [dbo].[myStoreProcedure] {(throwError ? 1 : 0)}, " +
                "@StatusOutput OUTPUT, " +
                "@ErrorOutput OUTPUT; " +
                "SELECT @StatusOutput AS Status, @ErrorOutput AS Error";

    var command = new SqlCommand(
        query, 
        connection);
    var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.Write("Ergebnis: ");
        for (var iColumn = 0; iColumn < reader.GetColumnSchema().Count; iColumn++)
        {
            Console.Write($"{reader.GetValue(iColumn)}, ");
        }
        Console.WriteLine("");
    }
    connection.Close();
}