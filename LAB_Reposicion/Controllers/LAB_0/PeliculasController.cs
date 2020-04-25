using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAB_Reposicion.Models;

namespace LAB_Reposicion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeliculasController : ControllerBase
    {
        [HttpGet]
        public List<Pelicula> DesplegarLista()
        {
            var cont = InstancePeliculas.Instance.listPelicula.Count < 10 ? InstancePeliculas.Instance.listPelicula.Count() : 10;
            var start = InstancePeliculas.Instance.listPelicula.Count < 10 ? 0 : InstancePeliculas.Instance.listPelicula.Count() - 10;
            var stackPelicula = new Stack<Pelicula>(InstancePeliculas.Instance.listPelicula.GetRange(start, cont).ToArray());
            var datosRetornar = new List<Pelicula>();

            for (int i = 0; i < cont; i++)
            {
                datosRetornar.Add(stackPelicula.Pop());
            }
            return datosRetornar;
        }

        [HttpPost]
        public void ObtenerDatos([FromForm] Pelicula nuevaPelicula)
        {
            InstancePeliculas.Instance.listPelicula.Add(nuevaPelicula);
        }
    }
}
