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
            matricaPlacanja += Environment.NewLine + Environment.NewLine + "Minimums of rows: ";
            foreach (var min in minimumiReda)
                matricaPlacanja += min.ToString()+ " ";

            matricaPlacanja += Environment.NewLine + "Maximum minimum of row - MAXMIN: " + maximumMinimumaReda;

            matricaPlacanja += Environment.NewLine + Environment.NewLine + "Maximums of columns: ";
            foreach (var max in maximumiStupca)
                matricaPlacanja += max.ToString() + " ";

            matricaPlacanja += Environment.NewLine + "Minimum maximum of column - MINMAX: " + minimumMaximumaStupca;

            matricaPlacanja += Environment.NewLine + Environment.NewLine + "As the MAXMIN = MINMAX there is a saddle.";

            if(minimumMaximumaStupca > 0)
                matricaPlacanja += Environment.NewLine + Environment.NewLine + "The value of this game is " + minimumMaximumaStupca+ " in favor of player A.";
            else if(minimumMaximumaStupca == 0)
                matricaPlacanja += Environment.NewLine + Environment.NewLine + "The value of this game is " + minimumMaximumaStupca + ". There is always a draw.";
            else
                matricaPlacanja += Environment.NewLine + Environment.NewLine + "The value of this game is " + minimumMaximumaStupca + " in favor of player B.";

            txtSedlo.Text = matricaPlacanja;


        }
    }
}
