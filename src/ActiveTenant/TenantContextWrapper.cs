// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveTenant
{
	public class TenantContextWrapper<TTenant> : ITenantContext<TTenant> where TTenant : class, new()
	{
		public TenantContextWrapper(TTenant tenant) => Value = tenant;

		public TTenant Value { get; }
	}
}