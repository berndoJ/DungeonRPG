using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.CommandEnvironment
{
    /// <summary>
    /// Interface for game commands. If this interface is implemented
    /// from a class, this class automatically gets added to the available
    /// commands in the game console.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public interface IGameCommand
    {
        /// <summary>
        /// A uique identifier string which identifies this command.
        /// </summary>
        string UIDString { get; }

        /// <summary>
        /// The string which calls this command.
        /// </summary>
        string Command { get; }

        /// <summary>
        /// A short description of this command.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// A short help / syntax message of this command.
        /// </summary>
        string Help { get; }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        void Execute(List<string> args);
    }
}
