using UnityEngine;

namespace _Project.Scripts
{
    public class Lazer : MonoBehaviour
    {
        private readonly float _laserDuration = 0.5f;

        private void Start()
        {
            Destroy(gameObject, _laserDuration);
        }
    }
}
