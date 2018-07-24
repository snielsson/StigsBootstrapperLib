// Copyright 2018 Stig Schmidt Nielsson. This file is open source and distributed under the MIT license - see LICENSE.txt or https://opensource.org/licenses/MIT. 

using Shouldly;
using Xunit;

namespace StigsBootstrapperLib.Tests.ExternalDependencies.SimpleInjector {
	public class SimpleInjectorIocContainerTests {
		public interface ISomeTestService { }
		public class SomeTestService : ISomeTestService { }
		public class FakeSomeTestService : ISomeTestService { }

		private (SimpleInjectorIocContainer target, ISomeTestService fakeTestService) SetupOverrideTest() {
			var target = new SimpleInjectorIocContainer();
			ISomeTestService testService = new SomeTestService();
			ISomeTestService fakeTestService = new FakeSomeTestService();
			target.RegisterInstance(testService);
			return (target, fakeTestService);
		}

		[Fact]
		public void CanOverrideInstanceRegistrationWithFactoryMethod() {
			var (target, _) = SetupOverrideTest();
			target.Override<ISomeTestService>(() => new FakeSomeTestService());
			target.Resolve<ISomeTestService>().ShouldBeOfType<FakeSomeTestService>("Expected the overriden instance to be returned when overriding with factory method.");
		}

		[Fact]
		public void CanOverrideInstanceRegistrationWithInstanceRegistration() {
			var (target, fakeTestService) = SetupOverrideTest();
			target.Override(fakeTestService);
			target.Resolve<ISomeTestService>().ShouldBeOfType<FakeSomeTestService>("Expected the overriden instance to be returned when overriding with instance registration.");
		}

		[Fact]
		public void CanOverrideInstanceRegistrationWithSingletonRegistration() {
			var (target, _) = SetupOverrideTest();
			target.Override<ISomeTestService, FakeSomeTestService>();
			target.Resolve<ISomeTestService>().ShouldBeOfType<FakeSomeTestService>("Expected the overriden instance to be returned when overriding with singleton registration.");
		}
	}
}