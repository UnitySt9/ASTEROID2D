using System;
using Zenject;

namespace _Project.Scripts
{
    public class EntryPoint: IInitializable, IDisposable
    {
        [Inject] private readonly ShipTransform _shipTransform;
        [Inject] private readonly GameStateManager _gameStateManager;
        [Inject] private readonly UFOFactory _ufoFactory;
        [Inject] private readonly SpaceObjectFactory _spaceObjectFactory;
        [Inject] private readonly BulletFactory _bulletFactory;
        [Inject] private readonly LazerFactory _lazerFactory;
        [Inject] private readonly Score _score;
        [Inject] private readonly SpawnManager _spawnManager;
        [Inject] private readonly InputHandler _inputHandler;
        [Inject] private readonly SpaceShipController _spaceShipController;
        [Inject] private readonly GameOverView _gameOverView;
        [Inject] private readonly GameOverUIController _gameOverUIController;
        //[Inject] private readonly ShipIndicatorsPresenter _shipIndicatorsPresenter;
        [Inject] private readonly ShipMovement _shipMovement;
        [Inject] private readonly SpaceShipShooting _spaceShipShooting;
        [Inject] private readonly CollisionHandler _collisionHandler;

        public EntryPoint(
            ShipTransform shipTransform,
            GameStateManager gameStateManager,
            Score score,
            SpawnManager spawnManager,
            UFOFactory ufoFactory,
            SpaceObjectFactory spaceObjectFactory,
            BulletFactory bulletFactory,
            LazerFactory lazerFactory,
            SpaceShipController spaceShipController,
            InputHandler inputHandler,
            GameOverView gameOverView,
            GameOverUIController gameOverUIController,
            //ShipIndicatorsPresenter shipIndicatorsPresenter,
            ShipMovement shipMovement,
            SpaceShipShooting spaceShipShooting,
            CollisionHandler collisionHandler
            )
        {
            _gameStateManager = gameStateManager;
            _shipTransform = shipTransform;
            _score = score;
            _spawnManager = spawnManager;
            _ufoFactory = ufoFactory;
            _spaceObjectFactory = spaceObjectFactory;
            _lazerFactory = lazerFactory;
            _bulletFactory = bulletFactory;
            _spaceShipController = spaceShipController;
            _inputHandler = inputHandler;
            _gameOverView = gameOverView;
            _gameOverUIController = gameOverUIController;
            //_shipIndicatorsPresenter = shipIndicatorsPresenter;
            _shipMovement = shipMovement;
            _spaceShipShooting = spaceShipShooting;
            _collisionHandler = collisionHandler;
            SubscribeToEvents();
        }

        public void Initialize()
        {
            _spaceShipController.Initialize(_shipMovement, _spaceShipShooting, _inputHandler);
            _ufoFactory.Initialize(_shipTransform.transform);
            _spawnManager.Initialize(_spaceObjectFactory, _ufoFactory, _gameStateManager);
            _gameOverUIController.Initialize(_gameOverView, _score, _gameStateManager);
            //_shipIndicatorsPresenter.Initialize(_spaceShipShooting, _score);
            _collisionHandler.Initialize(_gameStateManager);
            _spaceShipShooting.Initialize(_bulletFactory, _lazerFactory);
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
