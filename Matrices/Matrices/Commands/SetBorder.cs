using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SetBorder : ACommand
    {        
        private bool isChecked;
        public SetBorder(MatrixForm matrixForm, bool isChecked)
        {
            this.matrixForm = matrixForm;
            this.isChecked = isChecked;
        }
        protected override ICommand Clone()
        {
            return new SetBorder(matrixForm, isChecked);
        }

        protected override void doExecute()
        {
            matrixForm.ActionRecord("Border display has been changed.");
            matrixForm.SetBorder(isChecked);
        }
    }
}
