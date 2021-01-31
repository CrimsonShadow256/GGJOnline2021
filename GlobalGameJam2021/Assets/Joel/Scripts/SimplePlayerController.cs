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
        private Animator anim;
        private float currentMaxSpeed;
        private bool inFiringStance = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * 5.0f);
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Fire");
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("ReadyWeapon", true);
                inFiringStance = true;
            }
            else if (Input.GetKeyUp(KeyCode.Joystick1Button6) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("ReadyWeapon", false);
                inFiringStance = false;
            }
        }

        private void Movement()
        {
            if (!inFiringStance)
                currentMaxSpeed = maxSpeed;
            else
                currentMaxSpeed = 1.0f;

            Vector3 input = new Vector3(-Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * currentMaxSpeed;
            Vector3 camForward = Camera.main.transform.forward;
            camForward.y = 0;
            float rotationAngle = Vector3.Angle(Vector3.right, camForward);//Mathf.Cos(camForward.x / camForward.magnitude) * 180 / Mathf.PI;

            if (camForward.z < 0.0f)
            {
                rotationAngle = 360 - rotationAngle;
            }

            input = Quaternion.AngleAxis(rotationAngle, Vector3.up) * input;
            input.y = input.x;
            input.x = input.z;
            input.z = input.y;
            input.y = 0.0f;

            Debug.Log("Cam:" + camForward.normalized + " Input:" + input);

            rb.velocity = Vector3.MoveTowards(rb.velocity, input, acceleration * Time.deltaTime);

            // Stop Spinning if no input
            if (input.sqrMagnitude > 0.01f)
                transform.LookAt(transform.position + input);

            anim.SetFloat("Speed", rb.velocity.magnitude);
        }
    }
}
