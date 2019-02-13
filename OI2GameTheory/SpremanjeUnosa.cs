using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace OI2GameTheory
{
    public class SpremanjeUnosa
    {
        private DataGridView matricaUnosa;

        public List<Strategija> igracA = new List<Strategija>();
        public List<Strategija> igracB = new List<Strategija>();

        public SpremanjeUnosa(DataGridView matrica)
        {
            matricaUnosa = matrica;

            odrediDobitkeGubitkeIgraca();
        }

        private void odrediDobitkeGubitkeIgraca()//A i B su zamjenjeni
        {
            //igrac A
            for (int i = 0; i<matricaUnosa.Rows.Count; i++)// 1. je zaglavlje 
            {
                int[] nizDobitakaGubitaka = new int[matricaUnosa.Columns.Count - 1];

                for (int j = 1; j < matricaUnosa.Columns.Count; j++) 
                {
                    nizDobitakaGubitaka[j-1] = Convert.ToInt32(matricaUnosa.Rows[i].Cells[j].Value);
                }

                igracA.Add(new Strategija(nizDobitakaGubitaka));
            }

            //igrac B
            for (int i = 1; i < matricaUnosa.Columns.Count; i++)// 1. je zaglavlje 
            {
                int[] nizDobitakaGubitaka = new int[matricaUnosa.Rows.Count];

                for (int j = 0; j < matricaUnosa.Rows.Count; j++)
                {
                    nizDobitakaGubitaka[j] = Convert.ToInt32(matricaUnosa.Rows[j].Cells[i].Value);
                }

                igracB.Add(new Strategija(nizDobitakaGubitaka));
            }
        }
    }
}
