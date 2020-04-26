using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_Reposicion.Models.LAB_2
{
    public class Producto : IComparable
    {
        public string Nombre { get; set; }

        public string Sabor { get; set; }

        public double Volumen { get; set; }

        public double Precio { get; set; }

        public string CasaProductora { get; set; }

        public int CompareTo(object obj)
        {
            return this.Nombre.CompareTo(((Producto)obj).Nombre);
        }

        public static string ProductoToString(object info)
        {
            var Actual = (Producto)info;
            Actual.Nombre = Actual.Nombre == null ? "" : Actual.Nombre;
            Actual.Sabor = Actual.Sabor == null ? "" : Actual.Sabor;
            Actual.CasaProductora = Actual.CasaProductora == null ? "" : Actual.CasaProductora;

            return $"{string.Format("{0,-100}", Actual.Nombre)}{string.Format("{0,-100}", Actual.Sabor)}{string.Format("{0,-100}", Actual.Volumen.ToString())}{string.Format("{0,-100}", Actual.Precio.ToString())}{string.Format("{0,-100}", Actual.CasaProductora)}";
        }

        public static Producto StringToProducto(string info)
        {
            var infoSeparada = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                infoSeparada.Add(info.Substring(0, 100));
                info = info.Substring(100);
            }
            return new Producto() { Nombre = infoSeparada[0].Trim(), Sabor = infoSeparada[1].Trim(), Volumen = Convert.ToDouble(infoSeparada[2]), Precio = Convert.ToDouble(infoSeparada[3]), CasaProductora = infoSeparada[4].Trim() };
        }

    }
}
