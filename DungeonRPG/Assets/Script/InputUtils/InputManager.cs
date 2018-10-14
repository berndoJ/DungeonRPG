using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonRPG.Entities;
using DungeonRPG.UI;
using DungeonRPG.CommandEnvironment;

namespace DungeonRPG.InputUtils
{
    /// <summary>
    /// Enum for all classes that depend on input.
    /// </summary>
    public enum GameInputClass
    {
        PLAYER_AI,
        F2_EASER_EGG,
        FPS_COUNTER,
        MINIMAP_CAMERA_CONTROL,
        OBJECT_ACTIVE_TOGGLE,
        GAME_CONSOLE
    }

    public static class InputManager
    {
        /// <summary>
        /// Sets the INPUT_ENABLED state of the given class (enum type).
        /// </summary>
        /// <param name="inputClass">The input class.</param>
        /// <param name="state">The new state.</param>
        public static void SetInputEnabledState(GameInputClass inputClass, bool state)
        {
            switch (inputClass)
            {
                case GameInputClass.PLAYER_AI:
                    PlayerAI.INPUT_ENABLED = state;
                    break;
                case GameInputClass.F2_EASER_EGG:
                    F2EasterEgg.INPUT_ENABLED = state;
                    break;
                case GameInputClass.FPS_COUNTER:
                    FPSCounter.INPUT_ENABLED = state;
                    break;
                case GameInputClass.MINIMAP_CAMERA_CONTROL:
                    MinimapCameraControl.INPUT_ENABLED = state;
                    break;
                case GameInputClass.OBJECT_ACTIVE_TOGGLE:
                    ObjectActiveToggle.INPUT_ENABLED = state;
                    break;
                case GameInputClass.GAME_CONSOLE:
                    GameConsole.INPUT_ENABLED = state;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the INPUT_ENABLED state of the given class (enum type) to TRUE.
        /// </summary>
        /// <param name="inputClass">The input class.</param>
        public static void EnableInputState(GameInputClass inputClass)
        {
            SetInputEnabledState(inputClass, true);
        }

        /// <summary>
        /// Sets the INPUT_ENABLED state of the given class (enum type) to FALSE.
        /// </summary>
        /// <param name="inputClass">The input class.</param>
        public static void DisableInputState(GameInputClass inputClass)
        {
            SetInputEnabledState(inputClass, false);
        }

        /// <summary>
        /// Enables the input state of all classes.
        /// </summary>
        public static void EnableAllInputStates()
        {
            foreach (GameInputClass inputClass in Enum.GetValues(typeof(GameInputClass)))
            {
                EnableInputState(inputClass);
            }
        }

        /// <summary>
        /// Disables the input state of all classes.
        /// </summary>
        public static void DisableAllInputStates()
        {
            foreach (GameInputClass inputClass in Enum.GetValues(typeof(GameInputClass)))
            {
                DisableInputState(inputClass);
            }
        }
    }
}
