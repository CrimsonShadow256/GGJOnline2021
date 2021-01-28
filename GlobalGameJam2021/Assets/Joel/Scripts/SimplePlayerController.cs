using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_Online
{
    public class SimplePlayerController : MonoBehaviour
    {
        [Header("This Component Is For Testing Purposes Only")]
        [Header("Janky code and bad practices. DO NOT USE!")]

        [SerializeField] float maxSpeed = 5.0f;
        [SerializeField] float acceleration = 5.0f;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * maxSpeed;

            Debug.Log(input);
            rb.velocity = Vector3.MoveTowards(rb.velocity, input, acceleration * Time.deltaTime);
            transform.LookAt(transform.position + input);
        }
    }
}
