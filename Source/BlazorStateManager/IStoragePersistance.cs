using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public interface IStoragePersistance
	{
		ValueTask<T> Retreive<T>(string name) where T : class, new();
		ValueTask Store<T>(string name, T data);
	}
}
