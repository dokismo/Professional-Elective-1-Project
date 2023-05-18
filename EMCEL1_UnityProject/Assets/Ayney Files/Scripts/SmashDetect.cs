using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Control;

public class SmashDetect : MonoBehaviour
{
    public float SmashDmg;

    private void Start()
    {
        StartCoroutine(ColliderTimeAlive());
    }
    IEnumerator KnockbackTarget(Transform target, float Duration)
    {
        float duration = Duration;

        Vector3 KnockbackDirection = -target.forward; // Must be transform.right instead of .forward because we rotated the mesh 90 degrees,
                                                       // so the forward direction is the X axis.
        PlayerStatus.changeHealth?.Invoke(-(int)SmashDmg);
        StartCoroutine(target.GetComponent<Movement>().Stunned(Duration));
        // Do the knockback
        while (duration > 0f)
        {
            target.GetComponent<CharacterController>().Move(KnockbackDirection * SmashDmg/4 * Time.deltaTime);
            duration -= Time.deltaTime;

            // Using this will call the While loop every frame in a coroutine, instead of letting the while loop happen instantly in one frame.
            yield return null;
        }

        yield break;
    }

    IEnumerator ColliderTimeAlive()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(KnockbackTarget(other.transform, 0.4f));
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
