using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public interface IMediator
	{
		// Observalbe?
		// Extension Methods To Assist?

		ValueTask Subscribe(object subscriber, string topic, Action<object, string> handler);
		ValueTask Subscribe<T>(object subscriber, Action<object, T> handler);
		ValueTask Subscribe<T>(object subscriber, string topic, Action<object, T> handler);

		ValueTask Publish(object sender, string topic, string value);
		ValueTask Publish<T>(object sender, T value);
		ValueTask Publish<T>(object sender, T value, string topic);

		ValueTask UnSubscribe(object subscriber, string topic);
		ValueTask UnSubscribe<T>(object subscriber);
		ValueTask UnSubscribe<T>(object subscriber, string topic);
		ValueTask UnSubscribeAll(object subscriber);
	}
}
