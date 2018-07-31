using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class Sedlo
    {
        public SpremanjeUnosa uneseniPodaci;
        public List<string> varijableA = new List<string>();
        public List<string> varijableB = new List<string>();
        public List<string> varijableAInverted = new List<string>();
        public List<string> varijableBInverted = new List<string>();

        public Sedlo(SpremanjeUnosa podaci)
        {
            uneseniPodaci = podaci;

            for (int i = 0; i < uneseniPodaci.igracA.Count; i++)
            {
                varijableA.Add("X" + (i + 1) + "");
            }
 
            for (int i = 0; i < uneseniPodaci.igracB.Count; i++)
            {
                varijableB.Add("Y" + (i + 1) + "");
            }
        }

        public Tuple<bool, int, int> ProvjeriSedlo()
        {
            bool postojiSedlo = false;

            List<int> minimumiReda = new List<int>();
            List<int> maximumiStupca = new List<int>();
            
            //min max reda
            foreach (var strategija in uneseniPodaci.igracA)
            {
                minimumiReda.Add(strategija.DobitakGubitakStrategije.Min());
            }
            int maximumMinimuma = minimumiReda.Max();

            //max min stupca
            foreach (var strategija in uneseniPodaci.igracB)
            {
                maximumiStupca.Add(strategija.DobitakGubitakStrategije.Max());
            }
            int minimumMaximuma = maximumiStupca.Min();

            if (maximumMinimuma == minimumMaximuma)
                postojiSedlo = true;

            return new Tuple<bool, int, int>(postojiSedlo, maximumMinimuma, minimumiReda.Min());
        }

        private void ukloniDominantneIgracaA()
        {
            //za igrača A – red sa svim negativnim brojevima
            int brojacStrategijaA = 0;
            foreach (var strategija in uneseniPodaci.igracA.ToList())
            {
                bool sviNegativni = strategija.DobitakGubitakStrategije.All(x => x < 0);

                if (sviNegativni)
                {
                    uneseniPodaci.igracA.Remove(strategija);

                    varijableAInverted.Add(varijableA[brojacStrategijaA]);
                    varijableA.RemoveAt(brojacStrategijaA);

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
        }

        private void ukloniDominantneIgracaB()
        {
            //za igrača B – stupac sa svim pozitivnim brojevima
            int brojacStrategijaB = 0;
            foreach (var strategija in uneseniPodaci.igracB.ToList()) //treba ukloniti i kod A te brojke
            {
                bool sviPozitivni = strategija.DobitakGubitakStrategije.All(x => x >= 0);

                if (sviPozitivni)
                {
                    uneseniPodaci.igracB.Remove(strategija);

                    varijableBInverted.Add(varijableB[brojacStrategijaB]);
                    varijableB.RemoveAt(brojacStrategijaB);

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
        }

        private void ukloniDuplikateA()
        {          
            foreach (var strategijaPrva in uneseniPodaci.igracA.ToList())
            {
                int brojacStrategijaA = 0;
                int internHelp = 0; // za pomoc kod uspoređivanja da se izbjegne uspoređivanje istih
                foreach (var strategijaDruga in uneseniPodaci.igracA.ToList())
                {
                    if (strategijaDruga.DobitakGubitakStrategije.SequenceEqual(strategijaPrva.DobitakGubitakStrategije))
                    {
                        internHelp++;

                        if(internHelp >= 2)
                        {
                            uneseniPodaci.igracA.Remove(strategijaDruga);

                            varijableAInverted.Add(varijableA[brojacStrategijaA]);
                            varijableA.RemoveAt(brojacStrategijaA);

                            foreach (var strategijaB in uneseniPodaci.igracB.ToList())
                            {
                                List<int> pomoc = strategijaB.DobitakGubitakStrategije.ToList();
                                pomoc.RemoveAt(brojacStrategijaA); //brise one brojeve koji su gore uklonjeni
                                strategijaB.DobitakGubitakStrategije = pomoc.ToArray();
                            }
                            brojacStrategijaA--;
                        }
                    }
                    brojacStrategijaA++;
                }
            }
        }

        private void ukloniDuplikateB()
        {
            foreach (var strategijaPrva in uneseniPodaci.igracB.ToList())
            {
                int brojacStrategijaB = 0;
                int internHelp = 0; // za pomoc kod uspoređivanja da se izbjegne uspoređivanje istih
                foreach (var strategijaDruga in uneseniPodaci.igracB.ToList())
                {
                    if (strategijaDruga.DobitakGubitakStrategije.SequenceEqual(strategijaPrva.DobitakGubitakStrategije))
                    {
                        internHelp++;

                        if (internHelp >= 2)
                        {
                            uneseniPodaci.igracB.Remove(strategijaDruga);

                            varijableBInverted.Add(varijableB[brojacStrategijaB]);
                            varijableB.RemoveAt(brojacStrategijaB);

                            foreach (var strategijaB in uneseniPodaci.igracA.ToList())
                            {
                                List<int> pomoc = strategijaB.DobitakGubitakStrategije.ToList();
                                pomoc.RemoveAt(brojacStrategijaB); //brise one brojeve koji su gore uklonjeni
                                strategijaB.DobitakGubitakStrategije = pomoc.ToArray();
                            }
                            brojacStrategijaB--;
                        }
                    }
                    brojacStrategijaB++;
                }
            }
        }

        public void ukloniDominantneStrategije()
        {
            for(int i=0; i<uneseniPodaci.igracA.Count+uneseniPodaci.igracB.Count; i++)
            {
                ukloniDuplikateA();
                ukloniDominantneIgracaA();
                ukloniDuplikateB();
                ukloniDominantneIgracaB();
            }
        }

        public void ukloniDuplikatneStrategije()
        {
            for (int i = 0; i < uneseniPodaci.igracA.Count + uneseniPodaci.igracB.Count; i++)
            {
                ukloniDuplikateA();
                ukloniDuplikateB();
            }
        }
    }
}
