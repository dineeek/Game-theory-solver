using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OI2GameTheory
{
    public partial class PocetnaForma : Form
    {
        private SpremanjeUnosa uneseniDobiciGubitci = null;
        public PocetnaForma()
        {
            InitializeComponent();
        }

        private void btnGenerirajMatricu_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtStrA.Text) || string.IsNullOrEmpty(txtStrB.Text))
            {
                MessageBox.Show("Unesite broj strategija svakog igrača!");
            }
            else
            {
                try
                {
                    int brojStrategijaA = int.Parse(txtStrA.Text);
                    int brojStrategijaB = int.Parse(txtStrB.Text);

                    //crtanje matrice
                    CrtanjeMatrice matrica = new CrtanjeMatrice(brojStrategijaA, brojStrategijaB);
                    dgvMatrica.DataSource = matrica.NacrtajMatricu();

                    if(dgvMatrica.Rows.Count > 0 && dgvMatrica.Columns.Count > 0)
                    {
                        btnSimplex.Enabled = true;
                    }

                    //za izgled tablice - tako da je cijela popunjena
                    foreach (DataGridViewRow red in dgvMatrica.Rows)
                    {
                        red.Height = (dgvMatrica.ClientRectangle.Height - dgvMatrica.ColumnHeadersHeight) / dgvMatrica.Rows.Count;
                    }
                }
                catch
                {
                    MessageBox.Show("Unesite cijele brojeve!");
                }
            }
        }

        private void btnSimplex_Click(object sender, EventArgs e)
        {
            try
            {
                uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);
                
                //prvo provjera dominantnih strategija
                Sedlo provjeraSedla = new Sedlo(uneseniDobiciGubitci);
                

                //provjera sedla

                //simplex metoda
            }
            catch
            {
                MessageBox.Show("Unesite gubitke i dobitke pojedinih igrača!");
            }
            
        }



        //SITNICE
        private void txtStrA_MouseClick(object sender, MouseEventArgs e)
        {
            txtStrA.SelectionStart = 0;
        }

        private void txtStrB_MouseClick(object sender, MouseEventArgs e)
        {
            txtStrB.SelectionStart = 0;
        }

        private void dgvMatrica_SizeChanged(object sender, EventArgs e)
        {
            //za izgled tablice - tako da je cijela popunjena
            foreach (DataGridViewRow red in dgvMatrica.Rows)
            {
                red.Height = (dgvMatrica.ClientRectangle.Height - dgvMatrica.ColumnHeadersHeight) / dgvMatrica.Rows.Count;
            }
        }


    }
}
