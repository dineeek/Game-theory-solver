using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OI2GameTheory
{
    public class IzgradnjaModelaB
    {
        private SpremanjeUnosa upisaniPodaci;
        private int diferencija;
        private string zapisModela = "";
        private List<string> sveVarijableYB = new List<string>();//Y s crtom
        private List<string> sveVarijableY = new List<string>();//obicni y
        private List<string> sveVarijableYCrtano = new List<string>();//y'
        private List<string> sveVarijableU = new List<string>();//u - dopunske varijable

        public IzgradnjaModelaB(SpremanjeUnosa podaci, int minDif)
        {
            upisaniPodaci = podaci;

            if (minDif < 0)
                diferencija = Math.Abs(minDif) + 1;
            else
                diferencija = 0;

            stvoriOriginalniOblik();
            stvoriDiferenciraniOblik();
            supstituiraj();
            stvoriKanonskiOblik();
        }

        private void stvoriOriginalniOblik()
        {
            //stvaranje Y varijabli
            int brojacVarijabliB = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                sveVarijableY.Add("y" + (brojacVarijabliB + 1));
                sveVarijableYCrtano.Add("y'" + (brojacVarijabliB + 1));
                sveVarijableYB.Add("Ῡ" + (brojacVarijabliB + 1));
                brojacVarijabliB++;
            }

            int brojacVarijabliA = 0;
            foreach (var strategija in upisaniPodaci.igracA)
            {
                sveVarijableU.Add("u" + (brojacVarijabliA + 1));
                brojacVarijabliA++;
            }


            zapisModela += "Igrač B"+Environment.NewLine+"ORIGINALNI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for(int i=1; i<= sveVarijableY.Count; i++)
            {
                if(i != sveVarijableY.Count)
                    zapisModela += sveVarijableY[i-1] + " + ";
                else
                    zapisModela += sveVarijableY[i-1] + " -> max -> 1"+Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracA)
            {
                for(int i=0; i<strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableY[i] + " ≤ V" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableY[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableY[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "y(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }

        private void stvoriDiferenciraniOblik()
        {
            diferencirajPodatke();
            zapisModela += "Diferencija = " + diferencija + Environment.NewLine;
            zapisModela += "DIFERENCIRANI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableYCrtano.Count; i++)
            {
                if (i != sveVarijableYCrtano.Count)
                    zapisModela += sveVarijableYCrtano[i - 1] + " + ";
                else
                    zapisModela += sveVarijableYCrtano[i - 1] + " -> max -> 1" + Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracA)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYCrtano[i] + " ≤ V'" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYCrtano[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYCrtano[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "y'(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }
        private void diferencirajPodatke()
        {
            foreach (var strategija in upisaniPodaci.igracA.ToList())//ne mora se i kroz strategije igraca B ići
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    strategija.DobitakGubitakStrategije[i] += diferencija;
                }
            }
        }

        private void supstituiraj()
        {
            zapisModela += "SUPSTITUCIJA: " + Environment.NewLine + "Ῡ = y'(i)/V'" + Environment.NewLine + Environment.NewLine;

            zapisModela += "SUPSTITUIRANI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableYB.Count; i++)
            {
                if (i != sveVarijableYB.Count)
                    zapisModela += sveVarijableYB[i - 1] + " + ";
                else
                    zapisModela += sveVarijableYB[i - 1] + " -> max -> 1" + Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracA)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " ≤ 1" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "Ῡ(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }

        private void stvoriKanonskiOblik()
        {
            zapisModela += "KANONSKI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableYB.Count; i++)
            {
                if (i != sveVarijableYB.Count)
                    zapisModela += sveVarijableYB[i - 1] + " + ";
                else
                {
                    zapisModela += sveVarijableYB[i - 1] + " + 0 * (";
                }
            }
            
            for(int i= 1; i<=sveVarijableU.Count; i++)
            {
                if(i != sveVarijableU.Count)
                    zapisModela += sveVarijableU[i-1] + " + ";
                else
                    zapisModela += sveVarijableU[i-1] + ") -> max ->  1/V'" + Environment.NewLine;
            }
            
            int brojReda = 0;
            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracA)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " + "+sveVarijableU[brojReda]+" = 1" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableYB[i] + " + ";
                        }
                    }
                }
                brojReda++;
            }
            zapisModela += "Ῡ(i), u(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }


        public string DohvatiZapisModela()
        {
            return zapisModela;
        }
    }
}
