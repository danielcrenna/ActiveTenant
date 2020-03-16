// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveTenant
{
	public static class Constants
	{
		public static class MultiTenancy
		{
			public const string DefaultTenantName = "defaultTenant";
			public const string TenantHeader = "X-Api-Tenant";
			public const string ApplicationHeader = "X-Api-Application";
		}

		public static class Claims
		{
			public const string TenantId = "tenantId";
			public const string TenantName = "tenantName";
		}

		public static class ContextKeys
		{
			public const string Tenant = nameof(Tenant);
			public const string Application = nameof(Application);
		}
	}
}