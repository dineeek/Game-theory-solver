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
                MessageBox.Show("Unesite broj strategija svakog igrača!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        btnModelZadatka.Enabled = true;
                    }

                    //za izgled tablice - tako da je cijela popunjena
                    foreach (DataGridViewRow red in dgvMatrica.Rows)
                    {
                        red.Height = (dgvMatrica.ClientRectangle.Height - dgvMatrica.ColumnHeadersHeight) / dgvMatrica.Rows.Count;
                    }
                }
                catch
                {
                    MessageBox.Show("Unesite cijele brojeve!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            foreach (DataGridViewColumn stupac in dgvMatrica.Columns)
            {
                stupac.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private SimplexForma formaSimplexMetode;

        private void btnSimplex_Click(object sender, EventArgs e)
        {
           //try
           //{
                if(rbIgracA.Checked == true)
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);
                
                    //provjera postojanja sedla
                    Sedlo provjeraSedla = new Sedlo(uneseniDobiciGubitci);

                    bool postojiSedlo = provjeraSedla.ProvjeriSedlo().Item1;
                    int rezultatIgre = provjeraSedla.ProvjeriSedlo().Item2;
                    if (postojiSedlo)
                    {
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ProtuprirodnaIgra protuprirodnost = new ProtuprirodnaIgra(new SpremanjeUnosa(dgvMatrica));
                        int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();
                        if (vrstaIgre == 0)
                        {
                            provjeraSedla.ukloniDominantneStrategije(); //provjera dal postoje dominantnih strategija te ih eliminira

                            Sedlo provjeraSedlaIntern = new Sedlo(provjeraSedla.uneseniPodaci);

                            bool postojiSedloIntern = provjeraSedla.ProvjeriSedlo().Item1;
                            int rezultatIgreIntern = provjeraSedla.ProvjeriSedlo().Item2;
                            if (postojiSedloIntern)
                            {
                                MessageBox.Show("Postoji sedlo nakon uklanjanja dominantnih strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //simplex metoda 
                                SimplexKalkulatorA smplxCalcMI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3); //šalju se strategije bez onih dominantnih
                                formaSimplexMetode = new SimplexForma(smplxCalcMI.SimplexTabliceRazlomci, smplxCalcMI.Zakljucak, smplxCalcMI.indexiVodecihStupaca, smplxCalcMI.indexiVodecihRedaka, smplxCalcMI.brojRedaka, smplxCalcMI.brojStupaca);
                                formaSimplexMetode.ShowDialog();
                            }
                        }
                        else if (vrstaIgre == 1)
                        {
                            SimplexKalkulatorA smplxCalcPI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci);

                            formaSimplexMetode = new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca);
                            formaSimplexMetode.ShowDialog();
                        }
                        else//kontradiktorna
                        {
                            SimplexKalkulatorA smplxCalcKI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                            formaSimplexMetode = new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca);
                            formaSimplexMetode.ShowDialog();
                        }
                    } 
                }

                else //igracB.Check == true;
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    //provjera postojanja sedla
                    Sedlo provjeraSedla = new Sedlo(uneseniDobiciGubitci);

                    bool postojiSedlo = provjeraSedla.ProvjeriSedlo().Item1;
                    int rezultatIgre = provjeraSedla.ProvjeriSedlo().Item2;
                    if (postojiSedlo)
                    {
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ProtuprirodnaIgra protuprirodnost = new ProtuprirodnaIgra(new SpremanjeUnosa(dgvMatrica));

                        int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                        if (vrstaIgre == 0)
                        {
                            provjeraSedla.ukloniDominantneStrategije(); //provjera dal postoje dominantnih strategija te ih eliminira

                            Sedlo provjeraSedlaIntern = new Sedlo(provjeraSedla.uneseniPodaci);

                            bool postojiSedloIntern = provjeraSedla.ProvjeriSedlo().Item1;
                            int rezultatIgreIntern = provjeraSedla.ProvjeriSedlo().Item2;
                            if (postojiSedloIntern)
                            {
                                MessageBox.Show("Postoji sedlo nakon uklanjanja dominantnih strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //simplex metoda 
                                SimplexKalkulatorB smplxCalcMI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3); //šalju se strategije bez onih dominantnih
                                formaSimplexMetode = new SimplexForma(smplxCalcMI.SimplexTabliceRazlomci, smplxCalcMI.Zakljucak, smplxCalcMI.indexiVodecihStupaca, smplxCalcMI.indexiVodecihRedaka, smplxCalcMI.brojRedaka, smplxCalcMI.brojStupaca);
                                formaSimplexMetode.ShowDialog();
                            }
                        }
                        else if (vrstaIgre == 1)
                        {
                            SimplexKalkulatorB smplxCalcPI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci);

                            formaSimplexMetode = new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca);
                            formaSimplexMetode.ShowDialog();
                        }
                        else//kontradiktorna
                        {
                            SimplexKalkulatorB smplxCalcKI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                            formaSimplexMetode = new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca);
                            formaSimplexMetode.ShowDialog();
                        }
                    }
                }
       
            //}
            //catch
            //{
               // MessageBox.Show("Unesite gubitke i dobitke strategija pojedinih igrača!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}         
        }
        private void btnModelZadatka_Click(object sender, EventArgs e)
        {
            try
            {
                if(rbIgracA.Checked == true)
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    //provjera postojanja sedla
                    Sedlo provjeraSedla = new Sedlo(uneseniDobiciGubitci);
              
                    ProtuprirodnaIgra protuprirodnost = new ProtuprirodnaIgra(new SpremanjeUnosa(dgvMatrica));
                    IzgradnjaModelaA modelZadatka;
                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije();
                        modelZadatka = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci);

                        FormaModela modelMI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelMI.ShowDialog();
                    }
                    else if(vrstaIgre == 1)
                    {
                        modelZadatka = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci, 0);

                        FormaModela modelPI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelPI.ShowDialog();
                    }
                    else
                    {
                        modelZadatka = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci);

                        FormaModela modelKI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelKI.ShowDialog();
                    }
                }
                else //igracB.Check == true;
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    //provjera postojanja sedla
                    Sedlo provjeraSedla = new Sedlo(uneseniDobiciGubitci);

                    ProtuprirodnaIgra protuprirodnost = new ProtuprirodnaIgra(new SpremanjeUnosa(dgvMatrica));
                    IzgradnjaModelaB modelZadatka;
                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije();
                        modelZadatka = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci);

                        FormaModela modelMI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelMI.ShowDialog();
                    }
                    else if (vrstaIgre == 1)
                    {
                        modelZadatka = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci, 0);

                        FormaModela modelPI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelPI.ShowDialog();
                    }
                    else
                    {
                        modelZadatka = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci);

                        FormaModela modelKI = new FormaModela(modelZadatka.DohvatiZapisModela());
                        modelKI.ShowDialog();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unesite gubitke i dobitke strategija pojedinih igrača!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dgvMatrica_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null && e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Pazite na unos!");
            }
        }
    }
}
