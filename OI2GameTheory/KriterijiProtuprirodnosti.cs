using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class KriterijiProtuprirodnosti
    {
        private SpremanjeUnosa uneseniPodaci;
        private int vrstaIgraca;

        public List<string> varijableA = new List<string>();
        public List<string> varijableB = new List<string>();

        private List<double> vrijednostiLaplace = new List<double>();
        private List<double> vrijednostiSavage = new List<double>();
        private List<double> vrijednostiHurwicz = new List<double>();

        public KriterijiProtuprirodnosti(SpremanjeUnosa podaci, int igrac)
        {
            uneseniPodaci = podaci;
            vrstaIgraca = igrac;

            if(vrstaIgraca == 2)//moraju se sve pretvoriti u pozitivne vrijednosti kada je igrac A priroda, a B bas igrac
            {
                foreach(var strategija in uneseniPodaci.igracB)
                {
                    for(int i=0; i<strategija.DobitakGubitakStrategije.Count(); i++)
                    {
                        strategija.DobitakGubitakStrategije[i] = Math.Abs(strategija.DobitakGubitakStrategije[i]);
                    }
                }
            }

            for (int i = 0; i < uneseniPodaci.igracA.Count; i++)
            {
                varijableA.Add("X" + (i + 1) + "");
            }

            for (int i = 0; i < uneseniPodaci.igracB.Count; i++)
            {
                varijableB.Add("Y" + (i + 1) + "");
            }

            vrijednostiLaplace = Laplace();
            vrijednostiSavage = Savage();
            vrijednostiHurwicz = Hurwicz();
        }

        //moraju biti razdvojeni igrači jer je jedan igrač, a drugi priroda

        private List<double> Laplace()
        {
            List<double> rezultat = new List<double>();

            if (vrstaIgraca == 1)//igracA
            {
                double vjerojatnostPojaveA = ((double)1 /uneseniPodaci.igracA.Count()); // jednaka je za sve alternative - redove (strategije)
                foreach (var strategija in uneseniPodaci.igracA)
                {
                    double internHelp = 0;
                    foreach(var vrijednost in strategija.DobitakGubitakStrategije)
                    {
                        internHelp += (double) vjerojatnostPojaveA * vrijednost;
                    }

                    rezultat.Add(internHelp);
                }
            }
            else//igracB 
            {
                double vjerojatnostPojaveB = ((double)1 / uneseniPodaci.igracB.Count()); // jednaka je za sve alternative - stupce (strategije)

                foreach (var strategija in uneseniPodaci.igracB)
                {
                    double internHelp = 0;
                    foreach (var vrijednost in strategija.DobitakGubitakStrategije)
                    {
                        internHelp += (double) vjerojatnostPojaveB * vrijednost;
                    }
                    rezultat.Add(internHelp);
                }
            }

            return rezultat;
        }

        private List<double> Savage()
        {
            List<double> rezultat = new List<double>();
            List<Strategija> tablicaZaljenjaB = new List<Strategija>();
            List<Strategija> tablicaZaljenjaA = new List<Strategija>();

            int maxHelp = 0;

            if (vrstaIgraca == 1)//igracA - krtiterij minimalnog žaljenja - igrac A gleda najveću vrijednost u stupvu (u strategijama igraca B)
            {
                foreach (var strategija in uneseniPodaci.igracB)
                {
                    int[] pomocnaListaB = new int[uneseniPodaci.igracA.Count()];
                    maxHelp = strategija.DobitakGubitakStrategije.Max();

                    for(int i=0; i<strategija.DobitakGubitakStrategije.Count(); i++)
                    {
                        pomocnaListaB[i] = maxHelp - strategija.DobitakGubitakStrategije[i];
                    }

                    tablicaZaljenjaB.Add(new Strategija(pomocnaListaB));
                }

                for(int i=0; i < tablicaZaljenjaB.Count(); i++)
                {
                    int[] pomocnaListaA = new int[uneseniPodaci.igracB.Count()];

                    int brojacIntern = 0;
                    foreach (var strategija in tablicaZaljenjaB)
                    {
                        pomocnaListaA[brojacIntern] = strategija.DobitakGubitakStrategije[i];
                        brojacIntern++;
                        continue;
                    }

                    tablicaZaljenjaA.Add(new Strategija(pomocnaListaA));
                }

                //po recima uzeti max
                foreach (var strategija in tablicaZaljenjaA)
                {
                    rezultat.Add(strategija.DobitakGubitakStrategije.Max());
                }
            }

            else//igracB
            {
                foreach (var strategija in uneseniPodaci.igracA)
                {
                    int[] pomocnaListaA = new int[uneseniPodaci.igracB.Count()];
                    maxHelp = strategija.DobitakGubitakStrategije.Max();

                    for (int i = 0; i < strategija.DobitakGubitakStrategije.Count(); i++)
                    {
                        pomocnaListaA[i] = maxHelp - strategija.DobitakGubitakStrategije[i];
                    }

                    tablicaZaljenjaA.Add(new Strategija(pomocnaListaA));
                }

                for (int i = 0; i < tablicaZaljenjaA.Count(); i++)
                {
                    int[] pomocnaListaB = new int[uneseniPodaci.igracA.Count()];

                    int brojacIntern = 0;
                    foreach (var strategija in tablicaZaljenjaA)
                    {
                        pomocnaListaB[brojacIntern] = strategija.DobitakGubitakStrategije[i];
                        brojacIntern++;
                        continue;
                    }

                    tablicaZaljenjaB.Add(new Strategija(pomocnaListaB));
                }

                //po recima uzeti max
                foreach (var strategija in tablicaZaljenjaB)
                {
                    rezultat.Add(strategija.DobitakGubitakStrategije.Max());
                }
            }

            return rezultat;
        }
        private List<double> Hurwicz() //Alfa = 0.5
        {
            List<double> rezultat = new List<double>();
            double alfa = 0.5;

            if (vrstaIgraca == 1)//igracA
            {
                int maxVrijednost, minVrijednost = 0;
                foreach(var strategija in uneseniPodaci.igracA)
                {
                    maxVrijednost = strategija.DobitakGubitakStrategije.Max();
                    minVrijednost = strategija.DobitakGubitakStrategije.Min();

                    rezultat.Add((double)(alfa * maxVrijednost) + ((double)1 - alfa) * minVrijednost);
                }
            }
            else//igracB
            {
                int maxVrijednost, minVrijednost = 0;
                foreach (var strategija in uneseniPodaci.igracB)
                {
                    maxVrijednost = strategija.DobitakGubitakStrategije.Max();
                    minVrijednost = strategija.DobitakGubitakStrategije.Min();

                    rezultat.Add((double)(alfa * maxVrijednost) + ((double)1 - alfa) * minVrijednost);
                }
            }

            return rezultat;
        }

        public string IspisiVrijednostiKriterija()
        {
            string laplace = "";
            string savage = "";
            string hurwicz = "";

            string laplaceOdabir = "";
            string savageOdabir = "";
            string hurwiczOdabir = "";

            double maxVrijednost = 0;
            int maxIndex = 0;
            if (vrstaIgraca == 1)//igracA
            {
                for (int i = 0; i < vrijednostiLaplace.Count; i++)
                {
                    laplace += varijableA[i] + ": " + vrijednostiLaplace[i]+ "  ";
                }
                maxVrijednost = vrijednostiLaplace.Max();
                maxIndex = vrijednostiLaplace.IndexOf(maxVrijednost);
                laplaceOdabir = varijableA[maxIndex];

                for (int i = 0; i < vrijednostiSavage.Count; i++)
                {
                    savage += varijableA[i] + ": " + vrijednostiSavage[i] + "  ";
                }
                maxVrijednost = vrijednostiSavage.Min();
                maxIndex = vrijednostiSavage.IndexOf(maxVrijednost);
                savageOdabir = varijableA[maxIndex];

                for (int i = 0; i < vrijednostiHurwicz.Count; i++)
                {
                    hurwicz += varijableA[i] + ": " + vrijednostiHurwicz[i] + "  ";
                }
                maxVrijednost = vrijednostiLaplace.Max();
                maxIndex = vrijednostiLaplace.IndexOf(maxVrijednost);
                hurwiczOdabir = varijableA[maxIndex];
            }

            else//igracB
            {

                for (int i = 0; i < vrijednostiLaplace.Count; i++)
                {
                    laplace += varijableB[i] + ": " + vrijednostiLaplace[i] + "  ";
                }
                maxVrijednost = vrijednostiLaplace.Max();
                maxIndex = vrijednostiLaplace.IndexOf(maxVrijednost);
                laplaceOdabir = varijableB[maxIndex];

                for (int i = 0; i < vrijednostiSavage.Count; i++)
                {
                    savage += varijableB[i] + ": " + vrijednostiSavage[i] + "  ";
                }
                maxVrijednost = vrijednostiSavage.Min();
                maxIndex = vrijednostiSavage.IndexOf(maxVrijednost);
                savageOdabir = varijableB[maxIndex];

                for (int i = 0; i < vrijednostiHurwicz.Count; i++)
                {
                    hurwicz += varijableB[i] + ": " + vrijednostiHurwicz[i] + "  ";
                }
                maxVrijednost = vrijednostiLaplace.Max();
                maxIndex = vrijednostiLaplace.IndexOf(maxVrijednost);
                hurwiczOdabir = varijableB[maxIndex];

            }

            return "Optimalna strategija prema kriterijima: " + Environment.NewLine + "1. Laplaceov kriterij: "+laplace +" Optimalna -> " + laplaceOdabir + Environment.NewLine + "2. Savageov kriterij: " +savage + " Optimalna -> " + savageOdabir + Environment.NewLine + "3. Hurwiczow kriterij (alfa = 0.5): " + hurwicz+ " Optimalna -> " + hurwiczOdabir + Environment.NewLine;
        }
    }
}
