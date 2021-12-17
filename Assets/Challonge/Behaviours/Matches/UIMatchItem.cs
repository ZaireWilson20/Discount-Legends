using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Challonge;
using Challonge.API;
using Challonge.Models;
using Challonge.Properties;
using System;
using Challonge.Behaviours;

namespace Challonge.Behaviours.UI
{
    public abstract class UIMatchItem : MonoBehaviour
    {
        /// Summary:    Specifies the match.
        public Match match;

        /// Summary:    Live Match Data.
        public API.Data.ChallongeMatch challongeMatch;

        /// Summary:    List of i tournaments.
        [HideInInspector]
        public UIMatchList uIMatchList;

        /// Summary:    The scope.
        protected Scope scope;

        public abstract void ShowMatch(Match match);

        /// Function:   ApplyMatchSettings
        ///
        /// Summary:    Applies the match settings.
        ///
        /// Author: Ahmed
        public void ApplyMatchSettings()
        {
            challongeMatch.ApplyMatchSettings(this.match);
        }

        public void ClickButtonActions()
        {
            uIMatchList.onUiMatchItemClickAction.Invoke(match);
        }
    }
}