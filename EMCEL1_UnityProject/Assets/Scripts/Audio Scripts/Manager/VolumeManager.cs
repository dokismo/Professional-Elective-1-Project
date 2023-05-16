using UnityEngine;
using UnityEngine.Audio;

namespace SFX.Manager
{
    public class VolumeManager : MonoBehaviour
    {
        public delegate void MuteEvent(bool value);
        public static MuteEvent muteToggle;

        public delegate void VolumeEvent();
        public static VolumeEvent save;
        public static VolumeEvent load;
        
        public delegate float GetVolume(string name);
        public static GetVolume getVolume;
        
        public delegate void VolumeChangeSearch(string name, float value);
        public static VolumeChangeSearch volumeChangeSearch;
        
        public delegate void VolumeChange(float value);
        public static VolumeChange sfxVolume;
        public static VolumeChange musicVolume;
        
        public AudioMixer audioMixer;

        public const string MixerMusic = "MusicVolume";
        public const string MixerSfx = "SFXVolume";
        public const string MixerMaster = "MasterVolume";
        public const string MixerMute = "Mute";

        private int sfx;
        private int music;
        private int mute;

        private void OnEnable()
        {
            sfxVolume += SetSFXVolume;
            musicVolume += SetMusicVolume;
            volumeChangeSearch += SearchAndChangeVolume;
            save += SaveVolume;
            load += LoadVolume;
            muteToggle += Mute;
        }

        private void OnDisable()
        {
            sfxVolume -= SetSFXVolume;
            musicVolume -= SetMusicVolume;
            volumeChangeSearch -= SearchAndChangeVolume;
            save -= SaveVolume;
            load -= LoadVolume;
            muteToggle -= Mute;
        }

        private void Start()
        {
            LoadVolume();
        }

        private void OnApplicationQuit()
        {
            SaveVolume();
        }

        private void SearchAndChangeVolume(string mixerName, float value)
        {
            switch (mixerName)
            {
                case MixerMusic:
                    SetMusicVolume(value);
                    break;
                case MixerSfx:
                    SetSFXVolume(value);
                    break;
            }
        }

        private void SetSFXVolume(float value)
        {
            sfx = Mathf.RoundToInt(value * 10);
            audioMixer.SetFloat(MixerSfx, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
        }
        
        private void SetMusicVolume(float value)
        {
            music = Mathf.RoundToInt(value * 10);
            audioMixer.SetFloat(MixerMusic, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
        }
        
        private void SaveVolume()
        {
            PlayerPrefs.SetFloat(MixerMusic, music);
            PlayerPrefs.SetFloat(MixerSfx, sfx);
            PlayerPrefs.SetInt(MixerMute, mute);
        }

        private void LoadVolume()
        {
            SetSFXVolume(PlayerPrefs.GetFloat(MixerSfx, 1f) / 10);
            SetMusicVolume(PlayerPrefs.GetFloat(MixerMusic, 1f) / 10);
            mute = PlayerPrefs.GetInt(MixerMute, 0);
            Mute(mute);
        }

        private void Mute(bool value)
        {
            mute = value ? 1 : 0;

            audioMixer.SetFloat(MixerMaster, mute == 1 ? -80 : 0);
        }
        
        private void Mute(int value)
        {
            mute = value;

            audioMixer.SetFloat(MixerMaster, mute == 1 ? -80 : 0);
        }
    }
}
