using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Scripts
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_GAME_ID = "5834877";
        private const string IOS_GAME_ID = "5834876";
        private const string REWARD_AD_ID = "Rewarded_Android";
        private const string INTERSTITIAL_AD_ID = "Interstitial_Android";
        public event Action OnAdReadyChanged;
        
        private Action _onRewardedCallback;
        private Action _onInterstitialCompleted;
        private readonly GameData _gameData;
        private bool _isInitialized;
        public bool IsRewardedAdReady { get; private set; }
        public bool IsInterstitialAdReady { get; private set; }

        public UnityAdsService(GameData gameData)
        {
            _gameData = gameData;
            if (_gameData.AdsDisabled) 
                return;
            string gameId = Application.platform == RuntimePlatform.IPhonePlayer ? IOS_GAME_ID : ANDROID_GAME_ID;
            Advertisement.Initialize(gameId, true, this);
        }
        
        public void OnInitializationComplete()
        {
            _isInitialized = true;
            LoadRewardedAd();
            LoadInterstitialAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            _isInitialized = false;
        }

        private void LoadRewardedAd()
        {
            if (!_isInitialized) 
                return;
            IsRewardedAdReady = false;
            Advertisement.Load(REWARD_AD_ID, this);
        }

        private void LoadInterstitialAd()
        {
            if (!_isInitialized) 
                return;
            IsInterstitialAdReady = false;
            Advertisement.Load(INTERSTITIAL_AD_ID, this);
        }

        public void ShowRewardedAd(Action onRewarded)
        {
            if (_gameData.AdsDisabled)
            {
                onRewarded?.Invoke();
                return;
            }
            
            _onRewardedCallback = onRewarded;
            
            if (IsRewardedAdReady)
            {
                Advertisement.Show(REWARD_AD_ID, this);
            }
            else
            {
                LoadRewardedAd();
                onRewarded?.Invoke();
            }
        }

        public void ShowInterstitialAd(Action onCompleted)
        {
            if (_gameData.AdsDisabled)
            {
                onCompleted?.Invoke();
                return;
            }
            
            _onInterstitialCompleted = onCompleted;
            
            if (IsInterstitialAdReady)
            {
                Advertisement.Show(INTERSTITIAL_AD_ID, this);
            }
            else
            {
                LoadInterstitialAd();
                onCompleted?.Invoke();
            }
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            OnAdReadyChanged?.Invoke();
            
            if (placementId == REWARD_AD_ID)
            {
                IsRewardedAdReady = true;
            }
            else if (placementId == INTERSTITIAL_AD_ID)
            {
                IsInterstitialAdReady = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            OnAdReadyChanged?.Invoke();
            Debug.LogError($"Failed to load ad {placementId}: {error} - {message}");
        }
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Failed to show ad {placementId}: {error} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"Showing ad {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"Showing ad {placementId}");
        }
        
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == REWARD_AD_ID && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                _onRewardedCallback?.Invoke();
            }
            
            if (placementId == INTERSTITIAL_AD_ID)
            {
                _onInterstitialCompleted?.Invoke();
            }
            
            if (placementId == REWARD_AD_ID) 
                LoadRewardedAd();
            if (placementId == INTERSTITIAL_AD_ID) 
                LoadInterstitialAd();
        }
    }
}
