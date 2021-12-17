using UnityEngine;
using System.Collections;

namespace Challonge.Internal
{
    public class ChallongeMaster : MonoBehaviour
    {
        #region Singleton

        public static ChallongeMaster Instance
        {
            get
            {
                if (Equals(_instance, null))
                {                   
                    _instance = FindObjectOfType(typeof(ChallongeMaster)) as ChallongeMaster;

                    if (Equals(_instance, null))
                    {
                        GameObject newGameObject = GameObject.Instantiate(new GameObject());
                        newGameObject.name = "Challonge Master";
                        newGameObject.AddComponent<ChallongeMaster>();
                        _instance = newGameObject.GetComponent<ChallongeMaster>();
                        DontDestroyOnLoad(newGameObject);
                        //throw new UnityException("ChallongeMaster does not exist.");
                    }
                }

                return _instance;
            }
        }

        static ChallongeMaster _instance;
    
        #endregion

        public void CoroutineWrapper(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}
