using System;
using Zenject;

namespace _Project.Scripts
{
    public class StartGamePresenter : IDisposable, IInitializable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IIAPService _iapService;
        private readonly StartGameView _view;

        public StartGamePresenter(ISceneLoader sceneLoader, IIAPService iapService, StartGameView view)
        {
            _sceneLoader = sceneLoader;
            _iapService = iapService;
            _view = view;
        }

        public void Initialize()
        {
            _view.StartButton.onClick.AddListener(StartLevel);
            UpdateNoAdsUI();
            if (!_iapService.AreAdsDisabled)
            {
                _view.NoAdsButton.onClick.AddListener(OnNoAdsClicked);
            }
        }

        private void StartLevel() => _sceneLoader.LoadNextScene();

        private void OnNoAdsClicked() => _iapService.PurchaseNoAds();

        private void UpdateNoAdsUI()
        {
            bool adsDisabled = _iapService.AreAdsDisabled;
            _view.NoAdsButton.gameObject.SetActive(!adsDisabled);
            _view.AlreadyPurchasedText.SetActive(adsDisabled);
        }

        public void Dispose()
        {
            _view.StartButton.onClick.RemoveListener(StartLevel);
            _view.NoAdsButton.onClick.RemoveListener(OnNoAdsClicked);
        }
    }
}
