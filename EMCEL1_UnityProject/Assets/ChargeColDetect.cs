using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Control;
public class ChargeColDetect : MonoBehaviour
{
    BossAbilityScript BossScript;

    private void Start()
    {
        BossScript = transform.parent.parent.parent.GetComponentInChildren<BossAbilityScript>();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided with   " + other.transform.name);
        if(other.transform.tag == "Player" && BossScript.Charging)
        {
            
            BossScript.StopAllCoroutines();
            BossScript.Charging = false;
            BossScript.DoingAbility = false;
            PlayerStatus.changeHealth?.Invoke(-(int)BossScript.ChargeDamage);
        }else if(other.transform.tag == "Map" && BossScript.Charging)
        {
            Debug.Log("COLLIDED WITH MAP");
            BossScript.StartCoroutine(BossScript.StunTimer());
            BossScript.Charging = false;
            BossScript.DoingAbility = false;
        }
    }

    
}
