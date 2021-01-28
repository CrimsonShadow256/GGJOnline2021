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

            SetTotalScoreText(0.0f);
        }

        void UpdateScoreDisplay(float totalScore, float pointsAdded)
        {
            SetTotalScoreText(totalScore);
            StartCoroutine(PulseText(pointsAdded > 0.0f));
        }

        private void SetTotalScoreText(float score)
        {
            totalScoreText.text = "$ " + score.ToString("F2");
        }

        IEnumerator PulseText(bool scoreIncreased)
        {
            if (scoreIncreased)
                totalScoreText.color = scoreIncreaseColor;
            else
                totalScoreText.color = scoreDecreaseColor;

            float t = 0.0f;

            while(t < 1.0f)
            {
                t += (Time.deltaTime / pulseTime);

                if (t > 1.0f)
                    t = 1.0f;

                InterpolateFontSize(t);
                yield return null;
            }

            totalScoreText.color = defaultFontColor;
        }

        private void InterpolateFontSize(float t)
        {
            t = -4.0f*(t-0.5f)*(t-0.5f)+1; // Tweening function     y = -((x-0.5)*2)^2+1
            totalScoreText.fontSize = Mathf.Lerp(defaultFontSize, pulseFontSize, t);
        }
    }
}
