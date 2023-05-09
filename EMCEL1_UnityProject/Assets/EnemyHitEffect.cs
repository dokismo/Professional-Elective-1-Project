using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] float ColorTime;
    [SerializeField] Color HitColorValue;

    Material[] EnemyMaterials;
    private void Start()
    {
        EnemyMaterials = GetComponent<Renderer>().materials;
    }

    public void HitEffectOnMat()
    {
        StartCoroutine(HitColor());
    }
    public IEnumerator HitColor()
    {
        for (int i = 0; i < EnemyMaterials.Length; i++)
        {
            EnemyMaterials[i].color = HitColorValue;
            EnemyMaterials[i].SetColor("_ColorMultiply", HitColorValue);
        }
        
        yield return new WaitForSeconds(ColorTime);

        for (int i = 0; i < EnemyMaterials.Length; i++)
        {
            EnemyMaterials[i].SetColor("_ColorMultiply", Color.white);
            EnemyMaterials[i].color = Color.white;
        }
        
    }
}
