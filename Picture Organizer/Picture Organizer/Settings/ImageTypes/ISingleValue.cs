using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using Trebuchet.Helper_Classes;

namespace Trebuchet.Settings.ImageTypes
{
	interface ISingleValue
	{
		decimal Minimum{get;}
		decimal Maximum { get;}
		decimal Value { get;set;}
		FastColorMatrix ColMatrix { get;}
	}
}
