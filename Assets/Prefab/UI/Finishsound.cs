using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Finishsound : MonoBehaviour
{
    // Start is called before the first frame update
AudioSource Finish;

    private void Start()
    {
        Finish = GetComponent<AudioSource>();
        Finish.PlayDelayed(89.5f);


    }
}
