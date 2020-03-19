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
	internal sealed class DefaultApplicationContextResolver<TApplication> : IApplicationContextResolver<TApplication>
		where TApplication : class
	{
		private readonly ICache _applicationCache;
		private readonly IApplicationContextStore<TApplication> _applicationContextStore;
		private readonly ILogger _logger;
		private readonly IOptions<MultiTenancyOptions> _options;

		public DefaultApplicationContextResolver(ICache applicationCache,
			IApplicationContextStore<TApplication> applicationContextStore,
			IOptions<MultiTenancyOptions> options, ILogger<IApplicationContextResolver<TApplication>> logger)
		{
			_applicationCache = applicationCache;
			_applicationContextStore = applicationContextStore;
			_options = options;
			_logger = logger;
		}

		public async Task<ApplicationContext<TApplication>> ResolveAsync(HttpContext http)
		{
			if (string.IsNullOrWhiteSpace(_options.Value.ApplicationHeader) ||
			    !http.Request.Headers.TryGetValue(_options.Value.ApplicationHeader, out var tenantKey))
			{
				tenantKey = http?.Request?.Host.Value.ToUpperInvariant();
			}

			var useCache = _options.Value.ApplicationLifetimeSeconds.HasValue;
			if (!useCache)
			{
				return await _applicationContextStore.FindByKeyAsync(tenantKey);
			}

			if (_applicationCache.Get($"{Constants.ContextKeys.Application}:{tenantKey}") is
				ApplicationContext<TApplication> applicationContext)
			{
				return applicationContext;
			}

			applicationContext = await _applicationContextStore.FindByKeyAsync(tenantKey);
			if (applicationContext == null)
			{
				return null;
			}

			foreach (var identifier in applicationContext.Identifiers ?? Enumerable.Empty<string>())
			{
				_applicationCache.Set($"{Constants.ContextKeys.Application}:{identifier}", applicationContext,
					TimeSpan.FromSeconds(_options.Value.ApplicationLifetimeSeconds.Value));
			}

			return applicationContext;
		}
	}
}