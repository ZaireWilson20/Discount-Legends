using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challonge.Sample
{
    public class EnemyShip : ShipBase
    {
        #region Properties

        public int scoreOnDeath = 10;

        public GameObject pointsGained;

        #endregion

        #region Unity Methods

        public override void Start()
        {
            base.Start();

            shipRigidbody.velocity = transform.forward * speed;
            InvokeRepeating("HandleFire", fireRate, fireRate);
        }

        #endregion

        #region Helper Methods

        public override void HandleFire()
        {
            base.HandleFire();

            nextFire = Time.time + fireRate;

            GameObject BulletObject = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            ShipBullet newBullet = BulletObject.GetComponent<ShipBullet>();

            newBullet.shootingShip = this;
            newBullet.bulletType = BulletType.Enemy;

            GetComponent<AudioSource>().Play();
        }

        public override void HandleDeath()
        {
            base.HandleDeath();

            Destroy(this.gameObject);
        }

        public override void TakeBulletDamage(ShipBullet bullet)
        {
            base.TakeBulletDamage(bullet);

            GameObject gained = Instantiate(pointsGained, transform.position, Quaternion.identity);

            ((PlayerShip)bullet.shootingShip).AddToScore(this.scoreOnDeath);
        }

        #endregion
    }
}
