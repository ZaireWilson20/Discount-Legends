using UnityEngine;
using System.Collections;

namespace Challonge.Sample
{
    public enum ShipType
    {
        Player,
        Enemy
    }

    public class ShipBase : MonoBehaviour
    {
        #region Global Variables

        //Stat
        public int health = 3;
        private int maxHealth;
        public float speed;
        public ShipType shipType;

        //Attributes
        protected float tiltFactor = 3;
        public float fireRate = .4f;

        //Components
        protected Rigidbody shipRigidbody;
        public GameObject deathExplosion;
        public GameObject bulletPrefab;
        public Transform bulletSpawn;

        //Misc
        protected float nextFire = 0.0f;
        protected Vector3 smoothDirection;
        protected float smoothing = 5;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            this.maxHealth = health;
        }

        public virtual void Start()
        {
            shipRigidbody = GetComponent<Rigidbody>();
        }

        public  virtual void FixedUpdate()
        {
            HandleTilt();

        }

        #endregion

        #region Helper Methods

        /// Function:   HandleTilt
        ///
        /// Summary:    Tilts player ship based on speed
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        private void HandleTilt()
        {
            shipRigidbody.rotation = Quaternion.Euler(0, 0, shipRigidbody.velocity.x * -tiltFactor);
        }


        /// Function:   HandleFire
        ///
        /// Summary:    Handles the shooting
        ///
        /// Author: Khalil
        ///
        /// Date:   11/30/2021
        public virtual void HandleFire()
        {

        }

        private void OnDestroy()
        {
            CancelInvoke();
        }


        /// Function:   TakeBulletDamage
        ///
        /// Summary:    Take bullet damage.
        ///
        /// Author: Khalil
        ///
        /// Date:   11/30/2021
        ///
        /// Parameters:
        /// bullet -    The bullet.
        public virtual void TakeBulletDamage(ShipBullet bullet)
        {
            TakeDamage(bullet.damage);            
        }


        /// Function:   TakeDamage
        ///
        /// Summary:    Take damage.
        ///
        /// Author: Khalil
        ///
        /// Date:   11/30/2021
        ///
        /// Parameters:
        /// damage -    The damage.
        public void TakeDamage(int damage)
        {
            //Ignore if dead
            if (health <= 0)
                return;

            //Apply Damage
            health -= damage;

            //Die if health is zero
            if (health <= 0)
            {
                health = 0;
                HandleDeath();
            }
        }

        /// Function:   HandleDeath
        ///
        /// Summary:    Handles the death of the player
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        public virtual void HandleDeath()
        {
            //Spawn explosion
            Instantiate(deathExplosion, transform.position, transform.rotation);
        }


        /// Function:   Spawn
        ///
        /// Summary:    Spawns this object. (For Debug use only)
        ///
        /// Author: Khalil
        ///
        /// Date:   11/25/2021
        public void Spawn()
        {
            gameObject.SetActive(true);
            health = maxHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        #endregion
    }
}
