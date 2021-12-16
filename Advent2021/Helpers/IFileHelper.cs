namespace josephcarino.Advent2021.Helpers
{
    public interface IFileHelper
    {
        public abstract string ReadDescriptionFromFile(string basePath, int problemId, int partId);
    }
}
