using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] float ColorTime;
    public Color HitColorValue;
    public Color[] DefaultColor;

    [SerializeField] Material[] EnemyMaterials;
    private void Start()
    {
        EnemyMaterials = GetComponent<Renderer>().materials;

        DefaultColor = new Color[EnemyMaterials.Length];

        for (int i = 0; i < EnemyMaterials.Length; i++)
        {
            if(EnemyMaterials[i].HasColor("_ColorMultiply"))
            {
                DefaultColor[i] = EnemyMaterials[i].GetColor("_ColorMultiply");
            } 
            else if (EnemyMaterials[i].HasColor("_BaseColor"))
            {
                DefaultColor[i] = EnemyMaterials[i].GetColor("_BaseColor");
            }
            else if (EnemyMaterials[i].HasColor("_Color") || EnemyMaterials[i].color != null)
            {
                DefaultColor[i] = EnemyMaterials[i].color;
            }
            else
            {
                Debug.Log("NO COLOR FOUND ON MATERIAL " + i);
            }
            
        }
    }

    public void HitEffectOnMat()
    {
        StartCoroutine(HitColor());
    }
    public IEnumerator HitColor()
    {
        for (int i = 0; i < EnemyMaterials.Length; i++)
        {
            EnemyMaterials[i].SetColor("_BaseColor", HitColorValue);
            EnemyMaterials[i].color = HitColorValue;
            EnemyMaterials[i].SetColor("_ColorMultiply", HitColorValue);
        }
        
        yield return new WaitForSeconds(ColorTime);

        for (int i = 0; i < EnemyMaterials.Length; i++)
        {
            EnemyMaterials[i].SetColor("_BaseColor", DefaultColor[i]);
            EnemyMaterials[i].SetColor("_ColorMultiply", DefaultColor[i]);
            EnemyMaterials[i].color = DefaultColor[i];
        }
        
    }
}
