using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online
{
    [RequireComponent(typeof(Collider))]
    public class Switch : MonoBehaviour
    {
        [SerializeField] bool isSwitchOn = false;
        [SerializeField] float timeToRemainOn;
        [SerializeField] AudioClip switchOnSound;
        [SerializeField] AudioClip switchOffSound;
        [SerializeField] GameObject onGameObject;
        [SerializeField] GameObject offGameObject;


        AudioSource audioSource;

        void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != 8) // If not a player, return
                return;

            TurnOnSwitch();
        }

        void TurnOnSwitch()
        {
            if (audioSource && switchOffSound)
                audioSource.PlayOneShot(switchOnSound);

            if (offGameObject)
                offGameObject.SetActive(false);
            if (onGameObject)
                onGameObject.SetActive(true);

            isSwitchOn = true;
            Invoke("TurnOffSwitch", timeToRemainOn);
        }
        void TurnOffSwitch()
        {
            if (audioSource && switchOffSound)
                audioSource.PlayOneShot(switchOffSound);

            if (offGameObject)
                offGameObject.SetActive(true);
            if (onGameObject)
                onGameObject.SetActive(false);

            isSwitchOn = false;
        }

        public bool IsSwitchOn()
        {
            return isSwitchOn;
        }
    }
}