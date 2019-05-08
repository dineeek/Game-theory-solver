using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class SedloDominacija
    {
        public SpremanjeUnosa uneseniPodaci;
        public List<string> varijableA = new List<string>();
        public List<string> varijableB = new List<string>();
        public Dictionary<string, int> varijableAInvertedDominantne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableAInvertedDuplikatne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableBInvertedDominantne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableBInvertedDuplikatne = new Dictionary<string, int>();

        int kronoloskiBrojUklanjanja = 1;

        public SedloDominacija(SpremanjeUnosa podaci)
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

        public List<int> minimumiReda = new List<int>();
        public List<int> maximumiStupca = new List<int>();

        public Tuple<bool, int, int> ProvjeriSedlo()
        {
            bool postojiSedlo = false;

            //List<int> minimumiReda = new List<int>();
            //List<int> maximumiStupca = new List<int>();
            
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
                bool sviNegativni = strategija.DobitakGubitakStrategije.All(x => x <= 0);

                if (sviNegativni)
                {
                    uneseniPodaci.igracA.Remove(strategija);

                    varijableAInvertedDominantne.Add(varijableA[brojacStrategijaA], kronoloskiBrojUklanjanja);
                    kronoloskiBrojUklanjanja++;

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

                    varijableBInvertedDominantne.Add(varijableB[brojacStrategijaB], kronoloskiBrojUklanjanja);
                    kronoloskiBrojUklanjanja++;

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

                            varijableAInvertedDuplikatne.Add(varijableA[brojacStrategijaA], kronoloskiBrojUklanjanja);
                            kronoloskiBrojUklanjanja++;
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

                            varijableBInvertedDuplikatne.Add(varijableB[brojacStrategijaB], kronoloskiBrojUklanjanja);
                            kronoloskiBrojUklanjanja++;
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

        public string IspisUklonjenihStrategijaIgracaA()
        {
            string uklonjeneStrategijeA1 = "";
            string ispisADominantne = "";
            string ispisADuplikatne = "";
            foreach (var str in varijableAInvertedDominantne)
                uklonjeneStrategijeA1 += str.Value+".) "+ str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA1))
            {
                ispisADominantne += "Igrač A: " + uklonjeneStrategijeA1 + Environment.NewLine;
            }

            string uklonjeneStrategijeA2 = "";
            foreach (var str in varijableAInvertedDuplikatne)
                uklonjeneStrategijeA2 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA2))
            {
                ispisADuplikatne += "Igrač A: " + uklonjeneStrategijeA2 + Environment.NewLine;
            }

            string uklonjeneStrategijeB1 = "";
            foreach (var str in varijableBInvertedDominantne)
                uklonjeneStrategijeB1 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB1))
            {
                ispisADominantne += "Igrač B: " + uklonjeneStrategijeB1;
            }

            string uklonjeneStrategijeB2 = "";
            foreach (var str in varijableBInvertedDuplikatne)
                uklonjeneStrategijeB2 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB2))
            {
                ispisADuplikatne += "Igrač B: " + uklonjeneStrategijeB2;
            }

            if ((!String.IsNullOrEmpty(uklonjeneStrategijeA1) || !String.IsNullOrEmpty(uklonjeneStrategijeB1)) && (String.IsNullOrEmpty(uklonjeneStrategijeA2) && String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene dominantne strategije:" + Environment.NewLine + ispisADominantne);

            else if ((String.IsNullOrEmpty(uklonjeneStrategijeA1) && String.IsNullOrEmpty(uklonjeneStrategijeB1)) && (!String.IsNullOrEmpty(uklonjeneStrategijeA2) || !String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene duplikatne strategije:" + Environment.NewLine + ispisADuplikatne);

            else if ((!String.IsNullOrEmpty(uklonjeneStrategijeA1) || !String.IsNullOrEmpty(uklonjeneStrategijeA2)) && (!String.IsNullOrEmpty(uklonjeneStrategijeB1) || !String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene dominantne strategije:" + Environment.NewLine + ispisADominantne + Environment.NewLine + Environment.NewLine + "Uklonjene duplikatne strategije:" + Environment.NewLine + ispisADuplikatne);
            else
                return "";
        }

        public string IspisUklonjenihStrategijaIgracaB()
        {
            string uklonjeneStrategijeA1 = "";
            string ispisBDominantne = "";
            string ispisBDuplikatne = "";

            foreach (var str in varijableAInvertedDominantne)
                uklonjeneStrategijeA1 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA1))
            {
                ispisBDominantne += "Igrač A: " + uklonjeneStrategijeA1 + Environment.NewLine;
            }

            string uklonjeneStrategijeA2 = "";
            foreach (var str in varijableAInvertedDuplikatne)
                uklonjeneStrategijeA2 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA2))
            {
                ispisBDuplikatne += "Igrač A: " + uklonjeneStrategijeA2 + Environment.NewLine;
            }

            string uklonjeneStrategijeB1 = "";
            foreach (var str in varijableBInvertedDominantne)
                uklonjeneStrategijeB1 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB1))
            {
                ispisBDominantne += "Igrač B: " + uklonjeneStrategijeB1;
            }

            string uklonjeneStrategijeB2 = "";
            foreach (var str in varijableBInvertedDuplikatne)
                uklonjeneStrategijeB2 += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB2))
            {
                ispisBDuplikatne += "Igrač B: " + uklonjeneStrategijeB2;
            }

            if ((!String.IsNullOrEmpty(uklonjeneStrategijeA1) || !String.IsNullOrEmpty(uklonjeneStrategijeB1)) && (String.IsNullOrEmpty(uklonjeneStrategijeA2) && String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene dominantne strategije:" + Environment.NewLine + ispisBDominantne + Environment.NewLine);

            else if ((String.IsNullOrEmpty(uklonjeneStrategijeA1) && String.IsNullOrEmpty(uklonjeneStrategijeB1)) && (!String.IsNullOrEmpty(uklonjeneStrategijeA2) || !String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene duplikatne strategije:" + Environment.NewLine + ispisBDuplikatne + Environment.NewLine);

            else if ((!String.IsNullOrEmpty(uklonjeneStrategijeA1) || !String.IsNullOrEmpty(uklonjeneStrategijeA2)) && (!String.IsNullOrEmpty(uklonjeneStrategijeB1) || !String.IsNullOrEmpty(uklonjeneStrategijeB2)))
                return ("Uklonjene dominantne strategije:"  + Environment.NewLine + ispisBDominantne + Environment.NewLine + Environment.NewLine + "Uklonjene duplikatne strategije:" + Environment.NewLine + ispisBDuplikatne + Environment.NewLine);
            else
                return "";
        }

        public string IspisUklonjenihDuplikatnihA()
        {
            string uklonjeneStrategijeA = "";
            string ispisA = "";
            foreach (var str in varijableAInvertedDuplikatne)
                uklonjeneStrategijeA += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA))
            {
                ispisA += "Igrač A: " + uklonjeneStrategijeA + Environment.NewLine;
            }

            string uklonjeneStrategijeB = "";
            foreach (var str in varijableBInvertedDuplikatne)
                uklonjeneStrategijeB += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB))
            {
                ispisA += "Igrač B: " + uklonjeneStrategijeB;
            }

            if (!String.IsNullOrEmpty(uklonjeneStrategijeA) || !String.IsNullOrEmpty(uklonjeneStrategijeB))
                return ("Uklonjene duplikatne strategije:" + Environment.NewLine + ispisA);
            else
                return "";
        }

        public string IspisUklonjenihDuplikatnihB()
        {
            string uklonjeneStrategijeA = "";
            string ispisB = "";
            foreach (var str in varijableAInvertedDuplikatne)
                uklonjeneStrategijeA += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA))
            {
                ispisB += "Igrač A: " + uklonjeneStrategijeA + Environment.NewLine;
            }

            string uklonjeneStrategijeB = "";
            foreach (var str in varijableBInvertedDuplikatne)
                uklonjeneStrategijeB += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB))
            {
                ispisB += "Igrač B: " + uklonjeneStrategijeB;
            }

            if (!String.IsNullOrEmpty(uklonjeneStrategijeA) || !String.IsNullOrEmpty(uklonjeneStrategijeB))
                return ("Uklonjene duplikatne strategije:" + Environment.NewLine + ispisB);
            else
             return "";
        }
    }
}
