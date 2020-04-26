using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arboles;
using LAB_Reposicion.Models.LAB_2;
using Microsoft.AspNetCore.Mvc;

namespace LAB_Reposicion.Controllers
{
    delegate string ObjectToString(object o);
    delegate object StringToObject(string s);

    [Route("[controller]")]
    public class productoController : Controller
    {
        [HttpPost]
        public void Agregar([FromForm]Producto dato)
        {
            ArbolB<Producto>.IniciarArbol("Productos", new StringToObject(Producto.StringToProducto), new ObjectToString(Producto.ProductoToString));
            ArbolB<Producto>.InsertarArbol(dato);
        }

        [HttpGet]
        public List<Producto> RecorridoInOrder()
        {
            ArbolB<Producto>.IniciarArbol("Productos", new StringToObject(Producto.StringToProducto), new ObjectToString(Producto.ProductoToString));
            return ArbolB<Producto>.Recorrido(null, null);
        }

        [HttpGet, Route("busqueda")]
        public List<Producto> Busqueda([FromForm]Producto dato)
        {
            ArbolB<Producto>.IniciarArbol("Productos", new StringToObject(Producto.StringToProducto), new ObjectToString(Producto.ProductoToString));
            return ArbolB<Producto>.Recorrido(dato, null, 1);
        }
    }
}
