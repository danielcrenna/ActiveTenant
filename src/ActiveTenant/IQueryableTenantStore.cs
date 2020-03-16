// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;

namespace ActiveTenant
{
	public interface IQueryableTenantStore<TTenant> : ITenantStore<TTenant>, IDisposable
	{
		IQueryable<TTenant> Tenants { get; }
	}
}