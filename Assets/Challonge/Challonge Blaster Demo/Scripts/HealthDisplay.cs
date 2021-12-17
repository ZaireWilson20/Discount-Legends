using System;
using System.Collections.Generic;
using Challonge;
using Challonge.Properties;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Challonge.Sample
{
    public class HealthDisplay : MonoBehaviour
    {
        public Image healthRing;
        public Image playerRing;
        public TextMeshProUGUI score;
        public TextMeshProUGUI username;

        public Color greenHealth;
        public Color yellowHealth;
        public Color redHealth;

        public PlayerShip playerShip;

        [NonSerialized]
        public Models.Participant participant;

        private void Awake()
        {
            
        }

        public void InitHud(PlayerShip playerShip, Models.Participant participant, Color color)
        {
            this.playerShip = playerShip;
            this.participant = participant;
            username.text = participant.name;
            healthRing.fillAmount = 1;
            score.text = "0";
            this.playerRing.color = color;
            this.username.color = color;
            this.score.color = color;
        }

        private void Update()
        {
            healthRing.fillAmount = (float)playerShip.health / (float)playerShip.GetMaxHealth();
            score.text = participant.matchResult.score.ToString();

            if (healthRing.fillAmount <= .33f)
                healthRing.color = redHealth;
            else if (healthRing.fillAmount > .33f && healthRing.fillAmount < .66f)
                healthRing.color = yellowHealth;
            else
                healthRing.color = greenHealth;
        }
    }
}
