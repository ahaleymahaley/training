using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningTask
{
    public class Parser
    {
        public Input input;
        public Parser(string name)
        {
            StreamReader sr = new StreamReader(name);
            string cur = sr.ReadLine();
            int t0 = Convert.ToInt32(cur);
            cur = sr.ReadLine();
            int m = Convert.ToInt32(cur);
            cur = sr.ReadLine();
            int n = Convert.ToInt32(cur);
            cur = sr.ReadLine();
            int p = Convert.ToInt32(cur);
            int[] b = new int[m];
            cur = sr.ReadLine();
            string[] split = cur.Split(' ');
            for (int i = 0; i < m; i++)
                b[i] = Convert.ToInt32(split[i]);
            int[] d = new int[m];
            cur = sr.ReadLine();
            split = cur.Split(' ');
            for (int i = 0; i < m; i++)
                d[i] = Convert.ToInt32(split[i]);
            List<int>[] psi = new List<int>[m];
            for (int i = 0; i < m; i++)
            {
                psi[i] = new List<int>();
                cur = sr.ReadLine();
                split = cur.Split(' ');
                for (int j = 0; j < split.Length; j++)
                    psi[i].Add(Convert.ToInt32(split[j]) - 1);
            }
            List<int>[] gamma = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                gamma[i] = new List<int>();
                cur = sr.ReadLine();
                if (cur != "-")
                {
                    split = cur.Split(' ');
                    for (int j = 0; j < split.Length; j++)
                        gamma[i].Add(Convert.ToInt32(split[j]) - 1);
                }
            }
            int[] t = new int[n];
            cur = sr.ReadLine();
            split = cur.Split(' ');
            for (int i = 0; i < n; i++)
                t[i] = Convert.ToInt32(split[i]);
            List<int>[] phi = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                phi[i] = new List<int>();
                cur = sr.ReadLine();
                split = cur.Split(' ');
                for (int j = 0; j < split.Length; j++)
                    phi[i].Add(Convert.ToInt32(split[j]));
            }
            List<Order> orders = new List<Order>();
            for (int i = 0; i < m; i++) orders.Add(new Order(i, b[i], d[i], psi[i]));
            List<Operation> operations = new List<Operation>();
            for (int i = 0; i < n; i++) operations.Add(new Operation(i, gamma[i], t[i], phi[i]));
            input = new Input(t0, p, orders, operations);
            sr.Close();
        }
    }

    public class Input
    {
        int t0;
        int p;
        List<Order> orders;
        List<Operation> operations;

        public int GetT0() { return t0;}

        public int GetP() { return p; }

        public List<Order> GetOrders() { return orders; }

        public List<Operation> GetOperations() { return operations; }

        public Input(int t0, int p, List<Order> orders, List<Operation> operations)
        {
            this.t0 = t0;
            this.p = p;
            this.orders = orders;
            this.operations = operations;
        }
    }

    public class Front
    {
        public int key;
        public List<Operation> operations;

        public Front(int key)
        {
            this.key = key;
            operations = new List<Operation>();
        }

        public void Add(Operation op)
        {
            operations.Add(op);
        }

        public static int CompareFronts(Front f1, Front f2)
        {
            if (f1.key > f2.key) return 1;
            else if (f1.key < f2.key) return -1;
            else return 0;
        }
    }

    public class Operation
    {
        int number;
        List<int> gamma;
        int t;
        public int p;
        List<int> phi;

        public int GetNumber() { return number; }

        public List<int> GetGamma() { return gamma; }

        public int GetT() { return t; }

        public List<int> GetPhi() { return phi; }

        public Operation(int number, List<int> gamma, int t, List<int> phi)
        {
            this.number = number;
            this.gamma = gamma;
            this.t = t;
            p = 1;
            this.phi = phi;
        }

        public static int SortInAscendingOrder(Operation o1, Operation o2)
        {
            if (o1.t > o2.t) return 1;
            else if (o1.t < o2.t) return -1;
            else return 0;
        }

        public static int SortInDecreasingOrder(Operation o1, Operation o2)
        {
            if (o1.t < o2.t) return 1;
            else if (o1.t > o2.t) return -1;
            else return 0;
        }

        public static int SortByPriority(Operation o1, Operation o2)
        {
            if (o1.p < o2.p) return 1;
            else if (o1.p > o2.p) return -1;
            else return 0;
        }
    }

    public class Order
    {
        int number;
        int b;
        int d;
        List<int> psi;

        public int GetNumber() { return number; }

        public int GetB() { return b; }

        public int GetD() { return d; }

        public List<int> GetPsi() { return psi; }

        public Order(int number, int b, int d, List<int> psi)
        {
            this.number = number;
            this.b = b;
            this.d = d;
            this.psi = psi;
        }
    }

    public class Output
    {
        public int[] x;
        public int[] y;
        public int[] z;

        public Output(int n)
        {
            x = new int[n];
            y = new int[n];
            z = new int[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = -1;
                y[i] = -1;
                z[i] = -1;
            }
        }
    }
}