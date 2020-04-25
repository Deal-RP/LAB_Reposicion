using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAB_Reposicion.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using LAB_Reposicion.Models.LAB_4;
using System.Net.Mime;

namespace LAB_Reposicion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class compressController : Controller
    {
        [HttpPost, Route("{nombre}")]
        public async Task<FileStreamResult> CompresionLZW(IFormFile archivo, string nombre)
        {
            LZW.Comprimir(archivo, nombre);

            var newFile = new FileInfo(Path.Combine(Environment.CurrentDirectory, "compress", $"{nombre}.lzw"));

            NodoArchivo.ManejarCompressions(
                new NodoArchivo
                {
                    Algoritmo = "LZW",
                    NombreOriginal = archivo.FileName,
                    Nombre = $"{nombre}.lzw",
                    RutaArchivo = Path.Combine(Environment.CurrentDirectory, "compress", $"{nombre}.lzw"),
                    RazonCompresion = (double)newFile.Length / (double)archivo.Length,
                    FactorCompresion = (double)archivo.Length / (double)newFile.Length,
                    Porcentaje = 100 - (((double)newFile.Length / (double)archivo.Length) * 100)
                });

            return await Download(Path.Combine(Environment.CurrentDirectory, "compress", $"{nombre}.lzw"));
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
