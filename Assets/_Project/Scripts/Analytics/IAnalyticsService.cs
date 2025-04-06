namespace _Project.Scripts
{
    public interface IAnalyticsService
    {
        void TrackGameStart();
        void TrackGameEnd(int shotsFired, int lasersUsed, int objectsDestroyed);
        void TrackLaserUsed();
    }
}
