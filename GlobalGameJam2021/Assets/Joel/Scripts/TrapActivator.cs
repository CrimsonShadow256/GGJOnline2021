using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online {
    public class TrapActivator : MonoBehaviour
    {
        public delegate void ActivateTrap();

        [Header("Score Activation")]
        [SerializeField] bool useSocreActivation;
        [SerializeField] float scoreToActivate;
        [Header("Time Activation")]
        [SerializeField] bool useTimeActivation;
        [Tooltip("Time in seconds")]
        [SerializeField] float timeToActivate;

        ActivateTrap activate;

        private void Awake()
        {
            activate = new ActivateTrap(TurnOnTrap);
        }
        void Start()
        {
            if(useSocreActivation)
                ScoreManager.RegisterScoreChangeEvent(ScoreChanged);

            if (useTimeActivation)
                Invoke("Activate", timeToActivate);
        }

        void ScoreChanged(float totalScore, float pointsAdded)
        {
            if (totalScore > scoreToActivate)
                Activate();
        }

        void TurnOnTrap()
        {
            #if UNITY_EDITOR
            Debug.Log("Trap activated: " + name);
            #endif
        }

        void Activate()
        {
            activate();
        }

        public void RegisterActivationFunction(ActivateTrap function)
        {
            activate += function;
        }
    }
}
