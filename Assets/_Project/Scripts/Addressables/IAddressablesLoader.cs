using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts
{
    public interface IAddressablesLoader
    {
        UniTask<GameObject> LoadShipPrefab();
        UniTask<GameObject> LoadAsteroidPrefab();
        UniTask<GameObject> LoadUFOPrefab();
        UniTask<GameObject> LoadBulletPrefab();
        UniTask<GameObject> LoadLazerPrefab();
        void ReleaseAsset(Object asset);
    }
}
