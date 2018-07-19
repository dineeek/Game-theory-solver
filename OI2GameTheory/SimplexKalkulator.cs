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
        public SpremanjeUnosa podaciStrategija;
        private int diferencija;
        public DataTable SimplexTablica = new DataTable();
        public SimplexKalkulator(SpremanjeUnosa podaci, int minDif)
        {
            podaciStrategija = podaci;

            diferencija = Math.Abs(minDif) + 1;
            diferencirajPodatke();

            StvoriPocetnuTablicu();
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

        private void StvoriPocetnuTablicu()
        {
            //stupci
            SimplexTablica.Columns.Add("Cj", typeof(int));
            SimplexTablica.Columns.Add("Var", typeof(string));
            SimplexTablica.Columns.Add("Kol", typeof(int));

            for(int i=0; i<podaciStrategija.igracA.Count; i++)
                SimplexTablica.Columns.Add("Ῡ" + (i + 1) + "", typeof(double)); //Ῡ - supstitucija za yi/v'

            for (int i = 0; i < podaciStrategija.igracA.Count; i++)
                SimplexTablica.Columns.Add("u" + (i + 1) + "", typeof(double)); //dopunske varijable

            SimplexTablica.Columns.Add("Kontrola", typeof(double));
            SimplexTablica.Columns.Add("Rezultat", typeof(double));


            //redci
            int brojacStrategija = 0;
            foreach(var strategija in podaciStrategija.igracA)
            {
                double internKontrol = 0;
                var noviRedak = SimplexTablica.NewRow();

                noviRedak["Cj"] = 0;
                noviRedak["Var"] = "u" + (brojacStrategija + 1) + "";
                noviRedak["Kol"] = 1; //sve strategije (x1+x2.. = 1)
                internKontrol += 1;

                for (int j = 0; j < strategija.DobitakGubitakStrategije.Length; j++)
                {
                    noviRedak["Ῡ" + (j + 1) + ""] = strategija.DobitakGubitakStrategije[j];
                    internKontrol += strategija.DobitakGubitakStrategije[j];

                    if ((j+1) == (brojacStrategija + 1))
                    {
                        noviRedak["u" + (j + 1) + ""] = 1;
                        internKontrol += 1;
                    }
                    else
                        noviRedak["u" + (j + 1) + ""] = 0;
                }

                noviRedak["Kontrola"] = internKontrol;

                SimplexTablica.Rows.Add(noviRedak);
                brojacStrategija++;
            }

            //Zj-Cj redak
            var redakZjCj = SimplexTablica.NewRow();
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

            SimplexTablica.Rows.Add(redakZjCj);

            //prazan redak radi preglednosti
            var prazanRedak1 = SimplexTablica.NewRow();
            SimplexTablica.Rows.Add(prazanRedak1);

        }
    }
}
