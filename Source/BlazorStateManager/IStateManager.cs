using System;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public interface IStateManager
	{
		ValueTask CommitState<T>(string name, T value);
		ValueTask CommitState<T>(T value);
		ValueTask<T> GetState<T>() where T : class, new();

		ValueTask OnCommit<T>(object subscriber, Action<object, T> handler);
	}
}
