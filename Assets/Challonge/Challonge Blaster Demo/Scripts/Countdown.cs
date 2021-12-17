using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    public UnityEvent OnCountdownComplete = new UnityEvent();

    public void StartAnimation()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }

    public void CountdownComplete()
    {
        OnCountdownComplete.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
