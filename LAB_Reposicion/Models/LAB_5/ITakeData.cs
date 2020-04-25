using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_Reposicion.Models.LAB_5
{
    interface ITakeData <T>
    {
        IFormFile File { get; set; }
        string Name { get; set; }
        T key1 { get; set; }
        T key2 { get; set; }
    }
}
