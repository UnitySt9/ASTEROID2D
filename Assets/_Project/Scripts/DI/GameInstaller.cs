using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ShipSpawnPoint _shipSpawnPoint;
        [SerializeField] private Camera _cameraMain;
        [SerializeField] private AudioConfig _audioConfig;
        [SerializeField] private VfxConfig _vfxConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<SpaceShipController>().AsSingle();
            Container.Bind<KeyboardInputProvider>().AsSingle();
            Container.Bind<TeleportBounds>().AsTransient();
            Container.Bind<SpawnManager>().AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(_cameraMain).AsSingle();
            Container.Bind<ShipSpawnPoint>().FromInstance(_shipSpawnPoint).AsSingle();
            
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<LazerFactory>().AsSingle();
            Container.Bind<UFOFactory>().AsSingle();
            Container.Bind<SpaceObjectFactory>().AsSingle();
            Container.Bind<ShipFactory>().AsSingle();
            
            Container.Bind<Canvas>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameOverModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipIndicatorsPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            
            Container.Bind<AudioConfig>().FromInstance(_audioConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
            Container.Bind<VfxConfig>().FromInstance(_vfxConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<VfxService>().AsSingle();
        }
    }
}