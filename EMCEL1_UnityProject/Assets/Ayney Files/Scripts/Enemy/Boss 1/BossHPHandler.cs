using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPHandler : MonoBehaviour
{
    public Slider BossHPBar;

    [SerializeField] EnemyHitEffect[] HitEffect;

    
    public float BossHP = 1000;
    public float BossMaxHp;

    public bool HasRaged = false;
    private void Start()
    {
        BossFightManager.Instance?.AssignBossVar();
        HitEffect = GetComponentsInChildren<EnemyHitEffect>();
        BossMaxHp = BossHP;
    }
    public void TakeDamage(int Dmg)
    {
        if(BossFightManager.Instance.BossFightStarted)
        {
            for (int i = 0; i < HitEffect.Length; i++)
            {
                HitEffect[i].HitEffectOnMat();
            }

            BossHP -= Dmg;
            BossHPBar.value = BossHP;


            if (GetComponent<Boss2Script>() && BossHP <= BossMaxHp / 2 && !HasRaged)
            {

                GetComponent<Boss2Script>().Scream();
                GetComponent<Boss2Script>().ApplyEnragedStats();
                HasRaged = true;
            }
            if (BossHP < 0f)
            {
                if (!GetComponent<Boss2Script>())
                {
                    Destroy(gameObject);
                    NextSceneNameHolder.Instance.NextSceneName = "NextSceneSample";
                    SceneLoader.Instance.NextScene();
                }
                else
                {
                    GetComponent<Boss2Script>().IsDead = true;
                }

            }
        }
        
    }
}
