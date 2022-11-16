using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppLinkQ
{
    public partial class FormLambda : Form
    {
        public FormLambda()
        {
            InitializeComponent();
            CargarEstilos();
        }

        private void CargarEstilos()
        {
            using (videoclubBinarioEntities objBD = new videoclubBinarioEntities())
            {
                var qEstilos = from estilos in objBD.estilos orderby estilos.estilo select new { estilos.estilo };
                foreach (var est in qEstilos)
                {
                    cbxEstilo.Items.Add(est.estilo);
                }

                var qCategoria = from cat in objBD.categorias orderby cat.categoria select new { cat.categoria };
                foreach (var est in qCategoria)
                {
                    cbxCat.Items.Add(est.categoria);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (videoclubBinarioEntities objDB = new videoclubBinarioEntities())
            {
                var qConsulta = from pel in objDB.peliculas
                    join alquilere in objDB.alquileres on pel.codpeli equals alquilere.codAlquiler
                    join socio in objDB.socios on alquilere.socio equals socio.idSocio
                    orderby alquilere.fechaAlquiler descending
                    select new
                    {
                        pel.titulo, pel.anio, pel.director, pel.estilo, pel.categoria, alquilere.fechaAlquiler,
                        socio.apell1, socio.nombre
                    };

                if (!txtTitulo.Text.Trim().Equals(""))
                {
                    qConsulta = qConsulta.Where(x => x.titulo.ToUpper().StartsWith(txtTitulo.Text.ToUpper()));
                }

                if (!txtApellido.Text.Trim().Equals(""))
                {
                    qConsulta = qConsulta.Where(x => x.apell1.ToUpper().StartsWith(txtApellido.Text.ToUpper()));
                }

                if (!txtAño.Text.Trim().Equals(""))
                {
                    if (int.TryParse(txtAño.Text, out int año))
                    {
                        qConsulta = qConsulta.Where(x => x.anio >= año);
                    }
                }

                if (!txtNombre.Text.Trim().Equals(""))
                {
                    qConsulta = qConsulta.Where(x => x.nombre.ToUpper().StartsWith(txtNombre.Text.ToUpper()));
                }
            }
        }
    }
}