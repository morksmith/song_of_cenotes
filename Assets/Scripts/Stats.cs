using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public float MaxStamina;
    public float Stamina;
    public float StaminaRefill;
    public GameObject EnergyBarParent;
    public GameObject EnergyBarPrefab;
    public float EnergySlots = 3;
    public float MaxEnergy;
    public float Energy;
    public float StaminaCooldown;
    private float staminaTimer;
    public float Damage = 1;
    public float Lumenite = 0;
    public float LumeniteDrop = 1;
    public Attack MeleeAttack;

    public Slider HealthBar;
    public Slider StaminaBar;
    public List<Slider> EnergyBars;

    public void Start()
    {
        for(var i = 0; i < EnergySlots; i++)
        {
            var eBar = Instantiate(EnergyBarPrefab, EnergyBarParent.transform);
            EnergyBars.Add(eBar.GetComponent<Slider>());
        }
        MeleeAttack.Damage = 1 * Damage;
        UpdateUI();

    }

    public void Update()
    {
        UpdateUI();
        staminaTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Energy++;
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            Energy--;
        }

        if(staminaTimer > StaminaCooldown)
        {
            if (Stamina < MaxStamina)
            {
                Stamina += StaminaRefill * Time.deltaTime;
            }
        }
        

    }

    public void UpdateUI()
    {
        HealthBar.value = Health / MaxHealth;
        StaminaBar.value = Stamina / MaxStamina;
        for(var i = 0; i < EnergyBars.Count; i++)
        {
            var singleBarTotal = MaxEnergy / EnergySlots;
            EnergyBars[i].value = (Energy / singleBarTotal) - i;
        }
    }

    public void ConsumeStamina(float i)
    {
        Stamina -= i;
        staminaTimer = 0;
    }

    public void TakeDamage(float d)
    {
        Health -= d;
    }

}
