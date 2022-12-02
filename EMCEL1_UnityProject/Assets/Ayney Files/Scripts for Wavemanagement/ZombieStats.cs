using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie_Stats", menuName = "Zombie Stats")]
public class ZombieStats : ScriptableObject
{
    [Header("Default Stats")]
    public float Health;
    public float Damage;
    public float Speed;

    [Header("Multipliers")]
    public float HealthMultiplier;
    public float DamageMultiplier;
    public float SpeedMultiplier;
}
