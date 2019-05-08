using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace OI2GameTheory
{
    public class CrtanjeMatrice
    {
        private int brojA, brojB;
        public CrtanjeMatrice(int a, int b)
        {
            brojA = a;
            brojB = b;          
        }

        public DataTable NacrtajMatricu()
        {
            DataTable strukturaMatrice = new DataTable();
            for (int i = 0; i <= brojB; i++) //stupci, 0 je zaglavlje
            {
                if(i == 0)
                {
                    strukturaMatrice.Columns.Add(@"A  \  B", typeof(string));
                }
                else
                    strukturaMatrice.Columns.Add("Y("+i+")", typeof(int));
            }

            for (int i = 1; i<=brojA; i++) //redci
            {
                var novaStrategija = strukturaMatrice.NewRow();
                novaStrategija[@"A  \  B"] = "X(" + i + ")";
                strukturaMatrice.Rows.Add(novaStrategija);
            }

            return strukturaMatrice;
        }
    }
}
