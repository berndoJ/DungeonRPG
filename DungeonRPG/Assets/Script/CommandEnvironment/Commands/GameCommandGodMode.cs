using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Entities;

namespace DungeonRPG.CommandEnvironment.Commands
{
    /// <summary>
    /// Game Command:
    /// Toggles god mode.
    /// 
    /// In god mode the player does not take any damage, except for out-of-bounds
    /// damage. (e.g. falling off the map)
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameCommandGodMode : IGameCommand
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
        public GameCommandGodMode()
        {
            this.UIDString = "god_mode";
            this.Command = "god";
            this.Description = "Toggles god mode.";
            this.Help = "Syntax: god";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            GameObject playerObj = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
            if (playerObj == null)
            {
                GameConsole.LogError("Could not find a player.");
                return;
            }
            Player player = playerObj.GetComponent<Player>();
            if (player == null)
            {
                GameConsole.LogError("The found player game object does not contain the component 'Player'.");
                return;
            }
            player.GodMode = !player.GodMode;
            GameConsole.Log(GameConsole.CStr("Successfully " + (player.GodMode ? "enabled" : "disabled") + " god mode.", Color.green));
        }
    }
}
