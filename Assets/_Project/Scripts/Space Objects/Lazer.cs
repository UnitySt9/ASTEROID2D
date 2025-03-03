using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Lazer : MonoBehaviour
    {
        public event Action<int> OnLazerHit; 
        
        private readonly float _laserDuration = 0.5f;
        private readonly int _scoreValue = 1;
        private Score _score;

        private void Start()
        {
            Destroy(gameObject, _laserDuration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Rigidbody2D>() != null)
                OnLazerHit?.Invoke(_scoreValue);
        }

        private void OnDestroy()
        {
            _score.UnsubscribeFromLazer(this);
        }
        
        public void Initialize(Score score)
        {
            _score = score;
        }
    }
}
