using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Tooltip("Bullet Speed")] [SerializeField] float projectileSpeed;
   
    private void Start()
    {
      
    }
    private void Update()
    {

        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
}
