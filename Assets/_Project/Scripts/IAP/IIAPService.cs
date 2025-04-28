namespace _Project.Scripts
{
    public interface IIAPService
    {
        void Initialize();
        void PurchaseNoAds();
        bool AreAdsDisabled { get; }
    }
}
