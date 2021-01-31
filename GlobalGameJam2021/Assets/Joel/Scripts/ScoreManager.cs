using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ_Online
{
    public class ScoreManager:MonoBehaviour
    {
        public delegate void ScoreChangeEvent(float totalScore, float pointsAdded);

        #region Private_Variables
        private ScoreChangeEvent scoreChanged;
        private static ScoreManager instance;
        private static float score;
        #endregion

        #region Public_Statics

        private void Awake()
        {
            instance = this;
        }
        public static void AddScore(float points)
        {
            score += points;
            AlertListeners(points);
        }

        public static void ResetScore()
        {
            score = 0.0f;

        }

        public static void RegisterScoreChangeEvent(ScoreChangeEvent eventFunction)
        {
            CheckAndInstantiateScoreChangeEvent();
            instance.scoreChanged += eventFunction;
        }

        public static float GetScore()
        {
            return score;
        }

        private static void CheckAndInstantiateScoreChangeEvent()
        {
            if (instance.scoreChanged == null)
            {
                instance.scoreChanged = new ScoreChangeEvent(DebugScore);
            }
        }
        #endregion
        
        #region Private_Statics
        private static void AlertListeners(float points)
        {
            CheckAndInstantiateScoreChangeEvent();
            instance.scoreChanged(score, points);
        }

        private static void DebugScore(float totalScore, float pointsAdded)
        {
            #if UNITY_EDITOR
            Debug.Log("Score changed by " + pointsAdded + "\nNew Score: " + totalScore);
            #endif
        }
        #endregion
    }
}
