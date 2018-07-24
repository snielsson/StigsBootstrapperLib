// Copyright © 2014-2018 Stig Schmidt Nielsson. This file is open source and distributed under the MIT license - see LICENSE.txt or https://opensource.org/licenses/MIT. 

using System;
using System.Linq;
using SimpleInjector;

namespace StigsBootstrapperLib.SimpleInjector {
	public class SimpleInjectorIocContainer : IIocContainer {
		private readonly Container _container;

		public SimpleInjectorIocContainer() => _container = new Container();

		public T Resolve<T>() => (T) _container.GetInstance(typeof(T));
		public IIocContainer RegisterInstance<T>(T instance) where T : class {
			_container.RegisterInstance(instance);
			return this;
		}
		public IIocContainer RegisterSingleton<TInterface, TImplementaion>() where TInterface : class where TImplementaion : class, TInterface {
			_container.RegisterSingleton<TInterface, TImplementaion>();
			return this;
		}
		public IIocContainer RegisterSingleton<T>(Func<T> factoryMethod) where T : class {
			_container.RegisterSingleton(factoryMethod);
			return this;
		}
		public IIocContainer Override<T>(T instance) where T : class {
			return OverrideCore<T>(lifeStyle => _container.RegisterInstance(instance));
		}
		public IIocContainer Override<TInterface, TImplementaion>() where TInterface : class where TImplementaion : class, TInterface {
			return OverrideCore<TInterface>(lifeStyle => _container.Register<TInterface, TImplementaion>(lifeStyle));
		}
		public IIocContainer Override<T>(Func<T> factoryMethod) where T : class {
			return OverrideCore<T>(lifeStyle => _container.Register(factoryMethod, lifeStyle));
		}

		private IIocContainer OverrideCore<T>(Action<Lifestyle> action) {
			var type = typeof(T);
			var instanceProducer = _container.GetCurrentRegistrations().SingleOrDefault(x => x.ServiceType == type);
			if (instanceProducer == null) throw new ArgumentException($"No registration of type {type} to override.");
			_container.Options.AllowOverridingRegistrations = true;
			action(instanceProducer.Lifestyle);
			_container.Options.AllowOverridingRegistrations = false;
			return this;
		}
	}
}