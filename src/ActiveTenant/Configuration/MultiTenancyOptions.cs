// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net;
using ActiveRoutes;

namespace ActiveTenant.Configuration
{
	public class MultiTenancyOptions : IFeatureToggle
	{
		public int TenantRequiredStatusCode = (int) HttpStatusCode.NotFound;
		public bool RequireTenant { get; set; } = false;

		public string DefaultTenantId { get; set; } = "0";
		public string DefaultTenantName { get; set; } = Constants.MultiTenancy.DefaultTenantName;

		public string TenantHeader { get; set; } = Constants.MultiTenancy.TenantHeader;
		public int? TenantLifetimeSeconds { get; set; } = 180;

		public string ApplicationHeader { get; set; } = Constants.MultiTenancy.ApplicationHeader;
		public int? ApplicationLifetimeSeconds { get; set; } = 180;
		public bool Enabled { get; set; } = true;
	}
}