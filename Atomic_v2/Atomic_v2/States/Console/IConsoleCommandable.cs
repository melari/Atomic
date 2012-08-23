using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atomic
{
    public interface IConsoleCommandable
    {
        void RegisterCommands(ConsoleState cs);
    }
}
