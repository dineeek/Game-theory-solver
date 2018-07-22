using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class ProtuprirodnaIgra
    {
        private SpremanjeUnosa uneseniPodaci;
        public ProtuprirodnaIgra(SpremanjeUnosa podaci)
        {
            uneseniPodaci = podaci;
        }

        public bool ProvjeriProtuprirodnost()
        {
            //Sedlo provjeraSedla = new Sedlo(uneseniPodaci);
            bool protuprirodna = false;
            int igracA, igracB;

            int brojStrategijaA = uneseniPodaci.igracA.Count;
            int brojStrategijaB = uneseniPodaci.igracB.Count;

            Tuple<int, int> brojUklonjenih;
            brojUklonjenih = DohvatiBrojUklonjenih();

            igracA = brojUklonjenih.Item1;
            igracB = brojUklonjenih.Item2;

            if ((brojStrategijaA - igracA) == 0 || (brojStrategijaB - igracB) == 0)
            {
                System.Windows.Forms.MessageBox.Show("Unesena je protuprirodna ili kontradiktorna igra!\nNe uklanjam dominantne strategije.");
                protuprirodna = true;
                return protuprirodna;
            }
            else if((((brojStrategijaA - igracA) >= 2) && ((brojStrategijaB - igracB) < 2)) || (((brojStrategijaA - igracA) < 2) && ((brojStrategijaB - igracB) >= 2)))//kontradiktorna
            {
                System.Windows.Forms.MessageBox.Show("Unesena je kontradiktorna igra!\nNe uklanjam dominantne strategije.");
                protuprirodna = true;
                return protuprirodna;
            }
            else
                return protuprirodna;
        }

        //PROVJERA BRISANJA
        private int brojUklonjenihA()
        {
            //za igrača A – red sa svim negativnim brojevima
            int brojacStrategijaA = 0;
            int brojUklonjenih = 0;
            foreach (var strategija in uneseniPodaci.igracA.ToList())
            {
                bool sviNegativni = strategija.DobitakGubitakStrategije.All(x => x < 0);

                if (sviNegativni)
                {
                    uneseniPodaci.igracA.Remove(strategija);
                    brojUklonjenih++;

                    //brisanje kod igracaB
                    foreach (var strategijaB in uneseniPodaci.igracB.ToList())
                    {
                        List<int> pomoc = strategijaB.DobitakGubitakStrategije.ToList();
                        pomoc.RemoveAt(brojacStrategijaA); //brise one brojeve koji su gore uklonjeni
                        strategijaB.DobitakGubitakStrategije = pomoc.ToArray();
                    }

                    brojacStrategijaA--;
                }
                brojacStrategijaA++;
            }
            return brojUklonjenih;
        }

        private int brojUklonjenihB()
        {
            //za igrača B – stupac sa svim pozitivnim brojevima
            int brojacStrategijaB = 0;
            int brojUklonjenih = 0;
            foreach (var strategija in uneseniPodaci.igracB.ToList()) //treba ukloniti i kod A te brojke
            {
                bool sviPozitivni = strategija.DobitakGubitakStrategije.All(x => x >= 0);

                if (sviPozitivni)
                {
                    uneseniPodaci.igracB.Remove(strategija);
                    brojUklonjenih++;

                    //brisanje kod igracaA
                    foreach (var strategijA in uneseniPodaci.igracA.ToList())
                    {
                        List<int> pomoc = strategijA.DobitakGubitakStrategije.ToList();
                        pomoc.RemoveAt(brojacStrategijaB);
                        strategijA.DobitakGubitakStrategije = pomoc.ToArray();
                    }
                    brojacStrategijaB--;
                }
                brojacStrategijaB++;
            }
            return brojUklonjenih;
        }

        public Tuple<int, int> DohvatiBrojUklonjenih()
        {
            int igracA = 0;
            int igracB = 0;
            for (int i = 0; i < uneseniPodaci.igracA.Count + uneseniPodaci.igracB.Count; i++)
            {
                igracA += brojUklonjenihA();
                igracB += brojUklonjenihB();
            }

            return new Tuple<int, int>(igracA, igracB);
        }
    }
}
