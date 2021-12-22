using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Stats Stats;
    public float DashDistance;
    public float DashCost = 2;
    public float AttackCost = 2;
    public Transform PlayerMesh;
    //public FloatingJoystick Joystick;
    //private Vector3 fp;   //First touch position
    //private Vector3 lp;   //Last touch position
    //public float SwipeTime;
    //private float swipeTimer;
    //public float SwipeDistance;  //minimum distance for a swipe to be registered
    public Animator AttackAnimation;
    public float AttackCooldown;
    private float attackTimer;
    public float DashCooldown;
    public Animator Anim;
    public PlayerAttack PlayerAttack;
    private float dashTimer;
    private Rigidbody rb;
    private Vector3 moveVector;
    public TrailRenderer SwordTrail;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    public void Update()
    {
        
        attackTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        moveVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        if (Stats.Stamina > 0)
        {
            if (moveVector.magnitude > 0.1f)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    if (dashTimer > DashCooldown)
                    {
                        Dash(moveVector);
                        attackTimer = 0;
                        dashTimer = 0;
                    }

                }

            }
            if (Input.GetButtonDown("Attack") && dashTimer > DashCooldown)
            {
                if (attackTimer > AttackCooldown)
                {
                    Attack();
                    attackTimer = 0;
                    dashTimer = 0;
                }
            }
        }
    }
    public void FixedUpdate()
    {
        var xRot = Mathf.Clamp(rb.velocity.x * 10, -20, 20);
        rb.AddRelativeForce(moveVector * (MoveSpeed * 10) * Time.deltaTime, ForceMode.VelocityChange);
        if (rb.velocity.x > 0)
        {
            PlayerMesh.eulerAngles = new Vector3(xRot, 90, 0);
        }
        else
        {
            PlayerMesh.eulerAngles = new Vector3(-xRot, -90, 0);

        }


        var maxX = Mathf.Clamp(rb.velocity.x, -100, 100);
        var maxY = Mathf.Clamp(rb.velocity.y, -100, 100);
        rb.velocity = new Vector3(maxX, maxY, 0);

    }


    public void Dash(Vector3 dir)
    {
        Debug.Log(dir);
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            Anim.SetTrigger("Dash Forward");
        }
        else if (dir.y > 0)
        {
            Anim.SetTrigger("Dash Up");
        }
        else
        {
            Anim.SetTrigger("Dash Down");
        }
        rb.velocity = (dir.normalized * (DashDistance * 100) * Time.deltaTime);
        Stats.ConsumeStamina(DashCost);

    }

    public void Attack()
    {
        if (PlayerAttack.Type == PlayerAttack.WeaponType.Sword)
        {
            Anim.SetTrigger("Melee Attack");
            SwordTrail.gameObject.GetComponent<TrailEnd>().StartTrail();

        }
        else if (PlayerAttack.Type == PlayerAttack.WeaponType.Gun)
        {
            Anim.SetTrigger("Gun Attack");
        }
        else if (PlayerAttack.Type == PlayerAttack.WeaponType.Pulse)
        {
            Anim.SetTrigger("Pulse Attack");
        }
        Debug.Log("Attack!");
        Stats.ConsumeStamina(AttackCost);
        AttackAnimation.SetTrigger("Attacking");
        attackTimer = 0;

    }
}
