using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private StartGamePresenter _startGamePresenter;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartGamePresenter>().FromInstance(_startGamePresenter).AsSingle();
        }
    }
}