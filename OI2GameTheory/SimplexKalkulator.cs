using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OI2GameTheory
{
    public class SimplexKalkulator // standardi problem LP za maximum - za igracA se racuna (inače igracB je za min) 
    {
        private SpremanjeUnosa podaciStrategija;
        private int diferencija;

        private DataTable pocetnaSimplexTablica = new DataTable();
        private DataTable prethodnaSimplexTablica = new DataTable(); //pomocna
        private DataTable novaSimplexTablica = new DataTable();

        public DataTable SimplexTablice = new DataTable(); //svi postupci

        public SimplexKalkulator(SpremanjeUnosa podaci, int minDif)
        {
            podaciStrategija = podaci;

            diferencija = Math.Abs(minDif) + 1;
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
            pocetnaSimplexTablica.Columns.Add("Kol", typeof(int));

            for(int i=0; i<podaciStrategija.igracB.Count; i++)
                pocetnaSimplexTablica.Columns.Add("Ῡ" + (i + 1) + "", typeof(double)); //Ῡ - supstitucija za yi/v'

            for (int i = 0; i < podaciStrategija.igracA.Count; i++)
                pocetnaSimplexTablica.Columns.Add("u" + (i + 1) + "", typeof(double)); //dopunske varijable - ovise o broju jednadzbi tj. igracu 

            pocetnaSimplexTablica.Columns.Add("Kontrola", typeof(double));
            pocetnaSimplexTablica.Columns.Add("Rezultat", typeof(double));

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
            double kontrolaRedka = 0;//kolicina

            foreach(var strategija in podaciStrategija.igracA)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    redakZjCj["Ῡ" + (i + 1) + ""] = -1;
                }

                for(int i = 0; i < podaciStrategija.igracA.Count; i++)
                {
                    redakZjCj["u" + (i + 1) + ""] = 0;
                }
                kontrolaRedka += -1;
            }

            redakZjCj["Kontrola"] = kontrolaRedka;
            pocetnaSimplexTablica.Rows.Add(redakZjCj);

            //prazan redak radi preglednosti
            var prazanRedak1 = pocetnaSimplexTablica.NewRow();
            pocetnaSimplexTablica.Rows.Add(prazanRedak1);

            SimplexTablice.Merge(pocetnaSimplexTablica);

            prethodnaSimplexTablica = pocetnaSimplexTablica;
        }

        private int odrediVodeciStupac()
        {
            int indexStupca = 0; 
            int ukupnoVrijednosti = podaciStrategija.igracB.Count + podaciStrategija.igracA.Count;//zbroj var Y i dopunskih

            double[] vrijednostiRedaZjCj = new double[ukupnoVrijednosti];
            double najveci = 0;

            for(int i=3; i<prethodnaSimplexTablica.Columns.Count-2; i++)
            {
                double internHelp = Math.Abs(Convert.ToDouble(prethodnaSimplexTablica.Rows[prethodnaSimplexTablica.Rows.Count - 2][i].ToString()));
                vrijednostiRedaZjCj[i - 3] = internHelp;
                if (najveci < internHelp)
                {
                    najveci = internHelp;
                    indexStupca = i;
                }
            }

            double prviBroj = vrijednostiRedaZjCj[0];
            bool sviIsti = false;
            sviIsti = vrijednostiRedaZjCj.Skip(1)
              .All(s => double.Equals(prviBroj, s));

            if (sviIsti)
            {
                double najvecaVrijednostStupca = 0;
                for(int i = 3; i < prethodnaSimplexTablica.Columns.Count - 2; i++)
                {
                    for (int j = 0; j < podaciStrategija.igracA.Count; j++)
                    {
                        double vrijednostStupca = Convert.ToDouble(prethodnaSimplexTablica.Rows[j][i].ToString());
                        if (najvecaVrijednostStupca < vrijednostStupca)
                        {
                            najvecaVrijednostStupca = vrijednostStupca;
                            indexStupca = i;
                        }
                    }
                }

                return indexStupca;
            }
            else
            {
                return indexStupca;
            }

        }

        private int odrediVodeciRedak(int indexStupca) //kaj ak imam iste vrijednosti rezultata
        {
            int indexReda = 0;

            double internHelp = 0;
            double[] rezultati = new double[prethodnaSimplexTablica.Rows.Count - 2];

            for (int i = 0; i<prethodnaSimplexTablica.Rows.Count-2; i++)
            {
                internHelp = Convert.ToDouble(prethodnaSimplexTablica.Rows[i][2]) / Convert.ToDouble(prethodnaSimplexTablica.Rows[i][indexStupca]);
                prethodnaSimplexTablica.Rows[i][prethodnaSimplexTablica.Columns.Count - 1] = internHelp;
                rezultati[i] = internHelp;
            }

            double najmanji = rezultati[0];
            for(int i= 0; i<rezultati.Length; i++)
            {
                if(najmanji > rezultati[i])
                {
                    najmanji = rezultati[i];
                    indexReda = i;
                }
            }

            return indexReda;
        }

        private int odrediStozerniElement(int indexStupca, int indexReda)
        {
            int stozerniElement = 0;
            //pobojati prije u prethodnoj stozerni element, ili redak i stupac i onda mergati
            SimplexTablice.Merge(prethodnaSimplexTablica);
            return stozerniElement;
        }

        
        private void pokreniSimplexPostupak()
        {
            //novaSimplexTablica - temelji se na prethodnojSimplexTablici
            System.Windows.Forms.MessageBox.Show(odrediVodeciRedak(odrediVodeciStupac()).ToString());

            int indexStupca = odrediVodeciStupac();
            int indexRedka = odrediVodeciRedak(indexStupca);
            int stozerniElement = odrediStozerniElement(indexStupca, indexRedka);

            SimplexTablice.Merge(novaSimplexTablica); //prije if-a obavezno

            //if(novaSimplexTablica treba daljnji postupak)
                //prethodnaSimplexTablica = novaSimplexTablica;
                //pokreniSimplexPostupak()
            //else
                //gotovo
        }
    }
}
