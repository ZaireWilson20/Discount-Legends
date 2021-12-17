using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

namespace Challonge.Sample
{
    public class PlayerShip : ShipBase
    {
        #region Properties

        /// Summary:    The boundary which the player movement stays in
        public Boundaries boundary;

        /// Summary:    Player score.
        public int score = 0;

        public GameController gameController;

        private bool isFiring;

        [NonSerialized]
        public Challonge.Models.Participant participant;

        public Color color;

        #endregion

        #region Unity Methods

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            KeepInBounds();
        }

        private void Update()
        {
            if (participant != null)
                score = participant.matchResult.score;
            HandleFire();
        }

        #endregion

        #region Helper Method


        /// Function:   OnFire
        ///
        /// Summary:    Executes the 'fire' action off the input controller
        ///
        /// Author: Khalil
        ///
        /// Date:   12/3/2021
        void OnFire()
        {
            isFiring = !isFiring;
        }


        /// Function:   OnMovement
        ///
        /// Summary:    Executes the 'movement' action off the input controller
        ///
        /// Author: Khalil
        ///
        /// Date:   12/3/2021
        ///
        /// Parameters:
        /// value -     The value.
        void OnMovement(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();

            //shipRigidbody.velocity = new Vector3(direction.x, 0.0f, direction.y) * speed;

            //Calculate Smooth Direction
            smoothDirection = Vector3.MoveTowards(smoothDirection, direction, smoothing);

            //Grab direction values
            direction = smoothDirection;
            Vector3 movement = new Vector3(direction.x, 0, direction.y);

            //Set Speed
            shipRigidbody.velocity = movement * speed;
        }

        void OnMouseMovement(InputValue value)
        {
            HandleMouseControls();
        }


        /// Function:   HandleMouseControls
        ///
        /// Summary:    Handles the mouse controls.
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        private void HandleMouseControls()
        {
            if (shipRigidbody == null)
                return;

            //Grab Mouse Position
            Vector3 pos = Mouse.current.position.ReadValue();

            //Adjust mouse to camera and get center of ship
            pos.z = Camera.main.transform.position.y + 1;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            //Get current mouse position and directions
            Vector2 currentPosition = new Vector3(pos.x, pos.z);
            Vector3 directionRaw = pos - origin;
            Vector3 direction = directionRaw.normalized;
            
            //Calculate Smooth Direction
            smoothDirection = Vector3.MoveTowards(smoothDirection, direction, smoothing);

            //Grab direction values
            direction = smoothDirection;
            Vector3 movement = new Vector3(direction.x, 0, direction.z);

            //Set Speed
            shipRigidbody.velocity = movement * speed;
        }


        /// Function:   KeepInBounds
        ///
        /// Summary:   Keeps the Player ship in bounds
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        private void KeepInBounds()
        {
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );
        }     

        /// Function:   HandleFire
        ///
        /// Summary:    Handles player fire.
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        public override void HandleFire()
        {
            // Check Input and Fire Rate
            if (isFiring && Time.time > nextFire)
            {
                //Calculate the next shot
                nextFire = Time.time + fireRate;

                //Get Ship Bullet component
                GameObject BulletObject = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                ShipBullet newBullet = BulletObject.GetComponent<ShipBullet>();

                //Set Bullet to player type
                newBullet.shootingShip = this;
                newBullet.bulletType = BulletType.Player;

                GetComponent<AudioSource>().Play();
            }
        }

        public override void HandleDeath()
        {
            base.HandleDeath();

            //Remove ship from game
            gameObject.SetActive(false);
        }


        public void AddToScore(int addedScore)
        {
            this.participant.matchResult.score += addedScore;
            gameController.AddToTeamScore(addedScore);
        }

        #endregion
    }
}
