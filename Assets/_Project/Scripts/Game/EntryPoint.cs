using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class EntryPoint
    {
        [Inject]private readonly GameStateManager _gameStateManager;
        [Inject]private readonly ShipFactory _shipFactory;
        [Inject]private readonly UFOFactory _ufoFactory;
        [Inject]private readonly SpaceObjectFactory _spaceObjectFactory;
        [Inject]private readonly BulletFactory _bulletFactory;
        [Inject]private readonly LazerFactory _lazerFactory;
        [Inject]private readonly Score _score;
        [Inject]private readonly SpawnManager _spawnManager;
        [Inject]private readonly Transform _shipSpawnPoint;
        [Inject]private readonly InputHandler _inputHandler;
        [Inject]private readonly SpaceShipController _spaceShipController;
        [Inject]private readonly GameOverView _gameOverView;
        [Inject]private readonly GameOverUIController _gameOverUIController;
        [Inject]private readonly ShipIndicators _shipIndicators;

        public EntryPoint(
            GameStateManager gameStateManager,
            Transform shipSpawnPoint,
            Score score,
            SpawnManager spawnManager,
            ShipFactory shipFactory,
            UFOFactory ufoFactory,
            SpaceObjectFactory spaceObjectFactory,
            BulletFactory bulletFactory,
            LazerFactory lazerFactory,
            SpaceShipController spaceShipController,
            InputHandler inputHandler,
            GameOverView gameOverView,
            GameOverUIController gameOverUIController,
            ShipIndicators shipIndicators
            )
        {
            _gameStateManager = gameStateManager;
            _shipSpawnPoint = shipSpawnPoint;
            _score = score;
            _spawnManager = spawnManager;
            _shipFactory = shipFactory;
            _ufoFactory = ufoFactory;
            _spaceObjectFactory = spaceObjectFactory;
            _lazerFactory = lazerFactory;
            _bulletFactory = bulletFactory;
            _spaceShipController = spaceShipController;
            _inputHandler = inputHandler;
            _gameOverView = gameOverView;
            _gameOverUIController = gameOverUIController;
            _shipIndicators = shipIndicators;
            SubscribeToEvents();
        }

        public void StartGame()
        {
            var shipInstance = _shipFactory.Create(_shipSpawnPoint);
            var shipTransform = shipInstance.transform;
            var shipMovement = shipInstance.GetComponent<ShipMovement>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            var collisionHandler = shipInstance.GetComponent<CollisionHandler>();
            _spaceShipController.Initialize(shipMovement, spaceShipShooting, _inputHandler);
            _ufoFactory.Initialize(shipTransform);
            _spawnManager.Initialize(_spaceObjectFactory, _ufoFactory, _gameStateManager);
            _gameOverUIController.Initialize(_gameOverView, _score, _gameStateManager);
            _shipIndicators.Initialize(spaceShipShooting, _score);
            collisionHandler.Initialize(_gameStateManager);
            spaceShipShooting.Initialize(_bulletFactory, _lazerFactory);
        }
        
        private void SubscribeToEvents()
        {
            _ufoFactory.OnUFOCreated += OnUfoCreated;
            _spaceObjectFactory.OnSpaceObjectCreated += OnSpaceObjectCreated;
        }

        private void UnsubscribeFromEvents()
        {
            _ufoFactory.OnUFOCreated -= OnUfoCreated;
            _spaceObjectFactory.OnSpaceObjectCreated -= OnSpaceObjectCreated;
        }

        private void OnUfoCreated(UFO ufo)
        {
            _score.SubscribeToUfo(ufo);
            ufo.OnUfoDestroyed += OnUfoDestroyed;
        }

        private void OnSpaceObjectCreated(SpaceObject spaceObject)
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

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}
