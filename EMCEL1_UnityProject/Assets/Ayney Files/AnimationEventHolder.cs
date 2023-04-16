using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHolder : MonoBehaviour
{
    public void AttackPlayerAnimEvent()
    {
        GetComponentInParent<EnemyNavMeshScript>().attackTarget();
    }

    public void ResetVarAnimEvent()
    {
        GetComponentInParent<EnemyNavMeshScript>().ResetVariables();
    }
}
