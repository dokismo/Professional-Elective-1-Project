using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie5EffectScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.transform.tag == "Enemy")
        {
            if(other.GetComponent<EnemyHitScript>() != null)
            {
                other.GetComponent<EnemyHitScript>().StoppingTheCoroutine();
                other.GetComponent<EnemyHitScript>().isInRangeForZ5Effect = true;
                other.GetComponent<EnemyHitScript>().DmgReductionMultiplier = 0.7f; // 30% dmg reduction
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Enemy")
        {
            if (other.GetComponent<EnemyHitScript>() != null)
            {
                other.GetComponent<EnemyHitScript>().isInRangeForZ5Effect = false;
                other.GetComponent<EnemyHitScript>().DmgReductionMultiplier = 1; // No dmg reduction
            }
        }
    }

  
}
