using System;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public interface IStateManager
	{
		ValueTask CommitState<T>(string name, T value);
		ValueTask CommitState<T>(T value);

		ValueTask<T> GetState<T>() where T : class, new();
		ValueTask<T> GetState<T>(string name) where T : class, new();

		ValueTask OnCommitted<T>(object subscriber, Action<object, T> handler);
		ValueTask OnCommitted<T>(object subscriber, string name, Action<object, T> handler);

		// ValueTask<IObservable<T>>
	}
}
