using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class CollisionHandler : MonoBehaviour
    {
        private GameStateManager _gameStateManager;
        private TeleportBounds _teleportBounds;
        private Camera _cameraMain;

        [Inject]
        private void Construct(GameStateManager gameStateManager, Camera mainCamera)
        {
            _gameStateManager = gameStateManager;
            _cameraMain = mainCamera;
        }
        
        private void Start()
        {
            _teleportBounds = new TeleportBounds(transform, _cameraMain);
        }

        private void Update()
        {
            _teleportBounds.BoundsUpdate();
        }

        private void OnCollisionEnter2D()
        {
            _gameStateManager?.GameOver();
        }
    }
}
