// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ActiveCaching;
using ActiveOptions;
using ActiveTenant.Configuration;
using ActiveTenant.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveTenant
{
	public static class Add
	{
		public static IServiceCollection AddMultiTenancy<TTenant, TApplication>(this IServiceCollection services,
			IConfiguration config)
			where TTenant : class, new()
			where TApplication : class, new()
		{
			return services.AddMultiTenancy<TTenant, TApplication>(config.FastBind);
		}

		public static IServiceCollection AddMultiTenancy<TTenant, TApplication>(this IServiceCollection services,
			Action<MultiTenancyOptions> configureAction = null)
			where TTenant : class, new()
			where TApplication : class, new()
		{
			return services
				.AddMultiTenancy<DefaultTenantContextResolver<TTenant>, TTenant,
					DefaultApplicationContextResolver<TApplication>, TApplication>(configureAction);
		}

		public static IServiceCollection AddMultiTenancy<TTenantResolver, TTenant, TApplicationResolver, TApplication>(
			this IServiceCollection services,
			IConfiguration config)
			where TTenantResolver : class, ITenantContextResolver<TTenant>
			where TTenant : class, new()
			where TApplicationResolver : class, IApplicationContextResolver<TApplication>
			where TApplication : class, new()
		{
			return services.AddMultiTenancy<TTenantResolver, TTenant, TApplicationResolver, TApplication>(
				config.FastBind);
		}

		public static IServiceCollection AddMultiTenancy<TTenantResolver, TTenant, TApplicationResolver, TApplication>(
			this IServiceCollection services,
			Action<MultiTenancyOptions> configureAction = null)
			where TTenantResolver : class, ITenantContextResolver<TTenant>
			where TTenant : class, new()
			where TApplicationResolver : class, IApplicationContextResolver<TApplication>
			where TApplication : class, new()
		{
			services.AddHttpContextAccessor();

			if (configureAction != null)
				services.Configure(configureAction);

			services.AddInProcessCache();

			services.AddScoped<ITenantContextResolver<TTenant>, TTenantResolver>();
			services.AddScoped(r => r.GetService<IHttpContextAccessor>()?.HttpContext?.GetTenantContext<TTenant>());
			services.AddScoped<ITenantContext<TTenant>>(r =>
				new TenantContextWrapper<TTenant>(r.GetService<TTenant>()));

			services.AddScoped<IApplicationContextResolver<TApplication>, TApplicationResolver>();
			services.AddScoped(r =>
				r.GetService<IHttpContextAccessor>()?.HttpContext?.GetApplicationContext<TApplication>());
			services.AddScoped<IApplicationContext<TApplication>>(r =>
				new ApplicationContextWrapper<TApplication>(r.GetService<TApplication>()));

			return services;
		}
	}
}