// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="TextItem.cs" company="Zeroit Dev Technologies">
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

namespace Zeroit.Framework.Gauges.Compass
{
    partial class ZeroitAsanaCompass
    {
        /// <summary>
        /// 	Represents a text item object.
        /// </summary>
		/// <remarks>
		/// 	The location of a text item refers to the center of the drawn text.
		///		<c>Position</c> is the distance from the center of the control,
		/// 	where 1.0 is defined as the distance to the nearest edge,
		/// 	and <c>angle</c> is in the range [0, 360).
		///
		///		<para>
		/// 	A text item can either be fixed or variable size.
		/// 	A <c>TextItem</c> is fixed size if it is constructed without a <c>size</c> parameter, or with a <c>size</c> parameter that is zero.
		/// 	In this case, the text size is defined by the <c>font.Size</c> parameter.
		///		</para>
		///
		///		<para>
		/// 	A <c>TextItem</c> is variable size if it is constructed with a non-zero <c>size</c> parameter.
		/// 	In this case, the text size is relative to the control size, where 1.0 is defined
		/// 	as the distance from the center of the control to the nearest edge.
		/// 	Hence, variable size changes display size as the control is resized.
		///		</para>
		///
		///		<para>
		///		Refer to the Overview for more discussion.
		///		</para>
		/// </remarks>
		public class TextItem : CollectionItem
		{
		    /// <summary>
		    /// 	Initializes a new instance of the <c>TextItem</c> class with fixed size, unrotated text.
		    /// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
		    /// <param name="brush"><c>System.Drawing.Brush</c> that determines the color and texture of the text.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Brush brush, string text, float position, float angle)
				: this(font, brush, text, position, angle, 0.0f)
			{ }

			/// <summary>
		    /// 	Initializes a new instance of the <c>TextItem</c> class with fixed size text.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
		    /// <param name="brush"><c>System.Drawing.Brush</c> that determines the color and texture of the text.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <param name="rotation">Text rotation in degrees.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Brush brush, string text, float position, float angle, float rotation)
				: this(font, brush, 0.0f, text, position, angle, rotation)
			{ }

			/// <summary>
		    /// 	Initializes a new instance of the <c>TextItem</c> class with variable size, unrotated text.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
		    /// <param name="brush"><c>System.Drawing.Brush</c> that determines the color and texture of the text.</param>
			/// <param name="size">Size (height) of the text, where 1.0 is the distance from the center of the control to the nearest edge.  This value overrides the size specified in <paramref name="font" />.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Brush brush, float size, string text, float position, float angle)
				: this(font, brush, size, text, position, angle, 0.0f)
			{ }

			/// <summary>
		    /// 	Initializes a new instance of the <c>TextItem</c> class with variable size text.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
		    /// <param name="brush"><c>System.Drawing.Brush</c> that determines the color and texture of the text.</param>
			/// <param name="size">Size (height) of the text, where 1.0 is the distance from the center of the control to the nearest edge.  This value overrides the size specified in <paramref name="font" />.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <param name="rotation">Text rotation in degrees.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Brush brush, float size, string text, float position, float angle, float rotation) : base()
			{
				Core(font, brush, size, text, position, angle, rotation);
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>TextItem</c> class with fixed size, unrotated text, drawn with a solid color.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
			/// <param name="solidColor">Text color.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Color solidColor, string text, float position, float angle)
				: this(font, solidColor, text, position, angle, 0.0f)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>TextItem</c> class with fixed size text, drawn in a solid color.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
			/// <param name="solidColor">Text color.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <param name="rotation">Text rotation in degrees.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Color solidColor, string text, float position, float angle, float rotation)
				: this(font, solidColor, 0.0f, text, position, angle, rotation)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>TextItem</c> class with variable size, unrotated text, drawn in a solid color.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
			/// <param name="solidColor">Text color.</param>
			/// <param name="size">Size (height) of the text, where 1.0 is the distance from the center of the control to the nearest edge.  This value overrides the size specified in <paramref name="font" />.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Color solidColor, float size, string text, float position, float angle)
				: this(font, solidColor, size, text, position, angle, 0.0f)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>TextItem</c> class with variable size, drawn in a solid color.
			/// </summary>
		    /// <param name="font"><c>System.Drawing.Font</c> that defines the format of the text.</param>
			/// <param name="solidColor">Text color.</param>
			/// <param name="size">Size (height) of the text, where 1.0 is the distance from the center of the control to the nearest edge.  This value overrides the size specified in <paramref name="font" />.</param>
		    /// <param name="text">Text to draw.</param>
		    /// <param name="position">The distance from the center of the control to the center of the text.</param>
		    /// <param name="angle">The angular location of the center of the text.</param>
			/// <param name="rotation">Text rotation in degrees.</param>
			/// <remarks>
			///		Refer to <c>TextItem</c> for an explanation of fixed vs variable size text.
			///	</remarks>
			public TextItem(Font font, Color solidColor, float size, string text, float position, float angle, float rotation) : base()
			{
				Core(font, solidColor, size, text, position, angle, rotation);
			}

			private void Core(Font font, Brush brush, float size, string text, float position, float angle, float rotation)
			{
				Core(font, size, text, position, angle, rotation);
				if (brush == null)
				{
					throw new ArgumentNullException("brush");
				}
				this.brush = (Brush)brush.Clone();
			}

			private void Core(Font font, Color solidColor, float size, string text, float position, float angle, float rotation)
			{
				Core(font, size, text, position, angle, rotation);
				this.brush = new SolidBrush(solidColor);
			}

			private void Core(Font font, float size, string text, float position, float angle, float rotation)
			{
				if (font == null)
				{
					throw new ArgumentNullException("font");
				}
				if (text == null)
				{
					throw new ArgumentNullException("text");
				}

				this.font = (Font)font.Clone();
				this.size = size;
				this.text = text;
				this.position = position;
				this.angle = AdjustAngle(angle);
				this.rotation = AdjustAngle(rotation);

				drawRadius = 0.0f;
				drawFont = null;
			}

			/// <summary>
			/// 	Releases all resources used by this <c>TextItem</c> object.
			/// </summary>
			public void Dispose()
			{
				font.Dispose();
				brush.Dispose();
				DisposeDrawFont();
			}

			/// <summary>
			/// 	Produces a copy of this <c>TextItem</c>.
			/// 	The next instance does not belong to a <c>ZeroitAsanaCompass</c>.
			/// </summary>
			/// <returns>
			///		A new <c>TextItem</c>.
			///	</returns>
			public TextItem Clone()
			{
				return new TextItem(font, brush, size, text, position, angle, rotation);
			}

			private float AdjustAngle(float a)
			{
				a %= 360.0f;
				if (a < 0.0f)
				{
					a += 360.0f;
				}
				return a;
			}

			private Font font;
			/// <summary>
		    /// 	Gets or sets the <c>System.Drawing.Font</c> that defines the format of the text.
			/// </summary>
			/// <value>
		    /// 	The <c>System.Drawing.Font</c> that defines the format of the text.
			/// </value>
			public Font Font
			{
				get { return font; }
				set
				{
					font = value;
					Redraw(true);
				}
			}

			private Brush brush;
			/// <summary>
		    /// 	Gets or sets the <c>System.Drawing.Brush</c> that determines the color and texture of the text.
			/// </summary>
			/// <value>
		    /// 	The <c>System.Drawing.Brush</c> that determines the color and texture of the text.
			/// </value>
			public Brush Brush
			{
				get { return brush; }
				set
				{
					brush = value;
					Redraw(true);
				}
			}

			private float size;
			/// <summary>
			/// 	Gets or set the size (height) of the text, where 1.0 is the distance from the center of the control to the nearest edge.
			/// 	If <c>size</c> was not specified in the constructor, then this value will be zero.
			/// 	If set, this value overrides the size specified in the <c>font</c>.
			/// </summary>
			/// <value>
			/// 	The size (height) of the text.
			/// </value>
			public float Size
			{
				get { return size; }
				set
				{
					float v = Math.Max(value, 0.0f);
					if (v != size)
					{
						size = v;
						Redraw(true);
					}
				}
			}

			private string text;
			/// <summary>
			/// 	Gets or sets the text to draw.
			/// </summary>
			/// <value>
			/// 	The text to draw.
			/// </value>
			public string Text
			{
				get { return text; }
				set
				{
					string v = string.IsNullOrEmpty(value) ? string.Empty : value;
					if (string.Compare(text, v) != 0)
					{
						text = v;
						Redraw(true);
					}
				}
			}

			private float position;
			/// <summary>
			/// 	Gets or sets the distance from the center of the control to the center of the text.
			/// </summary>
			/// <value>
			/// 	The distance from the center of the control to the center of the text.
			/// </value>
			public float Position
			{
				get { return position; }
				set
				{
					float v = Math.Max(value, 0.0f);
					if (v != position)
					{
						position = v;
						Redraw(true);
					}
				}
			}

			private float angle;
			/// <summary>
			/// 	Gets or sets the angular location of the center of the text.
			/// </summary>
			/// <value>
			/// 	The angular location of the center of the text.
			/// </value>
			public float Angle
			{
				get { return angle; }
				set
				{
					float v = AdjustAngle(value);
					if (v != angle)
					{
						angle = v;
						Redraw(true);
					}
				}
			}

			private float rotation;
			/// <summary>
			/// 	Gets or sets the angle at which the text is rotated.
			/// </summary>
			/// <value>
			/// 	The angle at which the text is rotated.
			/// </value>
			public float Rotation
			{
				get { return rotation; }
				set
				{
					float v = AdjustAngle(value);
					if (v != rotation)
					{
						rotation = v;
						Redraw(true);
					}
				}
			}

			private float drawRadius;
			private Font drawFont;

			private void DisposeDrawFont()
			{
				if (drawFont != null)
				{
					drawFont.Dispose();
					drawFont = null;
				}
			}

			internal void Draw(Graphics g, float radius, float x, float y)
			{
				Font f = null;
				if (size == 0.0f)
				{
					f = font;
				}
				else
				{
					if (radius != drawRadius)
					{
						DisposeDrawFont();
					}

					if (drawFont == null)
					{
						// make new font such that height is equal to size*radius
                        float targetHeight = size * radius;

						SizeF s = g.MeasureString("M", font);
						float fontSize = font.Size * targetHeight / s.Height;
						drawFont = new Font(font.FontFamily, fontSize, font.Style);
						drawRadius = radius;
					}
					f = drawFont;
				}

				// Measure size of text
				SizeF ts = g.MeasureString(text, f);
				PointF point = new PointF(-ts.Width / 2.0f, -ts.Height / 2.0f);

				// 1. Perform offset translation to move origin to text position
				// 2. Perform rotation translation (Graphics class flips angles, must fix)
				g.TranslateTransform(x, y);
				g.RotateTransform(-rotation);
				g.DrawString(text, f, brush, point);
				g.ResetTransform();
			}
		}
	}
}
