using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public class FollowPlayer : MonoBehaviour
    {

        public Transform playerTransform;

        Vector3 velocity = Vector3.zero;

        Vector3 offset;

        public float smoothTime;

        void OnEnable()
        {
            PlayerController.PlayerDied += TurnOffFollow;
            CharacterScroller.SelectCurCharacter += ChangeCharacter;
        }

        void OnDisable()
        {
            PlayerController.PlayerDied -= TurnOffFollow;
            CharacterScroller.SelectCurCharacter -= ChangeCharacter;
        }

        void ChangeCharacter(int cur)
        {
            StartCoroutine(WaitingPlayerController());
        }

        void TurnOffFollow()
        {
            Destroy(gameObject);
        }


        void Start()
        {
            StartCoroutine(WaitingPlayerController());
        }

        IEnumerator WaitingPlayerController()
        {
            yield return new WaitForSeconds(0.05f);
            playerTransform = GameManager.Instance.playerController.transform;
            offset = transform.position - playerTransform.position;
        }

        void FixedUpdate()
        {
            if (playerTransform != null)
            {
                Vector3 targetPos = playerTransform.position + offset;
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
            }
        }

    }
}
