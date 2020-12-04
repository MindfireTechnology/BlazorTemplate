using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public class StateManager : IStateManager
	{
		protected IStoragePersistance Store { get; }
		public IMediator Mediator { get; }

		public StateManager(IStoragePersistance store, IMediator mediator)
		{
			Store = store;
			Mediator = mediator;
		}

		public async ValueTask<T> GetState<T>() where T : class, new()
		{
			var result = await Store.Retreive<T>(typeof(T).FullName);
			if (result == null)
				return new();
			else
				return result;
		}

		public ValueTask CommitState<T>(T value)
		{
			return CommitState(typeof(T).FullName, value);
		}

		public ValueTask CommitState<T>(string name, T value)
		{
			return Store.Store(name, value);
		}

		public async ValueTask OnCommit<T>(object subscriber, Action<object, T> handler)
		{
			await Mediator.Subscribe<T>(subscriber, handler);
		}
	}
}
