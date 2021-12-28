using UnityEngine;
using UnityEditor;
using Challonge.Behaviours;
using System.Collections;

namespace Challonge.Sample
{

    [CustomEditor(typeof(PlayerShip))]
    public class MatchComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            PlayerShip player = (PlayerShip)target;

            if (Application.isPlaying)
            {
                EditorGUILayout.Space();

                if (GUILayout.Button("Take Damage"))
                    player.TakeDamage(1);

                if (player.health <= 0 && GUILayout.Button("Respawn"))
                    player.Spawn();
            }
        }
    }

}