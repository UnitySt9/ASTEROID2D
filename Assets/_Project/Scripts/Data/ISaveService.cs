namespace _Project.Scripts
{
    public interface ISaveService
    {
        GameData Load();
        void Save(GameData data);
    }
}
