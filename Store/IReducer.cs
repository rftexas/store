namespace Store;

/// <summary>
/// A Reducer converts an Action into a state change.
/// </summary>
/// <typeparam name="TState"></typeparam>
public interface IReducer<TState> where TState : IEquatable<TState>
{
    /// <summary>
    /// Act against an Action in a Store.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    TState Act(TState state, IAction<TState> action);
}