using System.Linq.Expressions;
namespace Store;
/// <summary>
/// A store that holds the immutable application state.
/// </summary>
/// <typeparam name="TState"></typeparam>
public interface IStore<TState> where TState : IEquatable<TState> {
    /// <summary>
    /// Dispatch an Action to update the Store's state.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    TState Dispatch(IAction<TState> action);

    /// <summary>
    /// Register an Action to be run whenever the State changes.
    /// </summary>
    /// <param name="callback">A function to be run when the state changes.</param>
    void RegisterCallback(Action<TState> callback);
}
