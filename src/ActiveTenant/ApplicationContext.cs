// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveTenant
{
	public class ApplicationContext<TApplication> where TApplication : class
	{
		public TApplication Application { get; set; }
		public string[] Identifiers { get; set; }
	}
}