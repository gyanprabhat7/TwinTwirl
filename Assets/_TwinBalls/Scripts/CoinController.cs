using UnityEngine;
using System.Collections;

namespace SpinDots
{
    public class CoinController : MonoBehaviour
    {

        public float angularSpeed = 1.5f;

        public float bouceForce = 0.25f;

        private bool stop;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(Bounce());
            StartCoroutine(Rotate());
        }

        public void GoUp()
        {
            stop = true;
            StartCoroutine(Up());
        }

        IEnumerator Rotate()
        {
            while (true)
            {
                //            transform.Rotate(Vector3.up * 2f);
                transform.Rotate(Vector3.up * angularSpeed);
                yield return null;
            }
        }

        IEnumerator Bounce()
        {
            while (true)
            {
                float bounceTime = 1f;

                float startY = transform.position.y;
                //            float endY = startY + 0.5f;
                float endY = startY + bouceForce;

                float t = 0;
                while (t < bounceTime / 2f)
                {
                    if (stop)
                        yield break;
                    t += Time.deltaTime;
                    float fraction = t / (bounceTime / 2f);
                    float newY = Mathf.Lerp(startY, endY, fraction);
                    Vector3 newPos = transform.position;
                    newPos.y = newY;
                    transform.position = newPos;
                    yield return null;
                }

                float r = 0;
                while (r < bounceTime / 2f)
                {
                    if (stop)
                        yield break;
                    r += Time.deltaTime;
                    float fraction = r / (bounceTime / 2f);
                    float newY = Mathf.Lerp(endY, startY, fraction);
                    Vector3 newPos = transform.position;
                    newPos.y = newY;
                    transform.position = newPos;
                    yield return null;
                }
            }
        }

        //Move up
        IEnumerator Up()
        {
            float time = 1f;

            float startY = transform.position.y;
            float endY = startY + 10f;

            float t = 0;
            while (t < time / 2f)
            {
                t += Time.deltaTime;
                float fraction = t / (time / 2f);
                float newY = Mathf.Lerp(startY, endY, fraction);
                Vector3 newPos = transform.position;
                newPos.y = newY;
                transform.position = newPos;
                yield return null;
            }

            gameObject.SetActive(false);
            GetComponent<MeshCollider>().enabled = true;
            transform.position = Vector3.zero;
            transform.parent = CoinManager.Instance.transform;
        }
    }
}
