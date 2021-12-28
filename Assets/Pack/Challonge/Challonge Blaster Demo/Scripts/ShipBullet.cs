using UnityEngine;

namespace Challonge.Sample
{
    public enum BulletType
    {
        Enemy,
        Player
    }

    // A stub class that we use to check if an object is a Bullet (we could have used a Tag instead).
    public class ShipBullet : MonoBehaviour
    {
        //Bullet Stats
        public float speed;
        public int damage = 1;

        //Shooting Info
        public BulletType bulletType;
        public ShipBase shootingShip;

        Rigidbody bulletRigidBody;

        void Start()
        {
            //Set rigidbody
            bulletRigidBody = GetComponent<Rigidbody>();

            //Initialize constant speed
            bulletRigidBody.velocity = transform.forward * speed;

            //Destroy on time
            Invoke("TimedDestroy", 1.25f);
        }

        public void Update()
        {

        }

        public void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void TimedDestroy()
        {
            Destroy(gameObject);
        }

    }
}
