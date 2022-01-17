using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Stats PlayerStats;
    public enum RarityLevels
    {
        Common,
        Uncommon,
        Rare
    }
    public RarityLevels Rarity;
    public string UpgradeName;
    public enum StatUpgrades
    {
        Health,
        Stamina,
        EnergyGain,
        HealthRegen,
        StaminaRegen,
        EnergyBars,
        MoveSpeed,
        DashDistance,
        DashCost,
        AttackCost,
        Damage,
        SpecialDamage,
        ReflectDamage,
        DamageBlastChance,
        DashBlastChance,
        DashFreezeChance,
        DashPoisonChance,
        BossDamage,
        BombDamage,
        ExtraLives,
        SwordFreezeChance,
        SwordRange,
        GunPoisonChance,
        GunBulletSize,
        PulsarChainChance,
        PulsarRange,
        EnemyDropChance,
        LumeniteFound,
        StartEnergy,
        StartBombs,
        StartKeys,
        StartLives,
        StartRelic,
        DashShieldTime
    }
    public StatUpgrades UpgradeStat;
    public float UpgradeAmount;

    void Start()
    {
        PlayerStats = GameObject.FindObjectOfType<Stats>();
    }

    public void ApplyUpgrade()
    {
        if(UpgradeStat == StatUpgrades.Health)
        {
            PlayerStats.MaxHealth += UpgradeAmount;
        }
    }
}
