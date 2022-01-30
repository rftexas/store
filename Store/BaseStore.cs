using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("Store.Tests")]
namespace Store;

/// <inheritdoc />
internal class BaseStore<TState> : IStore<TState> where TState : IEquatable<TState>
{
    private readonly IReducer<TState> _reducers;
    private readonly List<TState> _stateHistory = new();

    private readonly Collection<Action<TState>> _callbacks = new();

    public BaseStore(IReducer<TState> reducer, TState initialState = default)
    {
        _reducers = reducer;
        _stateHistory.Add(initialState);
    }

    public TState Dispatch(IAction<TState> action)
    {
        var state = _stateHistory.Last();

        var newState = _reducers.Act(state, action);
        _callbacks.AsParallel().ForAll(c => c(newState));

        return newState;
    }

    public void RegisterCallback(Action<TState> callback)
    {
        _callbacks.Add(callback);
    }
}