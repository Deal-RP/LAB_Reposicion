using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using LAB_Reposicion.Models.LAB_5;
using Microsoft.AspNetCore.Mvc;

namespace LAB_Reposicion.Controllers.LAB_5
{
    [Route("[controller]")]
    public class cipherController : Controller
    {
        [HttpPost, Route("zigzag")]
        public async Task<FileStreamResult> cifrarZigZag([FromForm] NumbersDataTaken requestdata)
        {
            ZigZag.Cifrar(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("caesar")]
        public async Task<FileStreamResult> cifrarCesar([FromForm] ValuesDataTaken requestdata)
        {
            Cesar.Cifrar(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("rutaVertical")]
        public async Task<FileStreamResult> cifrarVertical(string nombre, [FromForm] NumbersDataTaken requestdata)
        {
            Route.CifradoVertical(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("rutaEspiral")]
        public async Task<FileStreamResult> cifrarEspiral(string nombre, [FromForm] NumbersDataTaken requestdata)
        {
            Route.CifradoEspiral(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        async Task<FileStreamResult> Download(string path)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Directory.Delete("temp", true);
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }
    }
}
