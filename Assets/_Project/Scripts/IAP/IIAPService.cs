namespace _Project.Scripts
{
    public interface IIAPService
    {
        void PurchaseNoAds();
        bool AreAdsDisabled { get; }
    }
}
