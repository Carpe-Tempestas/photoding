using System;
using System.Collections.Generic;
using System.Text;
using Trebuchet.Helper_Classes;

namespace Trebuchet.Settings.ImageTypes
{
	interface IMultipleMatricies
	{		
		List<ImageColorMatrix> Matricies
		{
			get;
			set;
		}

		bool InUse
		{
			get;
		}

		FastColorMatrix MultiplyMultipleMatricies();
	}
}
