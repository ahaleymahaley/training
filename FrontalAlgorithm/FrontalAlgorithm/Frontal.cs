using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningTask
{
    class Frontal : Algorithm
    { 
        List<int>[] gamma;
        List<Front> fronts;
        List<Operation> operations;
        bool[] isAdded;
        int var;

        public Frontal(Input input, int var) : base(input)
        {
            this.var = var;
        }

        public override Output Solution()
        {
            operations = input.GetOperations();
            gamma = new List<int>[operations.Count];
            fronts = new List<Front>();
            isAdded = new bool[operations.Count];
            step1();          
            return output;
        }

        void step1()
        {            
            for (int i = 0; i < operations.Count; i++) gamma[i] = new List<int>(operations[i].GetGamma());            
            foreach (Order order in input.GetOrders())
            {
                int b = order.GetB();
                if (b < input.GetT0())
                {
                    if (fronts.Find(f => f.key.Equals(b)) == null) fronts.Add(new Front(b));
                    foreach (int index in order.GetPsi())
                    {
                        if (gamma[index].Count == 0)
                        {
                            fronts.Find(f => f.key.Equals(b)).Add(operations.Find(op => op.GetNumber().Equals(index)));
                            isAdded[index] = true;
                        }
                    }
                }
            }
            step2();
        }

        void step2()
        {
            while (fronts.Count != 0)
            {
                fronts.Sort(Front.CompareFronts);
                while (fronts[0].operations.Count == 0)
                {
                    fronts.RemoveAt(0);
                    if (fronts.Count == 0) return;
                }
                switch (var)
                {
                    case 1:
                        fronts[0].operations.Sort(Operation.SortInAscendingOrder);
                        break;
                    case 2:
                        fronts[0].operations.Sort(Operation.SortInDecreasingOrder);
                        break;
                    case 3:
                        fronts[0].operations.Sort(Operation.SortByPriority);
                        break;
                }
                Operation j = fronts[0].operations[0];
                int min = Int32.MaxValue;
                List<int> jphi = new List<int>(j.GetPhi());
                List<int> free = new List<int>(jphi);
                for (int i = 0; i < operations.Count; i++)
                {
                    foreach (int p in jphi)
                    {
                        if (p == output.z[i] && fronts[0].key >= output.x[i] && fronts[0].key < output.y[i])
                        {
                            free.Remove(p);
                            if (min > output.y[i]) min = output.y[i];
                        }
                    }
                }
                if (free.Count > 0) step3(j, free[0]);
                else
                {
                    if (min < input.GetT0())
                    {
                        if (fronts.Find(f => f.key.Equals(min)) == null) fronts.Add(new Front(min));
                        fronts.Find(f => f.key.Equals(min)).Add(j);
                        fronts[0].operations.RemoveAt(0);
                    }
                    else
                    {
                        fronts[0].operations.RemoveAt(0);
                        isAdded[j.GetNumber()] = true;
                    }
                }
            }
        }

        void step3(Operation j, int p)
        {
            int jnumber = j.GetNumber();
            output.x[jnumber] = fronts[0].key;
            output.y[jnumber] = fronts[0].key + j.GetT();
            output.z[jnumber] = p;
            fronts[0].operations.RemoveAt(0);
            foreach (List<int> g in gamma)
            {
                if (g.Exists(index => index == jnumber)) g.Remove(jnumber);
            }            
            foreach (Operation op in operations)
            {
                int opnumber = op.GetNumber();
                if (gamma[opnumber].Count == 0 && !isAdded[opnumber])
                {
                    int tend = -1;
                    foreach (int i in op.GetGamma()) if (output.y[i] > tend) tend = output.y[i];
                    if (tend < input.GetT0())
                    {
                        if (fronts.Find(f => f.key.Equals(tend)) == null) fronts.Add(new Front(tend));
                        fronts.Find(f => f.key.Equals(tend)).Add(op);
                        isAdded[opnumber] = true;
                    }
                    else isAdded[opnumber] = true;
                }
            }
        }
    }
}
