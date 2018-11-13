using DungeonRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.CommandEnvironment.Commands
{
    /// <summary>
    /// Game command:
    /// Toggles the fly mode of the player.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameCommandFlyMode : IGameCommand
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
        public GameCommandFlyMode()
        {
            this.UIDString = "fly_mode";
            this.Command = "fly";
            this.Description = "Toggles fly mode.";
            this.Help = "Syntax: fly";
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
            player.AI.FlyMode = !player.AI.FlyMode;
            GameConsole.Log(GameConsole.CStr("Successfully " + (player.AI.FlyMode ? "enabled" : "disabled") + " fly mode.", Color.green));
        }
    }
}
