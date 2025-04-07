using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts
{
    public interface IAddressablesLoader
    {
        UniTask<GameObject> LoadPrefabAsync(string key);
        UniTask<T> LoadAssetAsync<T>(string key) where T : Object;
        void ReleaseAsset(Object asset);
    }
}
