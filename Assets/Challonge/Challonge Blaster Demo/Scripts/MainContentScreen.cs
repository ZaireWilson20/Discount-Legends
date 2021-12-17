using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Challonge.Examples
{
    public class MainContentScreen : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public UnityEvent loadActions = new UnityEvent();
    }
}
