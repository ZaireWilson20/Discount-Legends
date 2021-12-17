using UnityEngine;
using UnityEditor;
using Challonge.Behaviours;
using System.Collections;

namespace Challonge.Sample
{

    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            GameController gameController = (GameController)target;

            if (Application.isPlaying)
            {
                EditorGUILayout.Space();

                if (GUILayout.Button("Start Game"))
                    gameController.StartGame();

            }
        }
    }
}
