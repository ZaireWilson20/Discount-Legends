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
    [SerializeField] private CartMovement player;
    private float currentScore; // FOR DEBUGGING
    private PhotonView PV;

    void Awake()
    {
        currentScore = 0f;
        // scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>(); // FOR DEBUGGING
        PV = gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
        player = gameObject.transform.parent.gameObject.GetComponent<CartMovement>();
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


                 float point = points.getPoints(); 
                if (PV.IsMine)
                {
                   // FOR  Debugging
                    currentScore += point;
                    // FOR DEBUGGING
                    if(scoreText != null){
                    scoreText.text = "SCORE: " + currentScore; // FOR DEBUGGING
                    }
                }
    
                player.setPlayerScore(point);
                Destroy(other.gameObject);
            }
        }
    }

}
