using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public class BallController : MonoBehaviour
    {

        public static event System.Action CollisionObstacle;

        float alpha;

        public float angularSpeed;

        // Update is called once per frame
        void Update()
        {
            alpha += angularSpeed;
            transform.eulerAngles = new Vector3(alpha, alpha, alpha);
            if (alpha > 360f)
                alpha = 0f;
        }

        void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag == "Obstacle")
            {
                //			Debug.Log ("Dung Obstacle");
                if (CollisionObstacle != null)
                    CollisionObstacle();
            }
        }

        void OnTriggerEnter(Collider target)
        {
            if (target.gameObject.tag == "Gold")
            {
                CoinManager.Instance.AddCoins(1);
                SoundManager.Instance.PlaySound(SoundManager.Instance.coin);
                Destroy(target.gameObject);
            }
        }
    }
}
