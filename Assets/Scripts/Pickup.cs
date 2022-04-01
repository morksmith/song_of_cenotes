using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Vector3 newPosition;
    private bool moveToPlayer = false;
    public enum PickupType
    {
        Lumenite,
        Health,
        Upgrade
    }
    public PickupType Type;
    public float StationaryTime = 2;
    private float timer = 0;
    private Menu upgradeMenu;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        newPosition = transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5,5), 0);
        player = GameObject.Find("Player").transform;
        upgradeMenu = GameObject.Find("Upgrade Menu").GetComponent<Menu>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.State == GameManager.GameState.Paused)
        {
            return;
        }
        if (timer < StationaryTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            moveToPlayer = true;
        }
        if (!moveToPlayer)
        {
            if (Vector3.Distance(transform.position, newPosition) > 0.15f)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
            }
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * (20 / Vector3.Distance(transform.position, player.position)));

        }
        if (Vector3.Distance(transform.position, player.position) < 0.3f)
        {
            var stats = player.GetComponent<Stats>();
            Destroy(gameObject);
            if(Type == PickupType.Lumenite)
            {
                stats.AddLumenite();
            }
            else if (Type == PickupType.Health)
            {
                stats.Health += Mathf.CeilToInt(stats.MaxHealth / 5);
                stats.Health = Mathf.Clamp(stats.Health, 0, stats.MaxHealth);
            }
            else if (Type == PickupType.Upgrade)
            {
                upgradeMenu.Enable();
            }
        }


    }
}
