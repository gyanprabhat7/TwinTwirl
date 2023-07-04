 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public class ObstacleController : MonoBehaviour
    {

        public static ObstacleController Instance;

        public GameObject coin;

        public float distanceBetweenTwoObstacles;       //Distance between two obstacles

        public Transform checkPos;

        public Transform[] coinPositionList;

        public Transform pointPos;

        bool isBelowCheckPos;                           //Position value, it is used to check player's postion and create new obstacle

        bool wasGotPoint;                               //Player has already passed this obstacle?

        bool wasMakeCOin;                               //This obstacle has already made coins?

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
            isBelowCheckPos = false;
            wasMakeCOin = false;
            wasGotPoint = false;
        }

        void FixedUpdate()
        {
            ProcessSpawnerNewObstacle();
            AddPointWhenPlayerPassObstacle();

        }

        /// <summary>
        /// Process spawner new obstacle when player passed check position of the old obstacle
        /// </summary>
        void ProcessSpawnerNewObstacle()
        {
            if (!isBelowCheckPos)
            {
                if (GameManager.Instance.playerController)
                {
                    if (GameManager.Instance.playerController.transform.position.y < checkPos.position.y)
                    {
                        isBelowCheckPos = true;
                        GameObject newObstacle = Instantiate(ObstacleManager.Instance.obstacleList[ObstacleManager.Instance.GetIndexOfObstacleList()]);
                        newObstacle.transform.position = new Vector3(0, transform.position.y - distanceBetweenTwoObstacles, 0);
                        if (!newObstacle.activeSelf)
                            newObstacle.SetActive(true);
                        ProcessSpawnerCoins();
                    }
                }
            }
        }

        /// <summary>
        /// Process increase point for player when he pass the old obstacle
        /// </summary>
        void AddPointWhenPlayerPassObstacle()
        {
            if (!wasGotPoint)
            {
                if (GameManager.Instance.playerController)
                {
                    if (GameManager.Instance.playerController.transform.position.y < pointPos.position.y)
                    {
                        wasGotPoint = true;
                        ScoreManager.Instance.AddScore(1);
                        SoundManager.Instance.PlaySound(SoundManager.Instance.unlock);
                        //					Debug.Log ("Diem tang: <color=red> " + ScoreManager.Instance.testPoint+" </color>");
                    }
                }
            }
        }


        /// <summary>
        /// Process spawner coin when player passed obstacle
        /// </summary>
        void ProcessSpawnerCoins()
        {
            if (!wasMakeCOin)
            {
                wasMakeCOin = true;
                if (coinPositionList.Length == 0)
                    return;
                for (int i = 0; i < coinPositionList.Length; i++)
                {
                    float proportionSpawner = Random.Range(0f, 1f);
                    if (proportionSpawner <= GameManager.Instance.coinFrequency)
                    {
                        GameObject newCoin = Instantiate(coin);
                        newCoin.transform.position = coinPositionList[i].position;
                    }
                }
            }
        }
    }
}

