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
            return await LoadPrefabAsync(SHIP_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadAsteroidPrefab()
        {
            return await LoadPrefabAsync(ASTEROID_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadUFOPrefab()
        {
            return await LoadPrefabAsync(UFO_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadBulletPrefab()
        {
            return await LoadPrefabAsync(BULLET_PREFAB_KEY);
        }

        public async UniTask<GameObject> LoadLazerPrefab()
        {
            return await LoadPrefabAsync(LAZER_PREFAB_KEY);
        }

        private async UniTask<GameObject> LoadPrefabAsync(string key)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load prefab with key: {key}");
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
