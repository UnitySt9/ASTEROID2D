using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private StartGameView _startGameView;
        
        public override void InstallBindings()
        {
            Container.Bind<StartGameView>().FromInstance(_startGameView).AsSingle();
            Container.BindInterfacesAndSelfTo<StartGamePresenter>().AsSingle().NonLazy();
        }
    }
}