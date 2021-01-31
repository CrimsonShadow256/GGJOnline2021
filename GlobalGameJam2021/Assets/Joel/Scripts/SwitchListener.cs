using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online {
    public class SwitchListener : MonoBehaviour
    {
        [SerializeField] Switch[] switchesToListenTo;
        [SerializeField] bool requireAllSwitchesToBeOn;
        [SerializeField] GameObject gameObjectToMove;
        [SerializeField] Transform offTransformPosition;
        [SerializeField] float timeToMoveToOffPosition;
        [SerializeField] Transform onTransformPosition;
        [SerializeField] float timeToMoveToOnPosition;

        float startTime;
        [SerializeField] bool moveToOnPosition;

        private void Awake()
        {
            TurnOff();
            startTime = -timeToMoveToOffPosition;
        }
        void Update()
        {
            MoveObject();

            if(switchesToListenTo != null)
            {
                foreach (Switch s in switchesToListenTo)
                {
                    if (s.IsSwitchOn())
                    {
                        if (requireAllSwitchesToBeOn)
                            continue;

                        TurnOn();
                        return;
                    }
                    else
                    {
                        if (requireAllSwitchesToBeOn)
                        {
                            TurnOff();
                            return;
                        }
                    }
                }

                if (requireAllSwitchesToBeOn)
                    TurnOn();
                else
                    TurnOff();
            }


        }

        private void TurnOn()
        {
            if (moveToOnPosition != true)
            {
                moveToOnPosition = true;
                startTime = Time.time;
            }
        }

        private void TurnOff()
        {
            if (moveToOnPosition != false)
            {
                moveToOnPosition = false;
                startTime = Time.time;
            }
        }

        void MoveObject()
        {
            float t = Time.time - startTime;
            if (moveToOnPosition)
            {
                t = t / timeToMoveToOnPosition;
                if (t > 1.0f)
                    t = 1.0f;
            }
            else
            {
                t = t / timeToMoveToOffPosition;
                t = 1 - t;
                if (t < 0.0f)
                    t = 0.0f;
            }

            //Debug.Log(t);
            gameObjectToMove.transform.position =  Vector3.Lerp(offTransformPosition.position, onTransformPosition.position, t);

        }
    }
}
