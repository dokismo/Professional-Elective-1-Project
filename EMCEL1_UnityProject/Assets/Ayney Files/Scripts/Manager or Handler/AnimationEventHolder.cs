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

    public void LoadAsync(string StringName)
    {
        StartCoroutine(SceneLoader.Instance.LoadAsynch(NextSceneNameHolder.Instance.NextSceneName));
    }

    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
