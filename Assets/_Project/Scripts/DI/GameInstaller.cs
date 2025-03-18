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
            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<CollisionHandler>().FromSubContainerResolve().ByNewContextPrefab(_shipPrefab).AsSingle();
            Container.Bind<SpawnManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.Bind<ShipMovement>().FromSubContainerResolve().ByNewContextPrefab(_shipPrefab).AsSingle();
            Container.Bind<InputHandler>().AsSingle();
            Container.Bind<SpaceShipController>().AsSingle();
            Container.Bind<KeyboardInputProvider>().AsSingle();
            Container.Bind<SpaceShipShooting>().FromSubContainerResolve().ByNewContextPrefab(_shipPrefab).AsSingle();

            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);
            Container.Bind<LazerFactory>().AsSingle().WithArguments(_lazerPrefab);
            Container.Bind<UFOFactory>().AsSingle().WithArguments(_ufoPrefab);
            Container.Bind<SpaceObjectFactory>().AsSingle().WithArguments(_asteroidPrefab);

            Container.Bind<ShipFactory>().AsSingle().WithArguments(_shipPrefab);
            Container.Bind<Transform>().FromInstance(_shipSpawnPoint).WhenInjectedInto<EntryPoint>();
            
            Container.Bind<GameOverUIController>().FromInstance(_gameOverUIController).AsSingle();
            Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
            Container.Bind<ShipIndicators>().FromInstance(_shipIndicators).AsSingle();

            Container.Bind<EntryPoint>().AsSingle().NonLazy();
        }
    }
}