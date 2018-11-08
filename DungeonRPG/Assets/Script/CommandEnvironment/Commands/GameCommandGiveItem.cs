using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Items;
using DungeonRPG.ItemContainer;

namespace DungeonRPG.CommandEnvironment.Commands
{
    /// <summary>
    /// Game Command:
    /// Yields the player the given amount of specified items.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameCommandGiveItem : IGameCommand
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
        public GameCommandGiveItem()
        {
            this.UIDString = "give_item";
            this.Command = "giveitem";
            this.Description = "Gives the player a specified item.";
            this.Help = "Syntax: giveitem <item_uid> [count]";
        }

        /// <summary>
        /// Gets invoked when the command gets executed by the user.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        public void Execute(List<string> args)
        {
            if (args.Count < 1)
            {
                GameConsole.LogError(this.Help);
                return;
            }
            else
            {
                string itemUid = args[0];
                Item item = Item.GetItemByUID(itemUid);
                if (item == null)
                {
                    GameConsole.LogError("The given argument '" + itemUid + "' is not a valid item UID.");
                    return;
                }
                int count = 1;
                if (args.Count > 1)
                {
                    string countStr = args[1];
                    int newCount = 0;
                    if (!int.TryParse(countStr, out newCount))
                    {
                        GameConsole.LogError("The given argument '" + countStr + "' does not represent an integer.");
                        return;
                    }
                    if (newCount < 1)
                    {
                        GameConsole.LogError("The amont of items to be given to the player cannot be less than 1.");
                        return;
                    }
                    count = newCount;
                }
                GameObject playerObj = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
                if (playerObj == null)
                {
                    GameConsole.LogError("Could not find a player.");
                    return;
                }
                Inventory playerInventory = playerObj.GetComponent<Inventory>();
                if (playerInventory == null)
                {
                    GameConsole.LogError("The player that has been found has no inventory.");
                    return;
                }
                uint leftoverItems = playerInventory.AddItem(item, (uint)count);
                GameConsole.Log(GameConsole.CStr("Successfully added " + (count - leftoverItems) + " item(s) to the players inventory.", Color.green));
            }
        }
    }
}
