using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Challonge
{
    [CustomEditor(typeof(MatchEvent))]
    public class MatchEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            //MatchEvent myScript = (MatchEvent)target;
            //if (GUILayout.Button("Raise Event"))
            //{
            //    myScript.Raise();
            //}
        }
    }
}
