using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Audio/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip shootSound;
        public AudioClip objectExplosionSound;
        public AudioClip backgroundMusic;
    }
}
