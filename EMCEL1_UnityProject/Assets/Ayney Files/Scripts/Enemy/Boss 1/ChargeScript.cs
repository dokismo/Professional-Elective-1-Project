using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Control;

public class ChargeScript : MonoBehaviour
{
    [SerializeField] Boss1Script BossScript;
    float KnockbackDuration = 0.5f, StunDuration = 3f;
    [SerializeField] ParticleSystem WallHitParticle;

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("COLLIDED WITH PLAYER");
            StartCoroutine(KnockbackTarget(collision.transform, KnockbackDuration));
            StartCoroutine(FindObjectOfType<CameraEffectsHandler>().CameraShake(1f, 0.5f));
        }

        if(collision.transform.tag == "Map")
        {
            Debug.Log("Collided with MAP");
            StartCoroutine(Stunned());
            StartCoroutine(FindObjectOfType<CameraEffectsHandler>().CameraShake(0.5f, 0.5f));
            Instantiate(WallHitParticle, collision.contacts[0].point, Quaternion.FromToRotation(transform.parent.forward, collision.contacts[0].normal));
        }
    }
    IEnumerator KnockbackTarget(Transform target, float Duration)
    {
        float duration = Duration;
        
        Vector3 KnockbackDirection = -transform.right; // Must be transform.right instead of .forward because we rotated the mesh 90 degrees,
                                                       // so the forward direction is the X axis.

        StartCoroutine(target.GetComponent<Movement>().Stunned(Duration));
        PlayerStatus.changeHealth?.Invoke(-(int)BossScript.ChargeDamage);
        BossScript.StopCharge();
        // Do the knockback
        while (duration > 0f)
        {
            target.GetComponent<CharacterController>().Move(KnockbackDirection * BossScript.ChargePower * Time.deltaTime);
            duration -= Time.deltaTime;

            // Using this will call the While loop every frame in a coroutine, instead of letting the while loop happen instantly in one frame.
            yield return null;
        }
        
        yield break;
    }

    IEnumerator Stunned()
    {
        BossScript.StopCharge();
        BossScript.IsStunned = true;
        yield return new WaitForSeconds(StunDuration);
        BossScript.BossNavmesh.isStopped = false;
        BossScript.IsStunned = false;
    }
    
}
