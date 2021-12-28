using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Challonge.Sample
{
    public class MenuManager : MonoBehaviour
    {
        public API.Data.ChallongeMatch matchData;

        public API.Data.ChallongeUser credentials;

        public GameObject loginButton;

        public GameObject logoutButton;

        public GameObject userProfile;

        public UnityEngine.UI.RawImage pic;

        public TextMeshProUGUI username;

        public TournamentEvent OnTournamentLiveActions;

        public void SetSinglePlayerQuickPlay()
        {
            matchData.totalParticipants = 1;
            
            if(matchData.participantList.Count == 0)
            {
                matchData.participantList.Add(new Models.Participant("P1"));
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (credentials.user.accessToken != "")
            {
                loginButton.SetActive(false);
                logoutButton.SetActive(true);
                userProfile.SetActive(true);
                username.text = credentials.user.username;
                pic.texture = credentials.user.userImage;
            }
            else
            {
                loginButton.SetActive(true);
                logoutButton.SetActive(false);
                userProfile.SetActive(false);
            }

            if (Challonge.Sample.GlobalManager.liveTournament != null)
            {
                OnTournamentLiveActions.Raise(Challonge.Sample.GlobalManager.liveTournament);
            }
        }

        public void SetLiveTournament(Models.Tournament tournament)
        {
            Challonge.Sample.GlobalManager.liveTournament = tournament;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
