using System;
using UnityEngine;

namespace Challonge.Sample
{
    public class ShipCollision : MonoBehaviour
    {
        /// Summary:    The ship colliding with the bullet
        private ShipBase ship;


        /// Summary:    The game object holding bullet explosion
        public GameObject bulletExplosion;

        void Start()
        {
            ship = GetComponent<ShipBase>();
        }

        void OnTriggerEnter(Collider other)
        {
            //Grab the potential enemy ship you are colliding with
            EnemyShip enemy = other.GetComponent<EnemyShip>();

            // Ignore collision if the object is not an enemy ship
            if (enemy == null)
                return;          

            //Instantiate the explosion
            Instantiate(bulletExplosion, transform.position, transform.rotation);

            //Destroy both the player and the enemy
            ship.TakeDamage(ship.health);
            enemy.TakeDamage(enemy.health);
        }
    }
}

