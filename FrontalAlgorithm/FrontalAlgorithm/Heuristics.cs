using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningTask
{
    class Heuristics : Algorithm
    {
        public Heuristics(Input input) : base(input)
        {
        }

        public override Output Solution()
        {
            int[] finesi_1 = new int[input.GetOrders().Count];
            int[] finesi_2 = new int[input.GetOrders().Count];
            int[] tend = new int[input.GetOrders().Count];
            Output output_1;
            Output output_2;
            Algorithm fa = new Frontal(input, 1);
            output_1 = fa.Solution();
            for (int j = 0; j < input.GetOrders().Count; j++)
            {
                tend[j] = 0;
                for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                {
                    if (output_1.y[input.GetOrders()[j].GetPsi()[k]] > tend[j]) tend[j] = output_1.y[input.GetOrders()[j].GetPsi()[k]];
                }
                finesi_1[j] = Math.Max(0, tend[j] - input.GetOrders()[j].GetD());
            }
            for (int j = 0; j < input.GetOrders().Count; j++)
            {
                for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                {                   
                    input.GetOperations()[input.GetOrders()[j].GetPsi()[k]].p = finesi_1[j];                                    
                }
            }
            fa = new Frontal(input, 3);
            output_2 = fa.Solution();
            for (int j = 0; j < input.GetOrders().Count; j++)
            {
                tend[j] = 0;
                for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                {
                    if (output_2.y[input.GetOrders()[j].GetPsi()[k]] > tend[j]) tend[j] = output_2.y[input.GetOrders()[j].GetPsi()[k]];
                }
                finesi_2[j] = Math.Max(0, tend[j] - input.GetOrders()[j].GetD());
            }
            int fin1 = 0;
            for (int j = 0; j < input.GetOrders().Count; j++) fin1 += finesi_1[j];
            int fin2 = 0;
            for (int j = 0; j < input.GetOrders().Count; j++) fin2 += finesi_2[j];
            if (fin1 < fin2)
                output = output_1;
            else
                output = output_2;
            return output;
        }
    }
}
