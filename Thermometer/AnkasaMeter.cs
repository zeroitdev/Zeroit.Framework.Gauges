// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-28-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="AnkasaMeter.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Diagnostics;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;

#endregion

namespace Zeroit.Framework.Gauges.Meter.AnkasaMeter
{

    #region Base Class


    /// <summary>
    /// Base class for manometers
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    [Description("Base class for manometers")]
    public abstract class ManometerBase : Control
    {
        #region -- Members --

        /// <summary>
        /// The maximum
        /// </summary>
        private float max;
        /// <summary>
        /// The minimum
        /// </summary>
        private float min;
        /// <summary>
        /// The stored maximum
        /// </summary>
        private float storedMax;
        /// <summary>
        /// The store maximum
        /// </summary>
        private bool storeMax;
        /// <summary>
        /// The text unit
        /// </summary>
        private String textUnit;
        /// <summary>
        /// The text description
        /// </summary>
        private String textDescription;
        /// <summary>
        /// The value
        /// </summary>
        private float value;
        /// <summary>
        /// The start angle
        /// </summary>
        private int startAngle = startAngleDefault;
        /// <summary>
        /// The interval
        /// </summary>
        private float interval = defaultInterval;
        //Constants
        /// <summary>
        /// The default interval
        /// </summary>
        private const int defaultInterval = 10;
        /// <summary>
        /// The maximum default
        /// </summary>
        private const int maxDefault = 100;
        /// <summary>
        /// The minimum default
        /// </summary>
        private const int minDefault = 0;
        /// <summary>
        /// The start angle default
        /// </summary>
        private const int startAngleDefault = 230;
        /// <summary>
        /// The text unit default
        /// </summary>
        private const String textUnitDefault = "";
        /// <summary>
        /// The text description default
        /// </summary>
        private const String textDescriptionDefault = "";

        #endregion

        #region -- Properties --

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value</value>
        [Browsable(true)]
        [Description("Gets or sets the max value.")]
        [Category("Layout")]
        [DefaultValue(maxDefault)]
        public float Max
        {
            get { return max; }
            set
            {
                max = (max < min) ? min : value;
                if (MaxChanged != null)
                    MaxChanged(this, new EventArgs());
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min.</value>
        [Browsable(true)]
        [Description("Gets or sets the min value.")]
        [Category("Layout")]
        [DefaultValue(minDefault)]
        public float Min
        {
            get { return min; }
            set
            {
                min = (min > max) ? max : value;
                if (MinChanged != null)
                    MinChanged(this, new EventArgs());
                Invalidate();
            }
        }

        /// <summary>
        /// The intervals between Min and Max.
        /// </summary>
        /// <value>The min.</value>
        [Browsable(true)]
        [Description("The intervals between Min and Max.")]
        [Category("Layout")]
        [DefaultValue(defaultInterval)]
        public float Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                if (IntervalChanged != null)
                    IntervalChanged(this, new EventArgs());
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to store max.
        /// </summary>
        /// <value><c>true</c> if [store max]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Description("Gets or sets a value indicating whether to store the max value.")]
        [Category("Layout")]
        [DefaultValue(true)]
        public bool StoreMax
        {
            get { return storeMax; }
            set
            {
                storeMax = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description</value>
        [Browsable(true)]
        [Description("Gets or sets the description.")]
        [Category("Layout")]
        [DefaultValue(textDescriptionDefault)]
        [Localizable(true)]
        public string TextDescription
        {
            get { return textDescription; }
            set { textDescription = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the text unit.
        /// </summary>
        /// <value>The text unit.</value>
        [Browsable(true)]
        [Description("Gets or sets the description.")]
        [Category("Layout")]
        [DefaultValue(textUnitDefault)]
        [Localizable(true)]
        public string TextUnit
        {
            get { return textUnit; }
            set { textUnit = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the starting angle (degrees)
        /// </summary>
        /// <value>The starting angle</value>
        [Browsable(true)]
        [Description("Gets or sets the layout start (degrees).")]
        [Category("Layout")]
        [DefaultValue(startAngleDefault)]
        public int StartAngle
        {
            get { return startAngle; }
            set
            {
                if (value > 360)
                    value = 360;
                startAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the stored max.
        /// </summary>
        /// <value>The stored max.</value>
        [Browsable(true)]
        [Description("Gets or sets the stored max.")]
        [Category("Layout")]
        [DefaultValue(0)]
        public float StoredMax
        {
            get { return storedMax; }
            set
            {
                if (value < min)
                    value = min;
                if (value > max)
                    value = max;
                if (value < Value)
                    value = Value;
                storedMax = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Browsable(true)]
        [Description("Gets or sets the value.")]
        [Category("Layout")]
        [DefaultValue(0)]
        public float Value
        {
            get { return value; }
            set
            {
                if (value < min)
                    value = min;
                if (value > max)
                    value = max;
                this.value = value;
                //Fire events
                if ((value > storedMax) && storeMax)
                {
                    storedMax = value;
                    if (StoredMaxChanged != null)
                        StoredMaxChanged(this, new EventArgs());
                }
                if (ValueChanged != null)
                    ValueChanged(this, new EventArgs());
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control. Not relevant for this control
        /// </summary>
        /// <value>The text.</value>
        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region -- Events --

        /// <summary>
        /// Occurs when the value of the ManometerBase.Interval property changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Property Changed")]
        public event EventHandler IntervalChanged;
        /// <summary>
        /// Occurs when the value of the ManometerBase.Min property changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Property Changed")]
        public event EventHandler MinChanged;
        /// <summary>
        /// Occurs when the value of the ManometerBase.Max property changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Property Changed")]
        public event EventHandler MaxChanged;
        /// <summary>
        /// Occurs when the value of the ManometerBase.StoredMax property changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Property Changed")]
        public event EventHandler StoredMaxChanged;
        /// <summary>
        /// Occurs when the value of the ManometerBase.ValueChanged property changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Property Changed")]
        public event EventHandler ValueChanged;


        #endregion

        #region -- Constructor --

        /// <summary>
        /// Create a new instance of ManometerBase
        /// </summary>
        protected ManometerBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            Name = "ManometerBase";
            max = 100;
            min = 0;
            StartAngle = startAngleDefault;
            TextDescription = "";
            TextUnit = "";
            StoreMax = true;
            StoredMax = min;
        }

        #endregion

    }


    #endregion

    #region Control

    /// <summary>
    /// A termometer control
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Gauges.Meter.AnkasaMeter.ManometerBase" />
    //[ToolboxBitmap(typeof(ResourceLocator), "Manometers.Resources.ThermometerIcon.bmp")]
    [Designer(typeof(ZeroitThermometerDesigner))]
    public class ZeroitThermometer : ManometerBase
    {
        #region -- Members --

        /// <summary>
        /// The shadow rect
        /// </summary>
        private RectangleF shadowRect, backgroundRect, numberRect, bar1Rect, bar2Rect, bar3Rect;
        /// <summary>
        /// The arrow1 rect
        /// </summary>
        private RectangleF arrow1Rect, arrow2Rect;
        /// <summary>
        /// The number spacing
        /// </summary>
        private int numberSpacing = defaultSpacing;
        /// <summary>
        /// The back color
        /// </summary>
        private Color[] backColor = new Color[]{Color.FromArgb(240,240,240),Color.Peru};
        /// <summary>
        /// The border width
        /// </summary>
        private int borderWidth;
        /// <summary>
        /// The color arrow
        /// </summary>
        private Color[] colorArrow = new Color[]
        {
            Color.FromArgb(100, Color.Red),
            Color.Black
        };
        /// <summary>
        /// The bar color
        /// </summary>
        private Color barColor = Color.Black;
        /// <summary>
        /// The clock wise
        /// </summary>
        private bool clockWise = true;
        /// <summary>
        /// The decimals
        /// </summary>
        private int decimals;
        /// <summary>
        /// The bars between numbers
        /// </summary>
        private int barsBetweenNumbers = defaultBarsBetweenNumbers;
        /// <summary>
        /// The texture brush
        /// </summary>
        private Brush textureBrush;

        /// <summary>
        /// The draw shadow
        /// </summary>
        bool drawShadow = true;
        /// <summary>
        /// The draw background
        /// </summary>
        bool drawBackground = true;
        /// <summary>
        /// The draw border
        /// </summary>
        bool drawBorder = true;
        /// <summary>
        /// The draw inner shadow
        /// </summary>
        bool drawInnerShadow = true;
        /// <summary>
        /// The draw bars
        /// </summary>
        bool drawBars = true;
        /// <summary>
        /// The draw numbers
        /// </summary>
        bool drawNumbers = true;
        /// <summary>
        /// The draw text
        /// </summary>
        bool drawText = true;
        /// <summary>
        /// The draw arrows
        /// </summary>
        bool drawArrows = true;
        /// <summary>
        /// The draw reflex
        /// </summary>
        bool drawReflex = true;

        //Constants
        /// <summary>
        /// The default width
        /// </summary>
        private const int defaultWidth = 100;
        /// <summary>
        /// The default height
        /// </summary>
        private const int defaultHeight = 100;
        /// <summary>
        /// The default font size
        /// </summary>
        private const int defaultFontSize = 11;
        /// <summary>
        /// The default maximum
        /// </summary>
        private const int defaultMax = 100;
        /// <summary>
        /// The default minimum
        /// </summary>
        private const int defaultMin = 0;
        /// <summary>
        /// The default lighting angle
        /// </summary>
        private const int defaultLightingAngle = 90;
        /// <summary>
        /// The default border width
        /// </summary>
        private const int defaultBorderWidth = 6;
        /// <summary>
        /// The default decimals
        /// </summary>
        private const int defaultDecimals = 0;
        /// <summary>
        /// The default spacing
        /// </summary>
        private const int defaultSpacing = 30;
        /// <summary>
        /// The bar outer margin
        /// </summary>
        private const int barOuterMargin = 12;
        /// <summary>
        /// The bar inner margin
        /// </summary>
        private const int barInnerMargin = 4;
        /// <summary>
        /// The default bar height
        /// </summary>
        private const int defaultBarHeight = 5;
        /// <summary>
        /// The default bar width
        /// </summary>
        private const int defaultBarWidth = 3;
        /// <summary>
        /// The default bars between numbers
        /// </summary>
        private const int defaultBarsBetweenNumbers = 5;
        /// <summary>
        /// The default inner shadow width
        /// </summary>
        private const int defaultInnerShadowWidth = 2;
        /// <summary>
        /// The default outer shadow width
        /// </summary>
        private const int defaultOuterShadowWidth = 2;
        /// <summary>
        /// The number margin
        /// </summary>
        private const int numberMargin = barOuterMargin + barInnerMargin + defaultBorderWidth + defaultBarHeight;

        #endregion

        #region -- Properties --


        /// <summary>
        /// Gets or sets the decimals used for the numbers
        /// </summary>
        /// <value>The decimals.</value>
        [Browsable(true)]
        [Description("Gets or sets the decimals.")]
        [Category("Appearance")]
        [DefaultValue(defaultDecimals)]
        public int Decimals
        {
            get { return decimals; }
            set { decimals = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the space between numbers in degrees.
        /// </summary>
        /// <value>The number spacing.</value>
        [Browsable(true)]
        [DefaultValue(defaultSpacing)]
        [Description("Gets or sets the space between numbers in degrees.")]
        [Category("Layout")]
        [Localizable(true)]
        public int NumberSpacing
        {
            get { return numberSpacing; }
            set
            {
                if (numberSpacing <= 0)
                    Debug.Assert(false, "Number interval is less than 0");
                else
                {
                    numberSpacing = value; Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control, this property is not relevant for this control.
        /// </summary>
        /// <value>The color of the back.</value>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(true)]
        [Description("Set the background color of the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Peru")]
        public new Color[] BackColor
        {
            get { return backColor; }
            set { backColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        [Browsable(true)]
        [Description("Gets or sets the width of the border.")]
        [Category("Appearance")]
        [DefaultValue(defaultBorderWidth)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if ((value < 0) || value > 10)
                {
                    Debug.Assert(false, "Value must be between 0 and 10");
                    value = defaultBorderWidth;
                }
                borderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow.
        /// </summary>
        /// <value>The color of the arrow.</value>
        [Browsable(true)]
        [Description("Gets or sets the color of the arrow.")]
        [Category("Appearance")]
        [Localizable(true)]
        public Color[] ArrowColor
        {
            get { return colorArrow; }
            set { colorArrow = value; Invalidate(); }
        }

        /// <summary>
        /// Set to true if the layout should be clockwise.
        /// </summary>
        /// <value>true if Clockwise</value>
        [Browsable(true)]
        [Description("Set to true if the layout should be clockwise.")]
        [Category("Layout")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool ClockWise
        {
            get { return clockWise; }
            set { clockWise = value; Invalidate(); }
        }

        /// <summary>
        /// Number of bars between the numbers.
        /// </summary>
        /// <value>true if Clockwise</value>
        [Browsable(true)]
        [Description("Number of bars between the numbers.")]
        [Category("Layout")]
        [DefaultValue(defaultBarsBetweenNumbers)]
        public int BarsBetweenNumbers
        {
            get { return barsBetweenNumbers; }
            set
            {
                if (value >= 1 && value <= NumberSpacing)
                    barsBetweenNumbers = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw shadow].
        /// </summary>
        /// <value><c>true</c> if [draw shadow]; otherwise, <c>false</c>.</value>
        public bool DrawShadow
        {
            get { return drawShadow; }
            set { drawShadow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw background].
        /// </summary>
        /// <value><c>true</c> if [draw background]; otherwise, <c>false</c>.</value>
        public bool DrawBackground
        {
            get { return drawBackground; }
            set { drawBackground = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw border].
        /// </summary>
        /// <value><c>true</c> if [draw border]; otherwise, <c>false</c>.</value>
        public bool DrawBorder
        {
            get { return drawBorder; }
            set { drawBorder = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw inner shadow].
        /// </summary>
        /// <value><c>true</c> if [draw inner shadow]; otherwise, <c>false</c>.</value>
        public bool DrawInnerShadow
        {
            get { return drawInnerShadow; }
            set { drawInnerShadow = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw bars].
        /// </summary>
        /// <value><c>true</c> if [draw bars]; otherwise, <c>false</c>.</value>
        public bool DrawBars
        {
            get { return drawBars; }
            set { drawBars = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw numbers].
        /// </summary>
        /// <value><c>true</c> if [draw numbers]; otherwise, <c>false</c>.</value>
        public bool DrawNumbers
        {
            get { return drawNumbers; }
            set { drawNumbers = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw text].
        /// </summary>
        /// <value><c>true</c> if [draw text]; otherwise, <c>false</c>.</value>
        public bool DrawText
        {
            get { return drawText; }
            set { drawText = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw arrows].
        /// </summary>
        /// <value><c>true</c> if [draw arrows]; otherwise, <c>false</c>.</value>
        public bool DrawArrows
        {
            get { return drawArrows; }
            set { drawArrows = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw glass].
        /// </summary>
        /// <value><c>true</c> if [draw glass]; otherwise, <c>false</c>.</value>
        public bool DrawGlass
        {
            get { return drawReflex; }
            set { drawReflex = value; Invalidate(); }
        }


        /// <summary>
        /// The border over lay
        /// </summary>
        private Color[] borderOverLay = new Color[]
        {
            Color.White,
            Color.FromArgb(200, Color.White)
        };
        /// <summary>
        /// Gets or sets the border over lay.
        /// </summary>
        /// <value>The border over lay.</value>
        public Color[] BorderOverLay
        {
            get { return borderOverLay;}
            set
            {
                borderOverLay = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The inner shadow color
        /// </summary>
        private Color[] innerShadowColor = new Color[]
        {
            Color.FromArgb(60, Color.Black),
            Color.FromArgb(30, Color.White)
        };

        /// <summary>
        /// Gets or sets the color of the inner shadow.
        /// </summary>
        /// <value>The color of the inner shadow.</value>
        public Color[] InnerShadowColor
        {
            get { return innerShadowColor; }
            set
            {
                innerShadowColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The reflex
        /// </summary>
        private Color[] reflex = new Color[] {Color.Transparent, Color.FromArgb(100, Color.White)};
        /// <summary>
        /// Gets or sets the color of the glass.
        /// </summary>
        /// <value>The color of the glass.</value>
        public Color[] GlassColor
        {
            get { return reflex; }
            set
            {
                reflex = value;
                Invalidate();
            }
        }

        #endregion

        #region -- Constructor --
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitThermometer" /> class.
        /// </summary>
        public ZeroitThermometer()
        {
            //Styles
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.ContainerControl, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            textureBrush = new TextureBrush(Properties.Resources.Reflection);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            Name = "Termometer";
            Max = defaultMax;
            Min = defaultMin;
            ClockWise = true;
            BarsBetweenNumbers = defaultBarsBetweenNumbers;
            BorderWidth = defaultBorderWidth;
            Decimals = defaultDecimals;
            NumberSpacing = defaultSpacing;
            StoreMax = true;
            StoredMax = defaultMin;
            Width = defaultWidth;
            Height = defaultHeight;
            TextUnit = "°C";
            Font = new Font("Calibri", defaultFontSize, GraphicsUnit.Point);
            CalcRectangles();
            Resize += new EventHandler(Termometer_Resize);
            base.BackColor = Color.Transparent;
        }

        #endregion

        #region -- EventHandlers --

        /// <summary>
        /// Handles the Resize event of the Termometer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Termometer_Resize(object sender, EventArgs e)
        {
            CalcRectangles();
        }

        #endregion

        #region -- Protected Overrides --

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            // Set smoothingmode to AntiAlias
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            
            // Shadow
            if(DrawShadow)
                PaintShadow(e.Graphics);
            // Background
            if (DrawBackground)
                PaintBackground(e.Graphics);
            // Border
            if (DrawBorder)
                PaintBorder(e.Graphics);
            // Inner shadow
            if (DrawInnerShadow)
                PaintInnerShadow(e.Graphics);
            // Bars
            if (DrawBars)
                PaintBars(e.Graphics);
            // Numbers
            if (DrawNumbers)
                PaintNumbers(e.Graphics);
            // Paint the text(s)
            if (DrawText)
                PaintText(e.Graphics);
            // Paint the Arrows
            if (DrawArrows)
                PaintArrows(e.Graphics);
            // Reflex
            if (DrawGlass)
                PaintReflex(e.Graphics);
            // Reset smoothingmode
            e.Graphics.SmoothingMode = SmoothingMode.Default;

            
        }

        #endregion

        #region -- Protected Methods --

        #region PaintShadow
        /// <summary>
        /// Paints the outer shadow.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintShadow(Graphics g)
        {
            if (shadowRect.IsEmpty) return; //break if nothing to draw
            using (Pen p = new Pen(Color.FromArgb(60, Color.Black), defaultOuterShadowWidth))
            {
                g.DrawEllipse(p, shadowRect);
            }
        }
        #endregion

        #region PaintBackground
        /// <summary>
        /// Paints the background.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBackground(Graphics g)
        {
            if (backgroundRect.IsEmpty) return; //break if nothing to draw
            using (Brush b = new LinearGradientBrush(backgroundRect,
                backColor[0], backColor[1], defaultLightingAngle)) //From gray to BackColor
            {
                g.FillEllipse(b, backgroundRect);
            }
        }
        #endregion

        #region PaintBorder
        /// <summary>
        /// Paints the border.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBorder(Graphics g)
        {
            // First draw a image to reflect
            RectangleF r = backgroundRect;
            r.Inflate(-BorderWidth / 2, -BorderWidth / 2);
            if (r.IsEmpty) return; //break if nothing to draw
            using (Pen texturePen = new Pen(textureBrush, BorderWidth))
            {
                g.DrawEllipse(texturePen, r);
            }

            // Gradient overlay
            using (Brush b = new LinearGradientBrush(backgroundRect, BorderOverLay[0],
                BorderOverLay[1], defaultLightingAngle))
            {
                using (Pen p = new Pen(b, BorderWidth))
                {
                    g.DrawArc(p, r, defaultLightingAngle - 90, -180); // Upper half of ellipse
                }
            }
        }
        #endregion

        #region PaintInnerShadow
        /// <summary>
        /// Paints the inner shadow.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintInnerShadow(Graphics g)
        {
            RectangleF r = backgroundRect;
            // Adjust for pen and border width
            r.Inflate(-(BorderWidth + defaultInnerShadowWidth / 2),
                -(BorderWidth + defaultInnerShadowWidth / 2));
            if (r.IsEmpty) return; // Break if nothing to draw
            Brush b = new LinearGradientBrush(backgroundRect,
                InnerShadowColor[0], InnerShadowColor[1],defaultLightingAngle);
            using (Pen p = new Pen(b, defaultInnerShadowWidth))
            {
                g.DrawEllipse(p, r);
            }
            b.Dispose();
        }
        #endregion

        #region PaintNumbers
        /// <summary>
        /// Paints the numbers.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintNumbers(Graphics g)
        {
            double tmpAngle = StartAngle;
            for (double d = Min; d <= Max; d += Interval)
            {
                String text = Math.Round(d, Decimals).ToString();
                PointF p = CalcTextPosition(tmpAngle, MeasureText(g, text, Font, (int)numberRect.Width));
                if (ClockWise)
                    tmpAngle -= NumberSpacing;
                else
                    tmpAngle += NumberSpacing;
                using (Brush b = new SolidBrush(ForeColor))
                {
                    g.DrawString(text, Font, b, p);
                }
            }
        }
        #endregion

        #region PaintBars
        /// <summary>
        /// Paints the bars.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBars(Graphics g)
        {
            double tmpAngle = StartAngle;
            for (double d = Min; d <= Max; d += Interval)
            {
                PaintBar(g, bar2Rect, bar3Rect, tmpAngle, defaultBarWidth, barColor);
                if (ClockWise)
                    tmpAngle -= NumberSpacing;
                else
                    tmpAngle += NumberSpacing;
            }
            if (ClockWise)
            {
                for (double d = tmpAngle + NumberSpacing; d <= StartAngle; d += numberSpacing / BarsBetweenNumbers)
                    PaintBar(g, bar1Rect, bar2Rect, d, .5f, barColor);
            }
            else
            {
                for (double d = StartAngle; d <= tmpAngle - NumberSpacing; d += numberSpacing / BarsBetweenNumbers)
                    PaintBar(g, bar1Rect, bar2Rect, d, .5f, barColor);
            }
        }
        #endregion

        #region PaintArrows
        /// <summary>
        /// Paints the arrows.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintArrows(Graphics g)
        {
            //If the Max value is displayed using the red arrow
            if (StoreMax)
                DrawArrow(g, StoredMax, ArrowColor[0]);
            //Arrow
            DrawArrow(g, Value, ArrowColor[1]);
        }
        #endregion

        #region PaintText
        /// <summary>
        /// Paint the text properties TextUnit and TextDescription
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintText(Graphics g)
        {
            PointF center = new PointF(numberRect.Width / 2 + numberRect.X, numberRect.Height / 2 + numberRect.Y);
            if (TextUnit.Length > 0)
            {
                using (Font font = new Font(Font.FontFamily, Font.Size + 4, FontStyle.Bold))
                {
                    SizeF size = MeasureText(g, TextUnit, font, (int)numberRect.Width);
                    PointF p = new PointF(center.X - size.Width / 2, center.Y + numberRect.Height / 8);
                    g.DrawString(TextUnit, font, new SolidBrush(ForeColor), p);
                }
            }
            if (TextDescription.Length > 0)
            {
                SizeF size = MeasureText(g, TextDescription, Font, (int)numberRect.Width);
                PointF p = new PointF(center.X - size.Width / 2, center.Y + numberRect.Height / 4);
                g.DrawString(TextDescription, Font, new SolidBrush(ForeColor), p);
            }
        }

        #endregion

        #region PaintReflex
        /// <summary>
        /// Paint the reflex on top
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintReflex(Graphics g)
        {
            if (backgroundRect.IsEmpty) return; //break if nothing to draw
            using (Brush b = new LinearGradientBrush(backgroundRect, GlassColor[0], GlassColor[1], defaultLightingAngle))
            {
                GraphicsPath path = new GraphicsPath();
                RectangleF r = backgroundRect;
                r.Inflate(-borderWidth, -borderWidth);
                if (r.IsEmpty) return; //break if noting to draw
                path.AddArc(r, 0, -180);
                r.Height /= 2;
                r.Offset(0, r.Height);
                r.Height /= 8;
                path.AddArc(r, 180, -180);
                g.FillPath(b, path);
                path.Dispose();
            }
        }
        #endregion

        #endregion Protected Methods End

        #region -- Private Methods --

        #region DrawArrow
        /// <summary>
        /// Draws the arrow from 3 points
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="v">The value between Min and Max</param>
        /// <param name="c">The arrow color</param>
        private void DrawArrow(Graphics g, double v, Color c)
        {
            PointF p1, p2, p3;
            //Make v relative to Min
            v -= Min;
            double angleValue = (v / Interval) * NumberSpacing;
            if (ClockWise)
            {
                p1 = PointInEllipse(arrow1Rect, StartAngle - angleValue);
                p2 = PointInEllipse(arrow2Rect, StartAngle - angleValue - 170);
                p3 = PointInEllipse(arrow2Rect, StartAngle - angleValue - 190);
            }
            else
            {
                p1 = PointInEllipse(arrow1Rect, StartAngle + angleValue);
                p2 = PointInEllipse(arrow2Rect, StartAngle + angleValue - 170);
                p3 = PointInEllipse(arrow2Rect, StartAngle + angleValue - 190);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            //Fill the arrow
            using (Brush b = new SolidBrush(c))
            {
                g.FillPath(b, path);
            }
            path.Dispose();
        }
        #endregion

        #region PaintBar
        /// <summary>
        /// Paint a single bar
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="outerRect">The outer rectangle</param>
        /// <param name="innerRect">The inner rectangle</param>
        /// <param name="a">The angle from the</param>
        /// <param name="width">The width of the pen</param>
        /// <param name="c">The color of the bar</param>
        private static void PaintBar(Graphics g, RectangleF outerRect, RectangleF innerRect, double a, float width, Color c)
        {
            using (Pen pen = new Pen(c, width))
            {
                PointF p1 = PointInEllipse(innerRect, a);
                PointF p2 = PointInEllipse(outerRect, a);
                g.DrawLine(pen, p1, p2);
            }
        }
        #endregion

        #region MeasureText
        /// <summary>
        /// Measures the text size
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="text">The text to size up</param>
        /// <param name="f">The font</param>
        /// <param name="maxWidth">Max width of the text</param>
        /// <returns>The size of the text</returns>
        private static SizeF MeasureText(Graphics g, string text, Font f, int maxWidth)
        {
            //Get the size of the text
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            sf.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox;
            sf.Trimming = StringTrimming.None;
            SizeF size = g.MeasureString(text, f, maxWidth, sf);
            return size;
        }
        #endregion

        #region CalcTextPosition
        /// <summary>
        /// Calcs the position af the text based on the angle in the ellipse
        /// </summary>
        /// <param name="a">The angle</param>
        /// <param name="size">The size of the text to place</param>
        /// <returns>Calculated position as PointF</returns>
        private PointF CalcTextPosition(double a, SizeF size)
        {
            PointF p = PointInEllipse(numberRect, a);
            p.X -= (float)((size.Width / 2) * (1 + Math.Cos(Convert.ToRadians(a))));
            p.Y -= (float)((size.Height / 2) * (1 - Math.Sin(Convert.ToRadians(a))));
            return p;
        }
        #endregion

        #region PointInEllipse
        /// <summary>
        /// Return a point in an ellipse.
        /// </summary>
        /// <param name="rect">The rectectangle around the ellipse</param>
        /// <param name="angle">The angle.</param>
        /// <returns>PointF in the specified ellipse</returns>
        private static PointF PointInEllipse(RectangleF rect, double angle)
        {
            double r1 = rect.Width / 2;
            double r2 = rect.Height / 2;
            double x = (float)(r1 * Math.Cos(Convert.ToRadians(angle))) + r1 + rect.X;
            double y = -(float)(r2 * Math.Sin(Convert.ToRadians(angle))) + r2 + rect.Y;
            return new PointF((float)x, (float)y);
        }
        #endregion

        #region CalcRectangles
        /// <summary>
        /// Calc most rectangles used in the design
        /// Called on the Resize event.
        /// </summary>
        private void CalcRectangles()
        {
            //ShadowRectangle
            shadowRect = ClientRectangle;
            shadowRect.Inflate(-1, -1);
            //Reducing width and height of shadow to avoid clipping
            shadowRect.Width -= 1;
            shadowRect.Height -= 1;
            //Background Rectangle
            backgroundRect = shadowRect;
            backgroundRect.Inflate(.5f, .5f);
            backgroundRect.Offset(-1, -1);
            numberRect = backgroundRect;
            numberRect.Inflate(-(numberMargin + Font.Size), -(numberMargin + Font.Size));
            //The rectangle for the bars
            bar1Rect = backgroundRect;
            bar1Rect.Inflate(-(borderWidth + barOuterMargin), -(borderWidth + barOuterMargin));
            bar2Rect = numberRect;
            bar2Rect.Inflate(barInnerMargin + defaultBarHeight, barInnerMargin + defaultBarHeight);
            bar3Rect = numberRect;
            bar3Rect.Inflate(barInnerMargin, barInnerMargin);
            //Arrow Rectangles
            arrow1Rect = numberRect;
            int infl = barInnerMargin + defaultBarHeight * 2;
            arrow1Rect.Inflate(infl, infl);
            arrow2Rect = numberRect;
            arrow2Rect.Inflate(-numberRect.Width / 6, -numberRect.Width / 6);
        }
        #endregion

        #endregion

        #region * Internal Class Convert *
        /// <summary>
        /// Sealed class Convert
        /// </summary>
        internal static class Convert
        {
            /// <summary>
            /// Convert degrees to radians.
            /// </summary>
            /// <param name="degrees">The degrees.</param>
            /// <returns>Radians</returns>
            public static double ToRadians(double degrees)
            {
                double radians = (Math.PI / 180) * degrees;
                return (radians);
            }

            /// <summary>
            /// Convert radians to degrees
            /// </summary>
            /// <param name="radians">The radians.</param>
            /// <returns>Degrees</returns>
            public static double ToDegrees(double radians)
            {
                double degrees = (radians * 180) / Math.PI;
                return degrees;
            }
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
    }

    #endregion

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitThermometerDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitThermometerDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitThermometerDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitThermometerSmartTagActionList(this.Component));
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
    /// Class ZeroitThermometerSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitThermometerSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitThermometer colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitThermometerSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitThermometerSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitThermometer;

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
        public Color[] BackColor
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
        /// Gets or sets the color of the arrow.
        /// </summary>
        /// <value>The color of the arrow.</value>
        public Color[] ArrowColor
        {
            get
            {
                return colUserControl.ArrowColor;
            }
            set
            {
                GetPropertyByName("ArrowColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [clock wise].
        /// </summary>
        /// <value><c>true</c> if [clock wise]; otherwise, <c>false</c>.</value>
        public bool ClockWise
        {
            get
            {
                return colUserControl.ClockWise;
            }
            set
            {
                GetPropertyByName("ClockWise").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [store maximum].
        /// </summary>
        /// <value><c>true</c> if [store maximum]; otherwise, <c>false</c>.</value>
        public bool StoreMax
        {
            get
            {
                return colUserControl.StoreMax;
            }
            set
            {
                GetPropertyByName("StoreMax").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the bars between numbers.
        /// </summary>
        /// <value>The bars between numbers.</value>
        public int BarsBetweenNumbers
        {
            get
            {
                return colUserControl.BarsBetweenNumbers;
            }
            set
            {
                GetPropertyByName("BarsBetweenNumbers").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public float Max
        {
            get
            {
                return colUserControl.Max;
            }
            set
            {
                GetPropertyByName("Max").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public float Min
        {
            get
            {
                return colUserControl.Min;
            }
            set
            {
                GetPropertyByName("Min").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the decimals.
        /// </summary>
        /// <value>The decimals.</value>
        public int Decimals
        {
            get
            {
                return colUserControl.Decimals;
            }
            set
            {
                GetPropertyByName("Decimals").SetValue(colUserControl, value);
            }

        }

        /// <summary>
        /// Gets or sets the number spacing.
        /// </summary>
        /// <value>The number spacing.</value>
        public int NumberSpacing
        {
            get
            {
                return colUserControl.NumberSpacing;
            }
            set
            {
                GetPropertyByName("NumberSpacing").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public int BorderWidth
        {
            get
            {
                return colUserControl.BorderWidth;
            }
            set
            {
                GetPropertyByName("BorderWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public float Interval
        {
            get
            {
                return colUserControl.Interval;
            }
            set
            {
                GetPropertyByName("Interval").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        /// <value>The start angle.</value>
        public int StartAngle
        {
            get
            {
                return colUserControl.StartAngle;
            }
            set
            {
                GetPropertyByName("StartAngle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the stored maximum.
        /// </summary>
        /// <value>The stored maximum.</value>
        public float StoredMax
        {
            get
            {
                return colUserControl.StoredMax;
            }
            set
            {
                GetPropertyByName("StoredMax").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get
            {
                return colUserControl.Value;
            }
            set
            {
                GetPropertyByName("Value").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text unit.
        /// </summary>
        /// <value>The text unit.</value>
        public string TextUnit
        {
            get
            {
                return colUserControl.TextUnit;
            }
            set
            {
                GetPropertyByName("TextUnit").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text description.
        /// </summary>
        /// <value>The text description.</value>
        public string TextDescription
        {
            get
            {
                return colUserControl.TextDescription;
            }
            set
            {
                GetPropertyByName("TextDescription").SetValue(colUserControl, value);
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether [draw shadow].
        /// </summary>
        /// <value><c>true</c> if [draw shadow]; otherwise, <c>false</c>.</value>
        public bool DrawShadow
        {
            get
            {
                return colUserControl.DrawShadow;
            }
            set
            {
                GetPropertyByName("DrawShadow").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw background].
        /// </summary>
        /// <value><c>true</c> if [draw background]; otherwise, <c>false</c>.</value>
        public bool DrawBackground
        {
            get
            {
                return colUserControl.DrawBackground;
            }
            set
            {
                GetPropertyByName("DrawBackground").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw border].
        /// </summary>
        /// <value><c>true</c> if [draw border]; otherwise, <c>false</c>.</value>
        public bool DrawBorder
        {
            get
            {
                return colUserControl.DrawBorder;
            }
            set
            {
                GetPropertyByName("DrawBorder").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw inner shadow].
        /// </summary>
        /// <value><c>true</c> if [draw inner shadow]; otherwise, <c>false</c>.</value>
        public bool DrawInnerShadow
        {
            get
            {
                return colUserControl.DrawInnerShadow;
            }
            set
            {
                GetPropertyByName("DrawInnerShadow").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw bars].
        /// </summary>
        /// <value><c>true</c> if [draw bars]; otherwise, <c>false</c>.</value>
        public bool DrawBars
        {
            get
            {
                return colUserControl.DrawBars;
            }
            set
            {
                GetPropertyByName("DrawBars").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw numbers].
        /// </summary>
        /// <value><c>true</c> if [draw numbers]; otherwise, <c>false</c>.</value>
        public bool DrawNumbers
        {
            get
            {
                return colUserControl.DrawNumbers;
            }
            set
            {
                GetPropertyByName("DrawNumbers").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw text].
        /// </summary>
        /// <value><c>true</c> if [draw text]; otherwise, <c>false</c>.</value>
        public bool DrawText
        {
            get
            {
                return colUserControl.DrawText;
            }
            set
            {
                GetPropertyByName("DrawText").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw arrows].
        /// </summary>
        /// <value><c>true</c> if [draw arrows]; otherwise, <c>false</c>.</value>
        public bool DrawArrows
        {
            get
            {
                return colUserControl.DrawArrows;
            }
            set
            {
                GetPropertyByName("DrawArrows").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw glass].
        /// </summary>
        /// <value><c>true</c> if [draw glass]; otherwise, <c>false</c>.</value>
        public bool DrawGlass
        {
            get
            {
                return colUserControl.DrawGlass;
            }
            set
            {
                GetPropertyByName("DrawGlass").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the glass.
        /// </summary>
        /// <value>The color of the glass.</value>
        public Color[] GlassColor
        {
            get
            {
                return colUserControl.GlassColor;
            }
            set
            {
                GetPropertyByName("GlassColor").SetValue(colUserControl, value);
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
            items.Add(new DesignerActionHeaderItem("Behaviour"));

            items.Add(new DesignerActionPropertyItem("DrawShadow",
                "Draw Shadow", "Behaviour",
                "Set to draw shadow."));

            items.Add(new DesignerActionPropertyItem("DrawBackground",
                "Draw Background", "Behaviour",
                "Set to draw background."));

            items.Add(new DesignerActionPropertyItem("DrawBorder",
                "Draw Border", "Behaviour",
                "Set to draw border."));

            items.Add(new DesignerActionPropertyItem("DrawInnerShadow",
                "Draw Inner Shadow", "Behaviour",
                "Set to draw inner shadow."));

            items.Add(new DesignerActionPropertyItem("DrawBars",
                "Draw Bars", "Behaviour",
                "Set to draw bars."));

            items.Add(new DesignerActionPropertyItem("DrawNumbers",
                "Draw Numbers", "Behaviour",
                "Set to draw numbers."));

            items.Add(new DesignerActionPropertyItem("DrawText",
                "Draw Text", "Behaviour",
                "Set to draw text."));

            items.Add(new DesignerActionPropertyItem("DrawArrows",
                "Draw Arrows", "Behaviour",
                "Set to draw arrows."));

            items.Add(new DesignerActionPropertyItem("DrawGlass",
                "Draw Reflex", "Behaviour",
                "Set to draw reflex."));

            items.Add(new DesignerActionPropertyItem("ClockWise",
                "Clock Wise", "Behaviour",
                "Set to enable clock-wise mode."));
            
            items.Add(new DesignerActionPropertyItem("StoreMax",
                "Store Max", "Behaviour",
                "Set to enable max storage."));

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("ArrowColor",
                                 "Arrow Color", "Appearance",
                                 "Sets the arrow color."));

            items.Add(new DesignerActionPropertyItem("GlassColor",
                "Glass Color", "Appearance",
                "Sets the glass color."));

            items.Add(new DesignerActionPropertyItem("BarsBetweenNumbers",
                "Bars", "Appearance",
                "Sets the bars between the numbers."));

            items.Add(new DesignerActionPropertyItem("Max",
                "Max", "Appearance",
                "Sets the maximum value."));

            items.Add(new DesignerActionPropertyItem("Min",
                "Min", "Appearance",
                "Sets the minimum value."));

            items.Add(new DesignerActionPropertyItem("Decimals",
                "Decimals", "Appearance",
                "Sets the decimals."));

            items.Add(new DesignerActionPropertyItem("NumberSpacing",
                "Number Spacing", "Appearance",
                "Sets the number spacing."));

            items.Add(new DesignerActionPropertyItem("BorderWidth",
                "Border Width", "Appearance",
                "Sets the border width."));

            items.Add(new DesignerActionPropertyItem("Interval",
                "Interval", "Appearance",
                "Sets the interval."));

            items.Add(new DesignerActionPropertyItem("StartAngle",
                "Start Angle", "Appearance",
                "Sets the start angle."));

            items.Add(new DesignerActionPropertyItem("StoredMax",
                "Stored Max", "Appearance",
                "Sets the stored max."));

            items.Add(new DesignerActionPropertyItem("Value",
                "Value", "Appearance",
                "Sets the value."));

            items.Add(new DesignerActionPropertyItem("TextUnit",
                "Text Unit", "Appearance",
                "Sets the text unit."));

            items.Add(new DesignerActionPropertyItem("TextDescription",
                "Text Description", "Appearance",
                "Sets the text description."));
            

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
