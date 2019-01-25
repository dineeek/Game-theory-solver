using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OI2GameTheory
{
    public class MatricnaIgra
    {
        private SpremanjeUnosa uneseniPodaci;

        public List<string> varijableA = new List<string>();
        public List<string> varijableB = new List<string>();
        public MatricnaIgra(SpremanjeUnosa podaci)
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

        public string IspisMatricneIgre()
        {
            string matricnaIgra= Environment.NewLine+ "__|";

            foreach (var varijabla in varijableB)
                matricnaIgra += varijabla + "_";

            int brojac = 0;
            foreach(var redBrojeva in uneseniPodaci.igracA)
            {
                matricnaIgra += Environment.NewLine+" " + varijableA[brojac] + "|";

                foreach (var broj in redBrojeva.DobitakGubitakStrategije)
                    matricnaIgra += " "+ broj +"  "; 

                brojac++;
            }
                

            return matricnaIgra;
        }
    }
}
