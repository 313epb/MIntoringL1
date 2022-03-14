namespace Task.LinqExecutors
{
    public interface ILinqExecutor<in TSource, out TResult>
    {
        TResult Execute(TSource source);
    }
}