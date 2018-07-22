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
    public partial class SimplexForma : Form
    {
        private DataTable simplexTablica;
        private List<int> indexiVodecihStupaca;
        private List<int> indexiVodecihRedaka;
        private int brojRedaka1Tablice;
        private int brojStupaca1Tablice;

        public SimplexForma(DataTable simplexica, string zakljucak, List<int> stupci, List<int> redci, int brojRed, int brojStup)
        {
            InitializeComponent();
            simplexTablica = simplexica;
            txtRjesenje.Text = zakljucak;

            indexiVodecihStupaca = stupci;
            indexiVodecihRedaka = redci;
            brojRedaka1Tablice = brojRed;
            brojStupaca1Tablice = brojStup;
        }

        private void SimplexForma_Load(object sender, EventArgs e)
        {
            dgvSimplexTablica.DataSource = simplexTablica;

            //bojanje vodećih redova
            int brojac = 0;
            int trenutniRedak = brojRedaka1Tablice;
            int brojacIndexa = 0;
            foreach (DataGridViewRow red in dgvSimplexTablica.Rows)
            {
                if(brojac == trenutniRedak)
                {
                    for (int i=0; i<brojRedaka1Tablice-1; i++)// u 1. od tablica
                    {
                        if(brojacIndexa < indexiVodecihRedaka.Count)
                        {
                            if (i == indexiVodecihRedaka[brojacIndexa])
                            {
                                dgvSimplexTablica.Rows[red.Index+i].DefaultCellStyle.BackColor = Color.LightSalmon;
                                brojacIndexa++;
                                break;
                            }
                        }
                    }

                    trenutniRedak += brojRedaka1Tablice * 2;
                }

                brojac++;
            }

            //bojanje vodećih stupaca
            int brojac2 = 0;
            int trenutniRedak2 = brojRedaka1Tablice;
            int brojacIndexa2 = 0;
            foreach (DataGridViewRow red in dgvSimplexTablica.Rows)
            {
                if (brojac2 == trenutniRedak2)
                {
                    for (int i = 3; i < brojStupaca1Tablice - 2; i++)// u 1. od tablica
                    {
                        if (brojacIndexa2 < indexiVodecihStupaca.Count)
                        {
                            if (i == indexiVodecihStupaca[brojacIndexa2])
                            {
                                for(int j = 0; j<brojRedaka1Tablice-1; j++)
                                    dgvSimplexTablica.Rows[red.Index+j].Cells[i].Style.BackColor = Color.LightSalmon;
                                brojacIndexa2++;
                                break;
                            }
                        }
                    }
                    trenutniRedak2 += brojRedaka1Tablice * 2;
                }

                brojac2++;
            }
        }
    }
}
