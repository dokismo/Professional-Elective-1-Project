using System;
using Unity.Mathematics;
using UnityEngine;

namespace Audio_Scripts
{
    public class GlobalSfx : MonoBehaviour
    {
        public static Action<Vector3> wallEvent, fleshEvent;
        public static Action<Vector3, GameObject> grunt, death, bossScream, bossGrunt, bossDied, smashRocks, enraged;

        public GameObject wallShot, fleshShot;

        private AudioSource audioSource;

        private void OnEnable()
        {
            wallEvent += Wall;
            fleshEvent += Flesh;
            grunt += PlaceObjAt;
            death += PlaceObjAt;
            bossScream += PlaceObjAt;
            bossGrunt += PlaceObjAt;
            bossDied += PlaceObjAt;
            smashRocks += PlaceObjAt;
            enraged += PlaceObjAt;
            WaveDifficultyIncrement.waveStart += WaveStart;
        }

        private void OnDisable()
        {
            wallEvent -= Wall;
            fleshEvent -= Flesh;
            grunt -= PlaceObjAt;
            death -= PlaceObjAt;
            bossScream -= PlaceObjAt;
            bossGrunt -= PlaceObjAt;
            bossDied -= PlaceObjAt;
            smashRocks -= PlaceObjAt;
            enraged -= PlaceObjAt;
            WaveDifficultyIncrement.waveStart -= WaveStart;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void WaveStart() => audioSource.Play();

        private void PlaceObjAt(Vector3 arg1, GameObject arg2) => Instantiate(arg2, arg1, quaternion.identity);
        
        private void Flesh(Vector3 position) => Instantiate(fleshShot, position, quaternion.identity);

        private void Wall(Vector3 position) => Instantiate(wallShot, position, quaternion.identity);
    }


}
