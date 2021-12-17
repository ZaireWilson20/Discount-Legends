using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Challonge.Models;
using Challonge.Properties;

namespace Challonge.Behaviours.UI
{
    public class UITournamentList : MonoBehaviour
    {
        public GameObject parentGameObject;

        public GameObject uiTournamentItemPrefab;

        public TournamentStateFilter tournamentStateFilter = TournamentStateFilter.All;

        public int maxTournaments = 10;

        public TournamentUnityEvent onUiTournamentItemClickAction;

        public void ShowTournaments(List<Models.Tournament> tournaments)
        {
            List<UITournamentItem> items = new List<UITournamentItem>(parentGameObject.transform.GetComponentsInChildren<UITournamentItem>(true));

            List<Challonge.Models.Tournament> tourneys = new List<Challonge.Models.Tournament>();

            TournamentState targetState = TournamentState.Pending;

            if(tournamentStateFilter != TournamentStateFilter.All)
            {
                switch (tournamentStateFilter)
                {
                    case TournamentStateFilter.Pending:
                        targetState = TournamentState.Pending;
                        break;
                    case TournamentStateFilter.Underway:
                        targetState = TournamentState.Underway;
                        break;
                }
            }

            for (int i = 0; i < tournaments.Count; i++)
            {
                if (tournamentStateFilter == TournamentStateFilter.All)
                    tourneys.Add(tournaments[i]);
                else if (tournaments[i].tournamentState == targetState)
                    tourneys.Add(tournaments[i]);
            }

            for (int i = 0; i < items.Count; i++)
                DestroyImmediate(items[i].gameObject);

            for (int i = 0; i < tourneys.Count; i++)
            {
                if (i == maxTournaments)
                    break;
                UITournamentItem item = GameObject.Instantiate<GameObject>(uiTournamentItemPrefab).GetComponent<UITournamentItem>();
                item.transform.parent = parentGameObject.transform;
                item.ShowTournament(tourneys[i]);
                item.uITournamentList = this;
            }
        }
    }
}
