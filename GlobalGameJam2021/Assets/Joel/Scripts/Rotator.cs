using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Euler Angel Rotation Speeds")]
    [SerializeField] Vector3 degreesPerSecond;

    [Header("Random Rotation Properties")]
    [SerializeField] bool useRandomRotation;
    [SerializeField] float minDegreesPerSecond;
    [SerializeField] float maxDegreesPerSecond;

    private void Awake()
    {
        if (useRandomRotation)
        {
            degreesPerSecond = new Vector3(GetRandomDPS(), GetRandomDPS(), GetRandomDPS());
        }
    }
    void Update()
    {
        transform.Rotate(degreesPerSecond * Time.deltaTime);
    }

    private float GetRandomDPS()
    {
        return Random.Range(minDegreesPerSecond, maxDegreesPerSecond);
    }
}
