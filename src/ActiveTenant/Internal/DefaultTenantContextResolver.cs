// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using ActiveCaching;
using ActiveTenant.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ActiveTenant.Internal
{
	internal sealed class DefaultTenantContextResolver<TTenant> : ITenantContextResolver<TTenant> where TTenant : class
	{
		private readonly ILogger _logger;
		private readonly IOptions<MultiTenancyOptions> _options;
		private readonly ICache _tenantCache;
		private readonly ITenantContextStore<TTenant> _tenantContextStore;

		public DefaultTenantContextResolver(ICache tenantCache, ITenantContextStore<TTenant> tenantContextStore,
			IOptions<MultiTenancyOptions> options, ILogger<ITenantContextResolver<TTenant>> logger)
		{
			_tenantCache = tenantCache;
			_tenantContextStore = tenantContextStore;
			_options = options;
			_logger = logger;
		}

		public async Task<ITenantContext<TTenant>> ResolveAsync(HttpContext http)
		{
			if (string.IsNullOrWhiteSpace(_options.Value.TenantHeader) ||
			    !http.Request.Headers.TryGetValue(_options.Value.TenantHeader, out var tenantKey))
			{
				tenantKey = http?.Request?.Host.Value.ToUpperInvariant();
			}

			var useCache = _options.Value.TenantLifetimeSeconds.HasValue;
			if (!useCache)
			{
				return await _tenantContextStore.FindByKeyAsync(tenantKey);
			}

			if (_tenantCache.Get($"{Constants.ContextKeys.Tenant}:{tenantKey}") is TenantContext<TTenant> tenantContext)
			{
				return tenantContext;
			}

			tenantContext = await _tenantContextStore.FindByKeyAsync(tenantKey) as TenantContext<TTenant>;
			if (tenantContext == null)
			{
				return null;
			}

			foreach (var identifier in tenantContext.Identifiers ?? Enumerable.Empty<string>())
			{
				_tenantCache.Set($"{Constants.ContextKeys.Tenant}:{identifier}", tenantContext,
					TimeSpan.FromSeconds(_options.Value.TenantLifetimeSeconds.Value));
			}

			return tenantContext;
		}
	}
}