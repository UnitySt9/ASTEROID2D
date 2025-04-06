using Firebase.Analytics;

namespace _Project.Scripts
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public void TrackGameStart()
        {
            FirebaseAnalytics.LogEvent("game_start");
        }

        public void TrackGameEnd(int shotsFired, int lasersUsed, int objectsDestroyed)
        {
            FirebaseAnalytics.LogEvent(
                "game_end",
                new Parameter("shots_fired", shotsFired),
                new Parameter("lasers_used", lasersUsed),
                new Parameter("objects_destroyed", objectsDestroyed)
            );
        }

        public void TrackLaserUsed()
        {
            FirebaseAnalytics.LogEvent("laser_used");
        }
    }
}
