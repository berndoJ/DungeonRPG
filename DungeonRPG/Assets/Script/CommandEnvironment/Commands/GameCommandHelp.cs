using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.CommandEnvironment.Commands
{
    /// <summary>
    /// Game Command:
    /// Displays help for all commands / lists all available commands.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameCommandHelp : IGameCommand
    {
        /// <summary>
        /// The unique ID string of this command.
        /// </summary>
        public string UIDString
        {
            get;
            private set;
        }

        /// <summary>
        /// The command itself (invoke string)
        /// </summary>
        public string Command
        {
            get;
            private set;
        }

        /// <summary>
        /// The description of this command.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// The help of this command.
        /// </summary>
        public string Help
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public GameCommandHelp()
        {
            this.UIDString = "help";
            this.Command = "help";
            this.Description = "Lists all avialable commands.";
            this.Help = "Syntax: help";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            GameConsole.Log(GameConsole.CStr("All registered commands:", Color.cyan));
            GameConsole.Log(GameConsole.CStr("[Command] (UID) Description <Help>", Color.cyan));
            foreach (IGameCommand cmd in GameConsole.CommandRegistry.Values)
            {
                GameConsole.Log("[" + cmd.Command + "] (" + cmd.UIDString + ") " + cmd.Description + " <" + cmd.Help + ">");
            }
        }
    }
}
