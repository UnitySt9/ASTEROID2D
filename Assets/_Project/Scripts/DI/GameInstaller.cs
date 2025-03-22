using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ShipMovement _shipPrefab;
        [SerializeField] private ShipSpawnPoint _shipSpawnPoint;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private ShipIndicatorsView _shipIndicatorsView;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Lazer _lazerPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Asteroid _asteroidPrefab;

        public override void InstallBindings()
        {
            var shipInstance = Container.InstantiatePrefabForComponent<ShipMovement>(_shipPrefab, _shipSpawnPoint.transform.position, Quaternion.identity, null);
            Container.Bind<ShipMovement>().FromInstance(shipInstance).AsSingle();
            var shipTransform = shipInstance.GetComponent<ShipTransform>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            var collisionHandler = shipInstance.GetComponent<CollisionHandler>();

            Container.Bind<ShipTransform>().FromInstance(shipTransform).AsSingle();
            Container.Bind<ShipSpawnPoint>().FromInstance(_shipSpawnPoint).AsSingle();
            Container.Bind<SpaceShipShooting>().FromInstance(spaceShipShooting).AsSingle();
            Container.Bind<CollisionHandler>().FromInstance(collisionHandler).AsSingle();

            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<SpaceShipController>().AsSingle();
            Container.Bind<KeyboardInputProvider>().AsSingle();
            Container.Bind<TeleportBounds>().AsTransient();
            Container.Bind<SpawnManager>().AsSingle().WithArguments(this).NonLazy();

            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);
            Container.Bind<LazerFactory>().AsSingle().WithArguments(_lazerPrefab);
            Container.Bind<UFOFactory>().AsSingle().WithArguments(_ufoPrefab);
            Container.Bind<SpaceObjectFactory>().AsSingle().WithArguments(_asteroidPrefab);
            
            Container.Bind<GameOverUIController>().AsSingle();
            Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
            
            Container.Bind<ShipIndicatorsView>().FromInstance(_shipIndicatorsView).AsSingle();
            Container.Bind<ShipIndicatorsModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipIndicatorsPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        }
    }
}