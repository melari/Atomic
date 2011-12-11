using System;

namespace Atomic
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {            
            using (Engine game = new Engine())
            {
                game.Run();
            }
        }
    }
}

