using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMusicPlayer
{
    public class Cancion
    {

        public string nombre { get; set; }
        public string ruta { get; set; }
        public Cancion()
        {
            
        }

        public Cancion(string nombre, string ruta)
        {
            this.nombre = nombre;
            this.ruta = ruta;
        }



    }
}
