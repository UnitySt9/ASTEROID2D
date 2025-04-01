using UnityEngine;

namespace _Project.Scripts
{
    public interface IShipIndicatorsView
    {
        void UpdateIndicators(ShipIndicatorsMessage message);
    }
}
