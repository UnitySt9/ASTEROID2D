using System;

namespace _Project.Scripts
{
    public interface IAdsService
    {
        void ShowRewardedAd(Action onRewarded);
        void ShowInterstitialAd(Action onCompleted);
        bool IsRewardedAdReady { get; }
    }
}
