using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Zenject;

namespace _Project.Scripts
{
    public class UnityIAPService : IIAPService, IDetailedStoreListener, IInitializable
    {
        private const string NO_ADS_PRODUCT_ID = "no_ads";
        private IStoreController _storeController;
        private IExtensionProvider _extensions;
        private ISaveService _saveService;
        private ICloudSaveService _cloudSaveService;
        private GameData _gameData;
    
        public bool AreAdsDisabled => _gameData.AdsDisabled;

        [Inject]
        public UnityIAPService(ISaveService saveService, ICloudSaveService cloudSaveService)
        {
            _saveService = saveService;
            _cloudSaveService = cloudSaveService;
            _gameData = _saveService.Load();
        }

        public void Initialize()
        {
            if (_storeController != null) 
                return;
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(NO_ADS_PRODUCT_ID, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void PurchaseNoAds()
        {
            if (_storeController == null || AreAdsDisabled) 
                return;
            _storeController.InitiatePurchase(NO_ADS_PRODUCT_ID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensions = extensions;
            
            var product = _storeController.products.WithID(NO_ADS_PRODUCT_ID);
            if (product != null && product.hasReceipt)
            {
                UpdateAdsDisabledStatus(true);
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (purchaseEvent.purchasedProduct.definition.id == NO_ADS_PRODUCT_ID)
            {
                UpdateAdsDisabledStatus(true);
            }
            return PurchaseProcessingResult.Complete;
        }

        private async void UpdateAdsDisabledStatus(bool disabled)
        {
            _gameData.AdsDisabled = disabled;
            _gameData.SaveDateTime = DateTime.UtcNow;
            _saveService.Save(_gameData);
            try
            {
                await _cloudSaveService.InitializeAsync();
                await _cloudSaveService.SaveAsync(_gameData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to sync no-ads purchase to cloud: {e.Message}");
            }
        }
        
        public void OnInitializeFailed(InitializationFailureReason error) =>
            Debug.LogError($"IAP initialization failed: {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message) =>
            Debug.LogError($"IAP initialization failed: {error} - {message}");

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
            Debug.LogError($"Purchase failed: {product.definition.id}, {failureReason}");

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) =>
            Debug.LogError($"Purchase failed: {product.definition.id}, {failureDescription}");
    }
}
