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
            else if (int.Parse(txtStrA.Text) <= 1 || int.Parse(txtStrB.Text) <= 1)
            {
                MessageBox.Show("Broj strategija igrača mora biti veći od jedne strategije!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool nerijesenRezultatPocetakSedlo;
        private bool protuprirodnaIgraPocetakSedlo;
        private bool kontradiktornaIgraPocetakSedlo;

        private bool nerijesenRezultatInternSedlo;
        private bool protuprirodnaIgraInternSedlo;
        private bool kontradiktornaIgraInternSedlo;

        private SimplexForma simplexMetoda()
        {
            nerijesenRezultatPocetakSedlo = false;
            protuprirodnaIgraPocetakSedlo = false;
            kontradiktornaIgraPocetakSedlo = false;

            nerijesenRezultatInternSedlo = false;
            protuprirodnaIgraInternSedlo = false;
            kontradiktornaIgraInternSedlo = false;

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
                    ProtuprirodnaKontradiktornaIgra protuprirodnostSedla = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));
                    int vrstaIgre = protuprirodnostSedla.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 1)
                        protuprirodnaIgraPocetakSedlo = true;
                    else if (vrstaIgre == 2)
                        kontradiktornaIgraPocetakSedlo = true;

                    if (rezultatIgre > 0)
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " u korist igrača A.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (rezultatIgre == 0)
                    {
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " - neriješeno (pravedna igra).", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nerijesenRezultatPocetakSedlo = true;
                    }
                    else
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " u korist igrača B.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                else
                {
                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraSedla.ukloniDominantneStrategije(); //provjera dal postoje dominantnih i duplikatnih strategija te ih eliminira

                        Tuple<bool, int, int> postojanjeSedlaIntern = provjeraSedla.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        //protuprirodnaIgra = true;
                        MessageBox.Show("Unesena je protuprirodna igra!\nNe uključujem dominantne ili duplikatne strategije.");

                        provjeraSedla.ukloniDuplikatneStrategije();
                        protuprirodnost.ukloniDuplikatneStrategije();

                        SedloDominacija sedloIntern = new SedloDominacija(protuprirodnost.uneseniPodaci);

                        Tuple<bool, int, int> postojanjeSedlaIntern = sedloIntern.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            protuprirodnaIgraInternSedlo = true;
                            return null;
                        }
                        else
                        {
                            SimplexKalkulatorA smplxCalcPI = new SimplexKalkulatorA(protuprirodnost.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);
                            return new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca, smplxCalcPI.postupakIzracuna);
                        }
                    }
                    else//kontradiktorna
                    {
                        //kontradiktornaIgra = true;
                        MessageBox.Show("Unesena je kontradiktorna igra!");//kontradiktorna nastaje nakon uklanjanja strategija svođenjem jednog igrača na samo 1 strategiju
                                                                                                    //provjeraSedla.ukloniDuplikatneStrategije();

                        SedloDominacija sedloIntern = new SedloDominacija(protuprirodnost.uneseniPodaci);

                        Tuple<bool, int, int> postojanjeSedlaIntern = sedloIntern.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            kontradiktornaIgraInternSedlo = true;
                            return null;
                        }
                        else
                        {
                            SimplexKalkulatorA smplxCalcKI = new SimplexKalkulatorA(protuprirodnost.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);//protuprirodnost -> provjeraSedla.uneseniPodaci
                            return new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca, smplxCalcKI.postupakIzracuna);
                        }
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
                    ProtuprirodnaKontradiktornaIgra protuprirodnostSedla = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));
                    int vrstaIgre = protuprirodnostSedla.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 1)
                        protuprirodnaIgraPocetakSedlo = true;
                    else if (vrstaIgre == 2)
                        kontradiktornaIgraPocetakSedlo = true;

                    if (rezultatIgre > 0)
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " u korist igrača A.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (rezultatIgre == 0)
                    {
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " - neriješeno.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nerijesenRezultatPocetakSedlo = true;
                    }
                    else
                        MessageBox.Show("Postoji sedlo!\nVrijednost ove igre iznosi " + rezultatIgre + " u korist igrača B.", "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        //protuprirodnaIgra = true;
                        MessageBox.Show("Unesena je protuprirodna igra!\nNe uključujem dominantne ili duplikatne strategije.");

                        provjeraSedla.ukloniDuplikatneStrategije();
                        protuprirodnost.ukloniDuplikatneStrategije();

                        SedloDominacija sedloIntern = new SedloDominacija(protuprirodnost.uneseniPodaci);

                        Tuple<bool, int, int> postojanjeSedlaIntern = sedloIntern.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            protuprirodnaIgraInternSedlo = true;
                            return null;
                        }
                        else
                        {
                            SimplexKalkulatorB smplxCalcPI = new SimplexKalkulatorB(protuprirodnost.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);
                            return new SimplexForma(smplxCalcPI.SimplexTabliceRazlomci, smplxCalcPI.Zakljucak, smplxCalcPI.indexiVodecihStupaca, smplxCalcPI.indexiVodecihRedaka, smplxCalcPI.brojRedaka, smplxCalcPI.brojStupaca, smplxCalcPI.postupakIzracuna);
                        }
                    }
                    else//kontradiktorna
                    {
                        //kontradiktornaIgra = true;
                        MessageBox.Show("Unesena je kontradiktorna igra!");//kontradiktorna nastaje nakon uklanjanja strategija svođenjem jednog igrača na samo 1 strategiju
                                                                                                    //provjeraSedla.ukloniDuplikatneStrategije();
                        SedloDominacija sedloIntern = new SedloDominacija(protuprirodnost.uneseniPodaci);

                        Tuple<bool, int, int> postojanjeSedlaIntern = sedloIntern.ProvjeriSedlo();
                        bool postojiSedloIntern = postojanjeSedlaIntern.Item1;
                        int rezultatIgreIntern = postojanjeSedlaIntern.Item2;

                        if (postojiSedloIntern)
                        {
                            MessageBox.Show("Postoji sedlo nakon uklanjanja strategija!\nVrijednost ove igre iznosi: " + rezultatIgreIntern, "Kraj igre!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            kontradiktornaIgraInternSedlo = true;
                            return null;
                        }
                        else
                        {
                            SimplexKalkulatorB smplxCalcKI = new SimplexKalkulatorB(protuprirodnost.uneseniPodaci, provjeraSedla.ProvjeriSedlo().Item3);
                            return new SimplexForma(smplxCalcKI.SimplexTabliceRazlomci, smplxCalcKI.Zakljucak, smplxCalcKI.indexiVodecihStupaca, smplxCalcKI.indexiVodecihRedaka, smplxCalcKI.brojRedaka, smplxCalcKI.brojStupaca, smplxCalcKI.postupakIzracuna);
                        }
                    }
                }
            }
        }

        private SimplexForma simplexForma;

        private void btnSimplex_Click(object sender, EventArgs e)
        {
            try
            {
                simplexForma = simplexMetoda();

                if (simplexForma == null)
                {
                    SedloDominacija provjeraSedla = new SedloDominacija(uneseniDobiciGubitci);
                    provjeraSedla.ProvjeriSedlo();

                    if (nerijesenRezultatPocetakSedlo == true || protuprirodnaIgraPocetakSedlo == true || kontradiktornaIgraPocetakSedlo == true)
                    {
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        FormaSedla formaSedla = new FormaSedla(provjeraSedla.maximumiStupca, provjeraSedla.minimumiReda, matricnaIgra.IspisMatricneIgre());
                        formaSedla.Show();
                    }

                    else if (nerijesenRezultatInternSedlo == true || protuprirodnaIgraInternSedlo == true || kontradiktornaIgraInternSedlo == true)
                    {
                        ProtuprirodnaKontradiktornaIgra sedloProtKont = new ProtuprirodnaKontradiktornaIgra(uneseniDobiciGubitci);
                        sedloProtKont.ProvjeriProtuprirodnost();
                        SedloDominacija sedloIntern = new SedloDominacija(sedloProtKont.uneseniPodaci);
                        sedloIntern.ProvjeriSedlo();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(sedloProtKont.uneseniPodaci);
                        FormaSedla formaSedla = new FormaSedla(sedloIntern.maximumiStupca, sedloIntern.minimumiReda, matricnaIgra.IspisMatricneIgre());
                        formaSedla.Show();
                    }

                    else
                    {
                        provjeraSedla.ukloniDominantneStrategije();
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraSedla.uneseniPodaci);
                        FormaSedla formaSedla = new FormaSedla(provjeraSedla.maximumiStupca, provjeraSedla.minimumiReda, matricnaIgra.IspisMatricneIgre());
                        formaSedla.Show();
                    }
                }
                else
                {
                    simplexForma.Show();
                    ispisTablicaIteracijaToolStripMenuItem.Enabled = true;
                    ispisPostupkaIzračunaToolStripMenuItem.Enabled = true;
                }
            }

            catch
            {
                MessageBox.Show("Unesite gubitke i dobitke strategija pojedinih igrača!", "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private FormaModela formaModela;

        private string stvoriModelProblema()
        {
                if (rbIgracA.Checked == true)
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    SedloDominacija provjeraDominacije = new SedloDominacija(uneseniDobiciGubitci);

                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraDominacije.ukloniDominantneStrategije();
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraDominacije.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena je miješana matrična igra." + Environment.NewLine;
                        
                        uklonjeneStrategije += provjeraDominacije.IspisUklonjenihStrategijaIgracaA();
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uključujem dominantne ili duplikatne strategije.";//prikaz matricne igre

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(provjeraDominacije.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                    else if (vrstaIgre == 1) // izračun po 3 kriterija
                    {
                        provjeraDominacije.ukloniDuplikatneStrategije();
                        if(!protuprirodnost.ProvjeraSvihJednakihIsplata())
                            protuprirodnost.ukloniDuplikatneStrategije();
                        
                        //protuprirodnost.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        KriterijiProtuprirodnosti kriteriji = new KriterijiProtuprirodnosti(protuprirodnost.uneseniPodaci, 1); //rjesavanje po kriterijima

                        string uklonjeneStrategije = "Unesena igra je protuprirodna ili postaje protuprirodna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + kriteriji.IspisiVrijednostiKriterija() + Environment.NewLine + "Kod izrade modela ne uključujem dominantne ili duplikatne strategije." + Environment.NewLine;
                        
                        /* NEPOTREBNO- ponovni ispis uklonjenih strategija i matrice
                        if(protuprirodnost.IspisUklonjenihStrategijaIgraca() != string.Empty)
                        {
                            //uklonjeneStrategije += protuprirodnost.IspisUklonjenihStrategijaIgraca();
                            //matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);
                            //uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();
                        }
                        */

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(protuprirodnost.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                    else
                    {
                        //provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena igra je kontradiktorna ili postaje kontradiktorna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uklanjam strategije. " + Environment.NewLine;
                        uklonjeneStrategije += provjeraDominacije.IspisUklonjenihDuplikatnihA();

                        matricnaIgra = new MatricnaIgra(provjeraDominacije.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaA modelZadatkaA = new IzgradnjaModelaA(provjeraDominacije.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaA.DohvatiZapisModela();

                    }
                }
                else //igracB.Check == true;
                {
                    uneseniDobiciGubitci = new SpremanjeUnosa(dgvMatrica);

                    SedloDominacija provjeraDominacije = new SedloDominacija(uneseniDobiciGubitci);

                    ProtuprirodnaKontradiktornaIgra protuprirodnost = new ProtuprirodnaKontradiktornaIgra(new SpremanjeUnosa(dgvMatrica));

                    int vrstaIgre = protuprirodnost.ProvjeriProtuprirodnost();

                    if (vrstaIgre == 0)
                    {
                        provjeraDominacije.ukloniDominantneStrategije();
                        MatricnaIgra matricnaIgra = new MatricnaIgra(provjeraDominacije.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena je miješana matrična igra." + Environment.NewLine;

                        uklonjeneStrategije += provjeraDominacije.IspisUklonjenihStrategijaIgracaB();
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uključujem dominantne ili duplikatne strategije.";

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(provjeraDominacije.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + Environment.NewLine + modelZadatkaB.DohvatiZapisModela();
                    }
                    else if (vrstaIgre == 1) // izračun po 3 kriterija
                    {
                        provjeraDominacije.ukloniDuplikatneStrategije();

                        if (!protuprirodnost.ProvjeraSvihJednakihIsplata())
                            protuprirodnost.ukloniDuplikatneStrategije();

                        //protuprirodnost.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);
                        KriterijiProtuprirodnosti kriteriji = new KriterijiProtuprirodnosti(protuprirodnost.uneseniPodaci, 2); //rjesavanje po kriterijima
                        
                        string uklonjeneStrategije = "Unesena igra je protuprirodna ili postaje protuprirodna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + kriteriji.IspisiVrijednostiKriterija() + Environment.NewLine + "Kod izrade modela ne uključujem dominantne ili duplikatne strategije." + Environment.NewLine;

                        /* NEPOTREBNO
                        if (protuprirodnost.IspisUklonjenihStrategijaIgraca() != string.Empty)
                        {
                            uklonjeneStrategije += protuprirodnost.IspisUklonjenihStrategijaIgraca();
                            //matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);
                            //uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();
                        }
                        */

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(protuprirodnost.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

                        if (String.IsNullOrEmpty(uklonjeneStrategije))
                            uklonjeneStrategije = "Ne postoji niti jedna dominantna ili duplikatna strategija!";

                        return uklonjeneStrategije + Environment.NewLine + modelZadatkaB.DohvatiZapisModela();
                    }
                    else
                    {
                        //provjeraSedla.ukloniDuplikatneStrategije();

                        MatricnaIgra matricnaIgra = new MatricnaIgra(protuprirodnost.uneseniPodaci);

                        string uklonjeneStrategije = "Unesena igra je kontradiktorna ili postaje kontradiktorna igra nakon uklanjanja strategija:" + protuprirodnost.IspisUklonjenihStrategijaIgraca() + matricnaIgra.IspisMatricneIgre() + Environment.NewLine + Environment.NewLine + "Kod izrade modela ne uklanjam strategije. " + Environment.NewLine;
                        uklonjeneStrategije += provjeraDominacije.IspisUklonjenihDuplikatnihB();

                        matricnaIgra = new MatricnaIgra(provjeraDominacije.uneseniPodaci);
                        uklonjeneStrategije += matricnaIgra.IspisMatricneIgre();

                        IzgradnjaModelaB modelZadatkaB = new IzgradnjaModelaB(provjeraDominacije.uneseniPodaci, provjeraDominacije.ProvjeriSedlo().Item3);

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
                    formaModela.Show();

                    ispisModelaZadatkaToolStripMenuItem.Enabled = true;
                }
                else //igracB.Check == true;
                {
                    formaModela = new FormaModela(stvoriModelProblema());
                    formaModela.Show();

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
            try
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
            catch
            {
                MessageBox.Show("Ispravite prvo pogreške u matrici plaćanja!");
            }

        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ispisTablicaIteracijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnSimplex.PerformClick();
                
                DGVPrinter printer = new DGVPrinter();

                if(igrac == 1)
                    printer.Title = "Tablice iteracija igrača A: ";//zaglavlje
                else
                    printer.Title = "Tablice iteracija igrača B: ";//zaglavlje

                printer.TitleAlignment = StringAlignment.Near;
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.DataWidth;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.FooterSpacing = 10;
                printer.FooterAlignment = StringAlignment.Near;
                printer.printDocument.DefaultPageSettings.Landscape = true;

                printer.Footer = simplexForma.DohvatiRjesenjeProblema();
                printer.PrintDataGridView(simplexForma.DohvatiTabliceIteracije());
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
            string putPomoci = Application.StartupPath + @"\Pomoc.pdf";
            OtvoriDatoteku(putPomoci);

        }

        private void OtvoriDatoteku(string putDatoteke)
        {
            try
            {
                System.Diagnostics.Process.Start(putDatoteke);
            }
            catch
            {
                MessageBox.Show("Dogodila se pogreška kod otvaranja pomoći alata!\nPokušajte ponovno!");
            }
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
