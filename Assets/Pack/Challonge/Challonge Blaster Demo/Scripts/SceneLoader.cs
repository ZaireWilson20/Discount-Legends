using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Challonge.Sample
{
    public class SceneLoader : MonoBehaviour
    {
        public string sceneName;

        public Animator animator;

        private bool isUnloaded;

        public float delayBeforeLoad;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            // Invoke("LoadScene", 3f);
        }

        public void LoadScene()
        {
            Invoke("LoadSetup", delayBeforeLoad);
        }

        private void LoadSetup()
        {
            GetComponent<GameEventListener>().enabled = false;
            Invoke("InvokeLoadScene", .5f);
            animator.SetTrigger("Start");
        }

        public void LoadScene(string sceneName)
        {
            this.sceneName = sceneName;
            LoadScene();
        }

        private void InvokeLoadScene()
        {
            SceneManager.LoadSceneAsync(sceneName).completed += (asyncOperation) =>
            {
                animator.SetTrigger("Start");
                Invoke("SceneTransitionComplete", .55f);
            };
        }

        private void SceneTransitionComplete()
        {           
            GetComponent<GameEventListener>().Event.Raise();
            Destroy(this.gameObject);
        }
    }
}
