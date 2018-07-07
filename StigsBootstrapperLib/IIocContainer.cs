// Copyright 2018 Stig Schmidt Nielsson. This file is open source and distributed under the MIT license - see LICENSE.txt or https://opensource.org/licenses/MIT. 

using System;

namespace StigsBootstrapperLib {
	public interface IIocContainer : IServiceLocator {
		IIocContainer RegisterInstance<T>(T instance) where T : class;
		IIocContainer RegisterSingleton<TInterface, TImplementaion>() where TInterface : class where TImplementaion : class, TInterface;
		IIocContainer RegisterSingleton<T>(Func<T> factoryMethod) where T : class;
		IIocContainer Override<T>(T instance) where T : class;
		IIocContainer Override<TInterface, TImplementaion>() where TInterface : class where TImplementaion : class, TInterface;
		IIocContainer Override<T>(Func<T> factoryMethod) where T : class;
	}
}