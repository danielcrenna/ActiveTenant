// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveTenant
{
	public class ApplicationContextWrapper<TApplication> : IApplicationContext<TApplication>
		where TApplication : class, new()
	{
		public ApplicationContextWrapper(TApplication application) => Value = application;

		public TApplication Value { get; }
	}
}