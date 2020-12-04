using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStateManager
{
	public class BlazorMediator : IMediator
	{
		protected IList<TopicMap> Topics = new List<TopicMap>();

		public ILogger<BlazorMediator> Logger { get; }

		internal protected class TopicMap
		{
			public Type TopicType { get; set; }
			public string TopicString { get; set; }
			public virtual IList<SubscriberInfo> Subscribers { get; set; } = new List<SubscriberInfo>();
		}

		internal protected class SubscriberInfo
		{
			public WeakReference Subscriber { get; set; }
			public Delegate Action { get; set; }

			public SubscriberInfo() { }

			public SubscriberInfo(object subscriber, Delegate action)
			{
				Subscriber = new WeakReference(subscriber);
				Action = action;
			}
		}

		public BlazorMediator(ILogger<BlazorMediator> logger)
		{
			Logger = logger;
		}

		public async ValueTask Subscribe(object subscriber, string topic, Action<object, string> handler)
		{
			Add(null, topic, new SubscriberInfo(subscriber, handler));
			Logger?.LogInformation($"Subscription received from '{subscriber?.GetHashCode()}:{subscriber}' For Topic '{topic}'");
		}

		public async ValueTask Subscribe<T>(object subscriber, Action<object, T> handler)
		{
			Add(typeof(T), null, new SubscriberInfo(subscriber, handler));
			Logger?.LogInformation($"Subscription received from '{subscriber?.GetHashCode()}:{subscriber}' For Type '{typeof(T).Name}'");
		}

		public async ValueTask Subscribe<T>(object subscriber, string topic, Action<object, T> handler)
		{
			Add(typeof(T), topic, new SubscriberInfo(subscriber, handler));
			Logger?.LogInformation($"Subscription received from '{subscriber?.GetHashCode()}:{subscriber}' For Type '{typeof(T).Name}' / Topic '{topic}'");
		}


		public async ValueTask Publish(object sender, string topic, string value)
		{
			Logger?.LogInformation($"Publish received from '{sender?.GetHashCode()}:{sender}' For Topic '{topic}'");
			await PublishInternal(null, topic, sender, value);
		}

		public async ValueTask Publish<T>(object sender, T value)
		{
			Logger?.LogInformation($"Publish received from '{sender?.GetHashCode()}:{sender}' For Type '{typeof(T).Name}'");
			await PublishInternal(typeof(T), null, sender, value);
		}

		public async ValueTask Publish<T>(object sender, T value, string topic)
		{
			Logger?.LogInformation($"Publish received from '{sender?.GetHashCode()}:{sender}' For Type '{typeof(T).Name}' / Topic '{topic}'");
			await PublishInternal(typeof(T), topic, sender, value);
		}


		public async ValueTask UnSubscribe(object subscriber, string topic)
		{
			Logger?.LogInformation($"Unsubscribe received from '{subscriber?.GetHashCode()}:{subscriber}' Topic '{topic}'");
			await UnSubscribeInternal(null, topic, subscriber);
		}

		public async ValueTask UnSubscribe<T>(object subscriber)
		{
			Logger?.LogInformation($"Unsubscribe received from '{subscriber?.GetHashCode()}:{subscriber}' For Type '{typeof(T).Name}'");
			await UnSubscribeInternal(typeof(T), null, subscriber);
		}

		public async ValueTask UnSubscribe<T>(object subscriber, string topic)
		{
			Logger?.LogInformation($"Unsubscribe received from '{subscriber?.GetHashCode()}:{subscriber}' For Type '{typeof(T).Name}' / Topic '{topic}'");
			await UnSubscribeInternal(typeof(T), topic, subscriber);
		}

		public async ValueTask UnSubscribeAll(object subscriber)
		{
			Logger?.LogInformation($"Unsubscribe All received from '{subscriber?.GetHashCode()}:{subscriber}'");
			foreach (var topicMap in Topics)
			{
				topicMap.Subscribers.Where(n => n.Subscriber.IsAlive && n.Subscriber.Target == subscriber)
					.ToList()
					.ForEach(n => topicMap.Subscribers.Remove(n));
			}
		}


		protected void Add(Type type, string topic, SubscriberInfo subscriberInfo)
		{
			var topicMap = Topics.SingleOrDefault(n => n.TopicString == topic && n.TopicType == type);

			if (topicMap == null)
			{
				topicMap = new TopicMap
				{
					TopicString = topic,
					TopicType = type
				};

				Topics.Add(topicMap);
			}

			topicMap.Subscribers.Add(subscriberInfo);
		}

		protected virtual async ValueTask PublishInternal(Type T, string topicString, object sender, object value)
		{
			var topicList = Topics.Where(n => n.TopicString == topicString && ((T == null && n.TopicType == null) || n.TopicType.IsAssignableFrom(T)));

			foreach (var topic in topicList)
			{
				var deadSubscriberList = new Lazy<List<SubscriberInfo>>();
				foreach (var subscriber in topic.Subscribers)
				{
					if (!subscriber.Subscriber.IsAlive)
						deadSubscriberList.Value.Add(subscriber);

					Logger?.LogInformation(
						$"Invoking Topic '{(topic.TopicType == null ? string.Empty : $"Type: {topic.TopicType}")} {(string.IsNullOrWhiteSpace(topic.TopicString) ? string.Empty : topic.TopicString)}' '{subscriber?.GetHashCode()}:{subscriber}'");
					
					subscriber.Action.DynamicInvoke(sender, value);
				}

				if (deadSubscriberList.IsValueCreated)
				{
					// Prune the list
					foreach (var subscriber in deadSubscriberList.Value)
					{
						Logger?.LogInformation($"Pruning Dead Subscriber");
						topic.Subscribers.Remove(subscriber);
					}

					if (topic.Subscribers.Count == 0)
					{
						Logger?.LogInformation($"Removing Unused Topic '{(topic.TopicType == null ? string.Empty : $"Type: {topic.TopicType}")} {(string.IsNullOrWhiteSpace(topic.TopicString) ? string.Empty : topic.TopicString)}'");
						Topics.Remove(topic);
					}
				}
			}
		}

		protected virtual async ValueTask UnSubscribeInternal(Type type, string topic, object subscriber)
		{
			var topicMap = Topics
				.SingleOrDefault(n => (n.TopicString == topic) && (n.TopicType == type));

			if (topicMap != null)
			{
				topicMap.Subscribers
					.Where(n => n.Subscriber.IsAlive && n.Subscriber.Target == subscriber)
					.ToList()
					.ForEach(n => topicMap.Subscribers.Remove(n));
			}
		}
	}
}
