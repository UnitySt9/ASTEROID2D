using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class StartGameView : MonoBehaviour
    {
        [field:SerializeField] public Button StartButton { get; private set; }
        [field:SerializeField] public Button NoAdsButton { get; private set; }
        [field:SerializeField] public GameObject AlreadyPurchasedText { get; private set; }
    }
}
