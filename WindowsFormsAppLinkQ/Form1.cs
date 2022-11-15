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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using(videoclubBinarioEntities objBD = new videoclubBinarioEntities())
            {
                var qCategorias = from cat in objBD.categorias where cat.categoria.ToUpper().StartsWith(textBox1.Text.ToUpper()) select new { cat.categoria, cat.precio };
                dataGridView1.DataSource= qCategorias.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (videoclubBinarioEntities objBD = new videoclubBinarioEntities())
            {
                decimal precio = Decimal.Parse(textBox2.Text);
                var qPrecios = from cat in objBD.categorias where cat.precio >= precio orderby cat.categoria, cat.categoria select new { cat.categoria, cat.precio };
                dataGridView1.DataSource = qPrecios.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (videoclubBinarioEntities objBD = new videoclubBinarioEntities())
            {

                var qPeliculas = from peli in objBD.peliculas
                    join cat in objBD.categorias on peli.categoria equals cat.categoria
                    where cat.categoria.ToUpper().StartsWith(textBox3.Text.ToUpper())
                    orderby peli.titulo ascending 
                    select new { peli.titulo, peli.anio, peli.estilo, cat.categoria, cat.precio};
                dataGridView2.DataSource = qPeliculas.ToList();

            }
        }
    }
}
