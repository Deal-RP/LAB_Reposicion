using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_Reposicion.Models
{
    public class InstancePeliculas
    {
        #region INSTANCIA
        private static InstancePeliculas _instance = null;
        public static InstancePeliculas Instance
        {
            get
            {
                if (_instance == null) _instance = new InstancePeliculas();
                return _instance;
            }
        }
        #endregion

        public List<Pelicula> listPelicula = new List<Pelicula>();
    }
}
