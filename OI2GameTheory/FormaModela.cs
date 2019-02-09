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
    public partial class FormaModela : Form
    {
        public FormaModela(string uklonjeneStrategije, string model)
        {
            InitializeComponent();

            if (String.IsNullOrEmpty(uklonjeneStrategije))
                uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

            txtModel.Text = uklonjeneStrategije + Environment.NewLine;
            txtModel.Text += Environment.NewLine + model;
        }

        public string DohvatiModelProblema()
        {
            return txtModel.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
