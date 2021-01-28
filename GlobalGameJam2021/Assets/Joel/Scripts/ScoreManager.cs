using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online
{
    public class ScoreManager
    {
        public delegate void ScoreChangeEvent(float totalScore, float pointsAdded);

        #region Private_Variables
        private static ScoreChangeEvent scoreChanged;
        private static float score;
        #endregion

        #region Public_Statics
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
            scoreChanged += eventFunction;
        }

        private static void CheckAndInstantiateScoreChangeEvent()
        {
            if (scoreChanged == null)
            {
                scoreChanged = new ScoreChangeEvent(DebugScore);
            }
        }
        #endregion
        
        #region Private_Statics
        private static void AlertListeners(float points)
        {
            CheckAndInstantiateScoreChangeEvent();
            scoreChanged(score, points);
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
