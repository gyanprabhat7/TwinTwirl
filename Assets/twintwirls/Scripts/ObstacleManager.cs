using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinDots
{
    public class ObstacleManager : MonoBehaviour
    {

        public static ObstacleManager Instance;

        public GameObject[] obstacleList;

        //	[Header("Sample Space of Spawner Coin")]
        //	public int sampleSpaceProbablity; 			//Sample space of probability. 

        int[] indexList;

        int countCallTimes;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            indexList = Utilities.GenerateShuffleIndices(obstacleList.Length);
        }

        void Start()
        {
            countCallTimes = 0;
        }


        public int GetIndexOfObstacleList()
        {
            if (countCallTimes >= indexList.Length - 1)
            {
                countCallTimes = 0;
                indexList = Utilities.GenerateShuffleIndices(obstacleList.Length);
                return countCallTimes;
            }
            countCallTimes++;
            return indexList[countCallTimes];
        }

    }
}
