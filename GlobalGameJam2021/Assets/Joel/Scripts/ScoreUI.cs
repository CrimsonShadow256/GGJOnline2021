using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

namespace GGJ_Online {
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI totalScoreText;
        [SerializeField] float pulseFontSize = 45.0f;
        [SerializeField] float pulseTime = 0.5f;
        [SerializeField] Color scoreIncreaseColor;
        [SerializeField] Color scoreDecreaseColor;
        [SerializeField] TextMeshProUGUI popUpText;
        [SerializeField] float popUpStartFontSize = 60.0f;

        private float defaultFontSize;
        private Color defaultFontColor;

        private void Awake()
        {
            defaultFontSize = totalScoreText.fontSize;
            defaultFontColor = totalScoreText.color;
        }

        void Start()
        {
            ScoreManager.ResetScore();
            ScoreManager.RegisterScoreChangeEvent(UpdateScoreDisplay);

            totalScoreText.text = FormatScore(0.0f);
        }

        void UpdateScoreDisplay(float totalScore, float pointsAdded)
        {
            StartCoroutine(PulseText(totalScore, pointsAdded));
        }

        private string FormatScore(float score)
        {
            return ("$ " + score.ToString("F2"));
        }

        IEnumerator PulseText(float totalScore, float pointsToAdd)
        {
            float t = 0.0f;

            if (pointsToAdd > Mathf.Epsilon)
            {
                popUpText.color = scoreIncreaseColor;
                popUpText.text = "+ " + FormatScore(pointsToAdd);
            }
            else if (pointsToAdd < Mathf.Epsilon)
            {
                popUpText.color = scoreDecreaseColor;
                popUpText.text = "- " + FormatScore(pointsToAdd);
            }
            else
            {
                t = 1.0f;
            }



            while(t < 1.0f)
            {
                t += (Time.deltaTime / pulseTime);

                if (t > 1.0f)
                    t = 1.0f;

                if (t >= 0.5f)
                {
                    totalScoreText.text = FormatScore(totalScore);
                    InterpolateFontSize((t-0.5f)*2);
                }

                InterpolatePopUpFontSize(t);
                yield return null;
            }

            /*
            if (totalScore > Mathf.Epsilon)
                totalScoreText.color = scoreIncreaseColor;
            else if (totalScore < Mathf.Epsilon)
                totalScoreText.color = scoreDecreaseColor;
            else
                totalScoreText.color = defaultFontColor;
            */
            popUpText.text = "";
        }

        private void InterpolateFontSize(float t)
        {
            t = -4.0f*(t-0.5f)*(t-0.5f)+1; // Tweening function     y = -((x-0.5)*2)^2+1
            totalScoreText.fontSize = Mathf.Lerp(defaultFontSize, pulseFontSize, t);
        }
        private void InterpolatePopUpFontSize(float t)
        {
            t = (1 - t)*(1-t); // Tweening function
            popUpText.fontSize = Mathf.Lerp(0.0f, popUpStartFontSize, t);
        }
    }
}
