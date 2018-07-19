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

        private void odrediVodeciStupac()
        {

        }

        private void odrediVodeciRedak()
        {

        }

        
        private void pokreniSimplexPostupak()
        {
            //novaSimplexTablica - temelji se na prethodnojSimplexTablici
            odrediVodeciRedak();
            odrediVodeciStupac();
            //napraviti pomocni dgv gdje ce biti zapisano samo prethodno razdoblje i pomocu njega dodavati novo 
            //pokreniSimplexPostupak();

            prethodnaSimplexTablica = novaSimplexTablica;
        }
    }
}
