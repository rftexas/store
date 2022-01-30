using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Store.Tests;

public class BaseStoreTests
{
    [Fact]
    public void Calls_reducers()
    {
        var store = new BaseStore<State>(new Reducer(), new State("Initialize"));

        var action = new NewAction();

        store.Dispatch(action).LastAction.Should().Be(nameof(NewAction));
    }

    [Fact]
    public void Calls_callbacks()
    {
        var taskSource = new TaskCompletionSource();
        
        var store = new BaseStore<State>(new Reducer(), new State("Initialize"));
        store.RegisterCallback(s => taskSource.SetResult());

        store.Dispatch(new NewAction());
        taskSource.Task.IsCompletedSuccessfully.Should().BeTrue();

    }

    public class Reducer : IReducer<State>
    {
        public State Act(State state, IAction<State> action){
            if(action is NewAction newAction){
                return state with { LastAction = newAction.Type };
            }

            throw new NotImplementedException();
        }
    }

    public class NewAction : IAction<State>
    {
        public string Type => nameof(NewAction);
    }

    public sealed record State(string LastAction): IEquatable<State> {

        public bool Equals(State? other) => other?.LastAction?.Equals(LastAction) ?? false;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}