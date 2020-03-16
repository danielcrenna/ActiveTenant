// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using ActiveErrors;
using HQ.Platform.Identity.Models;

namespace ActiveTenant
{
	public interface ITenantService
	{
		Task<Operation<int>> GetCountAsync();
	}

	public interface ITenantService<TTenant> : ITenantService
	{
		Task<Operation<IEnumerable<TTenant>>> GetAsync();
		Task<Operation<TTenant>> CreateAsync(CreateTenantModel model);
		Task<Operation> UpdateAsync(TTenant tenant);
		Task<Operation> DeleteAsync(string id);

		Task<Operation<TTenant>> FindByIdAsync(string id);
		Task<Operation<TTenant>> FindByNameAsync(string name);

		Task<Operation<IEnumerable<TTenant>>> FindByPhoneNumberAsync(string phoneNumber);
		Task<Operation<IEnumerable<TTenant>>> FindByEmailAsync(string email);
		Task<Operation<IEnumerable<TTenant>>> FindByUserNameAsync(string username);
	}
}