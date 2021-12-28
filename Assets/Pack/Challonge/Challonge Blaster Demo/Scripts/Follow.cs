using System;
using System.Collections.Generic;
using Challonge;
using Challonge.Properties;
using UnityEngine;

namespace Challonge.Sample
{
    public class Follow : MonoBehaviour
    {
        public float xOffset;

        public float yOffset;

        public Transform target;

        private Transform transformCached;

        private void Awake()
        {
            transformCached = transform;
        }

        private void Update()
        {
            if (target != null)
                this.transformCached.position = new Vector3(target.position.x, -0.65f, target.position.z);
        }
    }
}
