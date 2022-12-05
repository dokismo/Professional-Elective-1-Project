using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Control;
using UnityEngine.AI;
public class ChargeColDetect : MonoBehaviour
{
    BossAbilityScript BossScript;

    float KnockbackTime = 0.2f;
    float DoKnockBack;

    Collider KnockbackObject;

    private void Start()
    {
        BossScript = transform.parent.parent.parent.GetComponentInChildren<BossAbilityScript>();
    }

    private void Update()
    {
        if(DoKnockBack > 0)
        {
            KnockbackObject.transform.GetComponent<CharacterController>().Move(-transform.forward * 20f * Time.deltaTime);
            DoKnockBack -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player" && BossScript.Charging)
        {
            BossScript.Charging = false;
            BossScript.DoingAbility = false;
            BossScript.StopAllCoroutines();

            // Do Knockback (Will optimize and clean codes next time, I'm sorry ;_;)
            KnockbackObject = other;
            DoKnockBack = KnockbackTime;

            transform.parent.parent.parent.GetComponentInChildren<BossAbilityScript>().BossAnimator.SetBool("isCharging", false);

            PlayerStatus.changeHealth?.Invoke(-(int)BossScript.ChargeDamage);
        }else if(other.transform.tag == "Map" && BossScript.Charging)
        {
            BossScript.StartCoroutine(BossScript.StunTimer());
            BossScript.Charging = false;
            BossScript.DoingAbility = false;
        }
    }

    
}
