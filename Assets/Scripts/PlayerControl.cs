using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Stats Stats;
    public float DashDistance;
    public float DashCost = 2;
    public float AttackCost = 2;
    public Transform PlayerMesh;
    public GameManager Manager;
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
    public GameObject BulletPrefab;
    public GameObject PulsePrefab;
    public GameObject BlastEffect;

    private bool startDash = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    public void Update()
    {
        if(Manager.State == GameManager.GameState.Paused)
        {
            
            return;
            
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(PlayerAttack.Type == PlayerAttack.WeaponType.Gun)
            {
                PlayerAttack.Type = PlayerAttack.WeaponType.Pulse;
            }
            else if (PlayerAttack.Type == PlayerAttack.WeaponType.Pulse)
            {
                PlayerAttack.Type = PlayerAttack.WeaponType.Sword;
            }
            else if (PlayerAttack.Type == PlayerAttack.WeaponType.Sword)
            {
                PlayerAttack.Type = PlayerAttack.WeaponType.Gun;
            }
        }
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
                        startDash = true;
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
        if (Manager.State == GameManager.GameState.Paused)
        {

            return;

        }
        if (startDash)
        {
            var newVelocity = (moveVector.normalized * (DashDistance * 50) * Time.deltaTime);
            Dash(newVelocity);
            attackTimer = 0;
            dashTimer = 0;
            startDash = false;
        }
        var xRot = Mathf.Clamp(rb.velocity.x * 10, -20, 20);
        rb.AddRelativeForce(moveVector * (Stats.MoveSpeed * 10) * Time.deltaTime, ForceMode.VelocityChange);
        if (rb.velocity.x > 0)
        {
            PlayerMesh.eulerAngles = new Vector3(xRot, 90, 0);
        }
        else if(rb.velocity.x < 0)
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
        rb.velocity = dir;
        Stats.ConsumeStamina(DashCost);

    }

    public void Attack()
    {
        if (PlayerAttack.Type == PlayerAttack.WeaponType.Sword)
        {
            Anim.SetTrigger("Melee Attack");
            SwordTrail.gameObject.GetComponent<TrailEnd>().StartTrail();
            AttackAnimation.SetTrigger("Attacking");

        }
        else if (PlayerAttack.Type == PlayerAttack.WeaponType.Gun)
        {
            Anim.SetTrigger("Gun Attack");
            var newBullet = Instantiate(BulletPrefab, transform.position + new Vector3(0, 1, 0) + PlayerMesh.forward * 2, Quaternion.Euler(0, PlayerMesh.transform.eulerAngles.y, 0));
            newBullet.GetComponent<Bullet>().Damage = Stats.Damage;
            var newBlast = Instantiate(BlastEffect, transform.position + new Vector3(0, 1, 0) + PlayerMesh.forward * 2, transform.rotation);
            newBlast.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);


        }
        else if (PlayerAttack.Type == PlayerAttack.WeaponType.Pulse)
        {
            Anim.SetTrigger("Pulse Attack");
            var newPulse = Instantiate(PulsePrefab, transform.position + new Vector3(0, 0.6f, 0), transform.rotation);
            newPulse.GetComponent<Pulse>().Damage = Stats.Damage;
            var newBlast = Instantiate(BlastEffect, transform.position + new Vector3(0, 0.6f, 0), transform.rotation);

        }
        Stats.ConsumeStamina(AttackCost);
        
        attackTimer = 0;

    }
}
