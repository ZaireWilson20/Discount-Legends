using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challonge.Sample
{
    [CreateAssetMenu(fileName = "New Boundary", menuName = "Challonge/Challonge Blaster/Level Boundary")]
    public class Boundaries : ScriptableObject
    {
        [SerializeField]
        public float xMin;
        [SerializeField]
        public float xMax;
        [SerializeField]
        public float zMin;
        [SerializeField]
        public float zMax;

    }
}
