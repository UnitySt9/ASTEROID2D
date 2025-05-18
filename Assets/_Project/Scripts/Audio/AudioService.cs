using UnityEngine;

namespace _Project.Scripts
{
    public class AudioService : IAudioService
    {
        private readonly AudioConfig _config;
        private AudioSource _musicSource;
        private AudioSource _sfxSource;

        public AudioService(AudioConfig config)
        {
            _config = config;
            InitializeAudioSources();
        }

        private void InitializeAudioSources()
        {
            var audioObject = new GameObject("AudioSources");
            _musicSource = audioObject.AddComponent<AudioSource>();
            _sfxSource = audioObject.AddComponent<AudioSource>();
            
            _musicSource.loop = true;
        }

        public void PlayShootSound()
        {
            _sfxSource.PlayOneShot(_config.shootSound);
        }

        public void PlayObjectExplosionSound()
        {
            _sfxSource.PlayOneShot(_config.objectExplosionSound);
        }

        public void PlayBackgroundMusic()
        {
            _musicSource.clip = _config.backgroundMusic;
            _musicSource.Play();
        }

        public void StopBackgroundMusic()
        {
            _musicSource.Stop();
        }
    }
}
