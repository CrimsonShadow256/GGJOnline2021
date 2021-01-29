using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Tooltip("Bullet Speed")] [SerializeField] float projectileSpeed;
   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(0, 0, projectileSpeed * Time.deltaTime);
    }
}
