using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningTask
{
    class Iterations : Algorithm
    {
        public Iterations(Input input) : base(input)
        {
        }

        public override Output Solution()
        {
            int nochange = 0;
            int i = 0;
            Output best_output;
            Output temp_output;
            int[] best_fines = new int[input.GetOrders().Count];
            int[] temp_fines = new int[input.GetOrders().Count];
            int[] tend = new int[input.GetOrders().Count];
            Algorithm fa = new Frontal(input, 1);
            best_output = fa.Solution();
            for (int j = 0; j < input.GetOrders().Count; j++)
            {
                tend[j] = 0;
                for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                {
                    if (best_output.y[input.GetOrders()[j].GetPsi()[k]] > tend[j]) tend[j] = best_output.y[input.GetOrders()[j].GetPsi()[k]];
                }
                best_fines[j] = Math.Max(0, tend[j] - input.GetOrders()[j].GetD());
            }
            while (nochange < 5 && i < 50)
            {
                fa = new Frontal(input, 3);
                temp_output = fa.Solution();
                for (int j = 0; j < input.GetOrders().Count; j++)
                {
                    tend[j] = 0;
                    for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                    {
                        if (temp_output.y[input.GetOrders()[j].GetPsi()[k]] > tend[j]) tend[j] = temp_output.y[input.GetOrders()[j].GetPsi()[k]];
                    }
                    temp_fines[j] = Math.Max(0, tend[j] - input.GetOrders()[j].GetD());
                }
                for (int j = 0; j < input.GetOrders().Count; j++)
                {
                    for (int k = 0; k < input.GetOrders()[j].GetPsi().Count; k++)
                    {
                        input.GetOperations()[input.GetOrders()[j].GetPsi()[k]].p += temp_fines[j];                         
                    }
                }
                int best = 0;
                for (int j = 0; j < input.GetOrders().Count; j++) best += best_fines[j];
                int temp = 0;
                for (int j = 0; j < input.GetOrders().Count; j++) temp += temp_fines[j];
                if (best > temp) 
                {
                    nochange = 0;
                    best_output = temp_output;
                    for (int j = 0; j < input.GetOrders().Count; j++) best_fines[j] = temp_fines[j];
                }
                else nochange++;              
                i++;
            }
            output = best_output;
            return output;
        }
    }
}
