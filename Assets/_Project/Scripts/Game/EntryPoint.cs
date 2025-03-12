using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ShipMovement _shipPrefab;
        [SerializeField] private Transform _shipSpawnPoint;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private ShipIndicators _shipIndicators;
        [SerializeField] private GameOverUIController _gameOverUIController;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Lazer _lazerPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Asteroid _asteroidPrefab;
        
        private GameStateManager _gameStateManager;
        private UFOFactory _ufoFactory;
        private SpaceObjectFactory _spaceObjectFactory;
        private Score _score;
        private InputHandler _inputHandler;
        private BulletFactory _bulletFactory;
        private LazerFactory _lazerFactory;
        private CoroutineHelper _coroutineHelper;
        private SpaceShipController _spaceShipController;
        private SpawnManager _spawnManager;

        private void Start()
        {
            _coroutineHelper = new CoroutineHelper(this);
            var shipInstance = Instantiate(_shipPrefab, _shipSpawnPoint.position, _shipSpawnPoint.rotation);
            var shipTransform = shipInstance.transform;
            _gameStateManager = new GameStateManager();
            _score = new Score();
            _ufoFactory = new UFOFactory(_ufoPrefab, _gameStateManager, shipTransform);
            _spaceObjectFactory = new SpaceObjectFactory(_asteroidPrefab, _gameStateManager);
            _ufoFactory.OnUFOCreated += OnUfoCreated;
            _spaceObjectFactory.OnSpaceObjectCreated += OnSpaceObjectCrated;
            _spawnManager = new SpawnManager(_spaceObjectFactory, _ufoFactory, _gameStateManager, _coroutineHelper);
            var collisionHandler = shipInstance.AddComponent<CollisionHandler>();
            collisionHandler.Initialize(_gameStateManager);
            var shipMovement = shipInstance.GetComponent<ShipMovement>();
            
            _bulletFactory = new BulletFactory(_bulletPrefab);
            _lazerFactory = new LazerFactory(_lazerPrefab);
            
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            spaceShipShooting.Initialize(_bulletFactory, _lazerFactory);

            _inputHandler = new InputHandler(_gameStateManager);
            _spaceShipController = new SpaceShipController(shipMovement, spaceShipShooting, _inputHandler);

            _gameOverUIController.Initialize(_gameOverView, _score, _gameStateManager);
            _shipIndicators.Initialize(spaceShipShooting, _score);
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
