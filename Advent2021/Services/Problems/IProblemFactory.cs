namespace josephcarino.Advent2021.Services.Problems
{
    public interface IProblemFactory
    {
        public abstract IEnumerable<int> GetProblemIds();

        public abstract IProblem GetProblemById(int id);
    }
}
