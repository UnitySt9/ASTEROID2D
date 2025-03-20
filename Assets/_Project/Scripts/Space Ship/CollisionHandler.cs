using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class CollisionHandler : MonoBehaviour
    {
        private GameStateManager _gameStateManager;
        private TeleportBounds _teleportBounds;

        private void Start()
        {
            _teleportBounds = new TeleportBounds(transform, Camera.main);
        }

        private void Update()
        {
            _teleportBounds.BoundsUpdate();
        }

        private void OnCollisionEnter2D()
        {
            _gameStateManager?.GameOver();
        }
        
        public void Initialize(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
    }
}
