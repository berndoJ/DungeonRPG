using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.CommandEnvironment.Commands
{
    public class GameCommandClear : IGameCommand
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
        public GameCommandClear()
        {
            this.UIDString = "clear";
            this.Command = "clear";
            this.Description = "Clears the game console window.";
            this.Help = "Syntax: clear";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            GameConsole.Clear();
            GameConsole.Log(GameConsole.CStr("Game console cleared.", Color.yellow));
        }
    }
}
