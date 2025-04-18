using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class StartGameView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        public Button StartButton => _startButton;
    }
}
