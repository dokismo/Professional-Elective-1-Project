using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Player;

public class CameraEffectsHandlerURP : MonoBehaviour
{
    public static CameraEffectsHandlerURP Instance;

    [Header("References")]
    [SerializeField] Transform OriginalCameraPosition;
    [SerializeField] Transform CameraTransform;

    [SerializeField] Volume PostProcessingVolume;
    [SerializeField] VolumeProfile EffectsProfile;

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
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        PostProcessingVolume = GameObject.Find("Post Processing").GetComponent<Volume>();
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
        for (int i = 0; i < EffectsProfile.components.Count; i++)
        {
            if (EffectsProfile.components[i].name == "Vignette")
            {
                CameraVignette = (Vignette)EffectsProfile.components[i];
            }
        }
        //PostProcessingVolume.profile.TryGetSettings(out CameraVignette);
        PostProcessingVolume.profile.TryGet(out CameraVignette);



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
