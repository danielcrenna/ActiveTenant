// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ActiveTenant
{
	public interface ITenantValidator<in TTenant, TKey> where TKey : IEquatable<TKey>
	{
		Task<IdentityResult> ValidateAsync(TTenant tenant);
	}
}