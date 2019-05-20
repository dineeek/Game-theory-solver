using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class ProtuprirodnaKontradiktornaIgra
    {
        public SpremanjeUnosa uneseniPodaci;

        public List<string> varijableA = new List<string>();
        public List<string> varijableB = new List<string>();

        public Dictionary<string, int> varijableAInvertedDominantne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableBInvertedDominantne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableAInvertedDuplikatne = new Dictionary<string, int>();
        public Dictionary<string, int> varijableBInvertedDuplikatne = new Dictionary<string, int>();
        int kronoloskiBrojUklanjanja = 1;

        public ProtuprirodnaKontradiktornaIgra(SpremanjeUnosa podaci)
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

        bool protuprirodnost = false;
        public int ProvjeriProtuprirodnost()
        {
            int vrstaIgre = 0;//0-mješana, 1-protuprirodna, 2-kontradiktorna

            int brojStrategijaA = uneseniPodaci.igracA.Count;
            int brojStrategijaB = uneseniPodaci.igracB.Count;

            protuprirodnost = protuprirodnaIgra();
            if (!protuprirodnost)         
                UkloniStrategije();
            
            if(uneseniPodaci.igracA.Count <= 1 || uneseniPodaci.igracB.Count <= 1)//kontradiktorna igra
            {
                vrstaIgre = 2;
                return vrstaIgre;
            }

            else if (protuprirodnost)//protuprirodna igra
            {
                vrstaIgre = 1;
                return vrstaIgre;
            }
            else
                return vrstaIgre;
        }

        private int protuprirodaZaIgraca = 0; //1-A, 2-B

        private bool protuprirodnaIgra()
        {
            int brojNegativnihA = 0;
            //igracA
            foreach(var strategija in uneseniPodaci.igracA)
            {
                bool sviNegativniA = strategija.DobitakGubitakStrategije.All(x => x <= 0);

                if (sviNegativniA)
                    brojNegativnihA++;
            }

            if (brojNegativnihA == uneseniPodaci.igracA.Count)
            {
                protuprirodnost = true;
                protuprirodaZaIgraca = 1;
            }


            else
            {
                int brojPozitivnihB = 0;
                //igracB
                foreach (var strategija in uneseniPodaci.igracB)
                {
                    bool sviPozitivniB = strategija.DobitakGubitakStrategije.All(x => x >= 0);
                    
                    if (sviPozitivniB)
                        brojPozitivnihB++;
                }

                if (brojPozitivnihB == uneseniPodaci.igracB.Count)
                {
                    protuprirodnost = true;
                    protuprirodaZaIgraca = 2;
                }
            }

            return protuprirodnost;
        }

        public int ProtupIgraca()
        {
            return protuprirodaZaIgraca;
        }

        //PROVJERA BRISANJA
        private void UkloniA()
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

        private void UkloniB()
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

                        if (internHelp >= 2)
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

        private void UkloniStrategije()
        {
            for (int i = 0; i < uneseniPodaci.igracA.Count + uneseniPodaci.igracB.Count; i++)
            {

                UkloniA();
                ukloniDuplikateA();
                protuprirodnaIgra();
                if (protuprirodnost || uneseniPodaci.igracA.Count < 2)
                    break;

                UkloniB();
                ukloniDuplikateB();
                protuprirodnaIgra();
                if (protuprirodnost || uneseniPodaci.igracB.Count < 2)
                    break;
            }
        }

        public string IspisUklonjenihStrategijaIgraca()
        {
            string uklonjeneStrategijeA = "";
            string ispisA = "";
            foreach (var str in varijableAInvertedDominantne)
                uklonjeneStrategijeA += str.Value + ".) " + str.Key + " ";
            foreach (var str in varijableAInvertedDuplikatne)
                uklonjeneStrategijeA += str.Value + ".) " + str.Key + " ";

            if (!String.IsNullOrEmpty(uklonjeneStrategijeA))
            {
                ispisA += "Igrač A: " + uklonjeneStrategijeA + Environment.NewLine;
            }

            string uklonjeneStrategijeB = "";
            string ispisB = "";
            foreach (var str in varijableBInvertedDominantne)
                uklonjeneStrategijeB += str.Value + ".) " + str.Key + " ";
            foreach (var str in varijableBInvertedDuplikatne)
                uklonjeneStrategijeB += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB))
            {
                ispisB += "Igrač B: " + uklonjeneStrategijeB;
            }

            string uklonjeneStrategije = ispisA + ispisB;

            /*
            string uklonjeneStrategijeA = "";
            string ispisADominantne = "";
            foreach (var str in varijableAInvertedDominantne)
                uklonjeneStrategijeA += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeA))
            {
                ispisADominantne += "Igrač A: " + uklonjeneStrategijeA + Environment.NewLine;
            }

            string uklonjeneStrategijeB = "";
            foreach (var str in varijableBInvertedDominantne)
                uklonjeneStrategijeB += str.Value + ".) " + str.Key + " ";
            if (!String.IsNullOrEmpty(uklonjeneStrategijeB))
            {
                ispisADominantne += "Igrač B: " + uklonjeneStrategijeB;
            }
            */
            if (!String.IsNullOrEmpty(uklonjeneStrategije))
                return (Environment.NewLine + uklonjeneStrategije + Environment.NewLine);

            else
                return "";
            
        }
    }
}
