using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float DashDistance;
    public Transform PlayerMesh;
    public FloatingJoystick Joystick;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    public float SwipeTime;
    private float swipeTimer;
    public float SwipeDistance;  //minimum distance for a swipe to be registered
    public Animator AttackAnimation;
    public float AttackCooldown;
    private float attackTimer;
    private Rigidbody rb;
    private Vector3 moveVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        attackTimer += Time.deltaTime;
        moveVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        moveVector = Joystick.Direction;
        rb.AddRelativeForce(moveVector * (MoveSpeed *10) * Time.deltaTime, ForceMode.VelocityChange);
        if (Input.touchCount == 0)
        {
            return;
        }
        swipeTimer += Time.deltaTime;
        Touch touch = Input.GetTouch(0); // get the touch
        if (touch.phase == TouchPhase.Began) //check for the first touch
        {
            fp = touch.position;
            lp = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
        {
            lp = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
        {
            lp = touch.position;
            if (Mathf.Abs(lp.x - fp.x) > 0.33f || Mathf.Abs(lp.y - fp.y) > 0.3f)
            {
                if (lp.x > fp.x)
                {
                    PlayerMesh.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    PlayerMesh.eulerAngles = new Vector3(0, -90, 0);
                }
            }
                
            if (swipeTimer < SwipeTime)
            {
                Debug.Log(Mathf.Abs(lp.x - fp.x));
                
                if (Mathf.Abs(lp.x - fp.x) > SwipeDistance || Mathf.Abs(lp.y - fp.y) > SwipeDistance)
                {
                    Dash(lp - fp);
                    swipeTimer = 0;
                }
                else
                {
                    if(attackTimer > AttackCooldown)
                    {
                        Attack();
                        swipeTimer = 0;
                    }
                    

                }
            }
            swipeTimer = 0;
        }

    }


    public void Dash(Vector3 dir)
    {
        rb.AddRelativeForce(dir.normalized * (DashDistance * 100) * Time.deltaTime, ForceMode.Impulse);
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        AttackAnimation.SetTrigger("Attacking");
        attackTimer = 0;
    }
}
