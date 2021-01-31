using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player_Character_Controller pc = other.gameObject.GetComponent<Player_Character_Controller>();

        if (pc)
        {
            pc.isDead = true;

            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            if (gm)
                gm.Victory();
#if UNITY_EDITOR
            else
                Debug.LogError("Game Manager is not in Scene!");
#endif
        }
    }
}
