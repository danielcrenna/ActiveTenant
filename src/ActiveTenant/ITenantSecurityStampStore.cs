// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace ActiveTenant
{
	public interface ITenantSecurityStampStore<TTenant> : ITenantStore<TTenant>
	{
		Task<string> GetSecurityStampAsync(TTenant tenant, CancellationToken cancellationToken);
		Task SetSecurityStampAsync(TTenant tenant, string stamp, CancellationToken cancellationToken);
	}
}