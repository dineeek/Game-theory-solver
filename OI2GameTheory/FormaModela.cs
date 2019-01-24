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
            txtModel.Text = uklonjeneStrategije + Environment.NewLine;
            txtModel.Text += Environment.NewLine + model;
        }
    }
}
