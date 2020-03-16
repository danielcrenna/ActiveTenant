// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace ActiveTenant
{
	public static class HttpContextExtensions
	{
		public static void SetTenantContext<TTenant>(this HttpContext context, ITenantContext<TTenant> tenantContext)
			where TTenant : class
		{
			context.Items[Constants.ContextKeys.Tenant] = tenantContext;
		}

		public static TenantContext<TTenant> GetTenantContext<TTenant>(this HttpContext context) where TTenant : class
		{
			return context.Items.TryGetValue(Constants.ContextKeys.Tenant, out var tenantContext)
				? tenantContext as TenantContext<TTenant>
				: default;
		}

		public static void SetApplicationContext<TApplication>(this HttpContext context,
			ApplicationContext<TApplication> tenantContext)
			where TApplication : class
		{
			context.Items[Constants.ContextKeys.Application] = tenantContext;
		}

		public static ApplicationContext<TApplication> GetApplicationContext<TApplication>(this HttpContext context)
			where TApplication : class
		{
			return context.Items.TryGetValue(Constants.ContextKeys.Application, out var tenantContext)
				? tenantContext as ApplicationContext<TApplication>
				: default;
		}
	}
}