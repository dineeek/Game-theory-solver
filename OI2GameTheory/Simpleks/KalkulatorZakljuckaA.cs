using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OI2GameTheory
{
    public class KalkulatorZakljuckaA
    {
        private DataTable zadnjaTablica;
        private SpremanjeUnosa podaciStrategija;
        private string zakljucak = "";
        private double diferencija;
        private string postupakZakljucka = "";

        public KalkulatorZakljuckaA(DataTable tablica, SpremanjeUnosa podaci, int dif)
        {
            zadnjaTablica = tablica;
            podaciStrategija = podaci;
            diferencija = Convert.ToDouble(dif);

            IzracunajZakljucak();
        }

        double V = 0; //vrijednost igre
        double VCrtano = 0;
        double[] igracAPostoci;
        double[] igracBPostoci;

        private void IzracunajZakljucak()
        {
            VCrtano = (double) 1 / Convert.ToDouble(zadnjaTablica.Rows[zadnjaTablica.Rows.Count - 3][2]);
            V = Math.Round((double) VCrtano - diferencija,2); //V' - D

            //ispis postupka
            double broj = Convert.ToDouble(zadnjaTablica.Rows[zadnjaTablica.Rows.Count - 3][2]);
            string brojRazlomak = "";
            if ((broj % 1) != 0)
                brojRazlomak = RealToFraction(broj, 0.0001).N + "/" + RealToFraction(broj, 0.0001).D;
            else
                brojRazlomak = broj.ToString();

            string VCrtanoRazlomak = "";
            if ((VCrtano % 1) != 0)
                VCrtanoRazlomak = RealToFraction(VCrtano, 0.0001).N + "/" + RealToFraction(VCrtano, 0.0001).D;
            else
                VCrtanoRazlomak = VCrtano.ToString();

            postupakZakljucka += "V' = 1 / (" + brojRazlomak +")" + " = " +VCrtanoRazlomak+ Environment.NewLine;
            postupakZakljucka += "Vrijednost igre V: " + VCrtanoRazlomak + " - " + diferencija.ToString() +" = "+ V.ToString() + Environment.NewLine + "Vjerojatnosti igranja strategija pojedinog igrača: " + Environment.NewLine + "Igrač B" + Environment.NewLine;



            //postoci strategija igraca
            igracAPostoci = new double[podaciStrategija.igracA.Count];
            igracBPostoci = new double[podaciStrategija.igracB.Count];

            int brojacB = 0;
            for(int i=2+podaciStrategija.igracA.Count+podaciStrategija.igracB.Count+1; i<zadnjaTablica.Columns.Count - 2; i++)//gledaju se w varijable i<zadnjaTablica.Columns.Count-(podaciStrategija.igracB.Count + 2)
            {
                igracBPostoci[brojacB] = Convert.ToDouble(zadnjaTablica.Rows[zadnjaTablica.Rows.Count - 3][i]) * VCrtano;
                brojacB++;

                //ispis postupka
                broj = Math.Abs(Convert.ToDouble(zadnjaTablica.Rows[zadnjaTablica.Rows.Count - 3][i]));
                if ((broj % 1) != 0)
                    brojRazlomak = RealToFraction(broj, 0.0001).N + "/" + RealToFraction(broj, 0.0001).D;
                else
                    brojRazlomak = broj.ToString();

                double rezultat = Math.Abs(broj * VCrtano);
                string rezultatRazlomak = "";
                if ((rezultat % 1) != 0)
                    rezultatRazlomak = RealToFraction(rezultat, 0.0001).N + "/" + RealToFraction(rezultat, 0.0001).D;
                else
                    rezultatRazlomak = rezultat.ToString();

                postupakZakljucka += "Y" + brojacB + " = "+brojRazlomak +" * "+VCrtanoRazlomak+" = " + rezultatRazlomak+ Environment.NewLine;
            }

            List<string> sveVarijableA = new List<string>();
            int brojacVarijabliA = 0;
            foreach (var strategija in podaciStrategija.igracA)
            {
                sveVarijableA.Add("x̄" + (brojacVarijabliA + 1));
                brojacVarijabliA++;
            }

            List<string> varijableAUTablici = new List<string>();
            for (int i = 0; i <= zadnjaTablica.Rows.Count - 4; i++)
                varijableAUTablici.Add(zadnjaTablica.Rows[i][1].ToString());

            postupakZakljucka += "Igrač A " + Environment.NewLine;

            //provjera podudaranja
            int brojacA = 0;
            foreach(var varijabla in sveVarijableA)
            {
                for (int i=0; i< varijableAUTablici.Count; i++)
                {
                    if (varijabla == (zadnjaTablica.Rows[i][1].ToString()))
                    {
                        igracAPostoci[brojacA] = (double) Convert.ToDouble(zadnjaTablica.Rows[i][2]) * VCrtano;
                        broj = Convert.ToDouble(zadnjaTablica.Rows[i][2]);
                        break;
                    }
                    else
                    {
                        igracAPostoci[brojacA] = 0;
                        broj = 0;
                    }
                }
                brojacA++;

                //ispis postupka
                if ((broj % 1) != 0)
                    brojRazlomak = RealToFraction(broj, 0.0001).N + "/" + RealToFraction(broj, 0.0001).D;
                else
                    brojRazlomak = broj.ToString();

                double rezultat = broj * VCrtano;
                string rezultatRazlomak = "";
                if ((rezultat % 1) != 0)
                    rezultatRazlomak = RealToFraction(rezultat, 0.0001).N + "/" + RealToFraction(rezultat, 0.0001).D;
                else
                    rezultatRazlomak = rezultat.ToString();

                postupakZakljucka += "X" + brojacA + " = " + brojRazlomak + " * " + VCrtanoRazlomak + " = " + rezultatRazlomak + Environment.NewLine;
            }
     }

        public string DohvatiZakljucak()
        {
            zakljucak = "Vrijednost zadane igre: " + V + Environment.NewLine + "Vjerojatnosti igranja strategija pojedinog igrača: "+Environment.NewLine;
            zakljucak += "Igrač A: ";
            int brojacA = 1;
            foreach (var vjerojatnost in igracAPostoci)
            {
                zakljucak += "X" + brojacA + " = " + Math.Round((vjerojatnost * 100), 2, MidpointRounding.AwayFromZero) + "%   ";
                brojacA++;
            }

            zakljucak += Environment.NewLine;
            zakljucak += "Igrač B: ";

            int brojacB = 1;
            foreach (var vjerojatnost in igracBPostoci)
            {
                zakljucak += "Y" + brojacB + " = " + Math.Round((vjerojatnost * 100), 2, MidpointRounding.AwayFromZero) + "%   ";
                brojacB++;
            }

            return zakljucak;
        }

        public string DohvatiPostupakZakljucka()
        {
            return postupakZakljucka;
        }

        public struct Fraction
        {
            public Fraction(int n, int d)
            {
                N = n;
                D = d;
            }

            public int N { get; private set; }
            public int D { get; private set; }
        }

        public Fraction RealToFraction(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            double maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    Seek(ref upper_n, ref upper_d, lower_n, lower_d, (un, ud) => (lower_d + ud) * (value + maxError) < (lower_n + un));
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    Seek(ref lower_n, ref lower_d, upper_n, upper_d, (ln, ld) => (ln + upper_n) < (value - maxError) * (ld + upper_d));
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }

        private void Seek(ref int a, ref int b, int ainc, int binc, Func<int, int, bool> f)
        {
            a += ainc;
            b += binc;

            if (f(a, b))
            {
                int weight = 1;

                do
                {
                    weight *= 2;
                    a += ainc * weight;
                    b += binc * weight;
                }
                while (f(a, b));

                do
                {
                    weight /= 2;

                    int adec = ainc * weight;
                    int bdec = binc * weight;

                    if (!f(a - adec, b - bdec))
                    {
                        a -= adec;
                        b -= bdec;
                    }
                }
                while (weight > 1);
            }
        }

    }
}
