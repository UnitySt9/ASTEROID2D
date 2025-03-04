namespace _Project.Scripts
{
    public class Debris : SpaceObject
    {
        private readonly int _timeOfDeath = 3;

        protected override void Start()
        {
            Speed = 10f;
            base.Start();
            Destroy(gameObject, _timeOfDeath);
        }
    }
}