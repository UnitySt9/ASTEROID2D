using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    public class AddressablesLoader : IAddressablesLoader
    {
        private const string SHIP_PREFAB_KEY = "ship_prefab";
        private const string ASTEROID_PREFAB_KEY = "asteroid_prefab";
        private const string UFO_PREFAB_KEY = "ufo_prefab";
        private const string BULLET_PREFAB_KEY = "bullet_prefab";
        private const string LAZER_PREFAB_KEY = "lazer_prefab";
        private const string GAME_OVER_VIEW_KEY = "game_over_view";
        private const string SHIP_INDICATORS_VIEW_KEY = "ship_indicators_view";

        public AddressablesLoader()
        {
            Addressables.InitializeAsync().ToUniTask().Forget();
        }

        public async UniTask<GameObject> LoadShipPrefab()
        {
            return await LoadPrefab(SHIP_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadAsteroidPrefab()
        {
            return await LoadPrefab(ASTEROID_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadUFOPrefab()
        {
            return await LoadPrefab(UFO_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadBulletPrefab()
        {
            return await LoadPrefab(BULLET_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadLazerPrefab()
        {
            return await LoadPrefab(LAZER_PREFAB_KEY);
        }
        
        public async UniTask<GameObject> LoadGameOverViewPrefab()
        {
            return await LoadPrefab(GAME_OVER_VIEW_KEY);
        }

        public async UniTask<GameObject> LoadShipIndicatorsViewPrefab()
        {
            return await LoadPrefab(SHIP_INDICATORS_VIEW_KEY);
        }

        private async UniTask<GameObject> LoadPrefab(string key)
        {
            try
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(key);
                await handle.ToUniTask();
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    return handle.Result;
                }
                Debug.LogError($"Failed to load prefab: {key}");
                return null;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading {key}: {e.Message}");
                return null;
            }
        }

        public void ReleaseAsset(Object asset)
        {
            if (asset != null)
            {
                Addressables.Release(asset);
            }
        }
    }
}
