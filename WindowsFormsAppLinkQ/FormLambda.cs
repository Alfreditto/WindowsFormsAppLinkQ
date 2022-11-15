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
            }
        }
    }
}
