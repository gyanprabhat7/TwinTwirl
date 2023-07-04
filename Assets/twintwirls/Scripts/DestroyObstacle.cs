using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public class DestroyObstacle : MonoBehaviour
    {


        void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag == "Obstacle")
            {
                Destroy(target.transform.parent.gameObject);
            }
        }

        void OnTriggerEnter(Collider target)
        {
            if (target.tag == "Gold")
            {
                //			Debug.Log ("DEstroy gold");
                Destroy(target.gameObject);
            }
        }

    }
}
