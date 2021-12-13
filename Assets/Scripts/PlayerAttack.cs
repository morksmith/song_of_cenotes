using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum WeaponType
    {
        Melee,
        AOE,
        Gun
    }
    public WeaponType Type = WeaponType.Melee;
    public GameObject MeleeWeapon;
    public Animator AttackAnimation;
    public float TapTime;
    public float AttackCooldown;
    private float attackTimer;
    private float tapTimer;


    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (Input.touchCount == 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0); // get the touch
        if(touch.phase == TouchPhase.Began)
        {
            tapTimer = 0;
            
        }
        tapTimer += Time.deltaTime;
        if (touch.phase == TouchPhase.Ended)
        {
            if(tapTimer < TapTime)
            {
                Debug.Log($"Tapped, {tapTimer}");
                if(attackTimer > AttackCooldown)
                {
                    Attack();
                    
                }
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        AttackAnimation.SetTrigger("Attacking");
        tapTimer = 0;
        attackTimer = 0;
    }
}
