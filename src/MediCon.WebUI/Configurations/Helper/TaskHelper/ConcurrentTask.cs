namespace MediCon.WebUI.Configurations.Helper.TaskHelper;

public class ConcurrentTask
{
    public static async Task<(T1, T2)> Execute<T1, T2>(
        Func<CancellationToken, Task<T1>> task1Func,
        Func<CancellationToken, Task<T2>> task2Func,
        CancellationToken cancellationToken)
    {
        var task1 = task1Func(cancellationToken);
        var task2 = task2Func(cancellationToken);

        await Task.WhenAll(task1, task2).ConfigureAwait(false);

        return (await task1.ConfigureAwait(false), await task2.ConfigureAwait(false));
    }
}
