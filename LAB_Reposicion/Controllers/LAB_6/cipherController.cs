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
    public class cipherController : Controller
    {
        [HttpGet, Route("getPublicKey/DIFFIE")]
        public async Task<FileStreamResult> GetKeyDiffie([FromForm] KeysDataTaken requestdata)
        {
            Diffie_Hellman.GenerarLlaves(requestdata);
            return await DownloadZip();
        }

        [HttpGet, Route("getPublicKey/RSA")]
        public async Task<FileStreamResult> GetKeyRSA([FromForm] KeysDataTaken requestdata)
        {
            RSA.GenerarLlaves(requestdata);
            return await DownloadZip();
        }

        [HttpPost, Route("caesar2")]
        public async Task<FileStreamResult> Decifrar([FromForm] CeasearDataTaken requestdata)
        {
            Ceasar2.CifrarDecifrado(requestdata, 1);
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

        async Task<FileStreamResult> DownloadZip()
        {
            var files = new List<string> { "K1.txt", "K2.txt" };
            var archivePath = Path.Combine(Environment.CurrentDirectory, "archive.zip");
            var tempPath = Path.Combine(Environment.CurrentDirectory, "temp");

            if (System.IO.File.Exists(archivePath))
            {
                System.IO.File.Delete(archivePath);
            }

            ZipFile.CreateFromDirectory(tempPath, archivePath);

            return await Download(archivePath);
        }
    }
}
