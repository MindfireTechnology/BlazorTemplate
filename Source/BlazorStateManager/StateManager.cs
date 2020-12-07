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

		public async ValueTask CommitState<T>(T value)
		{
			await CommitState(typeof(T).FullName, value);
			await Mediator.Publish<T>(this, value);
		}

		public async ValueTask CommitState<T>(string name, T value)
		{
			await Store.Store(name, value);
			await Mediator.Publish<T>(this, value, name);
		}

		public async ValueTask OnCommitted<T>(object subscriber, Action<object, T> handler)
		{
			await Mediator.Subscribe<T>(subscriber, handler);
		}

		public async ValueTask OnCommitted<T>(object subscriber, string topic, Action<object, T> handler)
		{
			await Mediator.Subscribe<T>(subscriber, topic, handler);
		}
	}
}
