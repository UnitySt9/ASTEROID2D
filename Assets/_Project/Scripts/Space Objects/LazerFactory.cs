using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class LazerFactory : MonoBehaviour
    {
        [SerializeField] private Lazer _lazerPrefab;

        public event Action<Lazer> OnLazerCreated;

        public void CreateLazer(Transform firePoint)
        {
            Lazer lazer = Instantiate(_lazerPrefab, firePoint.position, firePoint.rotation);
            OnLazerCreated?.Invoke(lazer);
        }
    }
}
