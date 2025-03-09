using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ShipMovement _shipPrefab;
        [SerializeField] private Transform _shipSpawnPoint;
        [SerializeField] private GameStateManager _gameStateManager;
        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _endScore;
        [SerializeField] private ShipIndicators _shipIndicators;

        private Score _score;
        private InputHandler _inputHandler;

        private void Start()
        {
            _score = new Score();
            ShipMovement shipInstance = Instantiate(_shipPrefab, _shipSpawnPoint.position, _shipSpawnPoint.rotation);
            var spaceShipController = shipInstance.GetComponent<SpaceShipController>();
            var gameOverUI = shipInstance.GetComponent<GameOverUI>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            _inputHandler = new InputHandler(_gameStateManager);
            spaceShipController.Initialize(_inputHandler);
            gameOverUI.Initialize(_gameOverPanel, _endScore, _score, _gameStateManager);
            _shipIndicators.Initialize(spaceShipShooting, _score);
            
            _ufoFactory.OnUFOCreated += OnUfoCreated;
        }

        private void Update()
        {
            _inputHandler?.Update();
        }

        private void OnDestroy()
        {
            _ufoFactory.OnUFOCreated -= OnUfoCreated;
        }
        
        private void OnUfoCreated(UFO ufo)
        {
            _score.SubscribeToUfo(ufo);
            ufo.OnUfoDestroyed += OnUfoDestroyed; 
        }

        private void OnUfoDestroyed(UFO ufo)
        {
            _score.UnsubscribeFromUfo(ufo); 
            ufo.OnUfoDestroyed -= OnUfoDestroyed; 
        }
        
    }
}
