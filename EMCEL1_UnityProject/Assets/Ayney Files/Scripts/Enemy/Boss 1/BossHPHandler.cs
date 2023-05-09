using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPHandler : MonoBehaviour
{
    public Slider BossHPBar;

    [SerializeField] EnemyHitEffect[] HitEffect;

    public float BossHP = 1000;
    private void Start()
    {
        BossFightManager.Instance?.AssignBossVar();
        HitEffect = GetComponentsInChildren<EnemyHitEffect>();
    }
    public void TakeDamage(int Dmg)
    {
        for (int i = 0; i < HitEffect.Length; i++)
        {
            HitEffect[i].HitEffectOnMat();
        }

        BossHP -= Dmg;
        BossHPBar.value = BossHP;

        if(BossHP < 0f)
        {
            Destroy(gameObject);
            NextSceneNameHolder.Instance.NextSceneName = "NextSceneSample";
            SceneLoader.Instance.NextScene();
        }
    }
}
