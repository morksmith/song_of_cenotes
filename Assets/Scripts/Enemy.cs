using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager Manager;
    public string EnemyName;
    public Transform Target;
    public Transform EnemyMesh;
    public bool LookAtPlayer = false;
    public enum EnemyType
    {
        Follow,
        Shoot,
        Directional,
        Float,
        Proximity,
        Mimic
    }
    public EnemyType Type = EnemyType.Follow;
    public float MaxHealth;
    public float Health;
    public float KnockBack = 100;
    public float Damage = 5;
    public float MoveSpeed = 5;
    private Rigidbody rb;
    public float AttackSpeed = 2;
    private float attackTimer;
    public GameObject Projectile;
    public GameObject LumenitePrefab;
    public GameObject UpgradePrefab;
    public GameObject HealthPrefab;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Target = GameObject.Find("Player").transform;
        Manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Update()
    {
       

        if (LookAtPlayer)
        {
            EnemyMesh.LookAt(Target);
        }

        if (Manager.State != GameManager.GameState.Playing)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

        if (Type == EnemyType.Follow)
        {
            rb.AddRelativeForce((Target.position - transform.position) * (MoveSpeed * 10) * Time.deltaTime);
        }
        else if(Type == EnemyType.Shoot)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > AttackSpeed)
            {
                var newProjectile = Instantiate(Projectile, transform.position, EnemyMesh.rotation);
                newProjectile.GetComponent<Projectile>().Target = Target;
                newProjectile.GetComponent<Projectile>().Damage = Damage;
                newProjectile.GetComponent<Projectile>().KnockBack = KnockBack;
                EnemyMesh.GetComponent<Animator>().SetTrigger("Attacking");
                attackTimer = 0;
            }
            
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform  == Target)
        {
            Target.GetComponent<Stats>().TakeDamage(Damage);
            Target.GetComponent<Rigidbody>().AddRelativeForce((Target.position - transform.position) * (KnockBack * 1000) * Time.deltaTime, ForceMode.VelocityChange);
        }
        
    }

    public void TakeDamage(float dmg)
    {
        var stats = Target.GetComponent<Stats>();
        stats.Energy += 1 * stats.EnergyGain;
        if(Health > dmg)
        {
            Health -= dmg;
        }
        else
        {
            var healthDrop = Random.Range(0, 5);
            var lumeniteDrop = Mathf.CeilToInt(Random.Range(1, 5) * stats.LumeniteDrop);
            var upgradeDrop = Random.Range(0, 100);
            for(var i = 0; i < lumeniteDrop; i++)
            {
                Instantiate(LumenitePrefab, transform.position, Quaternion.Euler(0,0,0));
            }
            if( healthDrop == 1)
            {
                Instantiate(HealthPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            }
            if(upgradeDrop < stats.UpgradeChance)
            {
                Instantiate(UpgradePrefab, transform.position, Quaternion.Euler(0, 0, 0));
            }
            Destroy(gameObject);
        }
    }
}
