using System.Threading.Tasks;

namespace _Project.Scripts
{
    public interface ICloudSaveService
    {
        Task InitializeAsync();
        Task SaveAsync(GameData data);
        Task<GameData> LoadAsync();
        Task<bool> SynchronizeAsync();
    }
}
