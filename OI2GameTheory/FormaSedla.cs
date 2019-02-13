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
    public partial class FormaSedla : Form
    {
        private List<int> minimumiReda;
        private List<int> maximumiStupca;
        private string matricaPlacanja;
        private int maximumMinimumaReda;
        private int minimumMaximumaStupca;

        public FormaSedla(List<int> maximumi, List<int> minimumi, string matrica)
        {
            InitializeComponent();

            maximumiStupca = maximumi;
            minimumiReda = minimumi;
            matricaPlacanja = matrica;

            maximumMinimumaReda = minimumiReda.Max();
            minimumMaximumaStupca = maximumiStupca.Min();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormaSedla_Load(object sender, EventArgs e)
        {
            matricaPlacanja += Environment.NewLine + Environment.NewLine + "Minimumi reda: ";
            foreach (var min in minimumiReda)
                matricaPlacanja += min.ToString()+ " ";

            matricaPlacanja += Environment.NewLine + "Maksimum minimuma reda: " + maximumMinimumaReda;

            matricaPlacanja += Environment.NewLine + Environment.NewLine + "Maksimumi stupca: ";
            foreach (var max in maximumiStupca)
                matricaPlacanja += max.ToString() + " ";

            matricaPlacanja += Environment.NewLine + "Minimum maksimuma stupca: " + minimumMaximumaStupca;

            matricaPlacanja += Environment.NewLine + Environment.NewLine + "Kako je MAXMIN reda = MINMAX stupca postoji sedlo.";

            if(minimumMaximumaStupca > 0)
                matricaPlacanja += Environment.NewLine + Environment.NewLine + "Vrijednost ove igre iznosi "+minimumMaximumaStupca+" u korist igrača A.";
            else
                matricaPlacanja += Environment.NewLine + Environment.NewLine + "Vrijednost ove igre iznosi " + minimumMaximumaStupca + " u korist igrača B.";

            txtSedlo.Text = matricaPlacanja;


        }
    }
}
