using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.CommandEnvironment
{
    public interface IGameCommand
    {
        string UIDString { get; }
        string Command { get; }
        string Description { get; }
        string Help { get; }

        void Execute(List<string> args);
    }
}
