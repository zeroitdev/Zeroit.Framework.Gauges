// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Ring.cs" company="Zeroit Dev Technologies">
//    This program is for creating Gauge controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zeroit.Framework.Gauges.Compass
{
    partial class ZeroitAsanaCompass
    {
        /// <summary>
        /// 	Represents a ring object.
        /// </summary>
		/// <remarks>
		/// 	A <c>Ring</c> is a circular background area in the control.
		///
		///		<para>
		///		The <c>size</c> is the radial size,
		/// 	relative to the control size, where 1.0 is defined as the distance from the center of the control
		/// 	to the nearest edge.
		///		</para>
		/// </remarks>
		public class Ring : CollectionItem
		{
			internal Ring(float size, Color borderColor, float borderSize) : base()
			{
				if (size <= 0.0)
				{
					throw new ArgumentException("size");
				}

				this.size = size;
				this.borderColor = borderColor;
				this.borderSize = borderSize;

				this.gradientMode = LinearGradientMode.Vertical;
				this.hatchStyle = HatchStyle.Cross;
				this.color1 = Color.Black;
				this.color2 = Color.White;

				this.brush = null;
			}
            
            /// <summary>
            /// 	Specifies how a <c>Ring</c> is filled.
            /// </summary>
			public enum BrushMode
			{
			    /// <summary>
			    /// 	Specifies a solid fill.
			    /// </summary>
				Solid,
				
				/// <summary>
				/// 	Specifies a linear gradient fill.
				/// 	The gradient is spread across the client area of the control.
				/// </summary>
				LinearGradient,
				
				/// <summary>
				/// 	Specifies a hatch fill.
				/// </summary>
				Hatch
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a solid color and no border.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="solidColor">Internal color.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, Color solidColor)
					: this(size, solidColor, Color.Empty, 0.0f)
			{ }
				
			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a solid color.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="solidColor">Internal color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, Color solidColor, Color borderColor, float borderSize)
					: this(size, borderColor, borderSize)
			{
				this.brushMode = BrushMode.Solid;
				this.color1 = solidColor;
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with linear gradient coloring and no border.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="gradientMode">A <c>System.Drawing.Drawing2D.LinearGradientMode</c> enumeration value that specifies the orientation of the gradient.</param>
			/// <param name="startColor">Starting color.</param>
			/// <param name="endColor">Ending color.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, LinearGradientMode gradientMode, Color startColor, Color endColor)
					: this(size, gradientMode, startColor, endColor, Color.Empty, 0.0f)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with linear gradient coloring.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="gradientMode">A <c>System.Drawing.Drawing2D.LinearGradientMode</c> enumeration value that specifies the orientation of the gradient.</param>
			/// <param name="startColor">Starting color.</param>
			/// <param name="endColor">Ending color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, LinearGradientMode gradientMode, Color startColor, Color endColor, Color borderColor, float borderSize)
					: this(size, borderColor, borderSize)
			{
				this.brushMode = BrushMode.LinearGradient;
				this.gradientMode = gradientMode;
				this.color1 = startColor;
				this.color2 = endColor;
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a hatch pattern and no border.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="hatchStyle">A <c>System.Drawing.Drawing2D.HatchStyle</c> enumeration value that specifies the style of hatching.</param>
			/// <param name="foreColor">Hatch lines color.</param>
			/// <param name="backColor">Background color.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, HatchStyle hatchStyle, Color foreColor, Color backColor)
					: this(size, hatchStyle, foreColor, backColor, Color.Empty, 0.0f)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>Ring</c> class with a hatch pattern.
			/// </summary>
			/// <param name="size">The size of the ring.</param>
			/// <param name="hatchStyle">A <c>System.Drawing.Drawing2D.HatchStyle</c> enumeration value that specifies the style of hatching.</param>
			/// <param name="foreColor">Hatch lines color.</param>
			/// <param name="backColor">Background color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			public Ring(float size, HatchStyle hatchStyle, Color foreColor, Color backColor, Color borderColor, float borderSize)
					: this(size, borderColor, borderSize)
			{
				this.brushMode = BrushMode.Hatch;
				this.hatchStyle = hatchStyle;
				this.color1 = foreColor;
				this.color2 = backColor;
			}

			/// <summary>
			/// 	Produces a copy of this <c>Ring</c>.
			/// 	The new instance does not belong to a <c>RingCollection</c> or a <c>ZeroitAsanaCompass</c>.
			/// </summary>
			/// <returns>
			///		A new <c>Ring</c>.
			///	</returns>
			public Ring Clone()
			{
				Ring r = new Ring(size, borderColor, borderSize);
				
				r.brushMode = brushMode;
				r.gradientMode = gradientMode;
				r.hatchStyle = hatchStyle;
				r.color1 = color1;
				r.color2 = color2;

				return r;
			}

			private Brush brush;

			internal Brush GetBrush(RectangleF rect) // bounding rect
			{
				switch (brushMode)
				{
					case BrushMode.Solid : 
					{
						if (brush == null)
						{
							brush = new SolidBrush(color1);
						}
						break;
					}
					case BrushMode.LinearGradient :
					{
						ClearBrush();
                        brush = new LinearGradientBrush(rect, color1, color2, gradientMode);
						break;
					}
					case BrushMode.Hatch :
					{
						if (brush == null)
						{
							brush = new HatchBrush(hatchStyle, color1, color2);
						}
						break;
					}
				}
				return brush;
			}

			private void ClearBrush()
			{
				if (brush != null)
				{
					brush.Dispose();
					brush = null;
				}
			}

            private float size;
			/// <summary>
			/// 	Gets or sets the ring size.
			/// </summary>
			/// <value>
			/// 	The ring size.
			/// </value>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>size</c>.
			///	</remarks>
			/// <exception cref="System.ArgumentException">
			///		Thrown if <c>Size</c> is less than or equal to zero.
			///	</exception>
			public float Size
			{
				get { return size; }
				set
				{
					if (size <= 0.0)
					{
						throw new ArgumentException("size");
					}
					if (value != size)
					{
						size = value;
						Redraw(true);
					}
				}
			}

			private BrushMode brushMode;
			/// <summary>
			/// 	Gets or sets the <c>BrushMode</c>.
			/// </summary>
			/// <value>
			/// 	One of the <c>BrushMode</c> values.
			/// </value>
			public BrushMode RingBrushMode
			{
				get { return brushMode; }
				set
				{
					if (value != brushMode)
					{
						brushMode = value;
						ClearBrush();
						Redraw(true);
					}
				}
			}

			private LinearGradientMode gradientMode;
			/// <summary>
			/// 	Gets or sets the <c>LinearGradientMode</c>.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.LinearGradient</c>.
			/// </summary>
			/// <value>
			/// 	One of the <c>LinearGradientMode</c> values.
			/// </value>
			public LinearGradientMode GradientMode
			{
				get { return gradientMode; }
				set
				{
					if (value != gradientMode || brushMode != BrushMode.LinearGradient)
					{
						gradientMode = value;
						brushMode = BrushMode.LinearGradient;
						ClearBrush();
						Redraw(true);
					}
				}
			}

			private HatchStyle hatchStyle;
			/// <summary>
			/// 	Gets or sets the <c>HatchStyle</c>.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.Hatch</c>.
			/// </summary>
			/// <value>
			/// 	One of the <c>HatchStyle</c> values.
			/// </value>
			public HatchStyle HatchStyle
			{
				get { return hatchStyle; }
				set
				{
					if (value != hatchStyle || brushMode != BrushMode.Hatch)
					{
						hatchStyle = value;
						brushMode = BrushMode.Hatch;
						ClearBrush();
						Redraw(true);
					}
				}
			}

            private Color color1;
			private Color color2;

			/// <summary>
			/// 	Gets or sets the solid fill color.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.Solid</c>.
			/// </summary>
			/// <value>
			/// 	The solid fill color.
			/// </value>
			public Color SolidColor
			{
				get { return color1; }
				set
				{
					if (brushMode != BrushMode.Solid || color1.ToArgb() != value.ToArgb())
					{
						color1 = value;
						brushMode = BrushMode.Solid;
						ClearBrush();
						Redraw(true);
					}
				}
			}

			/// <summary>
			/// 	Gets or sets the linear gradient start color.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.LinearGradient</c>.
			/// </summary>
			/// <value>
			/// 	The linear gradient start color.
			/// </value>
			public Color LinearGradientStartColor
			{
				get { return color1; }
				set	{ SetLinearGradientColors(value, color2); }
			}
			
			/// <summary>
			/// 	Gets or sets the linear gradient end color.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.LinearGradient</c>.
			/// </summary>
			/// <value>
			/// 	The linear gradient end color.
			/// </value>
			public Color LinearGradientEndColor
			{
				get { return color2; }
				set	{ SetLinearGradientColors(color1, value); }
			}
			
			/// <summary>
			/// 	Sets the linear gradient start and end colors, and forces <c>BrushMode</c> to <c>BrushMode.LinearGradient</c>.
			/// </summary>
			/// <param name="startColor">Start color.</param>
			/// <param name="endColor">End color.</param>
			public void SetLinearGradientColors(Color startColor, Color endColor)
			{
				if (brushMode != BrushMode.LinearGradient || startColor.ToArgb() != color1.ToArgb() || endColor.ToArgb() != color2.ToArgb())
				{
					color1 = startColor;
					color2 = endColor;
					brushMode = BrushMode.LinearGradient;
					ClearBrush();
					Redraw(true);
				}
			}

            /// <summary>
			/// 	Gets or sets the hatched foreground color.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.Hatch</c>.
            /// </summary>
            /// <value>
			/// 	The hatched foreground color.
            /// </value>
			public Color HatchForeColor
			{
				get { return color1; }
				set	{ SetHatchColors(value, color2); }
			}
			
			/// <summary>
			/// 	Gets or sets the hatched background color.
			/// 	Setting this value forces <c>BrushMode</c> to <c>BrushMode.Hatch</c>.
			/// </summary>
			/// <value>
			/// 	The hatched background color.
			/// </value>
			public Color HatchBackColor
			{
				get { return color2; }
				set	{ SetHatchColors(color1, value); }
			}
			
			/// <summary>
			/// 	Sets the hatched foreground and background colors, and forces <c>BrushMode</c> to <c>BrushMode.Hatch</c>.
			/// </summary>
			/// <param name="foreColor">Foreground color.</param>
			/// <param name="backColor">Background color.</param>
			public void SetHatchColors(Color foreColor, Color backColor)
			{
				if (brushMode != BrushMode.Hatch || foreColor.ToArgb() != color1.ToArgb() || backColor.ToArgb() != color2.ToArgb())
				{
					color1 = foreColor;
					color2 = backColor;
					brushMode = BrushMode.Hatch;
					ClearBrush();
					Redraw(true);
				}
			}

            private Color borderColor;
            /// <summary>
            /// 	Gets or set the border color.
            /// </summary>
            /// <value>
            /// 	The border color.
            /// </value>
			public Color BorderColor
			{
				get { return borderColor; }
				set
				{
					// Only update if new value is different
					if (borderColor.ToArgb() != value.ToArgb())
					{
						borderColor = value;
						Redraw(true);
					}
				}
			}

            private float borderSize;
            /// <summary>
            /// 	Gets or sets the border thickness (in pixels).
			/// 	Set to zero for no border.
            /// </summary>
            /// <value>
            /// 	The border thickness (in pixels).
            /// </value>
			public float BorderSize
			{
				get { return borderSize; }
				set
				{
					float v = Math.Max(0.0f, value);
					if (borderSize != v)
					{
						borderSize = v;
						Redraw(true);
					}
				}
			}
		}
	}
}
