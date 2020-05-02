using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LAB_Reposicion.Models;
using System.IO;
using System.IO.Compression;
using System.Net.Mime;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LAB_Reposicion.Controllers
{
    [Route("[controller]")]
    public class decipherController : Controller
    {
        [HttpPost, Route("caesar2")]
        public async Task<FileStreamResult> Decifrar([FromForm] CeasearDataTaken requestdata)
        {
            Ceasar2.CipherDechiper(requestdata, 2);
            return await Download($"temp\\{requestdata.File.FileName}");
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
