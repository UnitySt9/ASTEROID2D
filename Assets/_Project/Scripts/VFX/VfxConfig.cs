using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "VFX/VfxConfig")]
    public class VfxConfig : ScriptableObject
    {
        public GameObject shootVfxPrefab;
        public GameObject objectExplosionVfxPrefab;
    }
}
