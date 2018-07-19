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

        public Sedlo(SpremanjeUnosa podaci)
        {
            uneseniPodaci = podaci;
        }
        public (bool, int, int) ProvjeriSedlo()
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

            return (postojiSedlo, maximumMinimuma, minimumiReda.Min());
        }

        public void ukloniDominantneStrategije()
        {
            //za igrača A – red sa svim negativnim brojevima
            int brojacStrategijaA = 0;
            foreach (var strategija in uneseniPodaci.igracA.ToList())
            {
                bool sviNegativni = strategija.DobitakGubitakStrategije.All(x => x < 0);

                if (sviNegativni)
                {
                    uneseniPodaci.igracA.Remove(strategija);

                    //brisanje kod igracaB
                    foreach (var strategijaB in uneseniPodaci.igracB.ToList())
                    {
                        List<int> pomoc = strategijaB.DobitakGubitakStrategije.ToList();
                        pomoc.RemoveAt(brojacStrategijaA); //brise one brojeve koji su gore uklonjeni
                        strategijaB.DobitakGubitakStrategije = pomoc.ToArray();
                    }
                }

                brojacStrategijaA++;

            }

            //za igrača B – stupac sa svim pozitivnim brojevima
            int brojacStrategijaB = 0;
            foreach (var strategija in uneseniPodaci.igracB.ToList()) //treba ukloniti i kod A te brojke
            {
                bool sviPozitivni = strategija.DobitakGubitakStrategije.All(x => x >= 0);

                if (sviPozitivni)
                {
                    uneseniPodaci.igracB.Remove(strategija);

                    //brisanje kod igracaA
                    foreach(var strategijA in uneseniPodaci.igracA.ToList())
                    {
                        List<int> pomoc = strategijA.DobitakGubitakStrategije.ToList();
                        pomoc.RemoveAt(brojacStrategijaB);          
                        strategijA.DobitakGubitakStrategije = pomoc.ToArray();
                    }
                }

                brojacStrategijaB++;
            }
        }


    }
}
