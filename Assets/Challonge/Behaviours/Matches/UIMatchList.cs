using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Challonge.Models;
using Challonge.Properties;

namespace Challonge.Behaviours.UI
{
    public class UIMatchList : MonoBehaviour
    {
        public GameObject parentGameObject;

        public GameObject uiMatchItemPrefab;

        /// Summary:    Show matches that are currently open and ready to play
        public bool showOpenMatches = true;

        /// Summary:    Show upcoming matches in later rounds
        public bool showPendingMatches;

        /// Summary:    Show completed matches
        public bool showCompleteMatches;

        /// Summary:    A filter specifying the round. Ignores filter if = 0
        public int roundFilter;

        /// Summary:    Identifier for the participant.
        public string participantID = "";

        /// Summary:    The total items per page.
        public int maxMatchesShown = 25;

        public MatchUnityEvent onUiMatchItemClickAction;

        public void ShowMatches(List<Models.Match> matches)
        {
            List<UIMatchItem> items = new List<UIMatchItem>(parentGameObject.transform.GetComponentsInChildren<UIMatchItem>(true));

            List<Challonge.Models.Match> matchesFinal = new List<Challonge.Models.Match>();

            for (int i = 0; i < matches.Count; i++)
            {
                if (roundFilter == 0 || roundFilter == matches[i].round)
                {
                    if (matches[i].matchState == MatchState.Complete && showCompleteMatches)
                        matchesFinal.Add(matches[i]);
                    else if (matches[i].matchState == MatchState.Open && showOpenMatches)
                        matchesFinal.Add(matches[i]);
                    else if (matches[i].matchState == MatchState.Pending && showPendingMatches)
                        matchesFinal.Add(matches[i]);
                }
            }

            for (int i = 0; i < items.Count; i++)
                DestroyImmediate(items[i].gameObject);

            for (int i = 0; i < matchesFinal.Count; i++)
            {
                if (i == maxMatchesShown)
                    break;
                UIMatchItem item = GameObject.Instantiate<GameObject>(uiMatchItemPrefab).GetComponent<UIMatchItem>();
                item.transform.parent = parentGameObject.transform;
                item.ShowMatch(matchesFinal[i]);
                item.uIMatchList = this;
            }
        }
        
    }
}
