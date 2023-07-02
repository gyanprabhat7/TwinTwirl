using UnityEngine;
using System.Collections;

namespace SpinDots
{
    public class PlayerController : MonoBehaviour
    {
        public static event System.Action PlayerDied;
        private Rigidbody body;

        bool isAlive;

        public float moveSpeed = 200;

        public float angularSpeed = 2;

        public Color mainColor;

        float angularValue;

        float alpha = 0f;

        float originPosY;

        void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
            BallController.CollisionObstacle += Die;
        }

        void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
            BallController.CollisionObstacle -= Die;
        }

        void Awake()
        {
            body = GetComponent<Rigidbody>();
            originPosY = transform.position.y;
        }

        void Start()
        {
            angularValue = angularSpeed;
        }

        void Update()
        {
            if (isAlive)
            {
                alpha += angularValue;
                transform.localEulerAngles = new Vector3(0, 0, alpha);
                if (alpha > 360f)
                    alpha = 0f;
                if (Input.GetMouseButtonDown(0) && transform.position.y < originPosY)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.jump);
                    StartCoroutine(PushUpPlayer());
                }
            }
        }

        IEnumerator PushUpPlayer()
        {
            body.useGravity = false;
            body.velocity = Vector3.up * moveSpeed * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            //		yield return null;
            body.useGravity = true;
        }

        // Listens to changes in game state
        void OnGameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.Playing)
            {
                // Do whatever necessary when a new game starts
                StartCoroutine(EnableTrail());
                body.useGravity = true;
                isAlive = true;
            }
        }

        // Calls this when the player dies and game over
        public void Die()
        {
            // Fire event
            if (!IsInvoking())
            {
                StopAllCoroutines();
                Invoke("DelayBeforeShowUIOVer", 0);
            }

        }

        void DelayBeforeShowUIOVer()
        {
            if (PlayerDied != null)
            {
                PlayerDied();
                StopAllCoroutines();
                Invoke("SelfDestroy", 0.02f);
            }
        }

        void SelfDestroy()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        IEnumerator EnableTrail()
        {
            yield return new WaitForSeconds(0.4f);
            if (GameManager.Instance.GameState == GameState.Playing)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    TrailRenderer trailRender = transform.GetChild(i).GetComponent<TrailRenderer>();
                    if (trailRender != null)
                        trailRender.enabled = true;
                    trailRender.startColor = new Color(mainColor.r, mainColor.g, mainColor.b, 1);
                    trailRender.endColor = new Color(mainColor.r, mainColor.g, mainColor.b, 0);
                }
            }
        }
    }
}
