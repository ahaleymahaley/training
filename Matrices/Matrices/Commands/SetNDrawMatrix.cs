using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SetNDrawMatrix: ACommand
    {
        private IMatrix matrix;
        public SetNDrawMatrix(MatrixForm matrixForm, IMatrix matrix)
        {
            this.matrixForm = matrixForm;
            this.matrix = matrix;
        }
        protected override ICommand Clone()
        {
            return new SetNDrawMatrix(matrixForm, matrix);
        }

        protected override void doExecute()
        {
            matrixForm.ActionRecord("Matrix has been uploaded.");
            matrixForm.SetMatrix(matrix);
            matrixForm.DrawMatrix();
        }
    }
}
