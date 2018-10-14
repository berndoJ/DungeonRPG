﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.CommandEnvironment
{
    public class GameConsole : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The default console color.
        /// Default: Color.white
        /// </summary>
        [Header("Console")]
        [Tooltip("The default console color.")]
        public Color DefaultConsoleCololor = Color.white;

        /// <summary>
        /// The console text object. Here all the console output will be printed.
        /// </summary>
        [Header("UI Elements")]
        [Tooltip("The console text object. Here all the console output will be printed.")]
        public Text ConsoleText;

        /// <summary>
        /// The command input field.
        /// </summary>
        [Tooltip("The command input field.")]
        public InputField InputCommandField;

        /// <summary>
        /// The object which gets activated and deactivated when the console is opened / closed.
        /// </summary>
        [Tooltip("The object which gets activated and deactivated when the console is opened / closed.")]
        public GameObject ConsolePanelObject;

        #endregion

        #region Static Properties and Fields

        /// <summary>
        /// The instance of the game console.
        /// </summary>
        public static GameConsole Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// The command registry (dictionary) where all commands are registered.
        /// </summary>
        public static Dictionary<string, IGameCommand> CommandRegistry;

        /// <summary>
        /// Value that can prevent console input (open / close / enter)
        /// </summary>
        public static bool CONSOLE_INPUT_ENABLED = true;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There are more than one console instances in existance.");
                return;
            }
            Instance = this;
            CommandRegistry = new Dictionary<string, IGameCommand>();
            this.ConsoleText.text = string.Empty;
            this.ConsoleText.color = this.DefaultConsoleCololor;
            Log(CStr("Starting console.", Color.green));
            this.SearchAndRegisterCommands();
            this.ConsolePanelObject.SetActive(false);
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown("Console Toggle"))
            {
                this.ConsolePanelObject.SetActive(!this.ConsolePanelObject.activeInHierarchy);
                if (this.ConsolePanelObject.activeInHierarchy)
                    this.InputCommandField.Select();
            }
            if (CONSOLE_INPUT_ENABLED && this.ConsolePanelObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    string command = this.InputCommandField.text;
                    this.InputCommandField.Select();
                    this.InputCommandField.text = string.Empty;
                    Log(command);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers all classes which implement the <see cref="IGameCommand"/> interface. (Searches all assemblies for types, instanciates them and registers them)
        /// </summary>
        private void SearchAndRegisterCommands()
        {
            Log("Searching for commands...");
            IEnumerable<Type> commandTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(p => p.GetInterfaces()
                .Contains(typeof(IGameCommand)));
            int registeredCommands = 0;
            foreach (Type cmdType in commandTypes)
            {
                if (cmdType.IsClass)
                {
                    IGameCommand cmd = (IGameCommand)Activator.CreateInstance(cmdType);
                    if (cmd.UIDString == null) continue;
                    if (!CommandRegistry.ContainsKey(cmd.UIDString))
                    {
                        CommandRegistry.Add(cmd.UIDString, cmd);
                        registeredCommands++;
                    }
                }
            }
            Log("Registered " + registeredCommands + " commands.");
        }

        #endregion

        #region Static Methods

        #region LogInfo

        /// <summary>
        /// Logs a message to the console. (Does not add a new line at the end of the message.)
        /// </summary>
        /// <param name="msg">The message to log.</param>
        public static void LogRaw(string msg)
        {
            Instance.ConsoleText.text += msg;
        }

        /// <summary>
        /// Logs a message to the console. (Does not add a new line at the end of the message.) Alias for <see cref="LogRaw(string)"/>
        /// </summary>
        /// <param name="msg">The message to log.</param>
        public static void LogInfoRaw(string msg)
        {
            LogRaw(msg);
        }

        /// <summary>
        /// Logs a message to the console. (Adds a new line at the end of the message.)
        /// </summary>
        /// <param name="msg">The message to log.</param>
        public static void Log(string msg)
        {
            LogRaw(msg + "\n");
        }

        /// <summary>
        /// Logs a message to the console. (Adds a new line at the end of the message.) Alias for <see cref="Log(string)"/>
        /// </summary>
        /// <param name="msg">The message to log.</param>
        public static void LogInfo(string msg)
        {
            Log(msg);
        }

        #endregion

        #region LogWarning

        /// <summary>
        /// Logs a warning to the console. (Adds a new line at the end of the message.)
        /// </summary>
        /// <param name="warning">The warning message.</param>
        public static void LogWarning(string warning)
        {
            Log(CStr(warning, Color.yellow));
        }

        #endregion

        #region LogError

        /// <summary>
        /// Logs an error to the console. (Adds a new line at the end of the message.)
        /// </summary>
        /// <param name="error">The error message.</param>
        public static void LogError(string error)
        {
            Log(CStr(error, Color.red));
        }

        #endregion

        #region Color

        /// <summary>
        /// Colors a string with the given color. (RGBA)
        /// </summary>
        /// <param name="s">The string to color.</param>
        /// <param name="c">The color to use.</param>
        /// <returns>The colored string.</returns>
        public static string CStr(string s, Color c)
        {
            return "<color=#" + ColorToRGBAHex(c) + ">" + s + "</color>";
        }

        /// <summary>
        /// Formats a string with the given string format modifier.
        /// </summary>
        /// <param name="s">The stirng to format.</param>
        /// <param name="f">The format modifier to use.</param>
        /// <returns>The formatted string.</returns>
        public static string FStr(string s, StringFormatModifier f)
        {
            switch (f)
            {
                case StringFormatModifier.BOLD:
                    return "<b>" + s + "</b>";
                case StringFormatModifier.ITALIC:
                    return "<i>" + s + "</i>";
                default:
                    return s;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Maps a float from 0F to 1F to a byte from 0 to 255
        /// </summary>
        /// <param name="f">The float.</param>
        /// <returns>The mapped byte.</returns>
        public static byte MapFloat01ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        /// <summary>
        /// Converts a color to a rgb hex string. (Format: rrggbb)
        /// </summary>
        /// <param name="c">The color.</param>
        /// <returns>The converted hex string.</returns>
        public static string ColorToRGBHex(Color c)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", MapFloat01ToByte(c.r), MapFloat01ToByte(c.g), MapFloat01ToByte(c.b));
        }

        /// <summary>
        /// Converts a color to a rgba hex string. (Format: rrggbbaa)
        /// </summary>
        /// <param name="c">The color.</param>
        /// <returns>The converted hex string.</returns>
        public static string ColorToRGBAHex(Color c)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", MapFloat01ToByte(c.r), MapFloat01ToByte(c.g), MapFloat01ToByte(c.b), MapFloat01ToByte(c.a));
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Format modifiers for a string. Used for <see cref="GameConsole"/>
    /// </summary>
    public enum StringFormatModifier
    {
        BOLD,
        ITALIC
    }

    public class TestCommand : IGameCommand
    {
        public string UIDString { get; private set; }

        public string Command { get; private set; }

        public string Description { get; private set; }

        public string Help { get; private set; }

        public void Execute(List<string> args)
        {
            throw new NotImplementedException();
        }

        public TestCommand()
        {
            this.UIDString = "uid_str";
        }
    }
}
