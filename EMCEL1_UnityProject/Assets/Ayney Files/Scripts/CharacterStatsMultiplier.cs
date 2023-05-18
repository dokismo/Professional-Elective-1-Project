using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character/Multiplier", fileName = "Character Multiplier")]
public class CharacterStatsMultiplier : ScriptableObject
{
    public float DamageReductionMultiplier;
    public float MoneyMultiplier;
    public float AttackDamageMultiplier;
    public float StaminaRegenMultiplier;
    public float SpeedMultiplier;

}
