// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ActiveTenant
{
	public class CreateTenantModel
	{
		[Required] [ProtectedPersonalData] public string Name { get; set; }

		public string ConcurrencyStamp { get; set; }
	}
}