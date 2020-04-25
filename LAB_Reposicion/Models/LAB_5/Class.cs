using Microsoft.AspNetCore.Http;

namespace LAB_Reposicion.Models.LAB_5
{
    public class NumbersDataTaken : ITakeData<int>
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public int key1 { get; set; }
        public int key2 { get; set; }
    }

    public class ValuesDataTaken : ITakeData<string>
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string key1 { get; set; }
        public string key2 { get; set; }
    }
}
