using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Challonge.Sample
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameEvent myScript = (GameEvent)target;
            if (GUILayout.Button("Raise Event"))
            {
                myScript.Raise();
            }
        }
    }
}
