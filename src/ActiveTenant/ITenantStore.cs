// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ActiveTenant
{
	public interface ITenantStore<TTenant>
	{
		CancellationToken CancellationToken { get; }

		Task<string> GetTenantIdAsync(TTenant tenant, CancellationToken cancellationToken);
		Task<string> GetTenantNameAsync(TTenant tenant, CancellationToken cancellationToken);
		Task<int> GetCountAsync(CancellationToken cancellationToken);

		Task<IdentityResult> CreateAsync(TTenant tenant, CancellationToken cancellationToken);
		Task<IdentityResult> UpdateAsync(TTenant tenant, CancellationToken cancellationToken);
		Task<IdentityResult> DeleteAsync(TTenant tenant, CancellationToken cancellationToken);

		Task SetTenantNameAsync(TTenant tenant, string name, CancellationToken cancellationToken);
		Task SetNormalizedTenantNameAsync(TTenant tenant, string normalizedName, CancellationToken cancellationToken);

		Task<TTenant> FindByIdAsync(string tenantId, CancellationToken cancellationToken);
		Task<IEnumerable<TTenant>> FindByIdsAsync(IEnumerable<string> tenantIds, CancellationToken cancellationToken);
		Task<TTenant> FindByNameAsync(string normalizedTenantName, CancellationToken cancellationToken);
	}
}