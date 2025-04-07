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
        [SerializeField] private Camera _cameraMain;

        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<SpaceShipController>().AsSingle();
            Container.Bind<KeyboardInputProvider>().AsSingle();
            Container.Bind<TeleportBounds>().AsTransient();
            Container.Bind<SpawnManager>().AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(_cameraMain).AsSingle();
            
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<LazerFactory>().AsSingle();
            Container.Bind<UFOFactory>().AsSingle();
            Container.Bind<SpaceObjectFactory>().AsSingle();
            
            var shipInstance = Container.InstantiatePrefabForComponent<ShipMovement>(_shipPrefab, _shipSpawnPoint.transform.position, Quaternion.identity, null);
            Container.Bind<ShipMovement>().FromInstance(shipInstance).AsSingle();
            var shipTransform = shipInstance.GetComponent<ShipTransform>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            var collisionHandler = shipInstance.GetComponent<CollisionHandler>();
            
            Container.Bind<ShipTransform>().FromInstance(shipTransform).AsSingle();
            Container.Bind<ShipSpawnPoint>().FromInstance(_shipSpawnPoint).AsSingle();
            Container.Bind<SpaceShipShooting>().FromInstance(spaceShipShooting).AsSingle();
            Container.Bind<CollisionHandler>().FromInstance(collisionHandler).AsSingle();
            
            Container.Bind<GameOverModel>().AsSingle();
            Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle().NonLazy();
            
            Container.Bind<IShipIndicatorsView>().FromInstance(_shipIndicatorsView).AsSingle();
            Container.BindInterfacesAndSelfTo<ShipIndicatorsPresenter>().AsSingle().NonLazy();
            
            Container.Bind<ISaveService>().To<PlayerPrefsSaveService>().AsSingle();
            Container.Bind<GameData>().FromNew().AsTransient();
            
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        }
    }
}