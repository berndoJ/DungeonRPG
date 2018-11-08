using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Items;

namespace DungeonRPG.CommandEnvironment.Commands
{
    /// <summary>
    /// Game Command:
    /// Lists all available / loaded items in the game.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameCommandListItems : IGameCommand
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
        public GameCommandListItems()
        {
            this.UIDString = "list_items";
            this.Command = "listitems";
            this.Description = "Lists all avialable items in the game.";
            this.Help = "Syntax: listitems";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            GameConsole.Log(GameConsole.CStr("Game item list:", Color.cyan));
            GameConsole.Log(GameConsole.CStr("[ItemUID] DisplayName (MaxStackSize)", Color.cyan));
            foreach (Item item in Item.GetAllItems())
            {
                GameConsole.Log("[" + item.GetUID() + "] " + item.GetDisplayName() + " (" + item.GetMaxStackSize() + ")");
            }
        }
    }
}
