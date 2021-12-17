using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Challonge.Sample
{

    public class WinnerUI : MonoBehaviour
    {
        public TextMeshProUGUI score;
        public TextMeshProUGUI username;
        public GameObject gameover;
        public GameObject winner;

        public void ShowWinner(Models.Participant participant, bool isSolo)
        {
            gameObject.SetActive(true);

            if(isSolo)
            {
                gameover.SetActive(true);
                winner.SetActive(false);
            }
            else
            {
                gameover.SetActive(false);
                winner.SetActive(true);
            }

            score.text = participant.matchResult.score.ToString();
            username.text = participant.name.ToString();
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
}
