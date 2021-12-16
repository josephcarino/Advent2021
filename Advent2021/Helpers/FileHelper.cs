namespace josephcarino.Advent2021.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string ReadDescriptionFromFile(string basePath, int problemId, int partId) => File.Exists(Path.Combine(Directory.GetCurrentDirectory(), String.Format(basePath, problemId, partId)))
                ? File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), String.Format(basePath, problemId, partId))) : "???";
    }
}
