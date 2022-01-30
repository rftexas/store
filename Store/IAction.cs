namespace Store;

/// <summary>
/// An Action is used to tell the store what action to take against the Store.
/// </summary>
/// <typeparam name="TState">An item with state</typeparam>
public interface IAction<TState> where TState : IEquatable<TState> {
    string Type { get; }
}