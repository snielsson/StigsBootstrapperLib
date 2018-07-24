# StigsBootstrapperLib

A lib wrapping simple injector to help avoiding common registration errors and unwnated implicit behaviour.

Provides IIocContainer and IServiceLocator interfaces and an implementation that ensures that alle registrations must be done explicitly and checks that overriding registrations overrides an existing registration.

