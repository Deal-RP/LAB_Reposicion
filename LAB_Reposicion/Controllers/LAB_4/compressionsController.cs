using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LAB_Reposicion.Models.LAB_4;
using Microsoft.AspNetCore.Mvc;

namespace LAB_Reposicion.Controllers.LAB_4
{
    [Route("[controller]")]
    public class compressionsController : Controller
    {
        [HttpGet]
        public List<NodoArchivo> Get()
        {
            var compresiones = new List<NodoArchivo>();
            var logicaLIFO = new Stack<NodoArchivo>();
            var Linea = string.Empty;

            using (var Reader = new StreamReader("compressions.txt"))
            {
                while (!Reader.EndOfStream)
                {
                    var historialtemp = new NodoArchivo();
                    Linea = Reader.ReadLine();
                    historialtemp.Algoritmo = Linea;
                    Linea = Reader.ReadLine();
                    historialtemp.NombreOriginal = Linea;
                    Linea = Reader.ReadLine();
                    historialtemp.Nombre = Linea;
                    Linea = Reader.ReadLine();
                    historialtemp.RutaArchivo = Linea;
                    Linea = Reader.ReadLine();
                    historialtemp.RazonCompresion = Convert.ToDouble(Linea);
                    Linea = Reader.ReadLine();
                    historialtemp.FactorCompresion = Convert.ToDouble(Linea);
                    Linea = Reader.ReadLine();
                    historialtemp.Porcentaje = Convert.ToDouble(Linea);
                    logicaLIFO.Push(historialtemp);
                }
            }

            while (logicaLIFO.Count != 0)
            {
                compresiones.Add(logicaLIFO.Pop());
            }

            return compresiones;
        }
    }
}
