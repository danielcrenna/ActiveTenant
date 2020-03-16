// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ActiveTenant
{
	public interface IApplicationContextResolver<TApplication> where TApplication : class
	{
		Task<ApplicationContext<TApplication>> ResolveAsync(HttpContext http);
	}
}