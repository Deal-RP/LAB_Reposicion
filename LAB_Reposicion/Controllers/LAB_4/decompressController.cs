using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAB_Reposicion.Models;
using Microsoft.AspNetCore.Http;
using LAB_Reposicion.Models.LAB_4;
using System.IO;
using System.Net.Mime;

namespace LAB_Reposicion.Controllers.LAB_4
{
    [ApiController]
    [Route("[controller]")]
    public class decompressController : Controller
    {
        [HttpPost]
        public async Task<FileStreamResult> DesompresionLZW(IFormFile archivo)
        {
            var Archivos = NodoArchivo.CargarHistorial();

            var Original = Archivos.Find(c => Path.GetFileNameWithoutExtension(c.Nombre) == Path.GetFileNameWithoutExtension(archivo.FileName));

            var path = LZW.Descomprimir(archivo, Original == null ? $"{Path.GetFileNameWithoutExtension(archivo.FileName)}.txt" : Original.NombreOriginal);

            var newFile = new FileInfo(path);

            NodoArchivo.ManejarCompressions(
                new NodoArchivo
                {
                    Algoritmo = "LZW",
                    NombreOriginal = Original == null ? $"{Path.GetFileNameWithoutExtension(archivo.FileName)}.txt" : Original.NombreOriginal,
                    Nombre = archivo.FileName,
                    RutaArchivo = path,
                    RazonCompresion = 0,
                    FactorCompresion = 0,
                    Porcentaje = 0
                });

            return await Download(path);
        }

        async Task<FileStreamResult> Download(string path)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }
    }
}
