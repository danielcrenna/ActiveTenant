// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using ActiveRoutes;
using ActiveTenant.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveTenant
{
	public static class Use
	{
		public static IApplicationBuilder UseMultiTenancy<TTenant>(this IApplicationBuilder app)
			where TTenant : class, ITenant<string>, new()
		{
			return app.UseMultiTenancy<TTenant, string>();
		}

		public static IApplicationBuilder UseMultiTenancy<TTenant, TKey>(this IApplicationBuilder app)
			where TTenant : class, ITenant<TKey>, new()
		{
			return app.Use(async (context, next) =>
			{
				if (context.FeatureEnabled<MultiTenancyOptions>(out var options))
				{
					await ExecuteFeature(context, options, next);
				}
			});

			async Task ExecuteFeature(HttpContext c, MultiTenancyOptions o, Func<Task> next)
			{
				var tenantResolver = c.RequestServices.GetRequiredService<ITenantContextResolver<TTenant>>();
				var tenantContext = await tenantResolver.ResolveAsync(c);
				if (tenantContext != null)
				{
					c.SetTenantContext(tenantContext);
				}
				else
				{
					if (!o.RequireTenant)
					{
						if (!string.IsNullOrWhiteSpace(o.DefaultTenantId) &&
						    !string.IsNullOrWhiteSpace(o.DefaultTenantName))
						{
							c.SetTenantContext(new TenantContext<TTenant>
							{
								Value = new TTenant
								{
									Name = o.DefaultTenantName,
									Id = (TKey) (Convert.ChangeType(o.DefaultTenantId, typeof(TKey)) ??
									             default(TKey))
								}
							});
						}
					}
					else
					{
						c.Response.StatusCode = o.TenantRequiredStatusCode;
						return;
					}
				}

				await next();
			}
		}
	}
}