using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

    public abstract class BaseEditor : Editor
    {
        public bool alwaysShowDefaultInspector;

        public bool showDefaultInspectorToggle;

        public override void OnInspectorGUI()
        {
            if (!alwaysShowDefaultInspector)
            {
                toggle(ref showDefaultInspectorToggle, "Show Default Inspector");

                if (showDefaultInspectorToggle)
                    DrawDefaultInspector();
                else
                    DrawCustomInspector();
            }
            else
            {
                DrawDefaultInspector();
                DrawCustomInspector();
            }
        }

        public abstract void DrawCustomInspector();

        #region Basic Controls

        /// <summary>
        /// UI Button
        /// </summary>
        /// <param name="label">Button Label</param>
        /// <param name="color">Button Color</param>
        /// <returns></returns>
        public bool button(string label, Color color)
        {
            GUI.backgroundColor = color;

            bool isButtonPressed = GUILayout.Button(label);

            GUI.backgroundColor = Color.white;

            return isButtonPressed;
        }

        /// <summary>
        /// UI Button
        /// </summary>
        /// <param name="label">Button Label</param>
        /// <returns></returns>
        public bool button(string label)
        {
            GUI.backgroundColor = Color.white;

            bool isButtonPressed = GUILayout.Button(label);

            GUI.backgroundColor = Color.white;

            return isButtonPressed;
        }

        /// <summary>
        /// UI Toggle/Checkbox
        /// </summary>
        /// <param name="toggleValue">The Property Value</param>
        /// <param name="label">Toggle Label</param>
        public void toggle(ref bool toggleValue, string label)
        {
            toggleValue = EditorGUILayout.Toggle(label, toggleValue);
        }

        /// <summary>
        /// Color Field
        /// </summary>
        /// <param name="colorValue">The Property Value</param>
        /// <param name="label">Color Label</param>
        public void colorField(ref Color colorValue, string label)
        {
            colorValue = EditorGUILayout.ColorField(label, colorValue);
        }

        /// <summary>
        /// Float Field
        /// </summary>
        /// <param name="floatValue"></param>
        /// <param name="label"></param>
        public void floatField(ref float floatValue, string label)
        {
            floatValue = EditorGUILayout.FloatField(label, floatValue);
        }

        /// <summary>
        /// Int Field
        /// </summary>
        /// <param name="intValue"></param>
        /// <param name="label"></param>
        public void intField(ref int intValue, string label)
        {
            intValue = EditorGUILayout.IntField(label, intValue);
        }

        /// <summary>
        /// Text Field
        /// </summary>
        /// <param name="textValue">Property Value</param>
        /// <param name="label">Text Value</param>
        public void textField(ref string textValue, string label)
        {
            textValue = EditorGUILayout.TextField(label, textValue);
        }

        /// <summary>
        /// Slider
        /// </summary>
        /// <param name="floatValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="label"></param>
        public void slider(ref float floatValue, float min, float max, string label)
        {
            floatValue = EditorGUILayout.Slider(label, floatValue, min, max);
        }

        /// <summary>
        /// Slider
        /// </summary>
        /// <param name="floatValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="label"></param>
        public void slider(ref int intValue, int min, int max, string label)
        {
            intValue = EditorGUILayout.IntSlider(label, intValue, min, max);
        }

        /// <summary>
        /// Dropdown menu of string values
        /// </summary>
        /// <param name="indexValue">Index Property</param>
        /// <param name="stringArray">Array of strings</param>
        /// <param name="label">Label</param>
        public void dropDown(ref int indexValue, string[] stringArray, string label)
        {
            indexValue = EditorGUILayout.Popup(label, indexValue, stringArray);
        }

        #endregion

        #region Basic Labels

        public void header(string Label, bool startWithSpace = true)
        {
            if (startWithSpace)
                EditorGUILayout.Space();

            EditorGUILayout.LabelField(Label, EditorStyles.boldLabel);
        }

        public void subHeader(string Label, bool startWithSpace = true)
        {
            if (startWithSpace)
                EditorGUILayout.Space();

            EditorGUILayout.LabelField(Label, EditorStyles.largeLabel);
            EditorGUILayout.Space();
        }

        public void readOnlyValue(string label)
        {
            EditorGUILayout.LabelField(label);
        }

        public void readOnlyValue(string label, string value)
        {
            EditorGUILayout.LabelField(label, value);
        }

        public void readOnlyValue(string label, float value)
        {
            readOnlyValue(label, value.ToString());
        }

        public void helpBox(string label, MessageType messageType)
        {
            EditorGUILayout.HelpBox(label, messageType);
        }

        #endregion
    }

