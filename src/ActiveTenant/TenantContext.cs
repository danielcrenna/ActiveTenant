// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveTenant
{
	public class TenantContext<TTenant> : ITenantContext<TTenant>
	{
		public string[] Identifiers { get; set; }
		public TTenant Value { get; set; }
	}
}