// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="RingCollection.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zeroit.Framework.Gauges.Compass
{
    partial class ZeroitAsanaCompass
    {
        /// <summary>
        /// 	Represents the collection of <c>Ring</c> objects in a <c>ZeroitAsanaCompass</c> control.
        /// </summary>
		/// <remarks>
		/// 	The <c>Ring</c> objects are displayed in the order in which they were added to the <c>RingCollection</c>.
		/// 	The first <c>Ring</c> starts at the center of the control (the origin), and subsequent <c>Ring</c> objects
		/// 	start where the preceeding one ended.
		/// 	The width of a ring is relative to the control size, where 1.0 is defined as the distance from the center
		/// 	of the control to the nearest edge.
		/// 	Hence, the absolute size of a ring changes as the control is resized.
		///
		///		<para>
		/// 	Adding <c>Ring</c> objects is only necessary if a varied background is required.
		/// 	If a single background color is required, then set the <c>BackgroundColor</c> parameter, and leave the
		///		<c>RingCollection</c> empty.
		///		</para>
		///
		///		<para>
		///		Refer to the Overview for more discussion.
		///		</para>
		/// </remarks>
		public class RingCollection : Collection<Ring>
		{
			internal RingCollection(ZeroitAsanaCompass cc) : base(cc)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a solid color and no border, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="solidColor">Internal color.</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, Color solidColor)
			{
				Add(new Ring(size, solidColor));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a solid color, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="solidColor">Internal color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, Color solidColor, Color borderColor, float borderSize)
			{
				Add(new Ring(size, solidColor, borderColor, borderSize));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with linear gradient coloring and no border, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="gradientMode">A <c>System.Drawing.Drawing2D.LinearGradientMode</c> enumeration value that specifies the orientation of the gradient.</param>
			/// <param name="startColor">Starting color.</param>
			/// <param name="endColor">Ending color.</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, LinearGradientMode gradientMode, Color startColor, Color endColor)
			{
				Add(new Ring(size, gradientMode, startColor, endColor));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with linear gradient coloring, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="gradientMode">A <c>System.Drawing.Drawing2D.LinearGradientMode</c> enumeration value that specifies the orientation of the gradient.</param>
			/// <param name="startColor">Starting color.</param>
			/// <param name="endColor">Ending color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, LinearGradientMode gradientMode, Color startColor, Color endColor, Color borderColor, float borderSize)
			{
				Add(new Ring(size, gradientMode, startColor, endColor, borderColor, borderSize));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a hatch pattern and no border, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="hatchStyle">A <c>System.Drawing.Drawing2D.HatchStyle</c> enumeration value that specifies the style of hatching.</param>
			/// <param name="foreColor">Hatch lines color.</param>
			/// <param name="backColor">Background color.</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, HatchStyle hatchStyle, Color foreColor, Color backColor)
			{
				Add(new Ring(size, hatchStyle, foreColor, backColor));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a hatch pattern, and adds it to this <c>RingCollection</c>.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="hatchStyle">A <c>System.Drawing.Drawing2D.HatchStyle</c> enumeration value that specifies the style of hatching.</param>
			/// <param name="foreColor">Hatch lines color.</param>
			/// <param name="backColor">Background color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to <c>RingCollection</c> for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public void Add(float size, HatchStyle hatchStyle, Color foreColor, Color backColor, Color borderColor, float borderSize)
			{
				Add(new Ring(size, hatchStyle, foreColor, backColor, borderColor, borderSize));
			}
		}
	}
}
