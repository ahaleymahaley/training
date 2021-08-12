using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningTask
{
    abstract class Algorithm : IAlgorithm
    {
        protected Input input;        
        protected Output output;

        public Algorithm(Input input)
        {
            this.input = input;
            output = new Output(input.GetOperations().Count);
        }
        public abstract Output Solution();
    }
}
