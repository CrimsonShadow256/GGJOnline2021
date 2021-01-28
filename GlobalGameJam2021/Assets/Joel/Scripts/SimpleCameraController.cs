using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    private Vector3 initialOffset;

    private void Start()
    {
        initialOffset = target.position - transform.position;
    }
    void LateUpdate()
    {
        transform.position = target.position - initialOffset;
    }
}
