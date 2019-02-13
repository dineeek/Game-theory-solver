using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using DGVPrinterHelper;

namespace OI2GameTheory
{
    public partial class PocetnaForma : Form
    {
        private SpremanjeUnosa uneseniDobiciGubitci = null;
        private int igrac = 0; //za print

        public PocetnaForma()
        {
            InitializeComponent();

            ispisModelaZadatkaToolStripMenuItem.Enabled = false;
            ispisTablicaIteracijaToolStripMenuItem.Enabled = false;
            ispisPostupkaIzračunaToolStripMenuItem.Enabled = false;
        }

        private void btnGenerirajMatricu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStrA.Text) || string.IsNullOrEmpty(txtStrB.Text))
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

                    if (dgvMatrica.Rows.Count > 0 && dgvMatrica.Columns.Count > 0)
                    {
                        btnSimplex.Enabled = true;
                        btnModelZadatka.Enabled = true;
                    }

                    //za izgled tablice - tako da je cijela popunjena
                    foreach (DataGridViewRow red in dgvMatrica.Rows)
                    {
                        red.Height = (dgvMatrica.ClientRectangle.Height - dgvMatrica.ColumnHeadersHeight) / dgvMatrica.Rows.Count;
                    }

                    //isključena modifikacija prvog stupca
                    dgvMatrica.Columns[0].ReadOnly = true;

                    foreach (DataGridViewColumn stupac in dgvMatrica.Columns)
                    {
                        stupac.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    ispisModelaZadatkaToolStripMenuItem.Enabled = false;
                    ispisTablicaIteracijaToolStripMenuItem.Enabled = false;
                    ispisPostupkaIzračunaToolStripMenuItem.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("Unesite cijele brojeve!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private SimplexForma simplexMetoda()
        {
            if (rbIgracA.Checked == true)
            {
                igrac = 1;
                uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                //provjera postojanja sedla
                SedloDominacija provjeraSedla = new SedloDominacija(uneseniDobiciGubitci);

                Tuple<bool, int, int> postojanjeSedla = provjeraSedla.ProvjeriSedlo();
                bool postojiSedlo = postojanjeSedla.Item1;
                int rezultatIgre = postojanjeSedla.Item2;

                if (postojiSedlo)
                {
                    if (rezultatIgre > 0)
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre + " u korist igrača A.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre + " u korist igrača B.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije(); //provjera dal postoje dominantnih strategija te ih eliminira

                        Tuple<bool, int, int> postojanjeSedlaIntern = provjeraSedla.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja dominantnih strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return null;
                        }
                        else
                        {
                            //simplex metoda 
                            SimplexKalkulatorA smplxCalcMI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci, postojanjeSedlaIntern.Item3); //šalju se strategije bez onih dominantnih
                            return new SimplexForma(smplxCalcMI.SimplexTabliceRazlomci, smplxCalcMI.Zakljucak, smplxCalcMI.indexiVodecihStupaca, smplxCalcMI.indexiVodecihRedaka, smplxCalcMI.brojRedaka, smplxCalcMI.brojStupaca, smplxCalcMI.postupakIzracuna);
                        }
                    }
                    else if (vrstaIgre == 1)
                    {

                        MessageBox.Show("Unesena je protuprirodna igra!\nNe uklanjam dominantne strategije.");
                        provjeraSedla.ukloniDuplikatneStrategije();

                        SimplexKalkulatorA smplxCalcPI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        return new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca, smplxCalcPI.postupakIzracuna);
                    }
                    else//kontradiktorna
                    {
                        MessageBox.Show("Unesena je kontradiktorna igra!\nNe uklanjam dominantne strategije.");//kontradiktorna nastaje nakon uklanjanja strategija svođenjem jednog igrača na samo 1 strategiju

                        provjeraSedla.ukloniDuplikatneStrategije();

                        SimplexKalkulatorA smplxCalcKI = new SimplexKalkulatorA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        return new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca, smplxCalcKI.postupakIzracuna);
                    }
                }
            }

            else //igracB.Check == true;
            {
                igrac = 2;
                uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                //provjera postojanja sedla
                SedloDominacija provjeraSedla = new SedloDominacija(uneseniDobiciGubitci);

                Tuple<bool, int, int> postojanjeSedla = provjeraSedla.ProvjeriSedlo();
                bool postojiSedlo = postojanjeSedla.Item1;
                int rezultatIgre = postojanjeSedla.Item2;

                if (postojiSedlo)
                {
                    if (rezultatIgre > 0)
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre + " u korist igrača A.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi: " + rezultatIgre + " u korist igrača B.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));
                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();
                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije(); //provjera dal postoje dominantne i duplikatne strategije te ih eliminira                          

                        Tuple<bool, int, int> postojanjeSedlaIntern = provjeraSedla.ProvjeriSedlo();//provjera sedla nakon uklanjanja strategija
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja dominantnih strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return null;
                        }
                        else
                        {
                            //simplex metoda 
                            SimplexKalkulatorB smplxCalcMI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci, postojanjeSedlaIntern.Item3); //šalju se strategije bez onih dominantnih i duplikatnih
                            return new SimplexForma(smplxCalcMI.SimplexTabliceRazlomci, smplxCalcMI.Zakljucak, smplxCalcMI.indexiVodecihStupaca, smplxCalcMI.indexiVodecihRedaka, smplxCalcMI.brojRedaka, smplxCalcMI.brojStupaca, smplxCalcMI.postupakIzracuna);
                        }
                    }
                    else if (vrstaIgre == 1)
                    {
                        MessageBox.Show("Unesena je protuprirodna igra!\nNe uklanjam dominantne strategije.");

                        provjeraSedla.ukloniDuplikatneStrategije();

                        SimplexKalkulatorB smplxCalcPI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        return new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca, smplxCalcPI.postupakIzracuna);
                    }
                    else//kontradiktorna
                    {
                        MessageBox.Show("Unesena je kontradiktorna igra!\nNe uklanjam dominantne strategije.");//kontradiktorna nastaje nakon uklanjanja strategija svođenjem jednog igrača na samo 1 strategiju

                        provjeraSedla.ukloniDuplikatneStrategije();

                        SimplexKalkulatorB smplxCalcKI = new SimplexKalkulatorB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        return new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca, smplxCalcKI.postupakIzracuna);
                    }
                }
            }
        }

        private void btnSimplex_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbIgracA.Checked == true)
                {
                    simplexMetoda().ShowDialog();
                    ispisTablicaIteracijaToolStripMenuItem.Enabled = true;
                    ispisPostupkaIzračunaToolStripMenuItem.Enabled = true;
                }

                else //igracB.Check == true;
                {
                    simplexMetoda().ShowDialog();
                    ispisTablicaIteracijaToolStripMenuItem.Enabled = true;
                    ispisPostupkaIzračunaToolStripMenuItem.Enabled = true;
                }

            }
            catch
            {
                MessageBox.Show("Unesite gubitke i dobitke strategija pojedinih igrača!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private IzgradnjaModelaB modelZadatkaA = null;
        private IzgradnjaModelaA modelZadatkaB = null;
        private FormaModela formaModela;

        private string stvoriModelProblema()
        {
                if (rbIgracA.Checked == true)
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    //provjera postojanja sedla
                    SedloDominacija provjeraSedla = new SedloDominacija(uneseniDobiciGubitci);

                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije();
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);

                        string uklonjeneStrategije = provjeraSedla.IspisUklonjenihStrategijaIgracaA();
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();//prikaz matricne igre

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                    else if (vrstaIgre == 1) // izračun po 3 kriterija
                    {
                        provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        KriterijiProtuprirodnosti kriteriji = new KriterijiProtuprirodnosti(protuprirodnost.uneseniPodaci, 1); //rjesavanje po kriterijima

                        string uklonjeneStrategije = "Unesena igra je protuprirodna ili postaje protuprirodna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + kriteriji.IspisiVrijednostiKriterija() + Environment.NewLine + "Kod izrade modela ne uklanjam dominantne strategije. ";
                        uklonjeneStrategije += provjeraSedla.IspisUklonjenihDuplikatnihA();

                        matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                    else
                    {
                        provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena igra je kontradiktorna ili postaje kontradiktorna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uklanjam dominantne strategije. ";
                        uklonjeneStrategije += provjeraSedla.IspisUklonjenihDuplikatnihA();

                        matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                }
                else //igracB.Check == true;
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    //provjera postojanja sedla
                    SedloDominacija provjeraSedla = new SedloDominacija(uneseniDobiciGubitci);

                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije();
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);

                        string uklonjeneStrategije = provjeraSedla.IspisUklonjenihStrategijaIgracaB();
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaB.DohvatiZapisModela();
                    }
                    else if (vrstaIgre == 1) // izračun po 3 kriterija
                    {
                        provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);
                        KriterijiProtuprirodnosti kriteriji = new KriterijiProtuprirodnosti(protuprirodnost.uneseniPodaci, 2); //rjesavanje po kriterijima

                        string uklonjeneStrategije = "Unesena igra je protuprirodna ili postaje protuprirodna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + kriteriji.IspisiVrijednostiKriterija() + Environment.NewLine + "Kod izrade modela ne uklanjam dominantne strategije. ";
                        uklonjeneStrategije += provjeraSedla.IspisUklonjenihDuplikatnihB();

                        matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaB.DohvatiZapisModela();
                    }
                    else
                    {
                        provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena igra je kontradiktorna ili postaje kontradiktorna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uklanjam dominantne strategije. ";
                        uklonjeneStrategije += provjeraSedla.IspisUklonjenihDuplikatnihB();

                        matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(provjeraSedla.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaB.DohvatiZapisModela();
                    }              
            }
        }

        private void btnModelZadatka_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbIgracA.Checked == true)
                {
                    formaModela = new FormaModela(stvoriModelProblema());
                    formaModela.ShowDialog();

                    ispisModelaZadatkaToolStripMenuItem.Enabled = true;
                }
                else //igracB.Check == true;
                {
                    formaModela = new FormaModela(stvoriModelProblema());
                    formaModela.ShowDialog();

                    ispisModelaZadatkaToolStripMenuItem.Enabled = true;
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

        private void novaIgraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtStrA.Text = String.Empty;
            txtStrB.Text = String.Empty;
            dgvMatrica.DataSource = null;
            btnModelZadatka.Enabled = false;
            btnSimplex.Enabled = false;
            ispisModelaZadatkaToolStripMenuItem.Enabled = false;
            ispisTablicaIteracijaToolStripMenuItem.Enabled = false;
            ispisPostupkaIzračunaToolStripMenuItem.Enabled = false;
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ispisTablicaIteracijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //btnSimplex.PerformClick();
                DGVPrinter printer = new DGVPrinter();

                if(igrac == 1)
                    printer.Title = "Tablice iteracija igrača A: ";//zaglavlje
                else
                    printer.Title = "Tablice iteracija igrača B: ";//zaglavlje

                printer.TitleAlignment = StringAlignment.Near;
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = false;
                printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.DataWidth;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.FooterSpacing = 10;
                printer.FooterAlignment = StringAlignment.Near;
                printer.printDocument.DefaultPageSettings.Landscape = true;

                printer.Footer = this.simplexMetoda().DohvatiRjesenjeProblema();
                printer.PrintDataGridView(simplexMetoda().DohvatiTabliceIteracije());
            }
            catch
            {
                MessageBox.Show("Ugasite trenutačno upaljeni PDF dokument i pokušajte ponovno!");
            }
        }

        private void ispisPostupkaIzračunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Postupak izracuna simpleks algoritma";
                saveFileDialog.DefaultExt = ".pdf";

                string FONT = "c:/windows/fonts/arialbd.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);

                //btnSimplex.PerformClick();

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document dokument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(dokument, stream);
                        dokument.Open();
                        dokument.Add(new Phrase(this.simplexMetoda().postupakIzracuna, f));
                        dokument.Close();
                        stream.Close();
                    }
                }
            }

            catch
            {
                MessageBox.Show("Ugasite trenutačno upaljeni PDF dokument i pokušajte ponovno!");
            }
        }

        private void pomoćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pdf otvara iz diplomskog 6. poglavlje
        }

        private void ispisModelaZadatkaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Model problema";
                saveFileDialog.DefaultExt = ".pdf";

                string FONT = "c:/windows/fonts/arialbd.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document dokument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(dokument, stream);
                        dokument.Open();
                        string modelA = "Model unesenog problema: " + Environment.NewLine + stvoriModelProblema();
                        dokument.Add(new Phrase(modelA, f));
                        dokument.Close();
                        stream.Close();
                    }
                }               
            }
            catch
            {
                MessageBox.Show("Ugasite trenutačno upaljeni PDF dokument i pokušajte ponovno!");
            }
        }
    }
}
