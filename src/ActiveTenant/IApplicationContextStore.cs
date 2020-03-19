// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace ActiveTenant
{
	public interface IApplicationContextStore<TApplication> where TApplication : class
	{
		Task<ApplicationContext<TApplication>> FindByKeyAsync(string applicationKey);
	}
}