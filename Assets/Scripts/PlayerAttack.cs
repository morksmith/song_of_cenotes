using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum WeaponType
    {
        Sword,
        Pulse,
        Gun
    }
    public WeaponType Type = WeaponType.Sword;
    public GameObject Sword;
    public GameObject Gun;
    public GameObject Pulsar;
    public GameObject MeleeWeapon;


    // Update is called once per frame
    void Update()
    {   
        if(Type == WeaponType.Pulse)
        {
            Pulsar.SetActive(true);
            Sword.SetActive(false);
            Gun.SetActive(false);
        }
        else if (Type == WeaponType.Sword)
        {
            Pulsar.SetActive(false);
            Sword.SetActive(true);
            Gun.SetActive(false);
        }
        else if (Type == WeaponType.Gun)
        {
            Pulsar.SetActive(false);
            Sword.SetActive(false);
            Gun.SetActive(true);
        }
        
    }
}
