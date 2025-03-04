using UnityEngine;

namespace _Project.Scripts
{
    public class KeyboardInputProvider : IInputProvider
    {
        public float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }

        public bool IsAccelerating()
        {
            return Input.GetKey(KeyCode.UpArrow);
        }

        public bool IsShooting()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }

        public bool IsShootingLaser()
        {
            return Input.GetKeyDown(KeyCode.Mouse1);
        }
    }
}
