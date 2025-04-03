using System;
using Zenject;

namespace _Project.Scripts
{
    public class EntryPoint: IInitializable, IDisposable
    {
        private UFOFactory _ufoFactory;
        private SpaceObjectFactory _spaceObjectFactory;
        private Score _score;
        private SpawnManager _spawnManager;
        private SpaceShipController _spaceShipController;
        private SpaceShipShooting _spaceShipShooting;
        private GameStateManager _gameStateManager;

        [Inject]
        public void Construct(
            GameStateManager gameStateManager,
            Score score,
            SpawnManager spawnManager,
            UFOFactory ufoFactory,
            SpaceObjectFactory spaceObjectFactory,
            BulletFactory bulletFactory,
            LazerFactory lazerFactory,
            SpaceShipController spaceShipController,
            SpaceShipShooting spaceShipShooting,
            CollisionHandler collisionHandler)
        {
            _gameStateManager = gameStateManager;
            _score = score;
            _spawnManager = spawnManager;
            _ufoFactory = ufoFactory;
            _spaceObjectFactory = spaceObjectFactory;
            _spaceShipController = spaceShipController;
            _spaceShipShooting = spaceShipShooting;
        }

        public void Initialize()
        {
            SubscribeToEvents();
            _spaceShipController.Initialize();
            _ufoFactory.Initialize();
            _spawnManager.Initialize();
            _gameStateManager.GameStart();
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
            _gameStateManager.GameOverStats(
                _spaceShipShooting.ShotsFired,
                _spaceShipShooting.LasersUsed,
                _score.ObjectsDestroyed
            );
        }
    }
}
