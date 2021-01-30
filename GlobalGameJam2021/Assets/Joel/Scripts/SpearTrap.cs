using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online {
    public class SpearTrap : MonoBehaviour
    {
        enum SpearState { Up, Down, Retracting, Extending };
        [SerializeField] SpearState state = SpearState.Down;

        [SerializeField] bool trapIsEnabled = false;
        [SerializeField] float downTime = 5.0f;
        [SerializeField] float upTime = 1.0f;
        [SerializeField] float retractTime = 2.0f;
        [SerializeField] float extendTime = 0.5f;
        [SerializeField] int pulsesInDownState = 3;

        [SerializeField] Transform spears;
        [SerializeField] AudioClip extendSound;
        [SerializeField] AudioClip retractSound;
        [SerializeField] AudioClip enabledSound;

        const float upYPosition = 1.6f;
        const float downYPosition = 0.0f;

        float timeInState;
        AudioSource audio;

        private void Awake()
        {
            audio = GetComponent<AudioSource>();
        }
        void Start()
        {
            timeInState = 0.0f;
            TrapActivator activator = GetComponent<TrapActivator>();
            if(activator)
                activator.RegisterActivationFunction(EnableSpearTrap);
        }

        private void Update()
        {
            if (trapIsEnabled)
                timeInState += Time.deltaTime;
            else
                timeInState = 0.0f;

            if (state == SpearState.Down) DownState();
            else if (state == SpearState.Extending) ExtendingState();
            else if (state == SpearState.Up) UpState();
            else if (state == SpearState.Retracting) RetractingState();
        }

        void ChangeState(SpearState newState)
        {
            timeInState = 0.0f;
            state = newState;

            if (newState == SpearState.Extending)
            {
                audio.clip = extendSound;
                audio.Play();
            }
            else if(newState == SpearState.Retracting)
            {
                audio.clip = retractSound;
                audio.Play();
            }
        }
        void EnableSpearTrap()
        {
            audio.clip = enabledSound;
            audio.Play();
            trapIsEnabled = true;

            ChangeState(SpearState.Down);
        }

        void DownState()
        {
            float t = NormalizedTime(downTime);
            if (t >= 1.0f)
                ChangeState(SpearState.Extending);

            //Tweening func
            t = -Mathf.Abs(Mathf.Cos(t * 10.0f * pulsesInDownState / Mathf.PI)) + 1.0f;
            SetSpearPosition(t*0.2f);
        }
        void ExtendingState()
        {
            float t = NormalizedTime(extendTime);
            if (t >= 1.0f)
                ChangeState(SpearState.Up);

            SetSpearPosition(t);

        }
        void UpState()
        {
            SetSpearPosition(1.0f);

            if (NormalizedTime(upTime) >= 1.0f)
                ChangeState(SpearState.Retracting);
        }
        void RetractingState()
        {
            float t = NormalizedTime(retractTime);
            if (t >= 1.0f)
                ChangeState(SpearState.Down);

            SetSpearPosition(1-t);

        }

        float NormalizedTime(float fullStateTime)
        {
            return timeInState / fullStateTime;
        }

        void SetSpearPosition(float t)
        {
            spears.position = new Vector3(spears.position.x, Mathf.Lerp(downYPosition, upYPosition, t), spears.position.z);
        }
    }
}
