// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="MathFunc.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Zeroit.Framework.Gauges.AnalogMeterControl.Utils
{
	/// <summary>
	/// Mathematic Functions
	/// </summary>
	public class LBMath : Object
	{
		public static float GetRadian ( float val )
		{
			return (float)(val * Math.PI / 180);
		}
	}
}
