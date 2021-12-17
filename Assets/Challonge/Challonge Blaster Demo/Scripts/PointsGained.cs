using System;
using System.Collections.Generic;
using Challonge;
using Challonge.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Challonge.Sample
{
    public class PointsGained : MonoBehaviour
    {
        private void Awake()
        {

        }

        public void DeactivateGameObject()
        {
            Destroy(gameObject);
        }
    }
}
