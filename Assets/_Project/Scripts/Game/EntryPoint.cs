using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

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
        private GameOverPresenter _gameOverPresenter;
        private IAddressablesLoader _addressablesLoader;
        private ICloudSaveService _cloudSaveService;
        private Canvas _mainCanvas;

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
            GameOverPresenter gameOverPresenter,
            IAddressablesLoader addressablesLoader,
            ICloudSaveService cloudSaveService,
            Canvas mainCanvas)
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
            _gameOverPresenter = gameOverPresenter;
            _addressablesLoader = addressablesLoader;
            _cloudSaveService = cloudSaveService;
            _mainCanvas = mainCanvas;
        }

        public async void Initialize()
        {
            var gameOverViewPrefab = await _addressablesLoader.LoadGameOverViewPrefab();
            var gameOverViewInstance = Object.Instantiate(gameOverViewPrefab, _mainCanvas.transform);
            var gameOverView = gameOverViewInstance.GetComponent<GameOverView>();
            _container.Bind<GameOverView>().FromInstance(gameOverView).AsSingle();
            var restartGame = gameOverViewInstance.GetComponent<RestartGame>();
            _container.Inject(restartGame);
            
            var shipIndicatorsViewPrefab = await _addressablesLoader.LoadShipIndicatorsViewPrefab();
            var shipIndicatorsViewInstance = Object.Instantiate(shipIndicatorsViewPrefab, _mainCanvas.transform);
            var shipIndicatorsView = shipIndicatorsViewInstance.GetComponent<ShipIndicatorsView>();
            _container.Bind<IShipIndicatorsView>().FromInstance(shipIndicatorsView).AsSingle();
            _gameOverPresenter.Initialize(gameOverView);
            
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
            _shipIndicatorsPresenter.Initialize(shipIndicatorsView, _spaceShipShooting, _score);
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
