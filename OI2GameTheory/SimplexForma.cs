using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace OI2GameTheory
{
    public partial class SimplexForma : Form
    {
        private DataTable simplexTablica;
        public SimplexForma(DataTable simplexica)
        {
            InitializeComponent();
            simplexTablica = simplexica;
        }

        private void SimplexForma_Load(object sender, EventArgs e)
        {
            dgvSimplexTablica.DataSource = simplexTablica;
        }
    }
}
