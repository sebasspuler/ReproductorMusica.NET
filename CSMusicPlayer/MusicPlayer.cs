using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace CSMusicPlayer
{
    public partial class MusicPlayer : Form
    {
        List<Cancion> _canciones;

        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;                    //Establezco que sea multiselect.

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AgregarCancionesALaLista(dialog.SafeFileNames.ToList(), dialog.FileNames.ToList());
            }
        }

        private void AgregarCancionesALaLista(List<string> nombres, List<string> rutas)
        {
            if (_canciones == null)
                _canciones = new List<Cancion>();

            foreach (string item in nombres)
            {
                if (!ExisteEnLaLista(item))
                    _canciones.Add(new Cancion(item, GetRuta(item, rutas)));
            }

            OrdenarCanciones(); // Ordena las canciones después de agregarlas
            ActualizarListaCanciones();
        }

        private void OrdenarCanciones()
        {
            _canciones = _canciones.OrderBy(c => c.nombre).ToList(); // Ordena la lista por nombre
        }

        private bool ExisteEnLaLista(string cancion)
        {
            bool existe = false;
            foreach (var item in _canciones)
            {
                if (item.nombre == cancion)
                    existe = true;
            }

            return existe;
        }

        private string GetRuta(string nombreArchivo, List<string> rutaCanciones = null)
        {
            string rutaActual = string.Empty;

            if (rutaCanciones == null)
            {
                foreach (var cancion in _canciones)
                {
                    if (cancion.nombre == nombreArchivo)
                        rutaActual = cancion.ruta;
                }
            }
            else
            {
                foreach (var ruta in rutaCanciones)
                {
                    if (ruta.Contains(nombreArchivo))
                        rutaActual = ruta;
                }
            }
            return rutaActual;
        }

        private void cancionesLista_DoubleClick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = GetRuta(cancionesLista.Text);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Cancion cancionParaBorrar = null;

            foreach (var cancion in _canciones)
            {
                if (cancion.nombre == cancionesLista.Text)
                    cancionParaBorrar = cancion;
            }

            if (cancionParaBorrar != null)
            {
                _canciones.Remove(cancionParaBorrar);
            }

            OrdenarCanciones(); // Ordena las canciones después de borrarlas
            ActualizarListaCanciones();
        }

        private void ActualizarListaCanciones()
        {
            List<string> nombreCanciones = new List<string>();

            foreach (var item in _canciones)
            {
                nombreCanciones.Add(item.nombre);
            }

            cancionesLista.DataSource = null;
            cancionesLista.DataSource = nombreCanciones;
        }
    }
}
