using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Character_Controller : MonoBehaviour
{
    [Header("Movement Variables")]
    [Tooltip("Controls Move Speed")] [SerializeField] float moveSpeed = 3.5f;
    [Tooltip("Controls Run Speed Modifier")] [SerializeField] float runModifier = 2.0f;
    bool hMove = false, jump = false, fMove = false;
    float forwardMovement;



    enum CharacterState { Walk, Jump,Run, Idle, PistolAttack,Crash,HandAttack,WalkL,WalkR};
    [Header("Character State")]
    [Tooltip("Check or Change Character State")] [SerializeField] CharacterState currentState;
    [Tooltip("Needed for Pitch Rotation")] [SerializeField] GameObject playerCamera;
    Animator pAnim;
    

    [Header("Rotation Variables")]
    [Tooltip("Rotation speed")][SerializeField]float speedH = 2.0f;
    [Tooltip("Pitch Rotation Speed")][SerializeField]float speedV = 2.0f;
    float yaw = 0.0f;
    float pitch = 0.0f;

    bool armed = false;


    [Header("Punching Variables")]
    [Tooltip("Punch Cooldown")] [SerializeField] float punchCooldown = 2.0f;
    float nextPunch;
    bool hasPunched = false;
    


    // Start is called before the first frame update
    void Start()
    {
        Initializer();
    }
    void Initializer()
        {
            pAnim = GetComponentInChildren<Animator>();
            currentState = CharacterState.Idle;
        }
    // Update is called once per frame
    void Update()
    {
        SwitchStatement();
        FourWayMovement();
        LookRotation();
        RunningMovement();
        RaiseWeapon();
        PunchAttack();
    }

    private void PunchAttack()
    {
        if (Input.GetMouseButton(0) && !armed && Time.time > nextPunch)
        {
            nextPunch = Time.time + punchCooldown;
            pAnim.SetTrigger("Punch");
            hasPunched = true;
        }
        if (hasPunched)
        {
            currentState = CharacterState.HandAttack;
        }
    }

    private void RaiseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.E) && !armed)
        {
            armed = true;
            pAnim.SetBool("Armed", true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && armed)
        {
            armed = false;
            pAnim.SetBool("Armed", false);
        }
    }

    private void RunningMovement()
    {
        if (fMove && Input.GetKey(KeyCode.LeftShift) && forwardMovement > 0.1f)
        {
            transform.Translate(0, 0, (moveSpeed + runModifier) * Time.fixedDeltaTime);
            currentState = CharacterState.Run;

        }
    }

    private void LookRotation()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(0, yaw, 0.0f);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
    }

    private void FourWayMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        forwardMovement = Input.GetAxis("Vertical");
        if (horizontalMovement > 0.1f || horizontalMovement < -0.1f)
        {
            hMove = true;

        }
        else
        {
            hMove = false;
            currentState = CharacterState.Idle;
        }

        if (forwardMovement > 0.1f || forwardMovement < -0.1f)
        {
            fMove = true;
            currentState = CharacterState.Walk;
        }
        else
        {
            fMove = false;
            currentState = CharacterState.Idle;
        }

        if (hMove)
        {

            if (horizontalMovement > 0.1f)
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                currentState = CharacterState.WalkR;
            }
            else if (horizontalMovement < -0.1f)
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                currentState = CharacterState.WalkL;
            }
            else
            {
                currentState = CharacterState.Idle;
            }

        }

        if (fMove)
        {

            if (forwardMovement > 0.1f)
            {
                transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            }
            else if (forwardMovement < -0.1f)
            {
                transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
            }

        }
    }

    
    private void SwitchStatement()
    {
        switch (currentState)
        {
            case CharacterState.Crash:
                Crash();
                break;

            case CharacterState.HandAttack:
                HandAttack();
                break;

            case CharacterState.Idle:
                Idle();
                break;

            case CharacterState.Jump:
                Jump();
                break;

            case CharacterState.PistolAttack:
                PistolAttack();
                break;

            case CharacterState.Run:
                Run();
                break;

            case CharacterState.Walk:
                Walk();
                break;

            case CharacterState.WalkL:
                WalkL();
                break;

            case CharacterState.WalkR:
                WalkR();
                break;

            default: Debug.Log("Out of States, Check code or switchStatement method in player character");
                break;
        }
    }

    private void WalkR()
    {
        pAnim.SetBool("Run", false);
        pAnim.SetBool("Walk", false);
        pAnim.SetBool("WalkL", false);
        pAnim.SetBool("WalkR", true);
    }

    private void WalkL()
    {
        pAnim.SetBool("Run", false);
        pAnim.SetBool("Walk", false);
        pAnim.SetBool("WalkR", false);
        pAnim.SetBool("WalkL", true);
    }

    private void Walk()
    {
        pAnim.SetBool("Run", false);
        pAnim.SetBool("Walk", true);
    }

    private void Run()
    {
        pAnim.SetBool("Walk", false);
        pAnim.SetBool("Run", true);
    }

    private void PistolAttack()
    {
        
    }

    private void Jump()
    {
        
    }

    private void Idle()
    {
        pAnim.SetBool("Run", false);
        pAnim.SetBool("Walk", false);
        pAnim.SetBool("WalkR", false);
        pAnim.SetBool("WalkL", false);
    }

    private void HandAttack()
    {
        pAnim.SetBool("Run", false);
        pAnim.SetBool("Walk", false);
        pAnim.SetBool("WalkR", false);
        pAnim.SetBool("WalkL", false);
        hasPunched = false;
    }

    private void Crash()
    {
        
    }
}
