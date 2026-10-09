using System;
using System.Collections.Generic;
using System.Text;

namespace Conexion_A_GastosDB
{
    internal class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return $"{Id,-3} {Descripcion,-25} Q{Monto,8:N2} {Categoria}";
        }
    }
}
