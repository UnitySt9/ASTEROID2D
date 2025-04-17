using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _adsButton;
    
        private ISceneLoader _sceneLoader;
        private IAdsService _adsService;
        private GameStateManager _gameStateManager;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IAdsService adsService, GameStateManager gameStateManager)
        {
            _sceneLoader = sceneLoader;
            _adsService = adsService;
            _gameStateManager = gameStateManager;
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);  
            _menuButton.onClick.AddListener(LoadMenu);  
            _adsButton.onClick.AddListener(OnAdsClicked);  
            
            if (_adsService is UnityAdsService unityAdsService)  
            {  
                unityAdsService.OnAdReadyChanged += UpdateAdsButton;  
            }  
            UpdateAdsButton();  
        }
        
        private void UpdateAdsButton()
        {
            _adsButton.gameObject.SetActive(_adsService.IsRewardedAdReady);
        }

        private void OnRestartClicked()
        {
            _adsService.ShowInterstitialAd(() => { _sceneLoader.ReloadScene(); });
        }

        private void OnAdsClicked()
        {
            _adsService.ShowRewardedAd(() => { _gameStateManager.ContinueGame(); gameObject.SetActive(false); });
        }

        private void LoadMenu()
        {
            _sceneLoader.LoadMenu();
        }

        private void OnDestroy()
        {
            if (_adsService is UnityAdsService unityAdsService)  
            {  
                unityAdsService.OnAdReadyChanged -= UpdateAdsButton;  
            }  
            _restartButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _adsButton.onClick.RemoveAllListeners();
        }
    }
}
