using DungeonRPG.FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI.MainMenu
{
    public class LevelNameTextValidator : MonoBehaviour
    {
        /// <summary>
        /// The inputfiled to validate the characters for.
        /// </summary>
        [Tooltip("The inputfiled to validate the characters for.")]
        public InputField ValidationInputField;

        /// <summary>
        /// The button that gets enabled / disabled depending on the input validation.
        /// </summary>
        [Tooltip("The button that gets enabled / disabled depending on the input validation.")]
        public Button CreateGameButton;

        /// <summary>
        /// The regex to validate the char.
        /// </summary>
        private static Regex CharValidationRegex = new Regex(@"^[a-z\d\s]+$", RegexOptions.IgnoreCase);

        public static List<string> LoadedSafeUIDs;

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.ValidationInputField.onValidateInput += delegate(string currentInput, int inCharIndex, char inChar)
            {
                return this.ValidateChar(currentInput, inCharIndex, inChar);
            };
            this.ValidationInputField.onValueChanged.AddListener((newVal) => this.OnInputFieldValueChanged(newVal));
            UpdateSaveUIDList();
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Validates the given character with the char validation regex. (see <see cref="CharValidationRegex"/>)
        /// </summary>
        /// <param name="inChar">The char to check.</param>
        /// <returns>The char or \0 if the check was unsuccessful.</returns>
        private char ValidateChar(string currentInput, int inCharIndex, char inChar)
        {
            int matchCount = CharValidationRegex.Matches(inChar.ToString()).Count;
            if (matchCount < 1)
                return '\0';
            return inChar;
        }

        /// <summary>
        /// Gets invoked when the value (text) of the input field has changed.
        /// </summary>
        /// <param name="newValue">The new value the input field has changed to.</param>
        private void OnInputFieldValueChanged(string newValue)
        {
            if (newValue.Length > 0)
            {
                if (LoadedSafeUIDs.Contains(newValue))
                {
                    this.CreateGameButton.interactable = false;
                }
                else
                {
                    this.CreateGameButton.interactable = true;
                }
            }
            else
            {
                this.CreateGameButton.interactable = false;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Updates the save uid list.
        /// </summary>
        public static void UpdateSaveUIDList()
        {
            Debug.Log("Reloading save UID list...");
            LoadedSafeUIDs = GameSaveManager.GetAllSaveUIDs();
            Debug.Log("Loaded " + LoadedSafeUIDs.Count + " game-save uids.");
        }

        /// <summary>
        /// Fully verifies a game name.
        /// </summary>
        /// <param name="name">The game name to verify.</param>
        /// <returns>The verification result.</returns>
        public static bool FullyVerifyGameName(string name)
        {
            int matchCount = CharValidationRegex.Matches(name).Count;
            if (matchCount < 1)
                return false;
            if (name.Length > 0)
            {
                if (LoadedSafeUIDs.Contains(name))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
