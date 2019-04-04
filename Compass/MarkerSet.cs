// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="MarkerSet.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zeroit.Framework.Gauges.Compass
{
    partial class ZeroitAsanaCompass
    {
        /// <summary>
        /// 	The snap mode of the marker set.
        /// </summary>
		public enum SnapMode
		{
		    /// <summary>
		    /// 	Snap mode is disabled.  Marker set can be at any angle.
		    /// </summary>
		    Disabled,
		    
		    /// <summary>
		    /// 	Marker set can be at any angle while being dragged, but will move to the nearest snap angle
			/// 	when the mouse button is released.
		    /// </summary>
		    SnapOnMouseUp,
		    
		    /// <summary>
		    /// 	Marker set will always be at one of the snap angles, even while being dragged.
		    /// </summary>
		    SnapAlways
		};
			
        /// <summary>
        /// 	Represents a set of <c>Marker</c> objects which move as a group when any member of the set is dragged.
        /// </summary>
		public class MarkerSet : CollectionItem
		{
		    /// <summary>
		    /// 	Initializes a new instance of the <c>MarkerSet</c> class with an initial angle of 0.0,
			///     and includeInFixedBackground set to false.
		    /// </summary>
			public MarkerSet() : this(0.0f)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>MarkerSet</c> class with includeInFixedBackground set to false.
			/// </summary>
			/// <param name="angle">Initial angle.</param>
			public MarkerSet(float angle) : this(angle, false)
			{ }

			/// <summary>
			/// 	Initializes a new instance of the <c>MarkerSet</c> class.
			/// </summary>
			/// <param name="angle">Initial angle.</param>
			/// <param name="includeInFixedBackground"><c>True</c> if this marker set is part of the fixed background, <c>false</c> otherwise.</param>
			public MarkerSet(float angle, bool includeInFixedBackground) : base()
			{
				this.angle = angle;
				this.includeInFixedBackground = includeInFixedBackground;
				this.markers = new List<Marker>();
				this.snapAngles = new float[0];
			}

			/// <summary>
			/// 	Produces a copy of this <c>MarkerSet</c>.
			/// 	The new instance does not belong to any <c>ZeroitAsanaCompass</c>.
			/// </summary>
			/// <returns>A new <c>MarkerSet</c>.</returns>
			public MarkerSet Clone()
			{
				MarkerSet ms = new MarkerSet(angle);
				for (int i = 0; i < markers.Count; i++)
				{
					Marker m = markers[i].Clone();
					ms.Add(m);
				}
				return ms;
			}

			private float angle;
			/// <summary>
			/// 	Gets or sets the angle.
			/// </summary>
			/// <value>
			/// 	The angular location of the marker set.
			/// </value>
			public float Angle
			{
				get { return angle; }
				set { SetAngle(value); }
			}

			internal void ResetAngle() // recalc current angle due to change
			{
				SetAngle(angle);
			}

			internal void SetAngle(float a)
			{
				SetAngle(a, AngleChangedArgs.MouseState.Unknown);
			}

			internal void SetAngle(float a, AngleChangedArgs.MouseState mouseState)
			{
				float a2 = (Cc != null) ? Cc.AdjustAngle(a) : a;

				float a3 = a2;
				if (   (snapMode == SnapMode.SnapOnMouseUp && (mouseState == AngleChangedArgs.MouseState.Up || mouseState == AngleChangedArgs.MouseState.Unknown))
					||  snapMode == SnapMode.SnapAlways)
				{
					a3 = CalcSnapAngle(a2);
				}
				else
				{
					a3 = a2;
				}

				// Version 1.2.3 - June 14, 2015
				// - added checks for mouseState
				if (a3 != angle || mouseState == AngleChangedArgs.MouseState.Up || mouseState == AngleChangedArgs.MouseState.Down)
				{
					float change = a3 - angle;
					angle = a3;
					if (Cc != null)
					{
						Cc.AngleChangedEvent(this, change, mouseState);
					}
					Redraw(includeInFixedBackground);
				}
			}

			private float CalcSnapAngle(float preSnapAngle)
			{
				// Build alt list of snap angles (in case some of them are outside the range [0,360) AND wrap mode is set)
				List<float> validSnapAngles = new List<float>();
				if (Cc == null)
				{
					validSnapAngles.AddRange(snapAngles);
				}
				else if (Cc.AngleWraps)
				{
					foreach (float a in snapAngles)
					{
						if (a >= 0.0f && a < 360.0f)
						{
							validSnapAngles.Add(a);
						}
					}
				}
				else
				{
					foreach (float a in snapAngles)
					{
						if (a >= Cc.AngleMin && a <= Cc.AngleMax)
						{
							validSnapAngles.Add(a);
						}
					}
				}
				if (validSnapAngles.Count == 0)
				{
					return preSnapAngle;
				}

				float snapAngle = validSnapAngles[0];
				float snapDist = CalcSnapDist(preSnapAngle, validSnapAngles[0]);

				for (int a = 1; a < validSnapAngles.Count; a++)
				{
					float dist = CalcSnapDist(preSnapAngle, validSnapAngles[a]);
					if (dist < snapDist)
					{
						snapDist = dist;
						snapAngle = validSnapAngles[a];
					}
				}

				return snapAngle;
			}

            private float CalcSnapDist(float preSnapAngle, float snapAngle)
            {
                float dist = Math.Abs(preSnapAngle - snapAngle);
				// Version 1.2.1 - Sept 16, 2013
				// Added "Cc.AngleWraps &&"
                if (Cc.AngleWraps && dist > 180f)
                {
                    dist = 360 - dist;
                }
                return dist;
            }

			private bool includeInFixedBackground;
			/// <summary>
			/// 	Gets or sets the includeInFixedBackground flag.
			/// </summary>
			/// <value>
			/// 	<c>True</c> if this marker set is part of the fixed background, <c>false</c> otherwise.
			/// </value>
			/// <seealso cref="ZeroitAsanaCompass.FixedBackground" />
			public bool IncludeInFixedBackground
			{
				get { return includeInFixedBackground; }
				set
				{
					if (Cc == null)
					{
						includeInFixedBackground = value;
					}
					else if (includeInFixedBackground != value)
					{
						includeInFixedBackground = value;
						Redraw(true);
					}
				}
			}

			private SnapMode snapMode = SnapMode.Disabled;
			/// <summary>
			/// 	Gets or sets the snap mode.
			/// </summary>
			/// <value>
			/// 	The snap mode of the marker set.
			/// </value>
			public SnapMode SnapMode
			{
				get { return snapMode; }
				set
				{
					if (value != snapMode)
					{
						snapMode = value;
                        ResetAngle();
					}
				}
			}

			private float[] snapAngles;
			/// <summary>
			/// 	Gets or sets the collection of snap angles.
			/// </summary>
			/// <value>
			/// 	The array of angles to which the marker set may snap.
			/// </value>
			/// <exception cref="System.ArgumentNullException">
			/// 	Thrown if <c>SnapAngles</c> is null.
			/// </exception>
			public float[] SnapAngles
			{
				get { return snapAngles; }
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("SnapMode");
					}
					snapAngles = (float[])value.Clone();
                    ResetAngle();
				}
			}

			private List<Marker> markers;

			/// <summary>
			/// 	Gets the <c>Marker</c> at the specified index.
			/// </summary>
			/// <value>
			/// 	<c>Marker</c> at the specified index.
			/// </value>
			/// <param name="index">The zero-based index of the <c>Marker</c> to get.</param>
			/// <returns>
			///		The <c>Marker</c> at the specified index.
			/// </returns>
			/// <exception cref="System.ArgumentOutOfRangeException">
			///		Thrown if <paramref name="index" /> is less than zero or greater than <c>Count</c>.
			///	</exception>
			public Marker this[int index]
			{
				get { return markers[index]; }
			    set
			    {
                    markers[index] = value;
			    }
			}

		    

			/// <summary>
			/// 	Gets the number of <c>Marker</c> objects contained in the <c>MarkerSet</c>.
			/// </summary>
			/// <value>
			/// 	The number of <c>Marker</c> objects contained in the <c>MarkerSet</c>.
			/// </value>
			public int Count 
			{
				get { return markers.Count; }
			}

			internal void CheckIfNull(Marker marker)
			{
				if (marker == null)
				{
					throw new ArgumentNullException("marker");
				}
			}

			internal void Check(Marker marker)
			{
				CheckIfNull(marker);
				if (marker.Ms != null)
				{
					throw new ArgumentException(string.Format("Cannot add or insert the item '{0}' in more than one place.  You must first remove it from its current location or clone it.", marker),
												"marker");
				}
			}

			/// <summary>
			/// 	Returns the zero-based index of the occurence of a <c>Marker</c> in the <c>MarkerSet</c>.
			/// </summary>
			/// <param name="marker">The <c>Marker</c> to locate in the <c>MarkerSet</c>.</param>
			/// <returns>
			///		The zero-based index of <paramref name="marker" />, if found; otherwise -1.
			/// </returns>
			/// <exception cref="System.ArgumentNullException">
			/// 	Thrown if <paramref name="marker" /> is null.
			/// </exception>
			public int IndexOf(Marker marker)
			{
				CheckIfNull(marker);
				return markers.IndexOf(marker);
			}

			/// <summary>
			/// 	Adds a <c>Marker</c> to the end of the <c>MarkerSet</c>.
			/// </summary>
			/// <param name="marker">The <c>Marker</c> to be added to the end of the <c>MarkerSet</c>.</param>
			/// <exception cref="System.ArgumentNullException">
			/// 	Thrown if <paramref name="marker" /> is null.
			/// </exception>
			/// <exception cref="System.ArgumentException">
			/// 	Thrown if <paramref name="marker" /> already belongs to a <c>MarkerSet</c>.
			/// </exception>
			public void Add(Marker marker)
			{
				Check(marker);
				marker.Ms = this;
				markers.Add(marker);
				Redraw(includeInFixedBackground);
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with a solid color at offset angle 0.0 and adds it to the end of the <c>MarkerSet</c>.
			/// 	The marker can be dragged with the left mouse button and is visible.
			/// </summary>
			/// <param name="solidColor">Internal color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
			public void Add(Color solidColor, Color borderColor, float borderSize, PointF[] points)
			{
				Add(new Marker(solidColor, borderColor, borderSize, points));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with a solid color and adds it to the end of the <c>MarkerSet</c>.
			/// 	The marker is visible.
			/// </summary>
			/// <param name="solidColor">Internal color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <param name="offsetAngle">Offset angle of marker within <c>MarkerSet</c>.</param>
			/// <param name="dragButtons"><c>System.Windows.Forms.MouseButtons</c> value indicating which mouse button(s) drag this marker.  If the value is <c>MouseButtons.None</c>, then this marker cannot be dragged.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
			public void Add(Color solidColor, Color borderColor, float borderSize, PointF[] points, float offsetAngle, MouseButtons dragButtons)
			{
				Add(new Marker(solidColor, borderColor, borderSize, points, offsetAngle, dragButtons));
			}

			/// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with a solid color and adds it to the end of the <c>MarkerSet</c>.
			/// </summary>
			/// <param name="solidColor">Internal color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <param name="offsetAngle">Offset angle of marker within <c>MarkerSet</c>.</param>
			/// <param name="dragButtons"><c>System.Windows.Forms.MouseButtons</c> value indicating which mouse button(s) drag this marker.  If the value is <c>MouseButtons.None</c>, then this marker cannot be dragged.</param>
			/// <param name="visible"><c>True</c> if the marker is visible, <c>false</c> otherwise.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
			public void Add(Color solidColor, Color borderColor, float borderSize, PointF[] points, float offsetAngle, MouseButtons dragButtons, bool visible)
			{
				Add(new Marker(solidColor, borderColor, borderSize, points, offsetAngle, dragButtons, visible));
			}

            /// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with radial gradient coloring and adds it to the end of the <c>MarkerSet</c>.
            /// </summary>
			/// <param name="point1">Starting point of gradient.</param>
			/// <param name="point2">Ending point of gradient.</param>
			/// <param name="color1">Color of marker at starting point of gradient.</param>
			/// <param name="color2">Color of marker at ending point of gradient.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <param name="offsetAngle">Offset angle of marker within <c>MarkerSet</c>.</param>
			/// <param name="dragButtons"><c>System.Windows.Forms.MouseButtons</c> value indicating which mouse button(s) drag this marker.  If the value is <c>MouseButtons.None</c>, then this marker cannot be dragged.</param>
			/// <param name="visible"><c>True</c> if the marker is visible, <c>false</c> otherwise.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
            public void Add(PointF point1, PointF point2, Color color1, Color color2, Color borderColor, float borderSize, PointF[] points, float offsetAngle, MouseButtons dragButtons, bool visible)
            {
                Add(new Marker(point1, point2, color1, color2, borderColor, borderSize, points, offsetAngle, dragButtons, visible));
            }

			/// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with linear gradient coloring and adds it to the end of the <c>MarkerSet</c>.
			/// </summary>
			/// <param name="gradientMode">A <c>System.Drawing.Drawing2D.LinearGradientMode</c> enumeration value that specifies the orientation of the gradient.</param>
			/// <param name="startColor">Starting color.</param>
			/// <param name="endColor">Ending color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <param name="offsetAngle">Offset angle of marker within <c>MarkerSet</c>.</param>
			/// <param name="dragButtons"><c>System.Windows.Forms.MouseButtons</c> value indicating which mouse button(s) drag this marker.  If the value is <c>MouseButtons.None</c>, then this marker cannot be dragged.</param>
			/// <param name="visible"><c>True</c> if the marker is visible, <c>false</c> otherwise.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
			public void Add(LinearGradientMode gradientMode, Color startColor, Color endColor, Color borderColor, float borderSize, PointF[] points, float offsetAngle, MouseButtons dragButtons, bool visible)
			{
                Add(new Marker(gradientMode, startColor, endColor, borderColor, borderSize, points, offsetAngle, dragButtons, visible));
			}

            /// <summary>
			/// 	Initializes a new instance of the <c>Marker</c> class with a hatch pattern and adds it to the end of the <c>MarkerSet</c>.
            /// </summary>
			/// <param name="hatchStyle">A <c>System.Drawing.Drawing2D.HatchStyle</c> enumeration value that specifies the style of hatching.</param>
			/// <param name="foreColor">Hatch lines color.</param>
			/// <param name="backColor">Background color.</param>
			/// <param name="borderColor">Border color.</param>
			/// <param name="borderSize">Border thickness (in pixels).</param>
			/// <param name="points">Array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees.</param>
			/// <param name="offsetAngle">Offset angle of marker within <c>MarkerSet</c>.</param>
			/// <param name="dragButtons"><c>System.Windows.Forms.MouseButtons</c> value indicating which mouse button(s) drag this marker.  If the value is <c>MouseButtons.None</c>, then this marker cannot be dragged.</param>
			/// <param name="visible"><c>True</c> if the marker is visible, <c>false</c> otherwise.</param>
			/// <remarks>
			///		Refer to the Overview for a discussion of the coordinate system used by <c>points</c>.
			/// </remarks>
            public void Add(HatchStyle hatchStyle, Color foreColor, Color backColor, Color borderColor, float borderSize, PointF[] points, float offsetAngle, MouseButtons dragButtons, bool visible)
            {
                Add(new Marker(hatchStyle, foreColor, backColor, borderColor, borderSize, points, offsetAngle, dragButtons, visible));
            }

			/// <summary>
			/// 	Inserts a <c>Marker</c> into the <c>MarkerSet</c> at the specified index.
			/// </summary>
			/// <param name="index">The zero-based index at which <paramref name="marker" />should be inserted.</param>
			/// <param name="marker">The <c>Marker</c> to insert.</param>
			/// <exception cref="System.ArgumentNullException">
			/// 	Thrown if <paramref name="marker" /> is null.
			/// </exception>
			/// <exception cref="System.ArgumentException">
			/// 	Thrown if <paramref name="marker" /> already belongs to a <c>MarkerSet</c>.
			/// </exception>
			/// <exception cref="System.ArgumentOutOfRangeException">
			/// 	Thrown if <paramref name="index" /> is less than zero or greater than <c>Count</c>.
			/// </exception>
			public void Insert(int index, Marker marker)
			{
				Check(marker);
				if (index < 0 || index > Count)
				{
					throw new ArgumentOutOfRangeException();
				}
                marker.Ms = this;
				markers.Insert(index, marker);
				Redraw(includeInFixedBackground);
			}

			private void RemoveAt2(int index)
			{
				Marker marker = markers[index];
				marker.Ms = null;
				markers.RemoveAt(index);
			}

			/// <summary>
			/// 	Removes the <c>Marker</c> at the specified index of the <c>MarkerSet</c>
			/// </summary>
			/// <param name="index">The zero-based index of the <c>Marker</c> to remove.</param>
			/// <exception cref="System.ArgumentOutOfRangeException">
			/// 	Thrown if <paramref name="index" /> is less than zero or greater than <c>Count</c>.
			/// </exception>
			public void RemoveAt(int index)
			{
				if (index <= 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				RemoveAt2(index);
				Redraw(includeInFixedBackground);
			}

			/// <summary>
			/// 	Removes the occurence of the <c>Marker</c> from the <c>MarkerSet</c>.
			/// </summary>
			/// <param name="marker">The <c>Marker</c> to remove from <c>MarkerSet</c>.</param>
			/// <exception cref="System.ArgumentNullException">
			///  	Thrown if <paramref name="marker" /> is null.
			/// </exception>
			public void Remove(Marker marker)
			{
				int index = IndexOf(marker);
				if (index >= 0)
				{
					RemoveAt(index);
				}
			}

			/// <summary>
			/// 	Removes all the <c>Marker</c> objects from the <c>MarkerSet</c>.
			/// </summary>
			public void Clear()
			{
				while (Count > 0)
				{
					RemoveAt2(0);
				}
				Redraw(includeInFixedBackground);
			}
		}
	}
}
