using UnityEngine;
using System.Collections;

namespace SpinDots
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        protected Transform playerTransform;
        private Vector3 velocity = Vector3.zero;
        private Vector3 originalDistance;

        [Header("Camera Follow Smooth-Time")]
        [HideInInspector]
        public float smoothTime = 0;

        [Header("Shaking Effect")]
        // How long the camera shaking.
        public float shakeDuration = 0.1f;
        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.2f;
        public float decreaseFactor = 0.3f;
        [HideInInspector]
        public Vector3 originalPos;

        private float currentShakeDuration;
        private float currentDistance;
        Vector3 targetPos;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            smoothTime = 0;
        }

        void Start()
        {
            StartCoroutine(WaitingPlayerController());
        }

        void OnEnable()
        {
            CharacterScroller.SelectCurCharacter += ChangeCharacter;
        }

        void OnDisable()
        {
            CharacterScroller.SelectCurCharacter -= ChangeCharacter;
        }

        IEnumerator WaitingPlayerController()
        {
            yield return new WaitForSeconds(0.05f);
            playerTransform = GameManager.Instance.playerController.transform;
            originalDistance = transform.position - playerTransform.transform.position;
        }

        void LateUpdate()
        {
            if (GameManager.Instance.GameState == GameState.Playing && playerTransform != null)
            {

                targetPos = playerTransform.position + originalDistance;
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
            }
        }

        public void FixPosition()
        {
            transform.position = playerTransform.position + originalDistance;
        }

        public void ShakeCamera()
        {
            StartCoroutine(Shake());
        }

        IEnumerator Shake()
        {
            originalPos = transform.position;
            currentShakeDuration = shakeDuration;
            while (currentShakeDuration > 0)
            {
                transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
                currentShakeDuration -= Time.deltaTime * decreaseFactor;
                yield return null;
            }
            transform.position = originalPos;
        }

        void ChangeCharacter(int cur)
        {
            StartCoroutine(WaitingPlayerController());
        }
    }
}
