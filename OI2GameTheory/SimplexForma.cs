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
        public SimplexForma(DataTable simplexica, string zakljucak)
        {
            InitializeComponent();
            simplexTablica = simplexica;
            txtRjesenje.Text = zakljucak;
        }

        private void SimplexForma_Load(object sender, EventArgs e)
        {
            dgvSimplexTablica.DataSource = simplexTablica;
        }
    }
}
