using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] ShipMovement _shipPrefab;
        [SerializeField] Transform _shipSpawnPoint;
        [SerializeField] GameStateManager _gameStateManager;
        [SerializeField] UFOFactory _ufoFactory;
        [SerializeField] SpaceObjectFactory _spaceObjectFactory;
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _endScore;
        [SerializeField] ShipIndicators _shipIndicators;

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
            _spaceObjectFactory.OnSpaceObjectCreated += OnSpaceObjectCrated;
        }

        private void Update()
        {
            _inputHandler?.Update();
        }

        private void OnDestroy()
        {
            _ufoFactory.OnUFOCreated -= OnUfoCreated;
            _spaceObjectFactory.OnSpaceObjectCreated -= OnSpaceObjectCrated;
        }
        
        private void OnUfoCreated(UFO ufo)
        {
            _score.SubscribeToUfo(ufo);
            ufo.OnUfoDestroyed += OnUfoDestroyed; 
        }

        private void OnSpaceObjectCrated(SpaceObject spaceObject)
        {
            _score.SubscribeToSpaceObject(spaceObject);
            spaceObject.OnSpaceObjectDestroyed += OnSpaceObjectDestroyed;
        }

        private void OnUfoDestroyed(UFO ufo)
        {
            _score.UnsubscribeFromUfo(ufo); 
            ufo.OnUfoDestroyed -= OnUfoDestroyed; 
        }

        private void OnSpaceObjectDestroyed(SpaceObject spaceObject)
        {
            _score.UnsubscribeFromSpaceObject(spaceObject);
            spaceObject.OnSpaceObjectDestroyed -= OnSpaceObjectDestroyed;
        }
        
    }
}
