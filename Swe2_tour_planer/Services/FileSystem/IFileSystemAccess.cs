using System.IO;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public interface IFileSystemAccess
    {
        public Task<bool> SaveToFileAsync(string path, string output);
        public Task<string> ImportFromJsonFileAsync(string path);
        public bool RemoveFileFromFileSystem(string path);
        public byte[] ifFilExistReadAllBytes(string path);
        public Task<bool> SaveToFileSystemFromStreamAsync(string path, Stream stream);
    }
}
