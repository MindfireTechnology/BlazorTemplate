# Blazor Template

This template is meant to help someone get going with a new Blazor proeject faster by including features that are helpful for almost every project. It also includes examples of common scenerios.

This project also uses BlazorStateManager as a NuGet package. The BlazorStateManager has a few helpful tools:

1. `IStateManager` - This helps coordinate data that is perhaps shared between different parts of a page. When a change is committed, other observers can see that the OnCommited for this type has occurred and can get the new changes. The data can be keyed off of type alone or type and a string key. This data is also stored using the default `IStoragePersistance` instance.

1. `IStoragePersistance` - This is a place that data can be stored based on type or type and a string key. The three varients of this interface are the `LocalStoragePersistance`, `CookieStoragePersistance`, and `SessionStoragePersistance`

1. `IMediator` - This is a simple publish / subscribe eventing for the local system. Many of the features of the StateManager are built on top of this interface. The idea is that this is used for different events in the application to help de-couple different parts of the application. It also only keeps a weak reference to any observer so it will not interfeare with garbage collection.
