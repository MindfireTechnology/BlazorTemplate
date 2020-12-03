using System;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public interface IStateService
	{
		ValueTask CommitState<T>(string name, T value);
		ValueTask CommitState<T>(T value);
		ValueTask<T> GetState<T>() where T : class, new();
	}
}
