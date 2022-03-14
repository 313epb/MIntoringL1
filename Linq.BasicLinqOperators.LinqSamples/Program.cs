// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

// See the ReadMe.html for additional information

namespace Task
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			var harnesses = new List<SampleHarness>();


			var linqHarness = new LinqSamples();
			harnesses.Add(linqHarness);

			Application.EnableVisualStyles();

			using (var form = new SampleForm("HomeWork - Mihail Romanov", harnesses))
			{
				form.ShowDialog();
			}
		}
	}
}