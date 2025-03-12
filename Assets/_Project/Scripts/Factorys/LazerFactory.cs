using UnityEngine;

namespace _Project.Scripts
{
    public class LazerFactory
    {
        private Lazer _lazerPrefab;

        public LazerFactory(Lazer lazerPrefab)
        {
            _lazerPrefab = lazerPrefab;
        }

        public void CreateLazer(Transform firePoint)
        {
            Object.Instantiate(_lazerPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
