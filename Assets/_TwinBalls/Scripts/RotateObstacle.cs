using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public enum RotateDirection
    {
        CounterClockwise,           //turn around clock follow positive direction and diffenrence direction from clock's direction
        Clockwise                   //turn aorund clock follow negative direction and it have got same direction with clock's direction
    }

    public class RotateObstacle : MonoBehaviour
    {
        float alpha;

        [Header("Object's Rotate Direction")]
        public RotateDirection chooseRotateDirection;

        [Header("Angular speed of object")]
        public float angularSpeed;

        // Update is called once per frame
        void Update()
        {
            alpha += angularSpeed;
            ProcessRotateDirectionValue();
            if (alpha > 360f)
                alpha = 0f;
        }


        /// <summary>
        /// Process alpha angular when user choose rotate direction for object
        /// </summary>
        void ProcessRotateDirectionValue()
        {
            switch (chooseRotateDirection)
            {
                case RotateDirection.CounterClockwise:
                    transform.eulerAngles = new Vector3(0, 0, alpha);
                    break;
                case RotateDirection.Clockwise:
                    transform.eulerAngles = new Vector3(0, 0, -alpha);
                    break;
                default:
                    transform.eulerAngles = new Vector3(0, 0, alpha);
                    break;
            }
        }
    }
}
