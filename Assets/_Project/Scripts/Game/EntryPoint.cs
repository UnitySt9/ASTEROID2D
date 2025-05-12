using System;
using Zenject;

namespace _Project.Scripts
{
    public class EntryPoint: IInitializable, IDisposable
    {
        private UFOFactory _ufoFactory;
        private SpaceObjectFactory _spaceObjectFactory;
        private BulletFactory _bulletFactory;
        private LazerFactory _lazerFactory;
        private Score _score;
        private SpawnManager _spawnManager;
        private SpaceShipController _spaceShipController;
        private SpaceShipShooting _spaceShipShooting;
        private GameStateManager _gameStateManager;
        private ShipFactory _shipFactory;
        private DiContainer _container;
        private InputHandler _inputHandler;
        private ShipIndicatorsPresenter _shipIndicatorsPresenter;
        private IShipIndicatorsView _shipIndicatorsView;
        private ICloudSaveService _cloudSaveService;

        [Inject]
        public void Construct(
            GameStateManager gameStateManager,
            Score score,
            SpawnManager spawnManager,
            UFOFactory ufoFactory,
            SpaceObjectFactory spaceObjectFactory,
            SpaceShipController spaceShipController,
            BulletFactory bulletFactory,
            LazerFactory lazerFactory,
            ShipFactory shipFactory,
            DiContainer container,
            InputHandler inputHandler,
            ShipIndicatorsPresenter shipIndicatorsPresenter,
            IShipIndicatorsView shipIndicatorsView,
            ICloudSaveService cloudSaveService)
        {
            _gameStateManager = gameStateManager;
            _score = score;
            _spawnManager = spawnManager;
            _ufoFactory = ufoFactory;
            _spaceObjectFactory = spaceObjectFactory;
            _bulletFactory = bulletFactory;
            _lazerFactory = lazerFactory;
            _spaceShipController = spaceShipController;
            _shipFactory = shipFactory;
            _container = container;
            _inputHandler = inputHandler;
            _shipIndicatorsPresenter = shipIndicatorsPresenter;
            _shipIndicatorsView = shipIndicatorsView;
            _cloudSaveService = cloudSaveService;
        }

        public async void Initialize()
        {
            var shipInstance = await _shipFactory.CreateShip();
            _container.Bind<ShipMovement>().FromInstance(shipInstance).AsSingle();
            var shipTransform = shipInstance.GetComponent<ShipTransform>();
            _spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            var collisionHandler = shipInstance.GetComponent<CollisionHandler>();
            _container.Bind<ShipMovement>().FromInstance(shipInstance).AsTransient();
            _container.Bind<ShipTransform>().FromInstance(shipTransform).AsSingle();
            _container.Bind<CollisionHandler>().FromInstance(collisionHandler).AsSingle();
            _container.Bind<SpaceShipShooting>().FromInstance(_spaceShipShooting).AsSingle();
            await _cloudSaveService.InitializeAsync();
            await _cloudSaveService.SynchronizeAsync();
            SubscribeToEvents();
            await _bulletFactory.Initialize();
            await _lazerFactory.Initialize();
            _spaceShipController.Initialize(shipInstance, _spaceShipShooting, _inputHandler);
            await _ufoFactory.Initialize(shipTransform);
            await _spaceObjectFactory.Initialize();
            _spawnManager.Initialize();
            _shipIndicatorsPresenter.Initialize(_shipIndicatorsView, _spaceShipShooting, _score);
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
