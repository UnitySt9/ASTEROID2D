using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] ShipMovement _shipPrefab;
        [SerializeField] Transform _shipSpawnPoint;
        [SerializeField] GameStateManager _gameStateManager;
        [SerializeField] UFOFactory _ufoFactory;
        [SerializeField] SpaceObjectFactory _spaceObjectFactory;
        [SerializeField] GameOverView _gameOverView;
        [SerializeField] ShipIndicators _shipIndicators;
        [SerializeField] GameOverUIController _gameOverUIController;
        
        private Score _score;
        private InputHandler _inputHandler;

        private void Start()
        {
            _score = new Score();
            var shipInstance = Instantiate(_shipPrefab, _shipSpawnPoint.position, _shipSpawnPoint.rotation);
            var shipTransform = shipInstance.transform;
            var spaceShipController = shipInstance.GetComponent<SpaceShipController>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            _inputHandler = new InputHandler(_gameStateManager);
            _ufoFactory.Initialize(shipTransform);
            spaceShipController.Initialize(_inputHandler, _gameStateManager);
            _gameOverUIController.Initialize(_gameOverView, _score, _gameStateManager);
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
