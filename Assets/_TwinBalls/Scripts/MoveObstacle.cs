using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public enum MoveDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    public class MoveObstacle : MonoBehaviour
    {

        public float moveSpeed;

        public MoveDirection moveDirectionInit;

        public bool isLimit;                            //This value show it has got limited left or right?

        public float timeDelay;                         //Value time delay of obstacle is limit

        Vector3 leftStartPos;                           //left start position of obstacle which have got direction is left

        Vector3 rightStartPos;                          //right start position of obstacle which have got direction is right

        Vector3 moveDir;                                //Move direction of obstacle

        Rigidbody body;

        void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        void Start()
        {
            ProcessMoveDirectionDefault();

        }

        void FixedUpdate()
        {
            //		transform.Translate (moveDirection * moveSpeed * Time.deltaTime);
            body.velocity = moveDir * moveSpeed * Time.deltaTime;
        }

        void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag == "Obstacle")
            {
                moveDir *= -1;
                if (isLimit)
                    StartCoroutine(ProcessWhenObstacleIsLimit());
            }
        }

        IEnumerator ProcessWhenObstacleIsLimit()
        {
            yield return new WaitForSeconds(timeDelay);
            moveDir *= -1;
        }

        /// <summary>
        /// Processes default value when user choose move direction
        /// </summary>
        void ProcessMoveDirectionDefault()
        {
            switch (moveDirectionInit)
            {
                case MoveDirection.Right:
                    moveDir = Vector3.right;
                    break;
                case MoveDirection.Left:
                    moveDir = Vector3.left;
                    break;
                case MoveDirection.Up:
                    moveDir = Vector3.up;
                    break;
                case MoveDirection.Down:
                    moveDir = Vector3.down;
                    break;
                default:
                    break;
            }
        }
    }
}
