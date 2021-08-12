using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    abstract class ACommand : ICommand
    {
        protected MatrixForm matrixForm;
        public void Execute()
        {
            CM.GetInstance().Registry(Clone());
            doExecute();
        }

        abstract protected ICommand Clone();

        abstract protected void doExecute();
    }
}
