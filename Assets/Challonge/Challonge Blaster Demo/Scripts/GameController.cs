using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Challonge.Sample
{
    public class GameController : MonoBehaviour
    {
        #region Global Variables

        /// Summary:    The player ship prefab used to create players
        public GameObject playerShipPrefab;

        public WinnerUI winnerUI;

        /// Summary:    The enemy spawner.
        public EnemySpawner enemySpawner;

        private List<PlayerShip> AllPlayers;

        public List<Color> playerColors;

        public GameObject Hud;

        public int totalPlayers;

        public int teamScore;

        public List<Transform> spawnPoints;

        public Challonge.API.Data.ChallongeMatch matchData;

        public bool isGameStarted;

        public bool isGameOver;

        public UnityEvent OnGameOverActions;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            AllPlayers = new List<PlayerShip>(matchData.totalParticipants);
        }

        public void Start()
        {
            AddPlayersToGame();
        }

        private void Update()
        {
            if(!isGameOver && isGameStarted)
            {
                if(CheckGameOver())
                {
                    isGameOver = true;

                    if(matchData.totalParticipants == 1)
                    {
                        winnerUI.ShowWinner(GetWinner(), true);
                    }
                    else
                    {
                        winnerUI.ShowWinner(GetWinner(), false);
                    }

                    OnGameOverActions.Invoke();
                }             
            }
        }

        #endregion

        #region Helper Methods


        /// Function:   StartGame
        ///
        /// Summary:    Starts a game.
        ///
        /// Author: Khalil
        ///
        /// Date:   12/1/2021
        public void StartGame()
        {
            enemySpawner.StartEnemySpawning();
            isGameStarted = true;
        }


        /// Function:   AddPlayersToGame
        ///
        /// Summary:    Adds players to game.
        ///
        /// Author: Khalil
        ///
        /// Date:   12/1/2021
        private void AddPlayersToGame()
        {
            GameObject playerHolder = new GameObject("Player Holder");

            for(int i = 0; i < matchData.totalParticipants; i++)
            {
                //Create player and set position using spawn points
                GameObject nextPlayer = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
                nextPlayer.name = string.Concat(matchData.participantList[i].username);
                nextPlayer.transform.parent = playerHolder.transform;
                nextPlayer.transform.position = spawnPoints[i].position;

                //Init Game Controller
                PlayerShip nextPlayerShip = nextPlayer.GetComponent<PlayerShip>();
                nextPlayerShip.gameController = this;
                nextPlayerShip.participant = matchData.participantList[i];
                nextPlayerShip.participant.matchResult.score = 0;
                nextPlayerShip.color = playerColors[i];
                AllPlayers.Add(nextPlayerShip);

                // Add Hud
                GameObject myHud = Instantiate(Hud, Vector3.zero, Quaternion.identity);
                myHud.name = "HUD (" + nextPlayer.name + ")";
                myHud.GetComponent<Follow>().target = nextPlayer.transform;
                myHud.GetComponent<HealthDisplay>().InitHud(nextPlayerShip, matchData.participantList[i], playerColors[i]);

                InitPlayerShader(i);
            }
        }


        /// Function:   InitPlayerShader
        ///
        /// Summary:    Initializes the player shader based on the player index
        ///
        /// Author: Khalil
        ///
        /// Date:   12/4/2021
        ///
        /// Parameters:
        /// playerIndex -   Zero-based index of the player.
        private void InitPlayerShader(int playerIndex)
        {
            //Grab shader holder from the ship
            GameObject ShaderHolder = AllPlayers[playerIndex].transform.Find("Ship4Prefab/Shader Holder").gameObject;
            List<Transform> AllShaders = new List<Transform>(ShaderHolder.GetComponentsInChildren<Transform>());

            //Iterate through all available shaders
            for (int i = 0; i < AllShaders.Count; i++)
            {
                //Skip if this is the shader holder
                if (AllShaders[i].gameObject == ShaderHolder)
                    continue;

                //Skip if this is the correct color for the player
                if (AllShaders[i].gameObject.name == string.Concat("Shader P", playerIndex + 1))
                    continue;

                //Delete remaining shader objects
                Destroy(AllShaders[i].gameObject);
            }
        }


        /// Function:   AddToTeamScore
        ///
        /// Summary:    Adds to the team score.
        ///
        /// Author: Khalil
        ///
        /// Date:   12/1/2021
        ///
        /// Parameters:
        /// addedScore -    The added score.
        public void AddToTeamScore(int addedScore)
        {
            teamScore += addedScore;
        }


        /// Function:   CheckGameOver
        ///
        /// Summary:    Determines if we can check game over.
        ///
        /// Author: Khalil
        ///
        /// Date:   12/1/2021
        ///
        /// Returns:    True if it succeeds, false if it fails.
        private bool CheckGameOver()
        {
            for (int i = 0; i < AllPlayers.Count; i++)
            {
                if (AllPlayers[i].health > 0)
                    return false;
            }

            return true;
        }

        private Models.Participant GetWinner()
        {
            if (AllPlayers.Count == 1)
                return AllPlayers[0].participant;

            Models.Participant winner = AllPlayers[0].participant;
            winner.matchResult.isAdvancing = true;

            for (int i = 1; i < AllPlayers.Count; i++)
            {
                if (AllPlayers[i].participant.matchResult.score > winner.matchResult.score)
                {
                    winner.matchResult.isAdvancing = false;
                    winner = AllPlayers[i].participant;
                    winner.matchResult.isAdvancing = true;
                }
            }

            return winner;
        }

        #endregion
    }
}
