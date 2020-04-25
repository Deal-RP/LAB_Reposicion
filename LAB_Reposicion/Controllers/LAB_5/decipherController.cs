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
    public class decipherController : Controller
    {
        [HttpPost, Route("zigzag")]
        public async Task<FileStreamResult> decifrarZigZag([FromForm] NumbersDataTaken requestdata)
        {
            ZigZag.Decifrar(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("caesar")]
        public async Task<FileStreamResult> decifrarCesar([FromForm] ValuesDataTaken requestdata)
        {
            Cesar.Decifrar(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("rutaVertical")]
        public async Task<FileStreamResult> decifrarVertical(string nombre, [FromForm] NumbersDataTaken requestdata)
        {
            Route.DecifradoVertical(requestdata);
            return await Download($"temp\\{requestdata.Name}.txt");
        }

        [HttpPost, Route("rutaEspiral")]
        public async Task<FileStreamResult> decifrarEspiral(string nombre, [FromForm] NumbersDataTaken requestdata)
        {
            Route.DecifradoEspiral(requestdata);
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
