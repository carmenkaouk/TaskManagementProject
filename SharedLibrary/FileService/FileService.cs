namespace SharedLibrary.FileService;

public interface IFileService
{
    Task<string> ReadFile(string? path);
    Task<byte[]> ReadByteArray(string? path);
    Task WriteFile(string? path, string text);
    void ValidatePath(string? path);
    void CreateFile(string path);
    void CreateDirectoryIfMissing(string? path);
}

public class FileService : IFileService
{
    public async Task<string> ReadFile(string? path)
    {
        if (File.Exists(path))
            return await File.ReadAllTextAsync(path);
        else
            throw new FileNotFoundException();

    }

    public async Task<byte[]> ReadByteArray(string? path)
    {
        if (File.Exists(path))
            return await File.ReadAllBytesAsync(path);
        else
            throw new FileNotFoundException();

    }
    public async Task WriteFile(string? path, string text)
    {
        if (Directory.Exists(Path.GetDirectoryName(path)))
        {
            await File.WriteAllTextAsync(path, text);
        }
        else
        {

            throw new DirectoryNotFoundException();
        }

    }
    public void CreateFile(string path)
    {

        if (!File.Exists(path))
        {
            File.Create(path);
        }

    }
    public void ValidatePath(string? path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException();

    }
    public void CreateDirectoryIfMissing(string? path)
    {
        ValidatePath(path);
        Directory.CreateDirectory(path);
    }

}
