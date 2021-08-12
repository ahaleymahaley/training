using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    //CommandManagerSingleton
    class CM
    {
        private bool flag = true;
        private static CM instance = null;
        private List<ICommand> commands = new List<ICommand>();
        private List<ICommand> toredo = new List<ICommand>();
        private CM() { }
        public static CM GetInstance()
        {
            if (instance == null) instance = new CM();
            return instance;
        }
        public void Registry(ICommand c)
        {
            if (flag)
            {
                commands.Add(c);
                toredo.Clear();
            }
        }
        public void Undo()
        {
            flag = false;
            if (commands.Count > 1)
            {
                toredo.Add(commands[commands.Count - 1]);
                commands.RemoveAt(commands.Count - 1);
                foreach (ICommand c in commands) c.Execute();
            }
            flag = true;
        }

        public void Redo()
        {
            flag = false;
            if (toredo.Count > 0)
            {
                commands.Add(toredo[toredo.Count - 1]);
                toredo.RemoveAt(toredo.Count - 1);
                foreach (ICommand c in commands) c.Execute();
            }
            flag = true;
        }
    }
}
