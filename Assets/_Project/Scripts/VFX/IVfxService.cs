using UnityEngine;

namespace _Project.Scripts
{
    public interface IVfxService
    {
        void PlayShootVfx(Vector3 position);
        void PlayObjectExplosionVfx(Vector3 position);
    }
}
