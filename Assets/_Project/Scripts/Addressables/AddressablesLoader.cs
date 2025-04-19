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

        public async UniTask<GameObject> LoadShipPrefab()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(SHIP_PREFAB_KEY);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load ship prefab");
            return null;
        }

        public async UniTask<GameObject> LoadAsteroidPrefab()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(ASTEROID_PREFAB_KEY);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load asteroid prefab");
            return null;
        }

        public async UniTask<GameObject> LoadUFOPrefab()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(UFO_PREFAB_KEY);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load UFO prefab");
            return null;
        }

        public async UniTask<GameObject> LoadBulletPrefab()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(BULLET_PREFAB_KEY);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load bullet prefab");
            return null;
        }

        public async UniTask<GameObject> LoadLazerPrefab()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(LAZER_PREFAB_KEY);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load lazer prefab");
            return null;
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
