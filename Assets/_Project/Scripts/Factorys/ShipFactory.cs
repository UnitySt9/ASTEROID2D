using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ShipFactory 
    {
        private readonly DiContainer _container;
        private readonly ShipMovement _shipPrefab;

        public ShipFactory(DiContainer container, ShipMovement shipPrefab)
        {
            _container = container;
            _shipPrefab = shipPrefab;
        }

        public ShipMovement Create( Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ShipMovement>(_shipPrefab, parent);
        }
    }
}
