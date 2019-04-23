using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OI2GameTheory
{
    public class IzgradnjaModelaA
    {
        private SpremanjeUnosa upisaniPodaci;
        private int diferencija;
        private string zapisModela = "";
        private List<string> sveVarijableXA = new List<string>();//x s crtom x̄
        private List<string> sveVarijableX = new List<string>();//obicni x
        private List<string> sveVarijableXCrtano = new List<string>();//x'
        private List<string> sveVarijableU = new List<string>();//u - dopunske varijable - ovise o broju jednadzbi B
        private List<string> sveVarijableW = new List<string>();//w - artificijalne - ima ih isto kolko i u

        public IzgradnjaModelaA(SpremanjeUnosa podaci, int minDif)
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
            foreach (var strategija in upisaniPodaci.igracA)
            {
                sveVarijableX.Add("x" + (brojacVarijabliB + 1));
                sveVarijableXCrtano.Add("x'" + (brojacVarijabliB + 1));
                sveVarijableXA.Add("x̄" + (brojacVarijabliB + 1));
                brojacVarijabliB++;
            }

            int brojacVarijabliA = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                sveVarijableU.Add("u" + (brojacVarijabliA + 1));
                sveVarijableW.Add("w" + (brojacVarijabliA + 1));
                brojacVarijabliA++;
            }


            zapisModela += "Igrač A" + Environment.NewLine + "ORIGINALNI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for(int i=1; i<= sveVarijableX.Count; i++)
            {
                if(i != sveVarijableX.Count)
                    zapisModela += sveVarijableX[i-1] + " + ";
                else
                    zapisModela += sveVarijableX[i-1] + " -> min -> 1"+Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                for(int i=0; i<strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableX[i] + " ≥ V" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableX[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableX[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "x(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }

        private void stvoriDiferenciraniOblik()
        {
            diferencirajPodatke();
            zapisModela += "Diferencija = " + diferencija + Environment.NewLine;
            zapisModela += "DIFERENCIRANI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableXCrtano.Count; i++)
            {
                if (i != sveVarijableXCrtano.Count)
                    zapisModela += sveVarijableXCrtano[i - 1] + " + ";
                else
                    zapisModela += sveVarijableXCrtano[i - 1] + " -> min -> 1" + Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXCrtano[i] + " ≥ V'" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXCrtano[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXCrtano[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "x'(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }
        private void diferencirajPodatke()
        {
            foreach (var strategija in upisaniPodaci.igracB.ToList())//ne mora se i kroz strategije igraca A ići
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    strategija.DobitakGubitakStrategije[i] += diferencija;
                }
            }
        }

        private void supstituiraj()
        {
            zapisModela += "SUPSTITUCIJA: " + Environment.NewLine + "x̄(i) = x'(i)/V'" + Environment.NewLine + Environment.NewLine;

            zapisModela += "SUPSTITUIRANI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableXA.Count; i++)
            {
                if (i != sveVarijableXA.Count)
                    zapisModela += sveVarijableXA[i - 1] + " + ";
                else
                    zapisModela += sveVarijableXA[i - 1] + " -> min -> 1/V'" + Environment.NewLine;
            }

            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " ≥ 1" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " + ";
                        }
                    }
                }
            }
            zapisModela += "x̄(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }

        private void stvoriKanonskiOblik()
        {
            zapisModela += "KANONSKI OBLIK PROBLEMA: " + Environment.NewLine + "Z = ";

            for (int i = 1; i <= sveVarijableXA.Count; i++)
            {
                if (i != sveVarijableXA.Count)
                    zapisModela += sveVarijableXA[i - 1] + " + ";
                else
                {
                    zapisModela += sveVarijableXA[i - 1] + " - 0 * (";
                }
            }
            
            for(int i= 1; i<=sveVarijableU.Count; i++)
            {
                if(i != sveVarijableU.Count)
                    zapisModela += sveVarijableU[i-1] + " + ";
                else
                    zapisModela += sveVarijableU[i-1] + ") + M * (";
            } 
            
            //w varijable
            for (int i = 1; i <= sveVarijableW.Count; i++)
            {
                if (i != sveVarijableW.Count)
                    zapisModela += sveVarijableW[i - 1] + " + ";
                else
                    zapisModela += sveVarijableW[i - 1] + ") -> min ->  1/V'" + Environment.NewLine;
            }

            int brojReda = 0;
            double sljedeciBroj = 0;
            foreach (var strategija in upisaniPodaci.igracB)
            {
                for (int i = 0; i < strategija.DobitakGubitakStrategije.Length; i++)
                {
                    if ((i + 1) == strategija.DobitakGubitakStrategije.Length)
                    {
                        zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " - "+sveVarijableU[brojReda]+ " + " +sveVarijableW[brojReda]+" = 1" + Environment.NewLine;
                    }
                    else
                    {
                        sljedeciBroj = strategija.DobitakGubitakStrategije[i + 1];
                        if (sljedeciBroj < 0)
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " ";
                        }
                        else
                        {
                            zapisModela += strategija.DobitakGubitakStrategije[i] + "" + sveVarijableXA[i] + " + ";
                        }
                    }
                }
                brojReda++;
            }
            zapisModela += "x̄(i), u(i), w(i) ≥ 0";
            zapisModela += Environment.NewLine + Environment.NewLine;
        }


        public string DohvatiZapisModela()
        {
            return zapisModela;
        }
    }
}
