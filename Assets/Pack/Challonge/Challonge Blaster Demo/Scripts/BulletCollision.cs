using UnityEngine;
using System.Collections;

namespace Challonge.Sample
{
    public class BulletCollision : MonoBehaviour
    {

        /// Summary:    The ship checking collision
        private ShipBase ship;


        /// Summary:    The game object holding bullet explosion
        public GameObject bulletExplosion;

        void Start()
        {
            ship = GetComponent<ShipBase>();
        }

        void OnTriggerEnter(Collider other)
        {
            //Attempt to grab bullet component
            ShipBullet bullet = other.GetComponent<ShipBullet>();

            //Ignore if its not a bullet
            if (bullet == null)
                return;

            // ignore Boundary and collision between similar types
            if (other.name == "Boundary")
                return;
            if (bullet.bulletType == BulletType.Enemy && ship.shipType == ShipType.Enemy)
                return;
            if (bullet.bulletType == BulletType.Player && ship.shipType == ShipType.Player)
                return;

            //Instantiate explosion effect
            Instantiate(bulletExplosion, transform.position, transform.rotation);

            //Take Player damage
            ship.TakeBulletDamage(bullet);

            //Destroy Bullet
            Destroy(bullet.gameObject);
        }
    }
}
