using Zenject;

namespace _Project.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IAnalyticsService>().To<FirebaseAnalyticsService>().AsSingle();
            Container.Bind<IConfigService>().To<FirebaseConfigService>().AsSingle();
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<IAdsService>().To<UnityAdsService>().AsSingle();
            Container.Bind<ISaveService>().To<PlayerPrefsSaveService>().AsSingle();
            Container.Bind<ICloudSaveService>().To<UnityCloudSaveService>().AsSingle();
            Container.Bind<IIAPService>().To<UnityIAPService>().AsSingle();
            Container.Bind<GameData>().FromNew().AsTransient();
        }
    }
}
