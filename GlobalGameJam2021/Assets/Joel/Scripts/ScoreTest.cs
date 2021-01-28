using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online
{
    public class ScoreTest : MonoBehaviour
    {
        [SerializeField] float pointsToAdd = 50.0f;
        [SerializeField] KeyCode increaseKey = KeyCode.UpArrow;
        [SerializeField] KeyCode decreaseKey = KeyCode.DownArrow;

        void Update()
        {
            if (Input.GetKeyDown(increaseKey))
            {
                ScoreManager.AddScore(pointsToAdd);
            }
            else if (Input.GetKeyDown(decreaseKey))
            {
                ScoreManager.AddScore(-pointsToAdd);
            }
        }
    }
}
