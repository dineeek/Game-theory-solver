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
            foreach(var strategija in uneseniPodaci.igracA.ToList())
            {
                bool sviNegativni = strategija.DobitakGubitakStrategije.All(x => x < 0);

                if (sviNegativni)
                    uneseniPodaci.igracA.Remove(strategija);
            }

            //za igrača B – stupac sa svim pozitivnim brojevima
            foreach (var strategija in uneseniPodaci.igracB.ToList())
            {
                bool sviPozitivni = strategija.DobitakGubitakStrategije.All(x => x >= 0);

                if (sviPozitivni)
                    uneseniPodaci.igracB.Remove(strategija);
            }
        }


    }
}
