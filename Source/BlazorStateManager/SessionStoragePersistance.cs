using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public class SessionStoragePersistance : IStoragePersistance
	{
		protected Dictionary<string, object> State = new Dictionary<string, object>();

		public async ValueTask<T> Retreive<T>(string name) where T : class, new()
		{
			if (State.ContainsKey(name))
				return State[name] as T;

			return null;
		}

		public async ValueTask Store<T>(string name, T data)
		{
			if (State.ContainsKey(name))
				State[name] = data;
			else
				State.Add(name, data);
		}
	}
}
