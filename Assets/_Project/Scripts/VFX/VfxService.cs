using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class VfxService : IVfxService
    {
        private readonly VfxConfig _config;

        public VfxService(VfxConfig config)
        {
            _config = config;
        }

        public void PlayShootVfx(Vector3 position)
        {
            Object.Instantiate(_config.shootVfxPrefab, position, Quaternion.identity);
        }

        public void PlayObjectExplosionVfx(Vector3 position)
        {
            Object.Instantiate(_config.objectExplosionVfxPrefab, position, Quaternion.identity);
        }
    }
}
