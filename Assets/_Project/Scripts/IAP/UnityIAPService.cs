using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace _Project.Scripts
{
    public class UnityIAPService : IIAPService, IDetailedStoreListener
    {
        private const string NO_ADS_PRODUCT_ID = "no_ads";
        private IStoreController _storeController;
        private IExtensionProvider _extensions;
        private ISaveService _saveService;
        public bool AreAdsDisabled => _saveService.Load().AdsDisabled;

        public UnityIAPService(ISaveService saveService)
        {
            _saveService = saveService;
            Initialize();
        }

        public void Initialize()
        {
            if (_storeController != null) return;
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(NO_ADS_PRODUCT_ID, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void PurchaseNoAds()
        {
            if (_storeController == null || AreAdsDisabled) return;
            _storeController.InitiatePurchase(NO_ADS_PRODUCT_ID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensions = extensions;
            var product = _storeController.products.WithID(NO_ADS_PRODUCT_ID);
            if (product != null && product.hasReceipt)
            {
                var gameData = _saveService.Load();
                gameData.AdsDisabled = true;
                _saveService.Save(gameData);
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"IAP initialization failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"IAP initialization failed: {error} - {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (purchaseEvent.purchasedProduct.definition.id == NO_ADS_PRODUCT_ID)
            {
                var gameData = _saveService.Load();
                gameData.AdsDisabled = true;
                _saveService.Save(gameData);
            }
            
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"Purchase failed: {product.definition.id}, {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"Purchase failed: {product.definition.id}, {failureDescription}");
        }
    }
}
