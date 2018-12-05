using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using Trebuchet.Helper_Classes;

namespace Trebuchet.Settings.ImageTypes
{
	interface ITripleValue
	{
		decimal Minimum{get;}
		decimal Maximum{get;}
		decimal ValueR { get;set;}
		decimal ValueG { get;set;}
		decimal ValueB { get;set;}
		FastColorMatrix ColMatrix { get;}
	}
}
