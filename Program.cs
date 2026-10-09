using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
namespace Conexion_A_GastosDB
{
    internal class Program
    {
        private const string CadenaConexion =
        @"Server=(localdb)\ProjectModels;" +
        @"Database=GastosDB;" +
        @"Trusted_Connection=True;" +
        @"TrustServerCertificate=True;";    
        static void Main(string[] args)
        {
            List<Gasto> gastos = LeerGastosDesdeBaseDeDatos();
            if (gastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
            }
            else
            {
                Console.WriteLine("Id/       Descripcion/      Monto/       Categorio");
                Console.WriteLine("--------------------------------------------------");
                decimal total = 0;
                foreach (Gasto g in gastos)
                {
                    Console.WriteLine(g.ToString());
                    total += g.Monto;
                }
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"TOTAL GASTADO: {total:N2}");
            }
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para salir..."); 
            Console.ReadKey();
        }
        
        private static List<Gasto> LeerGastosDesdeBaseDeDatos()
        {
            List<Gasto> lista= new List<Gasto>();
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string sql = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos";
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Gasto g = new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        };
                        lista.Add(g);
                    }
                }
            }
            return lista;
        }
    }
}
