using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private StartGameView _startGameView;
        [SerializeField] private GameObject _iapListenerPrefab;
        
        public override void InstallBindings()
        {
            var iapListener = Container.InstantiatePrefab(_iapListenerPrefab);
            DontDestroyOnLoad(iapListener);
            Container.Bind<StartGameView>().FromInstance(_startGameView).AsSingle();
            Container.BindInterfacesAndSelfTo<StartGamePresenter>().AsSingle().NonLazy();
        }
    }
}