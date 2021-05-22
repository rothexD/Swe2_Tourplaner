using System;
using System.IO;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public class FileSystemAccess : IFileSystemAccess
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<bool> SaveToFileAsync(string path, string output)
        {
            try
            {
                await File.WriteAllTextAsync(path, output);
                return true;
            }
            catch (Exception e)
            {
                log.Error("could not save file");
                log.Debug(e.StackTrace); 
                throw e;
            }
        }
        public async Task<string> ImportFromJsonFileAsync(string path)
        {
            try
            {
                return await File.ReadAllTextAsync(path);
            }
            catch (Exception e)
            {
                log.Error("could not deserialize file");
                log.Debug(e.StackTrace); 
                throw e;
            }
        }
        public bool RemoveFileFromFileSystem(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                log.Error("could not delete file");
                log.Debug(e.StackTrace); 
                throw e;
            }
        }
        public byte[] ifFilExistReadAllBytes(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
           return Array.Empty<byte>();
        }
        public async Task<bool> SaveToFileSystemFromStreamAsync(string path,Stream stream)
        {
            try
            {
                var fileInfo = new FileInfo(path);
           using (var fileStream = fileInfo.OpenWrite())
           {
              await stream.CopyToAsync(fileStream);
              return true;
           }
        }
            catch (Exception e)
            {
                log.Error("could not delete file");
                log.Debug(e.StackTrace); 
                throw e;
            }
}
    }
}
