// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace HQ.Platform.Api.Models
{
	public interface IApplication<TKey>
	{
		TKey Id { get; set; }
		string Name { get; set; }
	}
}