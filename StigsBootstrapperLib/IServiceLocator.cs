// Copyright 2018 Stig Schmidt Nielsson. This file is open source and distributed under the MIT license - see LICENSE.txt or https://opensource.org/licenses/MIT. 

namespace StigsBootstrapperLib {
	public interface IServiceLocator {
		T Resolve<T>();
	}
}