using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
//THIS IS USED FOR DEBUGGING SCORE
using UnityEngine.UI;
using TMPro;

public class ItemDetection : MonoBehaviourPunCallbacks

{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore; // FOR DEBUGGING
    private PhotonView PV;

    void Awake()
    {
        currentScore = 0;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>(); // FOR DEBUGGING
        PV = gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Item points = other.gameObject.GetComponent<Item>();
            if (points == null)
            {
                return;
            }
            else
            {

                if (PV.IsMine)
                {
                    int point = points.getPoints(); // FOR DEBUGGING
                    currentScore += point; // FOR DEBUGGING
                    scoreText.text = "SCORE: " + currentScore; // FOR DEBUGGING
                }


                Destroy(other.gameObject);
            }
        }
    }

}
