using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class GameInstaller : MonoInstaller
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

        public override void InstallBindings()
        {
            var shipInstance = Container.InstantiatePrefabForComponent<ShipMovement>(_shipPrefab, _shipSpawnPoint.position, Quaternion.identity, null);
            Container.Bind<ShipMovement>().FromInstance(shipInstance).AsSingle();
            var shipTransform = shipInstance.transform;
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            var collisionHandler = shipInstance.GetComponent<CollisionHandler>();

            Container.Bind<Transform>().WithId("ShipTransform").FromInstance(shipTransform);
            Container.Bind<Transform>().WithId("ShipSpawnPoint").FromInstance(_shipSpawnPoint);
            Container.Bind<SpaceShipShooting>().FromInstance(spaceShipShooting).AsSingle();
            Container.Bind<CollisionHandler>().FromInstance(collisionHandler).AsSingle();

            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<SpawnManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.Bind<InputHandler>().AsSingle();
            Container.Bind<SpaceShipController>().AsSingle();
            Container.Bind<KeyboardInputProvider>().AsSingle();

            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);
            Container.Bind<LazerFactory>().AsSingle().WithArguments(_lazerPrefab);
            Container.Bind<UFOFactory>().AsSingle().WithArguments(_ufoPrefab);
            Container.Bind<SpaceObjectFactory>().AsSingle().WithArguments(_asteroidPrefab);
            
            Container.Bind<GameOverUIController>().FromInstance(_gameOverUIController).AsSingle();
            Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
            Container.Bind<ShipIndicators>().FromInstance(_shipIndicators).AsSingle();

            Container.Bind<EntryPoint>().AsSingle().NonLazy();
        }
    }
}