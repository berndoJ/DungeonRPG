using DungeonRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.CommandEnvironment
{
    public class GameCommandSetHealth : IGameCommand
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
        public GameCommandSetHealth()
        {
            this.UIDString = "set_health";
            this.Command = "sethealth";
            this.Description = "Sets the health of the player.";
            this.Help = "Syntax: sethealth <health>";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            if (args.Count != 1)
            {
                GameConsole.LogError(this.Help);
                return;
            }
            int newHealth = 0;
            if (!int.TryParse(args[0], out newHealth))
            {
                GameConsole.LogError("The argument <health> must be an integer.");
                return;
            }
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
            if (newHealth < 0 || newHealth > player.MaxHealth)
            {
                GameConsole.LogError("The argument <health> must be in the rage of 0 to " + player.MaxHealth + ".");
                return;
            }
            player.Health = newHealth;
            GameConsole.Log(GameConsole.CStr("Successfully set the player's health to " + newHealth + ".", Color.green));
        }
    }
}
