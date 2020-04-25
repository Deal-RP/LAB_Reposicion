using Microsoft.AspNetCore.Http;

namespace LAB_Reposicion.Models
{
    public class KeysDataTaken
    {
        public int clave { get; set; }
        public int key1 { get; set; }
        public int key2 { get; set; }
    }
    public class CeasearDataTaken
    {
        public IFormFile File { get; set; }
        public IFormFile key { get; set; }
    }
}
