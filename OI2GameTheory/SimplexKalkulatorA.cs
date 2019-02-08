using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace OI2GameTheory
{
    public class SimplexKalkulatorA // standardi problem LP za maximum - za igracA
    {
        private SpremanjeUnosa podaciStrategija;
        private int diferencija;

        private DataTable pocetnaSimplexTablica = new DataTable();
        private DataTable prethodnaSimplexTablica = new DataTable(); //pomocna
        private DataTable novaSimplexTablica = new DataTable();

        public DataTable SimplexTablice = new DataTable(); //svi postupci
        public DataTable SimplexTabliceRazlomci = new DataTable(); //svi postupci
        public string Zakljucak = "";

        public List<int> indexiVodecihStupaca = new List<int>();
        public List<int> indexiVodecihRedaka = new List<int>();
        public int brojRedaka;
        public int brojStupaca;
        public string postupakIzracuna;

        public SimplexKalkulatorA(SpremanjeUnosa podaci, int minDif)
        {
            podaciStrategija = podaci;

            if (minDif < 0)
                diferencija = Math.Abs(minDif) + 1;
            else
                diferencija = 0;
            diferencirajPodatke();

            stvoriPocetnuTablicu();
            pokreniSimplexPostupak();
        }

        private void diferencirajPodatke()
        {
            foreach(var strategija in podaciStrategija.igracA.ToList())//ne mora se i kroz strategije igraca B ići
            {
                for(int i=0; i<strategija.DobitakGubitakStrategije.Length; i++)
                {
                    strategija.DobitakGubitakStrategije[i] += diferencija;
                }
            }
        }

        private void stvoriPocetnuTablicu()
        {
            //stupci
            pocetnaSimplexTablica.Columns.Add("Cj", typeof(int));
            pocetnaSimplexTablica.Columns.Add("Var", typeof(string));
            pocetnaSimplexTablica.Columns.Add("Kol", typeof(string));

            for(int i=0; i<podaciStrategija.igracB.Count; i++)
                pocetnaSimplexTablica.Columns.Add("Ῡ" + (i + 1) + "", typeof(string)); //Ῡ - supstitucija za yi/v'

            for (int i = 0; i < podaciStrategija.igracA.Count; i++)
                pocetnaSimplexTablica.Columns.Add("u" + (i + 1) + "", typeof(string)); //dopunske varijable - ovise o broju jednadzbi tj. igracu 

            pocetnaSimplexTablica.Columns.Add("Kontrola", typeof(string));
            pocetnaSimplexTablica.Columns.Add("Rezultat", typeof(string));

            //redci
            int brojacStrategijaA = 0;
            foreach(var strategijaA in podaciStrategija.igracA)
            {
                double internKontrol = 0;
                var noviRedak = pocetnaSimplexTablica.NewRow();

                noviRedak["Cj"] = 0;
                noviRedak["Var"] = "u" + (brojacStrategijaA + 1) + "";
                noviRedak["Kol"] = 1; //sve strategije (x1+x2.. = 1)
                internKontrol += 1;

                for (int j = 0; j < strategijaA.DobitakGubitakStrategije.Length; j++)
                {
                    noviRedak["Ῡ" + (j + 1) + ""] = strategijaA.DobitakGubitakStrategije[j];
                    internKontrol += strategijaA.DobitakGubitakStrategije[j];
                }

                for (int j = 0; j < podaciStrategija.igracA.Count; j++)
                {

                    if ((j + 1) == (brojacStrategijaA + 1))
                    {
                        noviRedak["u" + (j + 1) + ""] = 1;
                        internKontrol += 1;
                    }
                    else
                        noviRedak["u" + (j + 1) + ""] = 0;
                }

                noviRedak["Kontrola"] = internKontrol;
                pocetnaSimplexTablica.Rows.Add(noviRedak);
                brojacStrategijaA++;
            }

            //Zj-Cj redak
            var redakZjCj = pocetnaSimplexTablica.NewRow();
            redakZjCj["Var"] = "Zj-Cj";
            redakZjCj["Kol"] = 0;

            for (int i = 0; i < podaciStrategija.igracB.Count; i++)
            {
                redakZjCj["Ῡ" + (i + 1) + ""] = -1;
            }

            for (int i = 0; i < podaciStrategija.igracA.Count; i++)
            {
                redakZjCj["u" + (i + 1) + ""] = 0;
            }

            double kontrolaRedka = 0;//kolicina
            foreach (var strategija in podaciStrategija.igracB)
                kontrolaRedka--;


            redakZjCj["Kontrola"] = kontrolaRedka;
            pocetnaSimplexTablica.Rows.Add(redakZjCj);

            //prazan redak radi preglednosti
            var prazanRedak1 = pocetnaSimplexTablica.NewRow();
            pocetnaSimplexTablica.Rows.Add(prazanRedak1);

            SimplexTablice.Merge(pocetnaSimplexTablica);

            prethodnaSimplexTablica = pocetnaSimplexTablica;
        }

        private (int, string) odrediVodeciStupac()
        {
            int indexStupca = 0;
            string nazivSupca = "";

            double najveci = 0;
            double internHelp = 0;

            //koliko ima neg. u zadnjem redku
            int brojNegativnih = 0;
            for (int i = 3; i < prethodnaSimplexTablica.Columns.Count - 2; i++)
            {
                internHelp = Convert.ToDouble(prethodnaSimplexTablica.Rows[prethodnaSimplexTablica.Rows.Count - 2][i].ToString());
                if (internHelp < 0)
                {
                    brojNegativnih++;
                }
            }

            double[] vrijednostiRedaZjCj = new double[brojNegativnih];
            int[] indexiVrijSupca = new int[brojNegativnih];
            int brojac = 0;
            for (int i=3; i<prethodnaSimplexTablica.Columns.Count-2; i++)
            {
                internHelp = Convert.ToDouble(prethodnaSimplexTablica.Rows[prethodnaSimplexTablica.Rows.Count - 2][i].ToString());
                if (internHelp < 0)
                {
                    internHelp = Math.Abs(internHelp);
                    vrijednostiRedaZjCj[brojac] = internHelp;
                    indexiVrijSupca[brojac] = i;
                    brojac++;

                    if (najveci < internHelp)
                    {
                        najveci = internHelp;
                        indexStupca = i;
                        nazivSupca = prethodnaSimplexTablica.Columns[i].ColumnName;
                    }
                }
            }

            double prviBroj = vrijednostiRedaZjCj[0];
            bool sviIsti = false;
            sviIsti = vrijednostiRedaZjCj.Skip(1)
              .All(s => double.Equals(prviBroj, s));

            if (vrijednostiRedaZjCj.Count() == 1)
            {
                return (indexStupca, nazivSupca);
            }
            else if (sviIsti) 
            {
                double najvecaVrijednostStupca = 0;
                for(int i = 0; i < indexiVrijSupca.Length; i++)
                {
                    double vrijednostStupca = 0;
                    for (int j = 0; j < podaciStrategija.igracA.Count; j++)
                    {
                        vrijednostStupca += Convert.ToDouble(prethodnaSimplexTablica.Rows[j][indexiVrijSupca[i]].ToString());
                    }

                    if (najvecaVrijednostStupca < vrijednostStupca)
                    {
                        najvecaVrijednostStupca = vrijednostStupca;
                        indexStupca = indexiVrijSupca[i];
                        nazivSupca = prethodnaSimplexTablica.Columns[indexStupca].ColumnName;
                    }
                }
                return (indexStupca, nazivSupca);
            }
            else
            {
                return (indexStupca, nazivSupca);
            }
        }

        private int odrediVodeciRedak(int indexStupca)
        {
            int indexReda = 0;

            double internHelp = 0;
            double[] rezultati = new double[prethodnaSimplexTablica.Rows.Count - 2];
            double djeljenik = 0;
            double djelitelj = 0;
            List<double> odabraniRez = new List<double>();

            for (int i = 0; i<prethodnaSimplexTablica.Rows.Count-2; i++) // djeljenik može biti negativan, djelitelj ne
            {
                djeljenik = Convert.ToDouble(prethodnaSimplexTablica.Rows[i][2]);
                djelitelj = Convert.ToDouble(prethodnaSimplexTablica.Rows[i][indexStupca]);

                internHelp = (double) djeljenik/djelitelj;              
                rezultati[i] = internHelp;
                if(djelitelj > 0)
                {
                    odabraniRez.Add(internHelp);
                    prethodnaSimplexTablica.Rows[i][prethodnaSimplexTablica.Columns.Count - 1] = Math.Round((double)internHelp, 6);
                }
            }
            SimplexTablice.Merge(prethodnaSimplexTablica);

            //double najmanji = rezultati.Where(x => x > 0).Min();
            double najmanji = odabraniRez.Min();

            int brojacIstihMin = 0;
            bool postojeIsteMinVrijednostiRez = false;
            for(int i=0; i<rezultati.Length; i++)
            {
                if (najmanji == rezultati[i])
                    brojacIstihMin++;
            }

            int[] indexiIstihRezultata = new int[brojacIstihMin];

            if (brojacIstihMin >= 2) // u slučaju da postoje isti koji nisu minimalni
            {
                postojeIsteMinVrijednostiRez = true;
            }

            int pomocniBrojac1 = 0;
            for (int i = 0; i < rezultati.Length; i++)
            {
                if (najmanji == rezultati[i])
                {
                    indexiIstihRezultata[pomocniBrojac1] = i;
                    pomocniBrojac1++;
                }
            }

            double[] degeneracija;
            if (postojeIsteMinVrijednostiRez)
            {
                for(int i=3; i<prethodnaSimplexTablica.Columns.Count-2; i++)//pomicanje po stupcima
                {
                    degeneracija = new double[indexiIstihRezultata.Length];
                    if (i != indexStupca)//indexStupca je vodeci stupac i njega preskačem
                    {
                        for (int j = 0; j < indexiIstihRezultata.Length; j++)//pomicanje po redcima
                        {
                            //System.Windows.Forms.MessageBox.Show(indexiIstihRezultata[j].ToString());
                            double djeljenikIntern = Convert.ToDouble(prethodnaSimplexTablica.Rows[indexiIstihRezultata[j]][i]);
                            double djeliteljIntern = Convert.ToDouble(prethodnaSimplexTablica.Rows[indexiIstihRezultata[j]][indexStupca]);

                            if (djeliteljIntern > 0)
                                degeneracija[j] = (double)djeljenikIntern / djeliteljIntern;
                            else
                                degeneracija[j] = Convert.ToDouble(999999 + j);

                        }

                        /* za gledanje svih rezltata redaka, a ne samo onih koji su isti
                        for (int j = 0; j < prethodnaSimplexTablica.Rows.Count - 2; j++)//pomicanje po redcima
                        {
                            double djeljenikIntern = Convert.ToDouble(prethodnaSimplexTablica.Rows[j][i]);
                            double djeliteljIntern = Convert.ToDouble(prethodnaSimplexTablica.Rows[j][indexStupca]);

                            if(djeliteljIntern > 0)
                                degeneracija[j] = (double) djeljenikIntern/djeliteljIntern;
                            else
                                degeneracija[j] = Convert.ToDouble(999999 + j);
                           
                        }
                        */
                        double najmanjiDegene = degeneracija.Min();

                        int brojacIstihUDeg = 0;
                        bool postojiJednistveniMin = true;
                        for (int d = 0; d < degeneracija.Length; d++)
                        {
                            if (najmanjiDegene == degeneracija[d])
                                brojacIstihUDeg++;
                        }

                        if (brojacIstihUDeg >= 2) // u slučaju da postoje isti koji nisu minimalni
                        {
                            postojiJednistveniMin = false;
                        }


                        if (postojiJednistveniMin)
                        {
                            double najmanjiRedak = degeneracija.Min();//PROMJENITI AKO NIJE DOBRA DEGENERACIJA TAK
                            for (int d = 0; d < degeneracija.Length; d++)
                            {
                                if (najmanjiRedak == degeneracija[d])
                                {
                                    indexReda = indexiIstihRezultata[d];
                                }
                            }
                            //return indexReda;
                            break;
                        }
                    }
                }
                return indexReda;
            }
            else
            {
                for (int i = 0; i < rezultati.Length; i++)
                {
                    if (najmanji == rezultati[i])
                        indexReda = i;
                }
                return indexReda;
            }
        }

        private double odrediStozerniElement(int indexStupca, int indexReda)
        {
            double stozerniElement = Convert.ToDouble(prethodnaSimplexTablica.Rows[indexReda][indexStupca]);
            //SimplexTablice.Merge(prethodnaSimplexTablica);
            return stozerniElement;
        }

        private void izracunajElementeVodecegRedka(int indexStupca, int indexRedka, double stozerniElement, string nazivVodSupca)
        {
            for(int i=2; i<prethodnaSimplexTablica.Columns.Count-1; i++)
            {
                double internHelp = Convert.ToDouble(prethodnaSimplexTablica.Rows[indexRedka][i]);
                novaSimplexTablica.Rows[indexRedka][i] = Math.Round((double) internHelp / (double) stozerniElement, 6);
            }

            novaSimplexTablica.Rows[indexRedka][0] = 1; //vrijednost Cj = 1
            novaSimplexTablica.Rows[indexRedka][1] = nazivVodSupca; //vrijednost var

            for(int i=0; i<prethodnaSimplexTablica.Rows.Count-1; i++)
            {
                if (i != indexRedka)
                    novaSimplexTablica.Rows[i][indexStupca] = 0;
            }
        }

        //TU DODATI ZA PRIKAZ IZRAČUNA
        private void simplexAlgoritam(int indexStupca, int indexRedka, double stozerniElement)
        {
            //od index 2 do count-2
            for (int i = 2; i < novaSimplexTablica.Columns.Count - 1; i++)//stupci
            {
                postupakIzracuna += novaSimplexTablica.Columns[i].ColumnName + Environment.NewLine + Environment.NewLine;

                if(i != indexStupca)
                {
                    for (int j = 0; j < novaSimplexTablica.Rows.Count - 1; j++)//redci
                    {
                        if(j != indexRedka)
                        {
                            //double internHelp = (double) (Convert.ToDouble(prethodnaSimplexTablica.Rows[j][i].ToString()) - ((double)((double)(Convert.ToDouble(prethodnaSimplexTablica.Rows[indexRedka][i].ToString()) / (double) stozerniElement) * Convert.ToDouble(prethodnaSimplexTablica.Rows[j][indexStupca].ToString()))));
                            //novaSimplexTablica.Rows[j][i] = Math.Round((double)internHelp, 6);

                            //broj1 - (broj2/stozerni) * broj3 = internHelp - ISPIS POSTUPKA
                            double broj1 = (double)(Convert.ToDouble(prethodnaSimplexTablica.Rows[j][i].ToString()));
                            double broj2 = (double)(Convert.ToDouble(prethodnaSimplexTablica.Rows[indexRedka][i].ToString()) / (double)stozerniElement);
                            double broj3 = (double) Convert.ToDouble(prethodnaSimplexTablica.Rows[j][indexStupca].ToString());

                            double internHelp = broj1 - (broj2 * broj3);
                            novaSimplexTablica.Rows[j][i] = Math.Round((double)internHelp, 6);

                            string broj1Razlomak;
                            string broj2Razlomak;
                            string broj3Razlomak;
                            string rezultat;

                            if ((broj1 % 1) != 0)
                                broj1Razlomak = RealToFraction(broj1, 0.0001).N + "/" + RealToFraction(broj1, 0.0001).D;
                            else
                                broj1Razlomak = broj1.ToString();

                            if ((broj2 % 1) != 0)
                                broj2Razlomak = RealToFraction(broj2, 0.0001).N + "/" + RealToFraction(broj2, 0.0001).D;
                            else
                                broj2Razlomak = broj2.ToString();

                            if ((broj3 % 1) != 0)
                                broj3Razlomak = RealToFraction(broj3, 0.0001).N + "/" + RealToFraction(broj3, 0.0001).D;
                            else
                                broj3Razlomak = broj3.ToString();

                            if ((internHelp % 1) != 0)
                                rezultat = RealToFraction(internHelp, 0.0001).N + "/" + RealToFraction(internHelp, 0.0001).D;
                            else
                                rezultat = internHelp.ToString();

                            postupakIzracuna += broj1Razlomak + " - (" + broj2Razlomak + " * " + broj3Razlomak +") = "+ rezultat + Environment.NewLine + Environment.NewLine; 
                        }
                    }
                }
            }
        }

        int brojIteracija = 2;
        int help = 1;
        private void pokreniSimplexPostupak()
        {
            brojRedaka = prethodnaSimplexTablica.Rows.Count;
            brojStupaca = prethodnaSimplexTablica.Columns.Count;

            int indexStupca = odrediVodeciStupac().Item1;
            string nazivVodecegStupca = odrediVodeciStupac().Item2;
            indexiVodecihStupaca.Add(indexStupca);

            int indexRedka = odrediVodeciRedak(indexStupca);
            indexiVodecihRedaka.Add(indexRedka);

            double stozerniElement = odrediStozerniElement(indexStupca, indexRedka);

            novaSimplexTablica = prethodnaSimplexTablica.Clone();//da naslijedi strukturu samo

            for(int i=0; i<prethodnaSimplexTablica.Rows.Count; i++)//dodavanje redaka u novu
            {
                var noviRedak = novaSimplexTablica.NewRow();
                novaSimplexTablica.Rows.Add(noviRedak);
            }

            for (int i = 0; i < prethodnaSimplexTablica.Rows.Count-1; i++)//prepisivanje statike
            {
                if(i != indexRedka)
                {
                    novaSimplexTablica.Rows[i][0] = prethodnaSimplexTablica.Rows[i][0];//Cj stupac
                    novaSimplexTablica.Rows[i][1] = prethodnaSimplexTablica.Rows[i][1];//Var stupac
                }
            }

            izracunajElementeVodecegRedka(indexStupca, indexRedka, stozerniElement, nazivVodecegStupca);

            postupakIzracuna += "Postupak izračuna za igrača A: " + Environment.NewLine;
            if (help == 1) // za prikaz postupka izracunavanja 
            {
                postupakIzracuna += "--------------------1. ITERACIJA--------------------" + Environment.NewLine + Environment.NewLine;
                help++;
            }

            simplexAlgoritam(indexStupca, indexRedka, stozerniElement);

            SimplexTablice.Merge(novaSimplexTablica); //prije if-a obavezno

            //koliko ima neg. u zadnjem redku
            int brojNegativnih = 0;
            for (int i = 3; i < novaSimplexTablica.Columns.Count - 2; i++)
            {
                double internHelp = Convert.ToDouble(novaSimplexTablica.Rows[novaSimplexTablica.Rows.Count - 2][i].ToString());
                if (internHelp < 0)
                {
                    brojNegativnih++;
                }
            }

            if(brojNegativnih != 0)
            {
                //prethodnaSimplexTablica = novaSimplexTablica;
                prethodnaSimplexTablica = new DataTable();
                prethodnaSimplexTablica = novaSimplexTablica.Copy();//da naslijedi strukturu samo
                novaSimplexTablica = new DataTable();

                postupakIzracuna += "--------------------" + brojIteracija + ". ITERACIJA--------------------" + Environment.NewLine + Environment.NewLine;
                brojIteracija++;

                pokreniSimplexPostupak();
            }
            else
            {
                //pisanje iteracija tako da je pregledno i jasnije
                int interHelp = SimplexTablice.Rows.Count-1;
                int brojRedovaIteracije = (novaSimplexTablica.Rows.Count*2)-1;
                int brojRedova = (novaSimplexTablica.Rows.Count * 2);
                int brojIteracija = 1;
                for (int i=0; i< interHelp; i++)
                {
                    if (i == brojRedovaIteracije)
                    {
                        SimplexTablice.Rows[i][1] ="Tablica "+ brojIteracija+". iteracije";
                        brojRedovaIteracije += brojRedova;
                        brojIteracija++;
                    }

                }

                KalkulatorZakljuckaA zakljucak = new KalkulatorZakljuckaA(novaSimplexTablica, podaciStrategija, diferencija);
                Zakljucak = zakljucak.DohvatiZakljucak();

                
                //pretvaranje decimalni u razlomke
                SimplexTabliceRazlomci = SimplexTablice.Copy();
                SimplexTablice = new DataTable();
                PretvoriURazlomke();
                
            }
        }

        private void PretvoriURazlomke()
        {
            for(int i = 2; i<SimplexTabliceRazlomci.Columns.Count; i++) //stupci
            {
                for(int j=0; j<SimplexTabliceRazlomci.Rows.Count-1; j++)//redci
                {
                    double broj = 0;
                    bool praznaCelija = false;
                    if (string.IsNullOrEmpty(SimplexTabliceRazlomci.Rows[j][i].ToString()))
                        praznaCelija = true;
                    else
                        broj = Convert.ToDouble(SimplexTabliceRazlomci.Rows[j][i]); 

                    if (!praznaCelija)
                    {
                        if ((broj % 1) != 0)
                            SimplexTabliceRazlomci.Rows[j][i] = RealToFraction(broj, 0.0001).N + "/" + RealToFraction(broj, 0.0001).D;
                    }
                }
            }
        }

        //PRETVARANJE double U RAZLOMKE
        public struct Fraction
        {
            public Fraction(int n, int d)
            {
                N = n;
                D = d;
            }

            public int N { get; private set; }
            public int D { get; private set; }
        }

        public Fraction RealToFraction(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            double maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    Seek(ref upper_n, ref upper_d, lower_n, lower_d, (un, ud) => (lower_d + ud) * (value + maxError) < (lower_n + un));
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    Seek(ref lower_n, ref lower_d, upper_n, upper_d, (ln, ld) => (ln + upper_n) < (value - maxError) * (ld + upper_d));
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }

        private void Seek(ref int a, ref int b, int ainc, int binc, Func<int, int, bool> f)
        {
            a += ainc;
            b += binc;

            if (f(a, b))
            {
                int weight = 1;

                do
                {
                    weight *= 2;
                    a += ainc * weight;
                    b += binc * weight;
                }
                while (f(a, b));

                do
                {
                    weight /= 2;

                    int adec = ainc * weight;
                    int bdec = binc * weight;

                    if (!f(a - adec, b - bdec))
                    {
                        a -= adec;
                        b -= bdec;
                    }
                }
                while (weight > 1);
            }
        }
    }
}
