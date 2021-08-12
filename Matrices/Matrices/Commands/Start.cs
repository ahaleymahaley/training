using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class Start : ACommand
    {
        public Start(MatrixForm matrixForm)
        {
            this.matrixForm = matrixForm;
        }
        protected override ICommand Clone()
        {
            return new Start(matrixForm);
        }
        protected override void doExecute()
        {
            matrixForm.Clear();
            matrixForm.SetBorder(true);
        }
    }
}
