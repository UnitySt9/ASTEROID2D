using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    public class AddressablesLoader : IAddressablesLoader
    {
        public async UniTask<GameObject> LoadPrefabAsync(string key)
        {
            return await LoadAssetAsync<GameObject>(key);
        }

        public async UniTask<T> LoadAssetAsync<T>(string key) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            Debug.LogError($"Failed to load asset with key: {key}");
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
