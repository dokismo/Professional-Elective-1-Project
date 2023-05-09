using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Player;

public class CameraEffectsHandler : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] Transform OriginalCameraPosition;
    [SerializeField] Transform CameraTransform;

    [SerializeField] PostProcessVolume PostProcessingVolume;
    [SerializeField] PostProcessProfile EffectsProfile;
        
    [Header("Variables")]
    [SerializeField] AnimationCurve ScreenShakeAnimCurve;
    [SerializeField] Vignette CameraVignette;
    [SerializeField] PlayerStatusScriptable PlayerStats;

    public float HealthPercent => PlayerStats.health / PlayerStats.maxHealth;
    public float MinVignetteIntensity;
    public float MaxVignetteIntensity;

    float ConstMinVI;
    float ConstMaxVI;
    float MaxAddIntensity = 0.7f;
    float VignetteIntensity = 0f;

    public float MinVignetteFrequency;
    public float MaxVignetteFrequency;
    
    public float VignetteFrequency = 0f;

    [SerializeField] float test;
   
    // For looping between adding and subtracting vignette.
    bool AddSanity = true;

    private void Awake()
    {
        PostProcessingVolume = GameObject.Find("PostFX").GetComponent<PostProcessVolume>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null && Player != gameObject)
        {
            Debug.Log(Player.transform.position);
            Destroy(Player);
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
        ConstMinVI = MinVignetteIntensity;
        ConstMaxVI = MaxVignetteIntensity;
        EffectsProfile = PostProcessingVolume.profile;
        for (int i = 0; i < EffectsProfile.settings.Count; i++)
        {
            if (EffectsProfile.settings[i].name == "Vignette")
            {
                CameraVignette = (Vignette)EffectsProfile.settings[i];
            }
        }
        PostProcessingVolume.profile.TryGetSettings(out CameraVignette);

        
    }

    void Update()
    {
        SanityEffect();
    }

    void SanityEffect()
    {
        VignetteFrequency = Mathf.Lerp(MinVignetteFrequency, MaxVignetteFrequency, HealthPercent);
        float Min = ConstMinVI + MaxAddIntensity - 0.2f;
        float Max = ConstMaxVI + MaxAddIntensity;
        
        MinVignetteIntensity = Mathf.Lerp(ConstMinVI, Min, 1-HealthPercent);
        MaxVignetteIntensity = Mathf.Lerp(ConstMaxVI, Max, 1-HealthPercent);

        // Loop to keep vignette
        if (AddSanity)
        {
            VignetteIntensity = Mathf.Lerp(VignetteIntensity, MaxVignetteIntensity, Time.deltaTime / VignetteFrequency);
            if (VignetteIntensity >= MaxVignetteIntensity - 0.01f) AddSanity = false;
        }
        else
        {
            VignetteIntensity = Mathf.Lerp(VignetteIntensity, MinVignetteIntensity, Time.deltaTime / VignetteFrequency);
            if (VignetteIntensity <= MinVignetteIntensity + 0.01f) AddSanity = true;
        }
        CameraVignette.intensity.value = VignetteIntensity;
    }
  
    public IEnumerator CameraShake(float Duration, float Intensity)
    {
        float duration = Duration;
        float DefaultDuration = duration;
        float TimeElapsed = 0f;
        while(duration > 0)
        {
            TimeElapsed += Time.deltaTime;
            float NormalizingValue = ScreenShakeAnimCurve.Evaluate(TimeElapsed / DefaultDuration);
            CameraTransform.position = OriginalCameraPosition.position + Random.insideUnitSphere * Intensity * NormalizingValue;
            duration -= Time.deltaTime;
            yield return null;
        }

        CameraTransform.position = OriginalCameraPosition.position;
        yield break;
    }
}
