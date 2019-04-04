// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-27-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="AsanaCompass.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Gauges.Compass
{
    /// <summary>
    /// Circle control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitAsanaCompassDesigner))]
    [Serializable]
    public partial class ZeroitAsanaCompass : UserControl 
    {

        #region ENUM

        /// <summary>
        /// Enum MouseMode
        /// </summary>
        internal enum MouseMode { CannotDrag, CanDrag, Dragging };

        #endregion

        #region Private Fields
        private bool showText = false;
        /// <summary>
        /// The show angle
        /// </summary>
        private bool showAngle = false;

        /// <summary>
        /// The cursor can drag
        /// </summary>
        private Cursor cursorCanDrag = Cursors.Hand;
        /// <summary>
        /// The cursor dragging
        /// </summary>
        private Cursor cursorDragging;
        /// <summary>
        /// The bg bitmap
        /// </summary>
        private Bitmap bgBitmap = null;

        // Painting radials
        /// <summary>
        /// The RAD
        /// </summary>
        private const float RAD = (float)(Math.PI / 180.0);
        /// <summary>
        /// The RAD inv
        /// </summary>
        private const float RADInv = 1.0F / RAD;


        /// <summary>
        /// The xmid
        /// </summary>
        private float xmid;
        /// <summary>
        /// The ymid
        /// </summary>
        private float ymid;
        /// <summary>
        /// The radius
        /// </summary>
        private float radius;

        /// <summary>
        /// The marker sets
        /// </summary>
        private Collection<MarkerSet> markerSets;
        /// <summary>
        /// The text items
        /// </summary>
        private TextItemCollection textItems;
        /// <summary>
        /// The rings
        /// </summary>
        private RingCollection rings;
        /// <summary>
        /// The angle wraps
        /// </summary>
        private bool angleWraps = true;
        /// <summary>
        /// The angle minimum
        /// </summary>
        private float angleMin = 0.0F;
        /// <summary>
        /// The angle maximum
        /// </summary>
        private float angleMax = 360.0F;
        /// <summary>
        /// The fixed background
        /// </summary>
        private bool fixedBackground = false;
        /// <summary>
        /// The paint time sum
        /// </summary>
        private double paintTimeSum;
        /// <summary>
        /// The paint time count
        /// </summary>
        private int paintTimeCount;
        /// <summary>
        /// The major ticks
        /// </summary>
        private int majorTicks = 10;
        /// <summary>
        /// The major tick color
        /// </summary>
        private Color majorTickColor = Color.Black;
        /// <summary>
        /// The major tick start
        /// </summary>
        private float majorTickStart = 0.40f;
        /// <summary>
        /// The major tick size
        /// </summary>
        private float majorTickSize = 0.45f;
        /// <summary>
        /// The major tick thickness
        /// </summary>
        private float majorTickThickness = 0.45f;
        /// <summary>
        /// The minor ticks per major tick
        /// </summary>
        private int minorTicksPerMajorTick = 0;
        /// <summary>
        /// The minor tick color
        /// </summary>
        private Color minorTickColor = Color.Black;
        /// <summary>
        /// The minor tick start
        /// </summary>
        private float minorTickStart = 0.50f;
        /// <summary>
        /// The minor tick size
        /// </summary>
        private float minorTickSize = 0.25f;
        /// <summary>
        /// The minor tick thickness
        /// </summary>
        private float minorTickThickness = 0.45f;
        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.AntiAlias;
        /// <summary>
        /// The cursor cannot drag
        /// </summary>
        private Cursor cursorCannotDrag = Cursors.Default;
        /// <summary>
        /// The mouse mode
        /// </summary>
        private MouseMode mouseMode;
        /// <summary>
        /// The mouse marker set
        /// </summary>
        private MarkerSet mouseMarkerSet;
        /// <summary>
        /// The mouse angle
        /// </summary>
        private float mouseAngle;
        /// <summary>
        /// The mouse angle offset
        /// </summary>
        private float mouseAngleOffset;


        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [show text].
        /// </summary>
        /// <value><c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        public bool ShowText
        {
            get { return showText; }
            set
            {
                showText = value;
                Invalidate();
            }
        } 

        
        /// <summary>
        /// Gets or sets a value indicating whether [show angle].
        /// </summary>
        /// <value><c>true</c> if [show angle]; otherwise, <c>false</c>.</value>
        public bool ShowAngle
        {
            get { return showAngle;}
            set
            {
                
                showAngle = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum { get; set; } = 100;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get { return (int) ((PrimaryMarkerAngle / AngleMax) * (Maximum-Minimum)); }
        }

        /// <summary>
        /// Indicates whether angle wrapping is enabled.
        /// </summary>
        /// <value>Enables or disable angle wrapping.  Default value is <c>true</c>.</value>
        /// <remarks>If <c>true</c>, the angle range is always [0,360).
        /// If <c>false</c>, then the valid angle range is from <c>AngleMin</c> to <c>AngleMax</c>, inclusive.
        /// The difference between this values can exceed 360.0.  The control keeps track of the actual location
        /// of each marker.</remarks>
        [Category("Behavior"), DefaultValue("true")]
        public bool AngleWraps
        {
            get { return angleWraps; }
            set
            {
                if (value != angleWraps)
                {
                    angleWraps = value;
                    AdjustAngles();
                }
            }
        }


        /// <summary>
        /// Gets or sets the minimum angle value.
        /// </summary>
        /// <value>Minimum angle value.</value>
        /// <remarks>Only applicable if <c>AngleWraps</c> is <c>false</c>.</remarks>
        [Category("Behavior"), DefaultValue(0.0)]
        public float AngleMin
        {
            get { return angleMin; }
            set
            {
                if (value != angleMin)
                {
                    angleMin = value;
                    angleMax = Math.Max(angleMin, angleMax);
                    AdjustAngles();
                }
            }
        }


        /// <summary>
        /// Gets or sets the maximum angle value.
        /// </summary>
        /// <value>Maximum angle value.</value>
        /// <remarks>Only applicable if <c>AngleWraps</c> is <c>false</c>.</remarks>
        [Category("Behavior"), DefaultValue(360.0)]
        public float AngleMax
        {
            get { return angleMax; }
            set
            {
                if (value != angleMax)
                {
                    angleMax = value;
                    angleMin = Math.Min(angleMin, angleMax);
                    AdjustAngles();
                }
            }
        }


        /// <summary>
        /// Gets or sets the fixed background flag.
        /// </summary>
        /// <value><c>True</c> if rings, ticks, and text items are considered fixed, <c>false</c> otherwise.
        /// <para>
        /// If fixed, the background color, rings, major and minor tick marks, and text items
        /// are rendered onto a bitmap, which is displayed during the Paint event.
        /// This bitmap is only re-rendered if and when any of these objects change,
        /// or the size of the control changes.
        /// </para><para>
        /// Set this property to <c>true</c> if the rings, ticks and text items
        /// are relatively unchanging, and the time to repaint the control should decrease.
        /// Set to <c>false</c> if any of the the background objects change frequently.
        /// </para></value>
        /// <seealso cref="PaintTimeCount" />
        /// <seealso cref="PaintTimeMean" />
        /// <seealso cref="ResetPaintTimeCounters" />
        /// <seealso cref="MarkerSet.IncludeInFixedBackground" />
        /// <remarks>Use the <c>PaintTimeCount</c> and <c>PaintTimeMean</c> properties to determine
        /// which setting for this property is faster.</remarks>
        [Category("Behavior"), DefaultValue("false")]
        public bool FixedBackground
        {
            get { return fixedBackground; }
            set
            {
                if (value != fixedBackground)
                {
                    fixedBackground = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Shortcut to the angle of the first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The angle of the first <c>MarkerSet</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Angle</c>.</remarks>
        [Category("Behavior"), DefaultValue(45.0)]
        public float PrimaryMarkerAngle
        {
            get { return PrimaryMarkerSet.Angle; }
            set { PrimaryMarkerSet.Angle = value; }
        }

        /// <summary>
        /// Shortcut to the solid fill color of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// Setting this value forces <c>MarkerBrushMode</c> to <c>BrushMode.Solid</c>.
        /// </summary>
        /// <value>The solid fill color of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// Default value is <c>DarkGray</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Marker[0].SolidColor</c>.</remarks>
        [Category("Appearance"), DefaultValue("DarkGray")]
        public Color PrimaryMarkerSolidColor
        {
            get { return PrimaryMarker.SolidColor; }
            set { PrimaryMarker.SolidColor = value; }
        }

        /// <summary>
        /// Shortcut to the border color of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The border color of the first <c>Marker</c> in the first <c>MarkerSet</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Markers[0].BorderColor</c>.</remarks>
        [Category("Appearance"), DefaultValue("Black")]
        public Color PrimaryMarkerBorderColor
        {
            get { return PrimaryMarker.BorderColor; }
            set { PrimaryMarker.BorderColor = value; }
        }

        /// <summary>
        /// Shortcut to the border thickness (in pixels) of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The border thickness (in pixels) of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// Set to zero for no border.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Markers[0].BorderSize</c>.</remarks>
        [Category("Appearance"), DefaultValue(2.0)]
        public float PrimaryMarkerBorderSize
        {
            get { return PrimaryMarker.BorderSize; }
            set { PrimaryMarker.BorderSize = value; }
        }

        /// <summary>
        /// Shortcut to the array of points that defines the shape of the of the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The array of <c>System.Drawing.PointF</c> structures that represent the vertices of the marker when the marker is at zero degrees
        /// of the first <c>Marker</c> in the first <c>MarkerSet</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Markers[0].Points</c>.</remarks>
        public PointF[] PrimaryMarkerPoints
        {
            get { return (PointF[])PrimaryMarker.Points.Clone(); }
            set { PrimaryMarker.Points = (PointF[])value.Clone(); }
        }


        /// <summary>
        /// Gets or sets the number of major ticks.
        /// </summary>
        /// <value>The number of major ticks.
        /// <para>
        /// The major ticks are placed around the control at even angular intervals, with the first at zero degrees.
        /// Set to zero for no major ticks.
        /// </para></value>
        [Category("Appearance"), DefaultValue(4)]
        public int MajorTicks
        {
            get { return majorTicks; }
            set
            {
                majorTicks = Math.Max(value, 0);
                Redraw(true);
            }
        }


        /// <summary>
        /// Gets or set the color of the major ticks.
        /// </summary>
        /// <value>The color of the major ticks.</value>
        [Category("Appearance"), DefaultValue("Black")]
        public Color MajorTickColor
        {
            get { return majorTickColor; }
            set
            {
                majorTickColor = value;
                Redraw(true);
            }
        }


        /// <summary>
        /// Gets or sets the start location of major ticks.
        /// </summary>
        /// <value>The start location of major ticks.
        /// <para>
        /// This value is the distance from the center of the control, where 1.0 is defined
        /// as the distance from the center of the control to the nearest edge.
        /// </para></value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than zero.</exception>
        [Category("Appearance"), DefaultValue(0.40)]
        public float MajorTickStart
        {
            get { return majorTickStart; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("MajorTickStart");
                }
                if (value != majorTickStart)
                {
                    majorTickStart = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the size of the major ticks.
        /// </summary>
        /// <value>The size of the major ticks.
        /// <para>
        /// This value indicates the length of the major ticks, where 1.0 is defined
        /// as the distance from the center of the control to the nearest edge.
        /// </para></value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than or equal to zero.</exception>
        [Category("Appearance"), DefaultValue(0.45)]
        public float MajorTickSize
        {
            get { return majorTickSize; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException("MajorTickSize");
                }
                if (value != majorTickSize)
                {
                    majorTickSize = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the thickness of the major ticks.
        /// </summary>
        /// <value>The thickness (in pixels) of the major ticks.</value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than zero.</exception>
        [Category("Appearance"), DefaultValue(1.0)]
        public float MajorTickThickness
        {
            get { return majorTickThickness; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("MajorTickThickness");
                }
                if (value != majorTickThickness)
                {
                    majorTickThickness = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the number of minor ticks per major tick.
        /// </summary>
        /// <value>The number of minor ticks per major tick.
        /// <para>
        /// Minor ticks are placed between major ticks, at regular angular intervals.
        /// This value is only significant if the number of major ticks is greater than zero.
        /// </para></value>
        [Category("Appearance"), DefaultValue(0)]
        public int MinorTicksPerMajorTick
        {
            get { return minorTicksPerMajorTick; }
            set
            {
                minorTicksPerMajorTick = Math.Max(value, 0);
                Redraw(true);
            }
        }


        /// <summary>
        /// Gets or sets the color of the minor ticks.
        /// </summary>
        /// <value>The color of the minor ticks.</value>
        [Category("Appearance"), DefaultValue("Black")]
        public Color MinorTickColor
        {
            get { return minorTickColor; }
            set
            {
                minorTickColor = value;
                Redraw(true);
            }
        }


        /// <summary>
        /// Gets or sets the start location of minor ticks.
        /// </summary>
        /// <value>The start location of minor ticks.
        /// <para>
        /// This value is the distance from the center of the control, where 1.0 is defined
        /// as the distance from the center of the control to the nearest edge.
        /// </para></value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than zero.</exception>
        [Category("Appearance"), DefaultValue(0.50)]
        public float MinorTickStart
        {
            get { return minorTickStart; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("MinorTickStart");
                }
                if (value != minorTickStart)
                {
                    minorTickStart = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the size of the minor ticks.
        /// </summary>
        /// <value>The size of the minor ticks.
        /// <para>
        /// This value indicates the length of the minor ticks, where 1.0 is defined
        /// as the distance from the center of the control to the nearest edge.
        /// </para></value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than or equal to zero.</exception>
        [Category("Appearance"), DefaultValue(0.25)]
        public float MinorTickSize
        {
            get { return minorTickSize; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException("MinorTickSize");
                }
                if (value != minorTickSize)
                {
                    minorTickSize = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the thickness of the minor ticks.
        /// </summary>
        /// <value>The thickness (in pixels) of the minor ticks.</value>
        /// <exception cref="System.ArgumentException">Thrown if value is less than zero.</exception>
        [Category("Appearance"), DefaultValue(1.0)]
        public float MinorTickThickness
        {
            get { return minorTickThickness; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("MinorTickThickness");
                }
                if (value != minorTickThickness)
                {
                    minorTickThickness = value;
                    Redraw(true);
                }
            }
        }


        /// <summary>
        /// Gets or sets the rendering quality used internally by <c>Graphics</c>.
        /// </summary>
        /// <value>One of the <c>Graphics.SmoothingMode</c> values.</value>
        [Category("Appearance"), DefaultValue("AntiAlias")]
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Redraw(true);
            }
        }


        /// <summary>
        /// Gets or sets the cursor that is displayed when the mouse pointer is over a <c>Marker</c> which cannot be dragged.
        /// </summary>
        /// <value>A <c>Cursor</c> value that represents the cursor to display when the mouse pointer is over a <c>Marker</c> which cannot be dragged.
        /// Default is <c>Cursors.Default</c>.</value>
        [Category("Appearance"), DefaultValue("Default")]
        public Cursor CursorCannotDrag
        {
            get { return cursorCannotDrag; }
            set
            {
                if (value != null)
                {
                    cursorCannotDrag = value;
                    UpdateCursor();
                }
            }
        }


        /// <summary>
        /// Gets or sets the cursor that is displayed when the mouse pointer is over a <c>Marker</c> which can be dragged.
        /// </summary>
        /// <value>A <c>Cursor</c> value that represents the cursor to display when the mouse pointer is over a <c>Marker</c> which can be dragged.
        /// Default is <c>Cursors.Hand</c>.</value>
        [Category("Appearance"), DefaultValue("Hand")]
        public Cursor CursorCanDrag
        {
            get { return cursorCanDrag; }
            set
            {
                if (value != null)
                {
                    cursorCanDrag = value;
                    UpdateCursor();
                }
            }
        }


        /// <summary>
        /// Gets or sets the cursor that is displayed when the mouse pointer is dragging a <c>Marker</c>.
        /// </summary>
        /// <value>A <c>Cursor</c> value that represents the cursor to display when the mouse pointer is dragging a <c>Marker</c>.
        /// Default is <c>Cursors.SizeAll</c>.</value>
        [Category("Appearance"), DefaultValue("SizeAll")]
        public Cursor CursorDragging
        {
            get { return cursorDragging; }
            set
            {
                if (value != null)
                {
                    cursorDragging = value;
                    UpdateCursor();
                }
            }
        }


        /// <summary>
        /// Gets the collection of <c>MarkerSet</c> objects for this <c>ZeroitAsanaCompass</c>.
        /// </summary>
        /// <value>The collection of <c>MarkerSet</c> objects for this <c>ZeroitAsanaCompass</c>.</value>
        /// <remarks>This value is never <c>null</c>.  If there are no marker sets, the collection is empty.</remarks>
        public Collection<MarkerSet> MarkerSets
        {
            [DebuggerStepThrough]
            get { return markerSets; }
        }


        /// <summary>
        /// Gets the collection of <c>TextItem</c> objects for this <c>ZeroitAsanaCompass</c>.
        /// </summary>
        /// <value>The collection of <c>TextItem</c> objects for this <c>ZeroitAsanaCompass</c>.</value>
        /// <remarks>This value is never <c>null</c>.  If there are no text items, the collection is empty.</remarks>
        public TextItemCollection TextItems
        {
            [DebuggerStepThrough]
            get { return textItems; }
        }


        /// <summary>
        /// Gets the collection of <c>Ring</c> objects for this <c>ZeroitAsanaCompass</c>.
        /// </summary>
        /// <value>The collection of <c>Ring</c> objects for this <c>ZeroitAsanaCompass</c>.</value>
        /// <remarks>This value is never <c>null</c>.  If there are no rings, the collection is empty.</remarks>
        public RingCollection Rings
        {
            [DebuggerStepThrough]
            get { return rings; }
        }



        /// <summary>
        /// Gets the number of times the control has been repainted since creation,
        /// or the last time <c>ResetPaintTimeCounters()</c> was called.
        /// </summary>
        /// <value>Number of repaints.</value>
        /// <seealso cref="FixedBackground" />
        /// <seealso cref="PaintTimeMean" />
        /// <seealso cref="ResetPaintTimeCounters" />
        public int PaintTimeCount
        {
            get { return paintTimeCount; }
        }

        /// <summary>
        /// Gets the mean paint time (in milliseconds) since creation,
        /// or the last time <c>ResetPaintTimeCounters()</c> was called.
        /// </summary>
        /// <value>Mean paint time (in milliseconds).</value>
        /// <seealso cref="FixedBackground" />
        /// <seealso cref="PaintTimeCount" />
        /// <seealso cref="ResetPaintTimeCounters" />
        /// <remarks>The PaintTime parameters (<c>PaintTimeCount</c> and <c>PaintTimeMean</c>)
        /// are intended to assist the developer in determining whether painting is faster
        /// with <c>FixedBackground</c> set to <c>true</c> or <c>false</c>.</remarks>
        public double PaintTimeMean
        {
            get
            {
                if (paintTimeCount > 0)
                {
                    return paintTimeSum / (double)paintTimeCount;
                }
                return 0.0;
            }
        }


        /// <summary>
        /// Shortcut to first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The first <c>MarkerSet</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of of <c>MarkerSet</c> objects is empty.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0]</c>.</remarks>
        public MarkerSet PrimaryMarkerSet
        {
            get { return markerSets[0]; }
        }

        /// <summary>
        /// Shortcut to the first <c>Marker</c> in the first <c>MarkerSet</c>.
        /// </summary>
        /// <value>The first <c>Marker</c> in the first <c>MarkerSet</c>.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the collection of <c>MarkerSet</c> objects is empty, or if the the first <c>MarkerSet</c> has no markers.</exception>
        /// <remarks>Equivalent to <c>MarkerSets[0].Marker[0]</c>.</remarks>
        public Marker PrimaryMarker
        {
            get { return PrimaryMarkerSet[0]; }
        }

        /// <summary>
        /// Gets the current mouse mode.
        /// </summary>
        /// <value>The current mouse mode.</value>
        internal MouseMode CurMouseMode
        {
            get { return mouseMode; }
        }


        // Allows a marker set to determine if it is being dragged (to prevent certain programatic angle changes
        // while being dragged).
        /// <summary>
        /// Gets the mouse marker set.
        /// </summary>
        /// <value>The mouse marker set.</value>
        internal MarkerSet MouseMarkerSet
        {
            get { return mouseMarkerSet; }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>The default constructor creates a control with one <c>MarkerSet</c> composed of one <c>Marker</c>.</remarks>
        public ZeroitAsanaCompass()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            markerSets = new Collection<MarkerSet>(this);
            textItems = new TextItemCollection(this);
            rings = new RingCollection(this);

            MarkerSet ms = new MarkerSet();
            markerSets.Add(ms);
            PointF[] markerPoly = new PointF[4];
            markerPoly[0] = new PointF(0.35F, 0.00F);
            markerPoly[1] = new PointF(0.80F, 0.20F);
            markerPoly[2] = new PointF(0.70F, 0.00F);
            markerPoly[3] = new PointF(0.80F, -0.20F);
            ms.Add(new Marker(MakeArgb(0.7f, Color.DarkGray), Color.Black, 2.0f, markerPoly));

            mouseMode = MouseMode.CannotDrag;

            ResetPaintTimeCounters();
        }

        #endregion

        #region Transparency

        #region Include In Constructor

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Include in Paint

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion

        #endregion

        #region Private Methods

        // Helper method - calc cosine of angle in degrees
        /// <summary>
        /// Coses the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>System.Single.</returns>
        private static float COS(float angle)
        {
            float rad = RAD * angle;
            float cos = (float)Math.Cos(rad);
            return cos;
        }

        // Helper method - calc sine of angle in degrees
        /// <summary>
        /// Sins the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>System.Single.</returns>
        private static float SIN(float angle)
        {
            float rad = RAD * angle;
            float sin = (float)Math.Sin(rad);
            return sin;
        }


        /// <summary>
        /// Angles the changed event.
        /// </summary>
        /// <param name="ms">The ms.</param>
        /// <param name="angleChange">The angle change.</param>
        /// <param name="state">The state.</param>
        internal void AngleChangedEvent(MarkerSet ms, float angleChange, AngleChangedArgs.MouseState state)
        {
            if (AngleChanged != null)
            {
                if (angleWraps)
                {
                    if (angleChange >= 180f)
                    {
                        angleChange = angleChange - 360f;
                    }
                    else if (angleChange <= -180f)
                    {
                        angleChange = angleChange + 360f;
                    }
                }
                AngleChanged(this, new AngleChangedArgs(ms, ms.Angle, angleChange, state));
            }
        }

        /// <summary>
        /// Sets the <c>PaintTimeCount</c> and <c>PaintTimeMean</c> values to zero.
        /// </summary>
        /// <seealso cref="FixedBackground" />
        /// <seealso cref="PaintTimeCount" />
        /// <seealso cref="PaintTimeMean" />
        public void ResetPaintTimeCounters()
        {
            paintTimeSum = 0.0;
            paintTimeCount = 0;
        }


        /// <summary>
        /// Helper method to generate a new <c>Color</c> object by applying an opacity value to an existing color.
        /// </summary>
        /// <param name="opacity">Opacity value.  Must be in the range [0.0,1.0].</param>
        /// <param name="color">Color value.</param>
        /// <returns>New color.</returns>
        public static Color MakeArgb(float opacity, Color color)
        {
            Debug.Assert(opacity >= 0.0f && opacity <= 1.0f);
            Color c = Color.FromArgb((int)(255.0f * opacity), color);
            return c;
        }

        /// <summary>
        /// Adjusts the offset angle.
        /// </summary>
        /// <param name="offsetAngle">The offset angle.</param>
        /// <returns>System.Single.</returns>
        internal static float AdjustOffsetAngle(float offsetAngle)
        {
            float a = offsetAngle % 360.0f;
            if (a < 0.0f)
            {
                a += 360f;
            }
            return a;
        }

        /// <summary>
        /// Checks the points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="System.ArgumentNullException">PointF[] points</exception>
        /// <exception cref="System.ArgumentException">Parameter points must be an array with at least two elements</exception>
        internal static void CheckPoints(PointF[] points)
        {
            if (points == null)
            {
                throw new ArgumentNullException("PointF[] points");
            }
            if (points.Length < 2)
            {
                throw new ArgumentException("Parameter points must be an array with at least two elements");
            }
        }

        /// <summary>
        /// Handles the Paint event of the CircleControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void CircleControl_Paint(object sender, PaintEventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Graphics g = e.Graphics;
            g.SmoothingMode = smoothing;

            int dy = this.ClientSize.Height;
            int dx = this.ClientSize.Width;

            xmid = (float)(dx - 1) / 2.0F;
            ymid = (float)(dy - 1) / 2.0F;
            radius = Math.Min(xmid, ymid);

            if (fixedBackground)
            {
                if (bgBitmap == null)
                {
                    bgBitmap = new Bitmap(dx, dy, PixelFormat.Format24bppRgb);
                    Graphics gb = Graphics.FromImage(bgBitmap);
                    gb.SmoothingMode = smoothing;
                    DrawBackground(gb);
                    gb.Dispose();
                }
                g.DrawImage(bgBitmap, 0, 0);
            }
            else
            {
                DrawBackground(g);
            }

            // Draw unfixed marker sets - in reverse order
            for (int i = markerSets.Count - 1; i >= 0; i--)
            {
                MarkerSet ms = markerSets[i];
                if (!ms.IncludeInFixedBackground)
                {
                    DrawMarkerSet(g, ms);
                }
            }

            sw.Stop();
            paintTimeSum += sw.Elapsed.TotalMilliseconds;
            paintTimeCount++;

            if (ShowText)
            {
                if (ShowAngle)
                {
                    CenterString(g, Convert.ToInt32(PrimaryMarkerAngle).ToString(), Font, ForeColor, ClientRectangle);

                }
                else
                {
                    CenterString(g, Value.ToString(), Font, ForeColor, ClientRectangle);

                }
            }
            
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawBackground(Graphics g)
        {
            Brush br = new SolidBrush(BackColor);
            // Tricky - FillRectangle fills the INTERNAL rectangle area
            // So must inflate
            Rectangle r = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            r.Inflate(1, 1);

            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
            else
            {
                g.FillRectangle(br, r);
                br.Dispose();
            }
            

            DrawRings(g);
            DrawTicks(g);
            DrawTextItems(g);

            // Draw fixed markers - in reverse order
            for (int i = markerSets.Count - 1; i >= 0; i--)
            {
                MarkerSet ms = markerSets[i];
                if (ms.IncludeInFixedBackground)
                {
                    DrawMarkerSet(g, ms);
                }
            }
        }

        /// <summary>
        /// Draws the rings.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawRings(Graphics g)
        {
            // Draw rings from outside in, then draw borders
            if (rings.Count > 0)
            {
                RectangleF[] rect = new RectangleF[rings.Count];
                float offset = 0.0f;
                for (int i = 0; i < rings.Count; i++)
                {
                    float rad = radius * (offset + rings[i].Size);
                    float diam = 2.0f * rad;
                    rect[i] = new RectangleF(xmid - rad, ymid - rad, diam, diam);
                    offset += rings[i].Size;
                }
                for (int i = rings.Count - 1; i >= 0; i--)
                {
                    DrawRing(g, rings[i], rect[i]);
                }
                for (int i = rings.Count - 1; i >= 0; i--)
                {
                    DrawRingBorder(g, rings[i], rect[i]);
                }
            }
        }

        /// <summary>
        /// Gets the ring path.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="rect">The rect.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath GetRingPath(Ring r, RectangleF rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);
            return path;
        }

        /// <summary>
        /// Draws the ring.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="rect">The rect.</param>
        private void DrawRing(Graphics g, Ring r, RectangleF rect)
        {
            GraphicsPath path = GetRingPath(r, rect);
            g.FillPath(r.GetBrush(rect), path);
            path.Dispose();
        }

        /// <summary>
        /// Draws the ring border.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="rect">The rect.</param>
        private void DrawRingBorder(Graphics g, Ring r, RectangleF rect)
        {
            if (r.BorderSize > 0.0f)
            {
                Pen pen = new Pen(r.BorderColor, r.BorderSize);
                GraphicsPath path = GetRingPath(r, rect);
                g.DrawPath(pen, path);
                path.Dispose();
                pen.Dispose();
            }
        }

        /// <summary>
        /// Draws the ticks.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawTicks(Graphics g)
        {
            if (majorTicks > 0)
            {
                Pen pen = new Pen(majorTickColor, majorTickThickness);
                Pen pen2 = new Pen(minorTickColor, minorTickThickness);

                double tickDiv = 360.0 / (double)(majorTicks * (minorTicksPerMajorTick + 1));

                for (int i = 0; i < majorTicks; i++)
                {
                    double majorAng = 360.0 * (double)i / (double)majorTicks;
                    DrawTick(g, pen, majorAng, majorTickStart, majorTickSize);

                    if (minorTicksPerMajorTick > 0)
                    {
                        for (int j = 1; j <= minorTicksPerMajorTick; j++)
                        {
                            double minorAng = majorAng + (double)j * tickDiv;
                            DrawTick(g, pen2, minorAng, minorTickStart, minorTickSize);
                        }
                    }
                }

                pen2.Dispose();
                pen.Dispose();
            }
        }

        /// <summary>
        /// Draws the tick.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="tickStart">The tick start.</param>
        /// <param name="tickSize">Size of the tick.</param>
        private void DrawTick(Graphics g, Pen pen, double angle, float tickStart, float tickSize)
        {
            float cos = COS((float)angle);
            float sin = SIN((float)angle);
            float tickEnd = tickStart + tickSize;
            float xa = radius * tickStart * cos;
            float xb = radius * tickEnd * cos;
            float ya = radius * -tickStart * sin; // flip on y-axis
            float yb = radius * -tickEnd * sin;

            g.DrawLine(pen, xmid + xa, ymid + ya, xmid + xb, ymid + yb);
        }

        /// <summary>
        /// Draws the text items.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawTextItems(Graphics g)
        {
            for (int i = 0; i < textItems.Count; i++)
            {
                TextItem ti = textItems[i];

                float cos = COS(ti.Angle);
                float sin = SIN(ti.Angle);

                // x,y is center of text
                float x = radius * ti.Position * cos;
                float y = radius * ti.Position * sin;

                ti.Draw(g, radius, xmid + x, ymid - y);
            }
        }

        /// <summary>
        /// Draws the marker set.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="ms">The ms.</param>
        private void DrawMarkerSet(Graphics g, MarkerSet ms)
        {
            for (int i = ms.Count - 1; i >= 0; i--)
            {
                if (ms[i].Visible)
                {
                    DrawMarker(g, ms[i], ms.Angle + ms[i].OffsetAngle);
                }
            }
        }

        /// <summary>
        /// Draws the marker.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="m">The m.</param>
        /// <param name="angle">The angle.</param>
        private void DrawMarker(Graphics g, Marker m, float angle)
        {
            float cos = COS(angle);
            float sin = SIN(angle);

            // Build a new polygon using angle
            PointF[] p = new PointF[m.Points.Length];
            for (int i = 0; i < m.Points.Length; i++)
            {
                float x = m.Points[i].X * radius;
                float y = -m.Points[i].Y * radius; // flip on y-axis

                p[i].X = xmid + x * cos + y * sin;
                p[i].Y = ymid - x * sin + y * cos;
            }

            m.ClearPath();
            m.Path = new GraphicsPath();
            m.Path.AddPolygon(p);
            m.Path.CloseFigure();

            Brush br = m.GetBrush(xmid, ymid, radius, cos, sin);
            g.FillPath(br, m.Path);

            Pen pen = new Pen(m.BorderColor, m.BorderSize);
            g.DrawPath(pen, m.Path);
            pen.Dispose();
        }

        /// <summary>
        /// Calculates the mouse angle.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        /// <returns>System.Single.</returns>
        private float CalcMouseAngle(MouseEventArgs e)
        {
            float x = (float)e.X;
            float y = (float)e.Y;

            float dx = x - xmid;
            float dy = -(y - ymid);
            float ang = RADInv * (float)Math.Acos(dx / Math.Sqrt(dx * dx + dy * dy));
            if (dy < 0.0F)
            {
                ang = 360.0F - ang;
            }

            return ang;
        }

        /// <summary>
        /// Updates the cursor.
        /// </summary>
        private void UpdateCursor()
        {
            if (mouseMode == MouseMode.CanDrag)
            {
                this.Cursor = CursorCanDrag;
            }
            else if (mouseMode == MouseMode.Dragging)
            {
                this.Cursor = CursorDragging;
            }
            else
            {
                this.Cursor = CursorCannotDrag;
            }
        }

        /// <summary>
        /// Finds the marker set under mouse.
        /// </summary>
        /// <param name="mouseLocation">The mouse location.</param>
        /// <param name="mouseButtons">The mouse buttons.</param>
        /// <returns>MarkerSet.</returns>
        private MarkerSet FindMarkerSetUnderMouse(Point mouseLocation, MouseButtons mouseButtons)
        {
            for (int i = 0; i < markerSets.Count; i++)
            {
                MarkerSet ms = markerSets[i];
                for (int j = 0; j < ms.Count; j++)
                {
                    Marker m = ms[j];
                    if ((m.DragButtons & mouseButtons) != MouseButtons.None
                        && m.Path != null && m.Path.IsVisible(mouseLocation.X, mouseLocation.Y))
                    {
                        return ms;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Handles the MouseDown event of the CircleControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CircleControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseMarkerSet = FindMarkerSetUnderMouse(e.Location, e.Button);

            if (mouseMarkerSet != null)
            {
                mouseAngle = CalcMouseAngle(e);
                mouseAngleOffset = mouseMarkerSet.Angle - mouseAngle;
                mouseMode = MouseMode.Dragging;
                mouseMarkerSet.SetAngle(mouseMarkerSet.Angle, AngleChangedArgs.MouseState.Down);
            }
            else
            {
                mouseMode = MouseMode.CannotDrag;
            }
            UpdateCursor();
        }

        /// <summary>
        /// Handles the MouseUp event of the CircleControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CircleControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseMode == MouseMode.Dragging)
            {
                mouseMarkerSet.SetAngle(mouseMarkerSet.Angle, AngleChangedArgs.MouseState.Up);
                mouseMode = MouseMode.CanDrag;
                UpdateCursor();
                mouseMarkerSet = null;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the CircleControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CircleControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseMode == MouseMode.Dragging)
            {
                float newMouseAngle = CalcMouseAngle(e);
                float angleChange = newMouseAngle - mouseAngle;

                // If change is positive, motion is CCW, if negative, motion is CW
                if (angleChange >= 180.0f)
                {
                    angleChange %= 360.0f;
                    if (angleChange >= 180.0f)
                    {
                        angleChange -= 360.0f;
                    }
                }
                else if (angleChange <= -180.0f)
                {
                    angleChange %= 360.0f;
                    if (angleChange <= -180.0f)
                    {
                        angleChange += 360.0f;
                    }
                }

                mouseAngle += angleChange;

                float markerAngle = mouseAngle + mouseAngleOffset;

                mouseMarkerSet.SetAngle(markerAngle, AngleChangedArgs.MouseState.Dragging);
            }
            else if (FindMarkerSetUnderMouse(e.Location, MouseButtons.Left | MouseButtons.Middle | MouseButtons.Right) != null)
            {
                mouseMode = MouseMode.CanDrag;
            }
            else
            {
                mouseMode = MouseMode.CannotDrag;
            }
            UpdateCursor();
        }


        /// <summary>
        /// Set minimum and maximum angle value.
        /// </summary>
        /// <param name="min">Minimum angle.</param>
        /// <param name="max">Maximum angle.</param>
        /// <remarks>If, by chance, <c>min</c> is greater than <c>max</c>, the two values are silently flipped.
        /// Only applicable if <c>AngleWraps</c> is <c>false</c>.</remarks>
        public void SetAngleMinMax(float min, float max)
        {
            float v1 = Math.Min(min, max);
            float v2 = Math.Max(min, max);
            if (v1 != angleMin || v2 != angleMax)
            {
                angleMin = v1;
                angleMax = v2;
                AdjustAngles();
            }
        }

        // Adjust angle value depending if angle wrapping is enabled.
        /// <summary>
        /// Adjusts the angle.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns>System.Single.</returns>
        private float AdjustAngle(float a)
        {
            if (angleWraps)
            {
                a %= 360.0F;
                if (a < 0.0F)
                {
                    a += 360.0F;
                }
                return a;
            }
            return Math.Max(Math.Min(a, angleMax), angleMin);
        }

        /// <summary>
        /// Adjusts the angles.
        /// </summary>
        private void AdjustAngles()
        {
            bool redraw = false;
            for (int i = 0; i < markerSets.Count; i++)
            {
                MarkerSet ms = markerSets[i];
                float a = AdjustAngle(ms.Angle);
                if (a != ms.Angle)
                {
                    ms.Angle = a;
                    redraw = true;
                }
            }
            if (redraw)
            {
                Redraw();
            }
        }


        /// <summary>
        /// Returns the angle of the major tick at the specified angle.
        /// </summary>
        /// <param name="idx">The zero-based index of the major tick.</param>
        /// <returns>The angle of the major tick mark at the specified.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="idx" /> is less than zero or greater than <c>MajorTicks</c>.</exception>
        /// <remarks>The angle of major tick at index zero is always zero.</remarks>
        public float GetMajorTickAngle(int idx)
        {
            if (idx < 0 || idx >= MajorTicks)
            {
                throw new System.ArgumentOutOfRangeException("idx");
            }
            return 360.0f * (float)idx / (float)MajorTicks;
        }

        /// <summary>
        /// Fixes the ratio.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns>System.Single.</returns>
        private static float FixRatio(float r)
        {
            return (float)Math.Max(r, 0.0f);
        }

        /// <summary>
        /// Redraws the specified redraw fixed background.
        /// </summary>
        /// <param name="redrawFixedBackground">if set to <c>true</c> [redraw fixed background].</param>
        private void Redraw(bool redrawFixedBackground)
        {
            if (redrawFixedBackground)
            {
                ClearFixedBackground();
            }
            Invalidate();
        }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        private void Redraw()
        {
            Redraw(false);
        }

        /// <summary>
        /// Clears the fixed background.
        /// </summary>
        private void ClearFixedBackground()
        {
            if (bgBitmap != null)
            {
                bgBitmap.Dispose();
                bgBitmap = null;
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the CircleControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CircleControl_SizeChanged(object sender, EventArgs e)
        {
            Redraw(true);
        }


        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.SuspendLayout();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ZeroitAsanaCompass";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CircleControl_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CircleControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CircleControl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CircleControl_MouseUp);
            this.SizeChanged += new System.EventHandler(this.CircleControl_SizeChanged);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Delegates and Events

        /// <summary>
        /// Represents the method that is called when the angle of a MarkerSet changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public delegate void AngleChangedHandler(object sender, AngleChangedArgs e);

        /// <summary>
        /// This delegate is called when the angle of a MarkerSet changes.
        /// </summary>
        public event AngleChangedHandler AngleChanged;



        #endregion

        #region Internal Class
        /// <summary>
        /// Provides data for the <c>AngleChanged</c> event.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class AngleChangedArgs : EventArgs
        {
            /// <summary>
            /// State of mouse button when angle changed.
            /// </summary>
            /// <remarks>It's not really the "state" of the mouse button, it's more like a grungy combination of mouse state and recent mouse events.
            /// Forgive the lack of purity...</remarks>
            public enum MouseState
            {
                /// <summary>
                /// Mouse button has just been clicked down, and the marker has been grabbed.
                /// Next state is Dragging or Up.
                /// </summary>
                Down,

                /// <summary>
                /// Mouse button was previously clicked down, and the marker is being dragged.
                /// Next state is Up.
                /// </summary>
                Dragging,

                /// <summary>
                /// Mouse button was just released, dragging has completed.
                /// Next state is Down.
                /// </summary>
                Up,

                /// <summary>
                /// Mouse state is not known - angle was been changed by setting <c>Angle</c> property.
                /// </summary>
                Unknown,
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="AngleChangedArgs"/> class.
            /// </summary>
            /// <param name="ms">The ms.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="mouseState">State of the mouse.</param>
            internal AngleChangedArgs(MarkerSet ms, float angle, MouseState mouseState)
                    : this(ms, angle, 0.0f, mouseState)
            { }

            /// <summary>
            /// Initializes a new instance of the <see cref="AngleChangedArgs"/> class.
            /// </summary>
            /// <param name="ms">The ms.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="angleChange">The angle change.</param>
            /// <param name="mouseState">State of the mouse.</param>
            internal AngleChangedArgs(MarkerSet ms, float angle, float angleChange, MouseState mouseState)
            {
                this.ms = ms;
                this.angle = angle;
                this.angleChange = angleChange;
                this.mouseState = mouseState;
            }

            /// <summary>
            /// The ms
            /// </summary>
            private MarkerSet ms;
            /// <summary>
            /// Gets the <c>MarkerSet</c> which is/was being dragged.
            /// </summary>
            /// <value>The <c>MarkerSet</c> which is/was being dragged.</value>
            public MarkerSet Ms
            {
                get { return ms; }
            }

            /// <summary>
            /// The angle
            /// </summary>
            private float angle;
            /// <summary>
            /// Gets the angle of the <c>MarkerSet</c> which is/was being dragged.
            /// </summary>
            /// <value>The angle of the <c>MarkerSet</c> which is/was being dragged.</value>
            public float Angle
            {
                get { return angle; }
            }

            /// <summary>
            /// The angle change
            /// </summary>
            private float angleChange;
            /// <summary>
            /// Gets Indicates the amount the angle value has changed since the last event.
            /// </summary>
            /// <value>Amount angle has changed since last event for this <c>MarkerSet</c>.</value>
            public float AngleChange
            {
                get { return angleChange; }
            }

            /// <summary>
            /// The mouse state
            /// </summary>
            private MouseState mouseState;
            /// <summary>
            /// Gets the state of the mouse when the angle changed.
            /// </summary>
            /// <value>One of the <c>MouseState</c> values.</value>
            public MouseState Mouse
            {
                get { return mouseState; }
            }
        }

        #endregion

        
        #region Center Text

        //------------------------------Include in Paint----------------------------//
        //
        // CenterString(G,Text,Font,ForeColor,this.ClientRectangle);
        //
        //------------------------------Include in Paint----------------------------//

        /// <summary>
        /// Center Text
        /// </summary>
        /// <param name="G">Set Graphics</param>
        /// <param name="T">Set string</param>
        /// <param name="F">Set Font</param>
        /// <param name="C">Set color</param>
        /// <param name="R">Set rectangle</param>
        private static void CenterString(System.Drawing.Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF TS = G.MeasureString(T, F);

            using (SolidBrush B = new SolidBrush(C))
            {
                G.DrawString(T, F, B, new Point((int)(R.Width / 2 - (TS.Width / 2)), (int)(R.Height / 2 - (TS.Height / 2))));
            }
        }

        #endregion

        
    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitAsanaCompassDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitAsanaCompassDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitAsanaCompassDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ZeroitAsanaCompassSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitAsanaCompassSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitAsanaCompassSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAsanaCompass colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaCompassSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitAsanaCompassSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAsanaCompass;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the primary marker solid.
        /// </summary>
        /// <value>The color of the primary marker solid.</value>
        public Color PrimaryMarkerSolidColor
        {
            get
            {
                return colUserControl.PrimaryMarkerSolidColor;
            }
            set
            {
                GetPropertyByName("PrimaryMarkerSolidColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the primary marker border.
        /// </summary>
        /// <value>The color of the primary marker border.</value>
        public Color PrimaryMarkerBorderColor
        {
            get
            {
                return colUserControl.PrimaryMarkerBorderColor;
            }
            set
            {
                GetPropertyByName("PrimaryMarkerBorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the major tick.
        /// </summary>
        /// <value>The color of the major tick.</value>
        public Color MajorTickColor
        {
            get
            {
                return colUserControl.MajorTickColor;
            }
            set
            {
                GetPropertyByName("MajorTickColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the minor tick.
        /// </summary>
        /// <value>The color of the minor tick.</value>
        public Color MinorTickColor
        {
            get
            {
                return colUserControl.MinorTickColor;
            }
            set
            {
                GetPropertyByName("MinorTickColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [angle wraps].
        /// </summary>
        /// <value><c>true</c> if [angle wraps]; otherwise, <c>false</c>.</value>
        public bool AngleWraps
        {
            get
            {
                return colUserControl.AngleWraps;
            }
            set
            {
                GetPropertyByName("AngleWraps").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the angle minimum.
        /// </summary>
        /// <value>The angle minimum.</value>
        public float AngleMin
        {
            get
            {
                return colUserControl.AngleMin;
            }
            set
            {
                GetPropertyByName("AngleMin").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the angle maximum.
        /// </summary>
        /// <value>The angle maximum.</value>
        public float AngleMax
        {
            get
            {
                return colUserControl.AngleMax;
            }
            set
            {
                GetPropertyByName("AngleMax").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fixed background].
        /// </summary>
        /// <value><c>true</c> if [fixed background]; otherwise, <c>false</c>.</value>
        public bool FixedBackground
        {
            get
            {
                return colUserControl.FixedBackground;
            }
            set
            {
                GetPropertyByName("FixedBackground").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the primary marker angle.
        /// </summary>
        /// <value>The primary marker angle.</value>
        public float PrimaryMarkerAngle
        {
            get
            {
                return colUserControl.PrimaryMarkerAngle;
            }
            set
            {
                GetPropertyByName("PrimaryMarkerAngle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the primary marker border.
        /// </summary>
        /// <value>The size of the primary marker border.</value>
        public float PrimaryMarkerBorderSize
        {
            get
            {
                return colUserControl.PrimaryMarkerBorderSize;
            }
            set
            {
                GetPropertyByName("PrimaryMarkerBorderSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the primary marker points.
        /// </summary>
        /// <value>The primary marker points.</value>
        public PointF[] PrimaryMarkerPoints
        {
            get
            {
                return colUserControl.PrimaryMarkerPoints;
            }
            set
            {
                GetPropertyByName("PrimaryMarkerPoints").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the major ticks.
        /// </summary>
        /// <value>The major ticks.</value>
        public int MajorTicks
        {
            get
            {
                return colUserControl.MajorTicks;
            }
            set
            {
                GetPropertyByName("MajorTicks").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the major tick start.
        /// </summary>
        /// <value>The major tick start.</value>
        public float MajorTickStart
        {
            get
            {
                return colUserControl.MajorTickStart;
            }
            set
            {
                GetPropertyByName("MajorTickStart").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the major tick.
        /// </summary>
        /// <value>The size of the major tick.</value>
        public float MajorTickSize
        {
            get
            {
                return colUserControl.MajorTickSize;
            }
            set
            {
                GetPropertyByName("MajorTickSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the major tick thickness.
        /// </summary>
        /// <value>The major tick thickness.</value>
        public float MajorTickThickness
        {
            get
            {
                return colUserControl.MajorTickThickness;
            }
            set
            {
                GetPropertyByName("MajorTickThickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minor ticks per major tick.
        /// </summary>
        /// <value>The minor ticks per major tick.</value>
        public int MinorTicksPerMajorTick
        {
            get
            {
                return colUserControl.MinorTicksPerMajorTick;
            }
            set
            {
                GetPropertyByName("MinorTicksPerMajorTick").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minor tick start.
        /// </summary>
        /// <value>The minor tick start.</value>
        public float MinorTickStart
        {
            get
            {
                return colUserControl.MinorTickStart;
            }
            set
            {
                GetPropertyByName("MinorTickStart").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the minor tick.
        /// </summary>
        /// <value>The size of the minor tick.</value>
        public float MinorTickSize
        {
            get
            {
                return colUserControl.MinorTickSize;
            }
            set
            {
                GetPropertyByName("MinorTickSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minor tick thickness.
        /// </summary>
        /// <value>The minor tick thickness.</value>
        public float MinorTickThickness
        {
            get
            {
                return colUserControl.MinorTickThickness;
            }
            set
            {
                GetPropertyByName("MinorTickThickness").SetValue(colUserControl, value);
            }
        }


        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("AngleWraps",
                                 "AngleWraps", "Appearance",
                                 "Set to enable angle wrap."));
            
            items.Add(new DesignerActionPropertyItem("FixedBackground",
                "Fixed Background", "Appearance",
                "Set to enable fixed background."));

            
            items.Add(new DesignerActionPropertyItem("AngleMin",
                                 "Minimum Angle", "Appearance",
                                 "Sets the minimum angle."));

            items.Add(new DesignerActionPropertyItem("AngleMax",
                                 "Maximum Angle", "Appearance",
                                 "Sets the maximum angle."));

            items.Add(new DesignerActionPropertyItem("PrimaryMarkerSolidColor",
                "Primary Marker Color", "Appearance",
                "Sets the primary color."));

            items.Add(new DesignerActionPropertyItem("PrimaryMarkerBorderColor",
                "Primary Marker Border", "Appearance",
                "Sets the primary border color."));

            items.Add(new DesignerActionPropertyItem("PrimaryMarkerAngle",
                "Primary Marker Angle", "Appearance",
                "Sets the primary marker angle."));

            items.Add(new DesignerActionPropertyItem("PrimaryMarkerBorderSize",
                "Primary Marker Border Size", "Appearance",
                "Sets the primary border size."));

            items.Add(new DesignerActionPropertyItem("PrimaryMarkerPoints",
                "Primary Marker Points", "Appearance",
                "Sets the primary marker points."));

            items.Add(new DesignerActionPropertyItem("MajorTickColor",
                "Major Tick Color", "Appearance",
                "Sets the major tick color."));

            
            items.Add(new DesignerActionPropertyItem("MajorTicks",
                "Major Ticks", "Appearance",
                "Sets the number of ticks."));

            items.Add(new DesignerActionPropertyItem("MajorTickStart",
                "Major Tick Distance", "Appearance",
                "Sets the major tick distance."));


            items.Add(new DesignerActionPropertyItem("MajorTickSize",
                "Major Tick Size", "Appearance",
                "Sets the major tick size."));

            items.Add(new DesignerActionPropertyItem("MajorTickThickness",
                "Major Tick Thickness", "Appearance",
                "Sets the major tick thickness."));

            items.Add(new DesignerActionPropertyItem("MinorTickColor",
                "Minor Tick Color", "Appearance",
                "Sets the minor tick color."));

            items.Add(new DesignerActionPropertyItem("MinorTicksPerMajorTick",
                "Sub Tick", "Appearance",
                "Sets the sub ticks per major tick."));

            items.Add(new DesignerActionPropertyItem("MinorTickStart",
                "Minor Tick Distance", "Appearance",
                "Sets the minor tick distance from the center point."));


            items.Add(new DesignerActionPropertyItem("MinorTickSize",
                "Minor Tick Size", "Appearance",
                "Sets the minor tick size."));

            items.Add(new DesignerActionPropertyItem("MinorTickThickness",
                "Minor Tick Thickness", "Appearance",
                "Sets the minor tick thickness."));



            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion



}
