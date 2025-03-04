namespace _Project.Scripts
{
    public interface IInputProvider
    {
        float GetHorizontalAxis();
        bool IsAccelerating();
        bool IsShooting();
        bool IsShootingLaser();
    }
}
