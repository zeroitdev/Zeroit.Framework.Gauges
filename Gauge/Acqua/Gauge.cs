// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-28-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Gauge.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region Zeroit Gauge

    #region AGauge    
    /// <summary>
    /// A class collection for rendering a gauge.
    /// <para>Displays a value on an analog gauge. Raises an event if the value enters one of the definable ranges.</para>
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ToolboxBitmapAttribute(typeof(ZeroitGaugeAll), "AGauge.bmp"),
    DefaultEvent("ValueInRangeChanged"),
    Description("Displays a value on an analog gauge. Raises an event if the value enters one of the definable ranges.")]
    public partial class ZeroitGaugeAll : Control
    {
        #region enum, var, delegate, event

        /// <summary>
        /// Enum representing the needle color
        /// </summary>
        public enum NeedleColor
        {
            /// <summary>
            /// The gray
            /// </summary>
            Gray = 0,
            /// <summary>
            /// The red
            /// </summary>
            Red = 1,
            /// <summary>
            /// The green
            /// </summary>
            Green = 2,
            /// <summary>
            /// The blue
            /// </summary>
            Blue = 3,
            /// <summary>
            /// The yellow
            /// </summary>
            Yellow = 4,
            /// <summary>
            /// The violet
            /// </summary>
            Violet = 5,
            /// <summary>
            /// The magenta
            /// </summary>
            Magenta = 6
        };

        /// <summary>
        /// The zero
        /// </summary>
        private const Byte ZERO = 0;
        /// <summary>
        /// The numofcaps
        /// </summary>
        private const Byte NUMOFCAPS = 5;
        /// <summary>
        /// The numofranges
        /// </summary>
        private const Byte NUMOFRANGES = 5;

        /// <summary>
        /// The font bound y1
        /// </summary>
        private Single fontBoundY1;
        /// <summary>
        /// The font bound y2
        /// </summary>
        private Single fontBoundY2;
        /// <summary>
        /// The gauge bitmap
        /// </summary>
        private Bitmap gaugeBitmap;
        /// <summary>
        /// The draw gauge background
        /// </summary>
        private bool drawGaugeBackground = true;

        /// <summary>
        /// The m value
        /// </summary>
        private Single m_value;
        /// <summary>
        /// The m value is in range
        /// </summary>
        private bool[] m_valueIsInRange = { false, false, false, false, false };
        /// <summary>
        /// The m cap index
        /// </summary>
        private Byte m_CapIdx = 1;
        /// <summary>
        /// The m cap color
        /// </summary>
        private Color[] m_CapColor = { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black };
        /// <summary>
        /// The m cap text
        /// </summary>
        private String[] m_CapText = { "", "", "", "", "" };
        /// <summary>
        /// The m cap position
        /// </summary>
        private Point[] m_CapPosition = { new Point(10, 10), new Point(10, 10), new Point(10, 10), new Point(10, 10), new Point(10, 10) };
        /// <summary>
        /// The m center
        /// </summary>
        private Point m_Center = new Point(100, 100);
        /// <summary>
        /// The m minimum value
        /// </summary>
        private Single m_MinValue = -100;
        /// <summary>
        /// The m maximum value
        /// </summary>
        private Single m_MaxValue = 400;

        /// <summary>
        /// The m base arc color
        /// </summary>
        private Color m_BaseArcColor = Color.Gray;
        /// <summary>
        /// The m base arc radius
        /// </summary>
        private Int32 m_BaseArcRadius = 80;
        /// <summary>
        /// The m base arc start
        /// </summary>
        private Int32 m_BaseArcStart = 135;
        /// <summary>
        /// The m base arc sweep
        /// </summary>
        private Int32 m_BaseArcSweep = 270;
        /// <summary>
        /// The m base arc width
        /// </summary>
        private Int32 m_BaseArcWidth = 2;

        /// <summary>
        /// The m scale lines inter color
        /// </summary>
        private Color m_ScaleLinesInterColor = Color.Black;
        /// <summary>
        /// The m scale lines inter inner radius
        /// </summary>
        private Int32 m_ScaleLinesInterInnerRadius = 73;
        /// <summary>
        /// The m scale lines inter outer radius
        /// </summary>
        private Int32 m_ScaleLinesInterOuterRadius = 80;
        /// <summary>
        /// The m scale lines inter width
        /// </summary>
        private Int32 m_ScaleLinesInterWidth = 1;

        /// <summary>
        /// The m scale lines minor number of
        /// </summary>
        private Int32 m_ScaleLinesMinorNumOf = 9;
        /// <summary>
        /// The m scale lines minor color
        /// </summary>
        private Color m_ScaleLinesMinorColor = Color.Gray;
        /// <summary>
        /// The m scale lines minor inner radius
        /// </summary>
        private Int32 m_ScaleLinesMinorInnerRadius = 75;
        /// <summary>
        /// The m scale lines minor outer radius
        /// </summary>
        private Int32 m_ScaleLinesMinorOuterRadius = 80;
        /// <summary>
        /// The m scale lines minor width
        /// </summary>
        private Int32 m_ScaleLinesMinorWidth = 1;

        /// <summary>
        /// The m scale lines major step value
        /// </summary>
        private Single m_ScaleLinesMajorStepValue = 50.0f;
        /// <summary>
        /// The m scale lines major color
        /// </summary>
        private Color m_ScaleLinesMajorColor = Color.Black;
        /// <summary>
        /// The m scale lines major inner radius
        /// </summary>
        private Int32 m_ScaleLinesMajorInnerRadius = 70;
        /// <summary>
        /// The m scale lines major outer radius
        /// </summary>
        private Int32 m_ScaleLinesMajorOuterRadius = 80;
        /// <summary>
        /// The m scale lines major width
        /// </summary>
        private Int32 m_ScaleLinesMajorWidth = 2;

        /// <summary>
        /// The m range index
        /// </summary>
        private Byte m_RangeIdx;
        /// <summary>
        /// The m range enabled
        /// </summary>
        private bool[] m_RangeEnabled = { true, true, false, false, false };
        /// <summary>
        /// The m range color
        /// </summary>
        private Color[] m_RangeColor = { Color.LightGreen, Color.Red, Color.FromKnownColor(KnownColor.Control), Color.FromKnownColor(KnownColor.Control), Color.FromKnownColor(KnownColor.Control) };
        /// <summary>
        /// The m range start value
        /// </summary>
        private Single[] m_RangeStartValue = { -100.0f, 300.0f, 0.0f, 0.0f, 0.0f };
        /// <summary>
        /// The m range end value
        /// </summary>
        private Single[] m_RangeEndValue = { 300.0f, 400.0f, 0.0f, 0.0f, 0.0f };
        /// <summary>
        /// The m range inner radius
        /// </summary>
        private Int32[] m_RangeInnerRadius = { 70, 70, 70, 70, 70 };
        /// <summary>
        /// The m range outer radius
        /// </summary>
        private Int32[] m_RangeOuterRadius = { 80, 80, 80, 80, 80 };

        /// <summary>
        /// The m scale numbers radius
        /// </summary>
        private Int32 m_ScaleNumbersRadius = 95;
        /// <summary>
        /// The m scale numbers color
        /// </summary>
        private Color m_ScaleNumbersColor = Color.Black;
        /// <summary>
        /// The m scale numbers format
        /// </summary>
        private String m_ScaleNumbersFormat;
        /// <summary>
        /// The m scale numbers start scale line
        /// </summary>
        private Int32 m_ScaleNumbersStartScaleLine;
        /// <summary>
        /// The m scale numbers step scale lines
        /// </summary>
        private Int32 m_ScaleNumbersStepScaleLines = 1;
        /// <summary>
        /// The m scale numbers rotation
        /// </summary>
        private Int32 m_ScaleNumbersRotation = 0;

        /// <summary>
        /// The m needle type
        /// </summary>
        private Int32 m_NeedleType = 0;
        /// <summary>
        /// The m needle radius
        /// </summary>
        private Int32 m_NeedleRadius = 80;
        /// <summary>
        /// The m needle color1
        /// </summary>
        private NeedleColor m_NeedleColor1 = NeedleColor.Gray;
        /// <summary>
        /// The m needle color2
        /// </summary>
        private Color m_NeedleColor2 = Color.DimGray;
        /// <summary>
        /// The m needle width
        /// </summary>
        private Int32 m_NeedleWidth = 2;

        /// <summary>
        /// Class ValueInRangeChangedEventArgs.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class ValueInRangeChangedEventArgs : EventArgs
        {
            /// <summary>
            /// The value in range
            /// </summary>
            public Int32 valueInRange;

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueInRangeChangedEventArgs"/> class.
            /// </summary>
            /// <param name="valueInRange">The value in range.</param>
            public ValueInRangeChangedEventArgs(Int32 valueInRange)
            {
                this.valueInRange = valueInRange;
            }
        }

        /// <summary>
        /// Delegate ValueInRangeChangedDelegate
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ValueInRangeChangedEventArgs"/> instance containing the event data.</param>
        public delegate void ValueInRangeChangedDelegate(Object sender, ValueInRangeChangedEventArgs e);
        /// <summary>
        /// Occurs when [value in range changed].
        /// </summary>
        [Description("This event is raised if the value falls into a defined range.")]
        public event ValueInRangeChangedDelegate ValueInRangeChanged;
        #endregion

        #region hidden , overridden inherited properties        
        /// <summary>
        /// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
        /// </summary>
        /// <value>The allow drop.</value>
        public new bool AllowDrop
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control should be automatically sized.
        /// </summary>
        /// <value><c>true</c> if automatic size; otherwise, <c>false</c>.</value>
        public new bool AutoSize
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value><c>true</c> if fore color; otherwise, <c>false</c>.</value>
        public new bool ForeColor
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Input Method Editor (IME) mode of the control.
        /// </summary>
        /// <value><c>true</c> if [IME mode]; otherwise, <c>false</c>.</value>
        public new bool ImeMode
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                drawGaugeBackground = true;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                drawGaugeBackground = true;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.
        /// </summary>
        /// <value>The background image layout.</value>
        public override System.Windows.Forms.ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
                drawGaugeBackground = true;
                Refresh();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitGaugeAll" /> class.
        /// </summary>
        public ZeroitGaugeAll()
        {
            InitializeComponent();


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);


            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);



        }
        #endregion

        #region properties        
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The value.")]
        public Single Value
        {
            get
            {
                return m_value;
            }
            set
            {
                if (m_value != value)
                {
                    m_value = Math.Min(Math.Max(value, m_MinValue), m_MaxValue);

                    if (this.DesignMode)
                    {
                        drawGaugeBackground = true;
                    }

                    for (Int32 counter = 0; counter < NUMOFRANGES - 1; counter++)
                    {
                        if ((m_RangeStartValue[counter] <= m_value)
                        && (m_value <= m_RangeEndValue[counter])
                        && (m_RangeEnabled[counter]))
                        {
                            if (!m_valueIsInRange[counter])
                            {
                                if (ValueInRangeChanged != null)
                                {
                                    ValueInRangeChanged(this, new ValueInRangeChangedEventArgs(counter));
                                }
                            }
                        }
                        else
                        {
                            m_valueIsInRange[counter] = false;
                        }
                    }
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the cap index. Set this to a value of 0 up to 4 to change the corresponding caption's properties
        /// </summary>
        /// <value>The index of the cap.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.RefreshProperties(RefreshProperties.All),
        System.ComponentModel.Description("The caption index. set this to a value of 0 up to 4 to change the corresponding caption's properties.")]
        public Byte CapIndex
        {
            get
            {
                return m_CapIdx;
            }
            set
            {
                if ((m_CapIdx != value)
                && (0 <= value)
                && (value < 5))
                {
                    m_CapIdx = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the caption text.
        /// </summary>
        /// <value>The color of the caption text.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the caption text.")]
        public Color CapColor
        {
            get
            {
                return m_CapColor[m_CapIdx];
            }
            set
            {
                if (m_CapColor[m_CapIdx] != value)
                {
                    m_CapColor[m_CapIdx] = value;
                    CapColors = m_CapColor;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption colors.
        /// </summary>
        /// <value>The caption colors.</value>
        [System.ComponentModel.Browsable(false)]
        public Color[] CapColors
        {
            get
            {
                return m_CapColor;
            }
            set
            {
                m_CapColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption text.
        /// </summary>
        /// <value>The caption text.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The text of the caption.")]
        public String CapText
        {
            get
            {
                return m_CapText[m_CapIdx];
            }
            set
            {
                if (m_CapText[m_CapIdx] != value)
                {
                    m_CapText[m_CapIdx] = value;
                    CapsText = m_CapText;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }


        /// <summary>
        /// Gets or sets the caps text.
        /// </summary>
        /// <value>The caps text.</value>
        [System.ComponentModel.Browsable(false)]
        public String[] CapsText
        {
            get
            {
                return m_CapText;
            }
            set
            {
                for (Int32 counter = 0; counter < 5; counter++)
                {
                    m_CapText[counter] = value[counter];
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption position.
        /// </summary>
        /// <value>The caption position.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The position of the caption.")]
        public Point CapPosition
        {
            get
            {
                return m_CapPosition[m_CapIdx];
            }
            set
            {
                if (m_CapPosition[m_CapIdx] != value)
                {
                    m_CapPosition[m_CapIdx] = value;
                    CapsPosition = m_CapPosition;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the caps position.
        /// </summary>
        /// <value>The caps position.</value>
        [System.ComponentModel.Browsable(false)]
        public Point[] CapsPosition
        {
            get
            {
                return m_CapPosition;
            }
            set
            {
                m_CapPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the The center of the gauge (in the control's client area).
        /// </summary>
        /// <value>The center.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The center of the gauge (in the control's client area).")]
        public Point Center
        {
            get
            {
                return m_Center;
            }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The minimum value to show on the scale.")]
        public Single MinValue
        {
            get
            {
                return m_MinValue;
            }
            set
            {
                if ((m_MinValue != value)
                && (value < m_MaxValue))
                {
                    m_MinValue = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The maximum value to show on the scale.")]
        public Single MaxValue
        {
            get
            {
                return m_MaxValue;
            }
            set
            {
                if ((m_MaxValue != value)
                && (value > m_MinValue))
                {
                    m_MaxValue = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the base arc.
        /// </summary>
        /// <value>The color of the base arc.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the base arc.")]
        public Color BaseArcColor
        {
            get
            {
                return m_BaseArcColor;
            }
            set
            {
                if (m_BaseArcColor != value)
                {
                    m_BaseArcColor = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the base arc radius.
        /// </summary>
        /// <value>The base arc radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The radius of the base arc.")]
        public Int32 BaseArcRadius
        {
            get
            {
                return m_BaseArcRadius;
            }
            set
            {
                if (m_BaseArcRadius != value)
                {
                    m_BaseArcRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the base arc start.
        /// </summary>
        /// <value>The base arc start.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The start angle of the base arc.")]
        public Int32 BaseArcStart
        {
            get
            {
                return m_BaseArcStart;
            }
            set
            {
                if (m_BaseArcStart != value)
                {
                    m_BaseArcStart = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the base arc sweep.
        /// </summary>
        /// <value>The base arc sweep.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The sweep angle of the base arc.")]
        public Int32 BaseArcSweep
        {
            get
            {
                return m_BaseArcSweep;
            }
            set
            {
                if (m_BaseArcSweep != value)
                {
                    m_BaseArcSweep = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the base arc.
        /// </summary>
        /// <value>The width of the base arc.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The width of the base arc.")]
        public Int32 BaseArcWidth
        {
            get
            {
                return m_BaseArcWidth;
            }
            set
            {
                if (m_BaseArcWidth != value)
                {
                    m_BaseArcWidth = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the inter
        /// scale lines which are the middle scale lines
        /// for an uneven number of minor scale lines.
        /// </summary>
        /// <value>The color of the scale lines inter.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the inter scale lines which are the middle scale lines for an uneven number of minor scale lines.")]
        public Color ScaleLinesInterColor
        {
            get
            {
                return m_ScaleLinesInterColor;
            }
            set
            {
                if (m_ScaleLinesInterColor != value)
                {
                    m_ScaleLinesInterColor = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the inner radius of the inter scale
        /// lines which are the middle scale lines for an
        /// uneven number of minor scale lines.
        /// </summary>
        /// <value>The scale lines inter inner radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The inner radius of the inter scale lines which are the middle scale lines for an uneven number of minor scale lines.")]
        public Int32 ScaleLinesInterInnerRadius
        {
            get
            {
                return m_ScaleLinesInterInnerRadius;
            }
            set
            {
                if (m_ScaleLinesInterInnerRadius != value)
                {
                    m_ScaleLinesInterInnerRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the outer radius of the inter
        /// scale lines which are the middle scale lines
        /// for an uneven number of minor scale lines.
        /// </summary>
        /// <value>The scale lines inter outer radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The outer radius of the inter scale lines which are the middle scale lines for an uneven number of minor scale lines.")]
        public Int32 ScaleLinesInterOuterRadius
        {
            get
            {
                return m_ScaleLinesInterOuterRadius;
            }
            set
            {
                if (m_ScaleLinesInterOuterRadius != value)
                {
                    m_ScaleLinesInterOuterRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the inter scale lines
        /// which are the middle scale lines for an
        /// uneven number of minor scale lines.
        /// </summary>
        /// <value>The width of the scale lines inter.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The width of the inter scale lines which are the middle scale lines for an uneven number of minor scale lines.")]
        public Int32 ScaleLinesInterWidth
        {
            get
            {
                return m_ScaleLinesInterWidth;
            }
            set
            {
                if (m_ScaleLinesInterWidth != value)
                {
                    m_ScaleLinesInterWidth = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of minor scale lines.
        /// </summary>
        /// <value>The scale lines minor number of.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The number of minor scale lines.")]
        public Int32 ScaleLinesMinorNumOf
        {
            get
            {
                return m_ScaleLinesMinorNumOf;
            }
            set
            {
                if (m_ScaleLinesMinorNumOf != value)
                {
                    m_ScaleLinesMinorNumOf = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the minor scale lines.
        /// </summary>
        /// <value>The color of the minor scale lines.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the minor scale lines.")]
        public Color ScaleLinesMinorColor
        {
            get
            {
                return m_ScaleLinesMinorColor;
            }
            set
            {
                if (m_ScaleLinesMinorColor != value)
                {
                    m_ScaleLinesMinorColor = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the inner radius of the minor scale lines.
        /// </summary>
        /// <value>The scale lines minor inner radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The inner radius of the minor scale lines.")]
        public Int32 ScaleLinesMinorInnerRadius
        {
            get
            {
                return m_ScaleLinesMinorInnerRadius;
            }
            set
            {
                if (m_ScaleLinesMinorInnerRadius != value)
                {
                    m_ScaleLinesMinorInnerRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the outer radius of the minor scale lines.
        /// </summary>
        /// <value>The scale lines minor outer radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The outer radius of the minor scale lines.")]
        public Int32 ScaleLinesMinorOuterRadius
        {
            get
            {
                return m_ScaleLinesMinorOuterRadius;
            }
            set
            {
                if (m_ScaleLinesMinorOuterRadius != value)
                {
                    m_ScaleLinesMinorOuterRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the minor scale lines.
        /// </summary>
        /// <value>The width of the minor scale lines.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The width of the minor scale lines.")]
        public Int32 ScaleLinesMinorWidth
        {
            get
            {
                return m_ScaleLinesMinorWidth;
            }
            set
            {
                if (m_ScaleLinesMinorWidth != value)
                {
                    m_ScaleLinesMinorWidth = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the step value of the major scale lines.
        /// </summary>
        /// <value>The scale lines major step value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The step value of the major scale lines.")]
        public Single ScaleLinesMajorStepValue
        {
            get
            {
                return m_ScaleLinesMajorStepValue;
            }
            set
            {
                if ((m_ScaleLinesMajorStepValue != value) && (value > 0))
                {
                    m_ScaleLinesMajorStepValue = Math.Max(Math.Min(value, m_MaxValue), m_MinValue);
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the major scale lines.
        /// </summary>
        /// <value>The color of the scale lines major.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the major scale lines.")]
        public Color ScaleLinesMajorColor
        {
            get
            {
                return m_ScaleLinesMajorColor;
            }
            set
            {
                if (m_ScaleLinesMajorColor != value)
                {
                    m_ScaleLinesMajorColor = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the inner radius of the major scale lines.
        /// </summary>
        /// <value>The scale lines major inner radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The inner radius of the major scale lines.")]
        public Int32 ScaleLinesMajorInnerRadius
        {
            get
            {
                return m_ScaleLinesMajorInnerRadius;
            }
            set
            {
                if (m_ScaleLinesMajorInnerRadius != value)
                {
                    m_ScaleLinesMajorInnerRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the outer radius of the major scale lines.
        /// </summary>
        /// <value>The scale lines major outer radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The outer radius of the major scale lines.")]
        public Int32 ScaleLinesMajorOuterRadius
        {
            get
            {
                return m_ScaleLinesMajorOuterRadius;
            }
            set
            {
                if (m_ScaleLinesMajorOuterRadius != value)
                {
                    m_ScaleLinesMajorOuterRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the major scale lines.
        /// </summary>
        /// <value>The width of the scale lines major.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The width of the major scale lines.")]
        public Int32 ScaleLinesMajorWidth
        {
            get
            {
                return m_ScaleLinesMajorWidth;
            }
            set
            {
                if (m_ScaleLinesMajorWidth != value)
                {
                    m_ScaleLinesMajorWidth = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the range index.
        /// Set this to a value of 0 up to 4 to
        /// change the corresponding range's properties.
        /// </summary>
        /// <value>The index of the range.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.RefreshProperties(RefreshProperties.All),
        System.ComponentModel.Description("The range index. set this to a value of 0 up to 4 to change the corresponding range's properties.")]
        public Byte RangeIndex
        {
            get
            {
                return m_RangeIdx;
            }
            set
            {
                if ((m_RangeIdx != value)
                && (0 <= value)
                && (value < NUMOFRANGES))
                {
                    m_RangeIdx = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable or disable the range selected by RangeIndex.
        /// </summary>
        /// <value><c>true</c> if range enabled; otherwise, <c>false</c>.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("Enables or disables the range selected by RangeIndex.")]
        public bool RangeEnabled
        {
            get
            {
                return m_RangeEnabled[m_RangeIdx];
            }
            set
            {
                if (m_RangeEnabled[m_RangeIdx] != value)
                {
                    m_RangeEnabled[m_RangeIdx] = value;
                    RangesEnabled = m_RangeEnabled;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the ranges enabled.
        /// </summary>
        /// <value>The ranges enabled.</value>
        [System.ComponentModel.Browsable(false)]
        public bool[] RangesEnabled
        {
            get
            {
                return m_RangeEnabled;
            }
            set
            {
                m_RangeEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the range.
        /// </summary>
        /// <value>The color of the range.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the range.")]
        public Color RangeColor
        {
            get
            {
                return m_RangeColor[m_RangeIdx];
            }
            set
            {
                if (m_RangeColor[m_RangeIdx] != value)
                {
                    m_RangeColor[m_RangeIdx] = value;
                    RangesColor = m_RangeColor;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the ranges.
        /// </summary>
        /// <value>The color of the ranges.</value>
        [System.ComponentModel.Browsable(false)]
        public Color[] RangesColor
        {
            get
            {
                return m_RangeColor;
            }
            set
            {
                m_RangeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the range start value. Must be less than RangeEndValue
        /// </summary>
        /// <value>The range start value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The start value of the range, must be less than RangeEndValue.")]
        public Single RangeStartValue
        {
            get
            {
                return m_RangeStartValue[m_RangeIdx];
            }
            set
            {
                if ((m_RangeStartValue[m_RangeIdx] != value)
                && (value < m_RangeEndValue[m_RangeIdx]))
                {
                    m_RangeStartValue[m_RangeIdx] = value;
                    RangesStartValue = m_RangeStartValue;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the ranges start value.
        /// </summary>
        /// <value>The ranges start value.</value>
        [System.ComponentModel.Browsable(false)]
        public Single[] RangesStartValue
        {
            get
            {
                return m_RangeStartValue;
            }
            set
            {
                m_RangeStartValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the range end value. Must be greater than RangeStartValue.
        /// </summary>
        /// <value>The range end value.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The end value of the range. Must be greater than RangeStartValue.")]
        public Single RangeEndValue
        {
            get
            {
                return m_RangeEndValue[m_RangeIdx];
            }
            set
            {
                if ((m_RangeEndValue[m_RangeIdx] != value)
                && (m_RangeStartValue[m_RangeIdx] < value))
                {
                    m_RangeEndValue[m_RangeIdx] = value;
                    RangesEndValue = m_RangeEndValue;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the ranges end value.
        /// </summary>
        /// <value>The ranges end value.</value>
        [System.ComponentModel.Browsable(false)]
        public Single[] RangesEndValue
        {
            get
            {
                return m_RangeEndValue;
            }
            set
            {
                m_RangeEndValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the range inner radius.
        /// </summary>
        /// <value>The range inner radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The inner radius of the range.")]
        public Int32 RangeInnerRadius
        {
            get
            {
                return m_RangeInnerRadius[m_RangeIdx];
            }
            set
            {
                if (m_RangeInnerRadius[m_RangeIdx] != value)
                {
                    m_RangeInnerRadius[m_RangeIdx] = value;
                    RangesInnerRadius = m_RangeInnerRadius;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the ranges inner radius.
        /// </summary>
        /// <value>The ranges inner radius.</value>
        [System.ComponentModel.Browsable(false)]
        public Int32[] RangesInnerRadius
        {
            get
            {
                return m_RangeInnerRadius;
            }
            set
            {
                m_RangeInnerRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the range outer radius.
        /// </summary>
        /// <value>The range outer radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The outer radius of the range.")]
        public Int32 RangeOuterRadius
        {
            get
            {
                return m_RangeOuterRadius[m_RangeIdx];
            }
            set
            {
                if (m_RangeOuterRadius[m_RangeIdx] != value)
                {
                    m_RangeOuterRadius[m_RangeIdx] = value;
                    RangesOuterRadius = m_RangeOuterRadius;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the ranges outer radius.
        /// </summary>
        /// <value>The ranges outer radius.</value>
        [System.ComponentModel.Browsable(false)]
        public Int32[] RangesOuterRadius
        {
            get
            {
                return m_RangeOuterRadius;
            }
            set
            {
                m_RangeOuterRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the radius of the scale numbers.
        /// </summary>
        /// <value>The scale numbers radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The radius of the scale numbers.")]
        public Int32 ScaleNumbersRadius
        {
            get
            {
                return m_ScaleNumbersRadius;
            }
            set
            {
                if (m_ScaleNumbersRadius != value)
                {
                    m_ScaleNumbersRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the scale numbers.
        /// </summary>
        /// <value>The color of the scale numbers.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The color of the scale numbers.")]
        public Color ScaleNumbersColor
        {
            get
            {
                return m_ScaleNumbersColor;
            }
            set
            {
                if (m_ScaleNumbersColor != value)
                {
                    m_ScaleNumbersColor = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers format.
        /// </summary>
        /// <value>The scale numbers format.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The format of the scale numbers.")]
        public String ScaleNumbersFormat
        {
            get
            {
                return m_ScaleNumbersFormat;
            }
            set
            {
                if (m_ScaleNumbersFormat != value)
                {
                    m_ScaleNumbersFormat = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of the scale line to start writing numbers next to.
        /// </summary>
        /// <value>The scale numbers start scale line.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The number of the scale line to start writing numbers next to.")]
        public Int32 ScaleNumbersStartScaleLine
        {
            get
            {
                return m_ScaleNumbersStartScaleLine;
            }
            set
            {
                if (m_ScaleNumbersStartScaleLine != value)
                {
                    m_ScaleNumbersStartScaleLine = Math.Max(value, 1);
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of scale line steps for writing numbers.
        /// </summary>
        /// <value>The scale numbers step scale lines.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The number of scale line steps for writing numbers.")]
        public Int32 ScaleNumbersStepScaleLines
        {
            get
            {
                return m_ScaleNumbersStepScaleLines;
            }
            set
            {
                if (m_ScaleNumbersStepScaleLines != value)
                {
                    m_ScaleNumbersStepScaleLines = Math.Max(value, 1);
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers rotation.
        /// The angle relative to the tangent of the
        /// base arc at a scale line that is used to
        /// rotate numbers. set to 0 for no rotation
        /// or e.g. set to 90
        /// </summary>
        /// <value>The scale numbers rotation.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The angle relative to the tangent of the base arc at a scale line that is used to rotate numbers. set to 0 for no rotation or e.g. set to 90.")]
        public Int32 ScaleNumbersRotation
        {
            get
            {
                return m_ScaleNumbersRotation;
            }
            set
            {
                if (m_ScaleNumbersRotation != value)
                {
                    m_ScaleNumbersRotation = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the needle.
        /// </summary>
        /// <value>The type of the needle.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The type of the needle, currently only type 0 and 1 are supported. Type 0 looks nicers but if you experience performance problems you might consider using type 1.")]
        public Int32 NeedleType
        {
            get
            {
                return m_NeedleType;
            }
            set
            {
                if (m_NeedleType != value)
                {
                    m_NeedleType = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the needle radius.
        /// </summary>
        /// <value>The needle radius.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The radius of the needle.")]
        public Int32 NeedleRadius
        {
            get
            {
                return m_NeedleRadius;
            }
            set
            {
                if (m_NeedleRadius != value)
                {
                    m_NeedleRadius = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the needle color.
        /// </summary>
        /// <value>The needle color1.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The first color of the needle.")]
        public NeedleColor NeedleColor1
        {
            get
            {
                return m_NeedleColor1;
            }
            set
            {
                if (m_NeedleColor1 != value)
                {
                    m_NeedleColor1 = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the needle color.
        /// </summary>
        /// <value>The needle color2.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The second color of the needle.")]
        public Color NeedleColor2
        {
            get
            {
                return m_NeedleColor2;
            }
            set
            {
                if (m_NeedleColor2 != value)
                {
                    m_NeedleColor2 = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the needle.
        /// </summary>
        /// <value>The width of the needle.</value>
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("The width of the needle.")]
        public Int32 NeedleWidth
        {
            get
            {
                return m_NeedleWidth;
            }
            set
            {
                if (m_NeedleWidth != value)
                {
                    m_NeedleWidth = value;
                    drawGaugeBackground = true;
                    Refresh();
                }
            }
        }
        #endregion

        #region helper
        /// <summary>
        /// Finds the font bounds.
        /// </summary>
        private void FindFontBounds()
        {
            //find upper and lower bounds for numeric characters
            Int32 c1;
            Int32 c2;
            bool boundfound;
            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);
            SolidBrush backBrush = new SolidBrush(Color.White);
            SolidBrush foreBrush = new SolidBrush(Color.Black);
            SizeF boundingBox = g.MeasureString("0123456789", Font, -1, StringFormat.GenericTypographic);
            //b = new Bitmap((Int32)(boundingBox.Width), (Int32)(boundingBox.Height));



            g = Graphics.FromImage(b);
            g.FillRectangle(backBrush, 0.0F, 0.0F, boundingBox.Width, boundingBox.Height);
            g.DrawString("0123456789", Font, foreBrush, 0.0F, 0.0F, StringFormat.GenericTypographic);

            fontBoundY1 = 0;
            fontBoundY2 = 0;
            c1 = 0;
            boundfound = false;
            while ((c1 < b.Height) && (!boundfound))
            {
                c2 = 0;
                while ((c2 < b.Width) && (!boundfound))
                {
                    if (b.GetPixel(c2, c1) != backBrush.Color)
                    {
                        fontBoundY1 = c1;
                        boundfound = true;
                    }
                    c2++;
                }
                c1++;
            }

            c1 = b.Height - 1;
            boundfound = false;
            while ((0 < c1) && (!boundfound))
            {
                c2 = 0;
                while ((c2 < b.Width) && (!boundfound))
                {
                    if (b.GetPixel(c2, c1) != backBrush.Color)
                    {
                        fontBoundY2 = c1;
                        boundfound = true;
                    }
                    c2++;
                }
                c1--;
            }
        }
        #endregion

        #region base member overrides
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            if ((Width < 10) || (Height < 10))
            {
                return;
            }

            if (drawGaugeBackground)
            {
                drawGaugeBackground = false;

                FindFontBounds();

                gaugeBitmap = new Bitmap(Width, Height, pe.Graphics);
                Graphics ggr = Graphics.FromImage(gaugeBitmap);

                ggr.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

                if (BackgroundImage != null)
                {
                    switch (BackgroundImageLayout)
                    {
                        case ImageLayout.Center:
                            ggr.DrawImageUnscaled(BackgroundImage, Width / 2 - BackgroundImage.Width / 2, Height / 2 - BackgroundImage.Height / 2);
                            break;
                        case ImageLayout.None:
                            ggr.DrawImageUnscaled(BackgroundImage, 0, 0);
                            break;
                        case ImageLayout.Stretch:
                            ggr.DrawImage(BackgroundImage, 0, 0, Width, Height);
                            break;
                        case ImageLayout.Tile:
                            Int32 pixelOffsetX = 0;
                            Int32 pixelOffsetY = 0;
                            while (pixelOffsetX < Width)
                            {
                                pixelOffsetY = 0;
                                while (pixelOffsetY < Height)
                                {
                                    ggr.DrawImageUnscaled(BackgroundImage, pixelOffsetX, pixelOffsetY);
                                    pixelOffsetY += BackgroundImage.Height;
                                }
                                pixelOffsetX += BackgroundImage.Width;
                            }
                            break;
                        case ImageLayout.Zoom:
                            if ((Single)(BackgroundImage.Width / Width) < (Single)(BackgroundImage.Height / Height))
                            {
                                ggr.DrawImage(BackgroundImage, 0, 0, Height, Height);
                            }
                            else
                            {
                                ggr.DrawImage(BackgroundImage, 0, 0, Width, Width);
                            }
                            break;
                    }
                }

                ggr.SmoothingMode = SmoothingMode.HighQuality;
                ggr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                GraphicsPath gp = new GraphicsPath();
                Single rangeStartAngle;
                Single rangeSweepAngle;
                for (Int32 counter = 0; counter < NUMOFRANGES; counter++)
                {
                    if (m_RangeEndValue[counter] > m_RangeStartValue[counter]
                    && m_RangeEnabled[counter])
                    {
                        rangeStartAngle = m_BaseArcStart + (m_RangeStartValue[counter] - m_MinValue) * m_BaseArcSweep / (m_MaxValue - m_MinValue);
                        rangeSweepAngle = (m_RangeEndValue[counter] - m_RangeStartValue[counter]) * m_BaseArcSweep / (m_MaxValue - m_MinValue);
                        gp.Reset();
                        gp.AddPie(new Rectangle(m_Center.X - m_RangeOuterRadius[counter], m_Center.Y - m_RangeOuterRadius[counter], 2 * m_RangeOuterRadius[counter], 2 * m_RangeOuterRadius[counter]), rangeStartAngle, rangeSweepAngle);
                        gp.Reverse();
                        gp.AddPie(new Rectangle(m_Center.X - m_RangeInnerRadius[counter], m_Center.Y - m_RangeInnerRadius[counter], 2 * m_RangeInnerRadius[counter], 2 * m_RangeInnerRadius[counter]), rangeStartAngle, rangeSweepAngle);
                        gp.Reverse();
                        ggr.SetClip(gp);
                        ggr.FillPie(new SolidBrush(m_RangeColor[counter]), new Rectangle(m_Center.X - m_RangeOuterRadius[counter], m_Center.Y - m_RangeOuterRadius[counter], 2 * m_RangeOuterRadius[counter], 2 * m_RangeOuterRadius[counter]), rangeStartAngle, rangeSweepAngle);
                    }
                }

                ggr.SetClip(ClientRectangle);
                if (m_BaseArcRadius > 0)
                {
                    ggr.DrawArc(new Pen(m_BaseArcColor, m_BaseArcWidth), new Rectangle(m_Center.X - m_BaseArcRadius, m_Center.Y - m_BaseArcRadius, 2 * m_BaseArcRadius, 2 * m_BaseArcRadius), m_BaseArcStart, m_BaseArcSweep);
                }

                String valueText = "";
                SizeF boundingBox;
                Single countValue = 0;
                Int32 counter1 = 0;
                while (countValue <= (m_MaxValue - m_MinValue))
                {
                    valueText = (m_MinValue + countValue).ToString(m_ScaleNumbersFormat);
                    ggr.ResetTransform();
                    boundingBox = ggr.MeasureString(valueText, Font, -1, StringFormat.GenericTypographic);

                    gp.Reset();
                    gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMajorOuterRadius, m_Center.Y - m_ScaleLinesMajorOuterRadius, 2 * m_ScaleLinesMajorOuterRadius, 2 * m_ScaleLinesMajorOuterRadius));
                    gp.Reverse();
                    gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMajorInnerRadius, m_Center.Y - m_ScaleLinesMajorInnerRadius, 2 * m_ScaleLinesMajorInnerRadius, 2 * m_ScaleLinesMajorInnerRadius));
                    gp.Reverse();
                    ggr.SetClip(gp);

                    ggr.DrawLine(new Pen(m_ScaleLinesMajorColor, m_ScaleLinesMajorWidth),
                    (Single)(Center.X),
                    (Single)(Center.Y),
                    (Single)(Center.X + 2 * m_ScaleLinesMajorOuterRadius * Math.Cos((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue)) * Math.PI / 180.0)),
                    (Single)(Center.Y + 2 * m_ScaleLinesMajorOuterRadius * Math.Sin((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue)) * Math.PI / 180.0)));

                    gp.Reset();
                    gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMinorOuterRadius, m_Center.Y - m_ScaleLinesMinorOuterRadius, 2 * m_ScaleLinesMinorOuterRadius, 2 * m_ScaleLinesMinorOuterRadius));
                    gp.Reverse();
                    gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMinorInnerRadius, m_Center.Y - m_ScaleLinesMinorInnerRadius, 2 * m_ScaleLinesMinorInnerRadius, 2 * m_ScaleLinesMinorInnerRadius));
                    gp.Reverse();
                    ggr.SetClip(gp);

                    if (countValue < (m_MaxValue - m_MinValue))
                    {
                        for (Int32 counter2 = 1; counter2 <= m_ScaleLinesMinorNumOf; counter2++)
                        {
                            if (((m_ScaleLinesMinorNumOf % 2) == 1) && ((Int32)(m_ScaleLinesMinorNumOf / 2) + 1 == counter2))
                            {
                                gp.Reset();
                                gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesInterOuterRadius, m_Center.Y - m_ScaleLinesInterOuterRadius, 2 * m_ScaleLinesInterOuterRadius, 2 * m_ScaleLinesInterOuterRadius));
                                gp.Reverse();
                                gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesInterInnerRadius, m_Center.Y - m_ScaleLinesInterInnerRadius, 2 * m_ScaleLinesInterInnerRadius, 2 * m_ScaleLinesInterInnerRadius));
                                gp.Reverse();
                                ggr.SetClip(gp);

                                ggr.DrawLine(new Pen(m_ScaleLinesInterColor, m_ScaleLinesInterWidth),
                                (Single)(Center.X),
                                (Single)(Center.Y),
                                (Single)(Center.X + 2 * m_ScaleLinesInterOuterRadius * Math.Cos((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue) + counter2 * m_BaseArcSweep / (((Single)((m_MaxValue - m_MinValue) / m_ScaleLinesMajorStepValue)) * (m_ScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)),
                                (Single)(Center.Y + 2 * m_ScaleLinesInterOuterRadius * Math.Sin((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue) + counter2 * m_BaseArcSweep / (((Single)((m_MaxValue - m_MinValue) / m_ScaleLinesMajorStepValue)) * (m_ScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)));

                                gp.Reset();
                                gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMinorOuterRadius, m_Center.Y - m_ScaleLinesMinorOuterRadius, 2 * m_ScaleLinesMinorOuterRadius, 2 * m_ScaleLinesMinorOuterRadius));
                                gp.Reverse();
                                gp.AddEllipse(new Rectangle(m_Center.X - m_ScaleLinesMinorInnerRadius, m_Center.Y - m_ScaleLinesMinorInnerRadius, 2 * m_ScaleLinesMinorInnerRadius, 2 * m_ScaleLinesMinorInnerRadius));
                                gp.Reverse();
                                ggr.SetClip(gp);
                            }
                            else
                            {
                                ggr.DrawLine(new Pen(m_ScaleLinesMinorColor, m_ScaleLinesMinorWidth),
                                (Single)(Center.X),
                                (Single)(Center.Y),
                                (Single)(Center.X + 2 * m_ScaleLinesMinorOuterRadius * Math.Cos((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue) + counter2 * m_BaseArcSweep / (((Single)((m_MaxValue - m_MinValue) / m_ScaleLinesMajorStepValue)) * (m_ScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)),
                                (Single)(Center.Y + 2 * m_ScaleLinesMinorOuterRadius * Math.Sin((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue) + counter2 * m_BaseArcSweep / (((Single)((m_MaxValue - m_MinValue) / m_ScaleLinesMajorStepValue)) * (m_ScaleLinesMinorNumOf + 1))) * Math.PI / 180.0)));
                            }
                        }
                    }

                    ggr.SetClip(ClientRectangle);

                    if (m_ScaleNumbersRotation != 0)
                    {
                        ggr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        ggr.RotateTransform(90.0F + m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue));
                    }

                    ggr.TranslateTransform((Single)(Center.X + m_ScaleNumbersRadius * Math.Cos((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue)) * Math.PI / 180.0f)),
                                           (Single)(Center.Y + m_ScaleNumbersRadius * Math.Sin((m_BaseArcStart + countValue * m_BaseArcSweep / (m_MaxValue - m_MinValue)) * Math.PI / 180.0f)),
                                           System.Drawing.Drawing2D.MatrixOrder.Append);


                    if (counter1 >= ScaleNumbersStartScaleLine - 1)
                    {
                        ggr.DrawString(valueText, Font, new SolidBrush(m_ScaleNumbersColor), -boundingBox.Width / 2, -fontBoundY1 - (fontBoundY2 - fontBoundY1 + 1) / 2, StringFormat.GenericTypographic);
                    }

                    countValue += m_ScaleLinesMajorStepValue;
                    counter1++;
                }

                ggr.ResetTransform();
                ggr.SetClip(ClientRectangle);

                if (m_ScaleNumbersRotation != 0)
                {
                    ggr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                }

                for (Int32 counter = 0; counter < NUMOFCAPS; counter++)
                {
                    if (m_CapText[counter] != "")
                    {
                        ggr.DrawString(m_CapText[counter], Font, new SolidBrush(m_CapColor[counter]), m_CapPosition[counter].X, m_CapPosition[counter].Y, StringFormat.GenericTypographic);
                    }
                }
            }

            pe.Graphics.DrawImageUnscaled(gaugeBitmap, 0, 0);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Single brushAngle = (Int32)(m_BaseArcStart + (m_value - m_MinValue) * m_BaseArcSweep / (m_MaxValue - m_MinValue)) % 360;
            Double needleAngle = brushAngle * Math.PI / 180;

            switch (m_NeedleType)
            {
                case 0:
                    PointF[] points = new PointF[3];
                    Brush brush1 = Brushes.White;
                    Brush brush2 = Brushes.White;
                    Brush brush3 = Brushes.White;
                    Brush brush4 = Brushes.White;

                    Brush brushBucket = Brushes.White;
                    Int32 subcol = (Int32)(((brushAngle + 225) % 180) * 100 / 180);
                    Int32 subcol2 = (Int32)(((brushAngle + 135) % 180) * 100 / 180);

                    pe.Graphics.FillEllipse(new SolidBrush(m_NeedleColor2), Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                    switch (m_NeedleColor1)
                    {
                        case NeedleColor.Gray:
                            brush1 = new SolidBrush(Color.FromArgb(80 + subcol, 80 + subcol, 80 + subcol));
                            brush2 = new SolidBrush(Color.FromArgb(180 - subcol, 180 - subcol, 180 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(80 + subcol2, 80 + subcol2, 80 + subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(180 - subcol2, 180 - subcol2, 180 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Gray, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Red:
                            brush1 = new SolidBrush(Color.FromArgb(145 + subcol, subcol, subcol));
                            brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 100 - subcol, 100 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, subcol2, subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 100 - subcol2, 100 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Red, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Green:
                            brush1 = new SolidBrush(Color.FromArgb(subcol, 145 + subcol, subcol));
                            brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 245 - subcol, 100 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(subcol2, 145 + subcol2, subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 245 - subcol2, 100 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Green, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Blue:
                            brush1 = new SolidBrush(Color.FromArgb(subcol, subcol, 145 + subcol));
                            brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 100 - subcol, 245 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(subcol2, subcol2, 145 + subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 100 - subcol2, 245 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Blue, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Magenta:
                            brush1 = new SolidBrush(Color.FromArgb(subcol, 145 + subcol, 145 + subcol));
                            brush2 = new SolidBrush(Color.FromArgb(100 - subcol, 245 - subcol, 245 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(subcol2, 145 + subcol2, 145 + subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(100 - subcol2, 245 - subcol2, 245 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Magenta, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Violet:
                            brush1 = new SolidBrush(Color.FromArgb(145 + subcol, subcol, 145 + subcol));
                            brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 100 - subcol, 245 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, subcol2, 145 + subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 100 - subcol2, 245 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Violet, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                        case NeedleColor.Yellow:
                            brush1 = new SolidBrush(Color.FromArgb(145 + subcol, 145 + subcol, subcol));
                            brush2 = new SolidBrush(Color.FromArgb(245 - subcol, 245 - subcol, 100 - subcol));
                            brush3 = new SolidBrush(Color.FromArgb(145 + subcol2, 145 + subcol2, subcol2));
                            brush4 = new SolidBrush(Color.FromArgb(245 - subcol2, 245 - subcol2, 100 - subcol2));
                            pe.Graphics.DrawEllipse(Pens.Violet, Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);
                            break;
                    }

                    if (Math.Floor((Single)(((brushAngle + 225) % 360) / 180.0)) == 0)
                    {
                        brushBucket = brush1;
                        brush1 = brush2;
                        brush2 = brushBucket;
                    }

                    if (Math.Floor((Single)(((brushAngle + 135) % 360) / 180.0)) == 0)
                    {
                        brush4 = brush3;
                    }

                    points[0].X = (Single)(Center.X + m_NeedleRadius * Math.Cos(needleAngle));
                    points[0].Y = (Single)(Center.Y + m_NeedleRadius * Math.Sin(needleAngle));
                    points[1].X = (Single)(Center.X - m_NeedleRadius / 20 * Math.Cos(needleAngle));
                    points[1].Y = (Single)(Center.Y - m_NeedleRadius / 20 * Math.Sin(needleAngle));
                    points[2].X = (Single)(Center.X - m_NeedleRadius / 5 * Math.Cos(needleAngle) + m_NeedleWidth * 2 * Math.Cos(needleAngle + Math.PI / 2));
                    points[2].Y = (Single)(Center.Y - m_NeedleRadius / 5 * Math.Sin(needleAngle) + m_NeedleWidth * 2 * Math.Sin(needleAngle + Math.PI / 2));
                    pe.Graphics.FillPolygon(brush1, points);

                    points[2].X = (Single)(Center.X - m_NeedleRadius / 5 * Math.Cos(needleAngle) + m_NeedleWidth * 2 * Math.Cos(needleAngle - Math.PI / 2));
                    points[2].Y = (Single)(Center.Y - m_NeedleRadius / 5 * Math.Sin(needleAngle) + m_NeedleWidth * 2 * Math.Sin(needleAngle - Math.PI / 2));
                    pe.Graphics.FillPolygon(brush2, points);

                    points[0].X = (Single)(Center.X - (m_NeedleRadius / 20 - 1) * Math.Cos(needleAngle));
                    points[0].Y = (Single)(Center.Y - (m_NeedleRadius / 20 - 1) * Math.Sin(needleAngle));
                    points[1].X = (Single)(Center.X - m_NeedleRadius / 5 * Math.Cos(needleAngle) + m_NeedleWidth * 2 * Math.Cos(needleAngle + Math.PI / 2));
                    points[1].Y = (Single)(Center.Y - m_NeedleRadius / 5 * Math.Sin(needleAngle) + m_NeedleWidth * 2 * Math.Sin(needleAngle + Math.PI / 2));
                    points[2].X = (Single)(Center.X - m_NeedleRadius / 5 * Math.Cos(needleAngle) + m_NeedleWidth * 2 * Math.Cos(needleAngle - Math.PI / 2));
                    points[2].Y = (Single)(Center.Y - m_NeedleRadius / 5 * Math.Sin(needleAngle) + m_NeedleWidth * 2 * Math.Sin(needleAngle - Math.PI / 2));
                    pe.Graphics.FillPolygon(brush4, points);

                    points[0].X = (Single)(Center.X - m_NeedleRadius / 20 * Math.Cos(needleAngle));
                    points[0].Y = (Single)(Center.Y - m_NeedleRadius / 20 * Math.Sin(needleAngle));
                    points[1].X = (Single)(Center.X + m_NeedleRadius * Math.Cos(needleAngle));
                    points[1].Y = (Single)(Center.Y + m_NeedleRadius * Math.Sin(needleAngle));

                    pe.Graphics.DrawLine(new Pen(m_NeedleColor2), Center.X, Center.Y, points[0].X, points[0].Y);
                    pe.Graphics.DrawLine(new Pen(m_NeedleColor2), Center.X, Center.Y, points[1].X, points[1].Y);
                    break;
                case 1:
                    Point startPoint = new Point((Int32)(Center.X - m_NeedleRadius / 8 * Math.Cos(needleAngle)),
                                               (Int32)(Center.Y - m_NeedleRadius / 8 * Math.Sin(needleAngle)));
                    Point endPoint = new Point((Int32)(Center.X + m_NeedleRadius * Math.Cos(needleAngle)),
                                             (Int32)(Center.Y + m_NeedleRadius * Math.Sin(needleAngle)));

                    pe.Graphics.FillEllipse(new SolidBrush(m_NeedleColor2), Center.X - m_NeedleWidth * 3, Center.Y - m_NeedleWidth * 3, m_NeedleWidth * 6, m_NeedleWidth * 6);

                    switch (m_NeedleColor1)
                    {
                        case NeedleColor.Gray:
                            pe.Graphics.DrawLine(new Pen(Color.DarkGray, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.DarkGray, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Red:
                            pe.Graphics.DrawLine(new Pen(Color.Red, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Red, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Green:
                            pe.Graphics.DrawLine(new Pen(Color.Green, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Green, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Blue:
                            pe.Graphics.DrawLine(new Pen(Color.Blue, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Blue, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Magenta:
                            pe.Graphics.DrawLine(new Pen(Color.Magenta, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Magenta, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Violet:
                            pe.Graphics.DrawLine(new Pen(Color.Violet, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Violet, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                        case NeedleColor.Yellow:
                            pe.Graphics.DrawLine(new Pen(Color.Yellow, m_NeedleWidth), Center.X, Center.Y, endPoint.X, endPoint.Y);
                            pe.Graphics.DrawLine(new Pen(Color.Yellow, m_NeedleWidth), Center.X, Center.Y, startPoint.X, startPoint.Y);
                            break;
                    }
                    break;




            }

            switch (_gaugeType)
            {
                case GaugeType.Default:
                    this.m_BaseArcColor = m_BaseArcColor;
                    this.m_BaseArcRadius = m_BaseArcRadius;
                    this.m_BaseArcStart = m_BaseArcStart;
                    this.m_BaseArcSweep = m_BaseArcSweep;
                    this.m_BaseArcWidth = m_BaseArcWidth;

                    this.m_CapIdx = 1;
                    this.m_CapPosition[0] = m_CapPosition[0];
                    this.m_CapPosition[1] = m_CapPosition[1];
                    this.m_CapPosition[2] = m_CapPosition[2];
                    this.m_CapPosition[3] = m_CapPosition[3];
                    this.m_CapPosition[4] = m_CapPosition[4];


                    this.m_CapText[0] = m_CapText[0];
                    this.m_CapText[1] = m_CapText[1];
                    this.m_CapText[2] = m_CapText[2];
                    this.m_CapText[3] = m_CapText[3];
                    this.m_CapText[4] = m_CapText[4];
                    this.m_Center = m_Center;
                    this.m_MaxValue = m_MaxValue;
                    this.m_MinValue = -m_MinValue;

                    this.m_NeedleColor1 = m_NeedleColor1;
                    this.m_NeedleColor2 = m_NeedleColor2;
                    this.m_NeedleRadius = m_NeedleRadius;
                    this.m_NeedleType = m_NeedleType;
                    this.m_NeedleWidth = m_NeedleWidth;

                    this.m_RangeIdx = 1;
                    this.m_RangeColor[0] = m_RangeColor[0];
                    this.m_RangeColor[1] = m_RangeColor[1];
                    this.m_RangeColor[2] = m_RangeColor[2];
                    this.m_RangeColor[3] = m_RangeColor[3];
                    this.m_RangeColor[4] = m_RangeColor[4];
                    //this.m_RangeColor[5] = m_RangeColor[5];

                    this.m_RangeEnabled[0] = m_RangeEnabled[0];
                    this.m_RangeEnabled[1] = m_RangeEnabled[1];
                    this.m_RangeEnabled[2] = m_RangeEnabled[2];
                    this.m_RangeEnabled[3] = m_RangeEnabled[3];
                    this.m_RangeEnabled[4] = m_RangeEnabled[4];
                    this.m_RangeEndValue[0] = m_RangeEndValue[0];
                    this.m_RangeEndValue[1] = m_RangeEndValue[1];
                    this.m_RangeEndValue[2] = m_RangeEndValue[2];
                    this.m_RangeEndValue[3] = m_RangeEndValue[3];
                    this.m_RangeEndValue[4] = m_RangeEndValue[4];
                    this.m_RangeInnerRadius[0] = m_RangeInnerRadius[0];
                    this.m_RangeInnerRadius[1] = m_RangeInnerRadius[1];
                    this.m_RangeInnerRadius[2] = m_RangeInnerRadius[2];
                    this.m_RangeInnerRadius[3] = m_RangeInnerRadius[3];
                    this.m_RangeInnerRadius[4] = m_RangeInnerRadius[4];
                    this.m_RangeOuterRadius[0] = m_RangeOuterRadius[0];
                    this.m_RangeOuterRadius[1] = m_RangeOuterRadius[1];
                    this.m_RangeOuterRadius[2] = m_RangeOuterRadius[2];
                    this.m_RangeOuterRadius[3] = m_RangeOuterRadius[3];
                    this.m_RangeOuterRadius[4] = m_RangeOuterRadius[4];
                    this.m_RangeStartValue[0] = m_RangeStartValue[0];
                    this.m_RangeStartValue[1] = m_RangeStartValue[1];
                    this.m_RangeStartValue[2] = m_RangeStartValue[2];
                    this.m_RangeStartValue[3] = m_RangeStartValue[3];
                    this.m_RangeStartValue[4] = m_RangeStartValue[4];


                    this.m_ScaleLinesInterColor = m_ScaleLinesInterColor;
                    this.m_ScaleLinesInterInnerRadius = m_ScaleLinesInterInnerRadius;
                    this.m_ScaleLinesInterOuterRadius = m_ScaleLinesInterOuterRadius;
                    this.m_ScaleLinesInterWidth = m_ScaleLinesInterWidth;

                    this.m_ScaleLinesMajorColor = m_ScaleLinesMajorColor;
                    this.m_ScaleLinesMajorInnerRadius = m_ScaleLinesMajorInnerRadius;
                    this.m_ScaleLinesMajorOuterRadius = m_ScaleLinesMajorOuterRadius;
                    this.m_ScaleLinesMajorStepValue = m_ScaleLinesMajorStepValue;
                    this.m_ScaleLinesMajorWidth = m_ScaleLinesMajorWidth;

                    this.m_ScaleLinesMinorColor = m_ScaleLinesMinorColor;
                    this.m_ScaleLinesMinorInnerRadius = m_ScaleLinesMinorInnerRadius;
                    this.m_ScaleLinesMinorNumOf = m_ScaleLinesMinorNumOf;
                    this.m_ScaleLinesMinorOuterRadius = m_ScaleLinesMinorOuterRadius;
                    this.m_ScaleLinesMinorWidth = m_ScaleLinesMinorWidth;

                    this.m_ScaleNumbersColor = m_ScaleNumbersColor;
                    this.m_ScaleNumbersFormat = m_ScaleNumbersFormat;
                    this.m_ScaleNumbersRadius = m_ScaleNumbersRadius;
                    this.m_ScaleNumbersRotation = m_ScaleNumbersRotation;
                    this.m_ScaleNumbersStartScaleLine = m_ScaleNumbersStartScaleLine;
                    this.m_ScaleNumbersStepScaleLines = m_ScaleNumbersStepScaleLines;

                    break;
                case GaugeType.QuarterNormal:
                    this.m_BaseArcColor = Color.LightSlateGray;
                    this.m_BaseArcRadius = 80;
                    this.m_BaseArcStart = 135;
                    this.m_BaseArcSweep = 270;
                    this.m_BaseArcWidth = 1;

                    this.m_CapIdx = 1;
                    this.m_CapPosition[0] = new Point(10, 10);
                    this.m_CapPosition[1] = new Point(10, 10);
                    this.m_CapPosition[2] = new Point(10, 10);
                    this.m_CapPosition[3] = new Point(10, 10);
                    this.m_CapPosition[4] = new Point(10, 10);


                    this.m_CapText[0] = ""; this.m_CapText[1] = ""; this.m_CapText[2] = ""; this.m_CapText[3] = ""; this.m_CapText[4] = "";
                    this.m_Center = new Point(100, 105);
                    this.m_MaxValue = 400;
                    this.m_MinValue = -100;

                    this.m_NeedleColor1 = m_NeedleColor1;
                    this.m_NeedleColor2 = Color.DimGray;
                    this.m_NeedleRadius = 80;
                    this.m_NeedleType = 0;
                    this.m_NeedleWidth = 2;

                    this.m_RangeIdx = 1;
                    this.m_RangeColor[0] = Color.LightGreen;
                    this.m_RangeColor[1] = Color.FromArgb(255, 128, 128);
                    this.m_RangeColor[2] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeColor[3] = Color.LightGreen;
                    this.m_RangeColor[4] = Color.FromKnownColor(KnownColor.Control);
                    //this.m_RangeColor[5] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeEnabled[0] = true;
                    this.m_RangeEnabled[1] = true;
                    this.m_RangeEnabled[2] = false;
                    this.m_RangeEnabled[3] = false;
                    this.m_RangeEnabled[4] = false;
                    this.m_RangeEndValue[0] = 300.0f;
                    this.m_RangeEndValue[1] = 400.0f;
                    this.m_RangeEndValue[2] = 0.0f;
                    this.m_RangeEndValue[3] = 0.0f;
                    this.m_RangeEndValue[4] = 0.0f;
                    this.m_RangeInnerRadius[0] = 1;
                    this.m_RangeInnerRadius[1] = 1;
                    this.m_RangeInnerRadius[2] = 1;
                    this.m_RangeInnerRadius[3] = 1;
                    this.m_RangeInnerRadius[4] = 1;
                    this.m_RangeOuterRadius[0] = 75;
                    this.m_RangeOuterRadius[1] = 75;
                    this.m_RangeOuterRadius[2] = 75;
                    this.m_RangeOuterRadius[3] = 75;
                    this.m_RangeOuterRadius[4] = 75;
                    this.m_RangeStartValue[0] = -100.0f;
                    this.m_RangeStartValue[1] = 300.0f;
                    this.m_RangeStartValue[2] = 0.0f;
                    this.m_RangeStartValue[3] = 0.0f;
                    this.m_RangeStartValue[4] = 0.0f;


                    this.m_ScaleLinesInterColor = Color.Black;
                    this.m_ScaleLinesInterInnerRadius = 76;
                    this.m_ScaleLinesInterOuterRadius = 80;
                    this.m_ScaleLinesInterWidth = 1;

                    this.m_ScaleLinesMajorColor = Color.Black;
                    this.m_ScaleLinesMajorInnerRadius = 70;
                    this.m_ScaleLinesMajorOuterRadius = 80;
                    this.m_ScaleLinesMajorStepValue = 50;
                    this.m_ScaleLinesMajorWidth = 2;

                    this.m_ScaleLinesMinorColor = Color.Gray;
                    this.m_ScaleLinesMinorInnerRadius = 76;
                    this.m_ScaleLinesMinorNumOf = 9;
                    this.m_ScaleLinesMinorOuterRadius = 80;
                    this.m_ScaleLinesMinorWidth = 1;

                    this.m_ScaleNumbersColor = Color.Black;
                    this.m_ScaleNumbersFormat = "";
                    this.m_ScaleNumbersRadius = 95;
                    this.m_ScaleNumbersRotation = 0;
                    this.m_ScaleNumbersStartScaleLine = 0;
                    this.m_ScaleNumbersStepScaleLines = 1;


                    break;
                case GaugeType.Voltage:
                    this.m_BaseArcColor = Color.Gray;
                    this.m_BaseArcRadius = 150;
                    this.m_BaseArcStart = 215;
                    this.m_BaseArcSweep = 110;
                    this.m_BaseArcWidth = 2;

                    this.m_CapIdx = 1;
                    this.m_CapPosition[0] = new Point(10, 10);
                    this.m_CapPosition[1] = new Point(10, 10);
                    this.m_CapPosition[2] = new Point(10, 10);
                    this.m_CapPosition[3] = new Point(10, 10);
                    this.m_CapPosition[4] = new Point(10, 10);


                    this.m_CapText[0] = ""; this.m_CapText[1] = ""; this.m_CapText[2] = ""; this.m_CapText[3] = ""; this.m_CapText[4] = "";
                    this.m_Center = new Point(150, 180);
                    this.m_MaxValue = 300;
                    this.m_MinValue = -300;

                    this.m_NeedleColor1 = m_NeedleColor1;
                    this.m_NeedleColor2 = Color.DimGray;
                    this.m_NeedleRadius = 150;
                    this.m_NeedleType = 0;
                    this.m_NeedleWidth = 2;

                    this.m_RangeIdx = 1;
                    this.m_RangeColor[0] = Color.LightGreen;
                    this.m_RangeColor[1] = Color.FromArgb(255, 128, 128);
                    this.m_RangeColor[2] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeColor[3] = Color.LightGreen;
                    this.m_RangeColor[4] = Color.FromKnownColor(KnownColor.Control);
                    //this.m_RangeColor[5] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeEnabled[0] = false;
                    this.m_RangeEnabled[1] = false;
                    this.m_RangeEnabled[2] = false;
                    this.m_RangeEnabled[3] = false;
                    this.m_RangeEnabled[4] = false;
                    this.m_RangeEndValue[0] = 300.0f;
                    this.m_RangeEndValue[1] = 400.0f;
                    this.m_RangeEndValue[2] = 0.0f;
                    this.m_RangeEndValue[3] = 0.0f;
                    this.m_RangeEndValue[4] = 0.0f;
                    this.m_RangeInnerRadius[0] = 10;
                    this.m_RangeInnerRadius[1] = 10;
                    this.m_RangeInnerRadius[2] = 10;
                    this.m_RangeInnerRadius[3] = 10;
                    this.m_RangeInnerRadius[4] = 10;
                    this.m_RangeOuterRadius[0] = 40;
                    this.m_RangeOuterRadius[1] = 40;
                    this.m_RangeOuterRadius[2] = 40;
                    this.m_RangeOuterRadius[3] = 40;
                    this.m_RangeOuterRadius[4] = 40;
                    this.m_RangeStartValue[0] = -100.0f;
                    this.m_RangeStartValue[1] = 300.0f;
                    this.m_RangeStartValue[2] = 0.0f;
                    this.m_RangeStartValue[3] = 0.0f;
                    this.m_RangeStartValue[4] = 0.0f;


                    this.m_ScaleLinesInterColor = Color.Red;
                    this.m_ScaleLinesInterInnerRadius = 145;
                    this.m_ScaleLinesInterOuterRadius = 150;
                    this.m_ScaleLinesInterWidth = 2;

                    this.m_ScaleLinesMajorColor = Color.Black;
                    this.m_ScaleLinesMajorInnerRadius = 140;
                    this.m_ScaleLinesMajorOuterRadius = 150;
                    this.m_ScaleLinesMajorStepValue = 100;
                    this.m_ScaleLinesMajorWidth = 2;

                    this.m_ScaleLinesMinorColor = Color.Gray;
                    this.m_ScaleLinesMinorInnerRadius = 145;
                    this.m_ScaleLinesMinorNumOf = 9;
                    this.m_ScaleLinesMinorOuterRadius = 150;
                    this.m_ScaleLinesMinorWidth = 1;

                    this.m_ScaleNumbersColor = Color.Black;
                    this.m_ScaleNumbersFormat = "";
                    this.m_ScaleNumbersRadius = 162;
                    this.m_ScaleNumbersRotation = 90;
                    this.m_ScaleNumbersStartScaleLine = 1;
                    this.m_ScaleNumbersStepScaleLines = 2;


                    break;
                case GaugeType.Compass:
                    this.m_BaseArcColor = Color.Gray;
                    this.m_BaseArcRadius = 40;
                    this.m_BaseArcStart = -90;
                    this.m_BaseArcSweep = 360;
                    this.m_BaseArcWidth = 2;

                    this.m_CapIdx = 1;
                    this.m_CapPosition[0] = new Point(10, 10);
                    this.m_CapPosition[1] = new Point(10, 10);
                    this.m_CapPosition[2] = new Point(10, 10);
                    this.m_CapPosition[3] = new Point(10, 10);
                    this.m_CapPosition[4] = new Point(10, 10);


                    this.m_CapText[0] = ""; this.m_CapText[1] = ""; this.m_CapText[2] = ""; this.m_CapText[3] = ""; this.m_CapText[4] = "";
                    this.m_Center = new Point(70, 70);
                    this.m_MaxValue = 10;
                    this.m_MinValue = 0;

                    this.m_NeedleColor1 = NeedleColor.Green;
                    this.m_NeedleColor2 = Color.Black;
                    this.m_NeedleRadius = 40;
                    this.m_NeedleType = 0;
                    this.m_NeedleWidth = 10;

                    this.m_RangeIdx = 0;
                    this.m_RangeColor[0] = Color.LightGreen;
                    this.m_RangeColor[1] = Color.FromArgb(255, 128, 128);
                    this.m_RangeColor[2] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeColor[3] = Color.LightGreen;
                    this.m_RangeColor[4] = Color.FromKnownColor(KnownColor.Control);
                    //this.m_RangeColor[5] = Color.FromKnownColor(KnownColor.Control);
                    this.m_RangeEnabled[0] = false;
                    this.m_RangeEnabled[1] = false;
                    this.m_RangeEnabled[2] = false;
                    this.m_RangeEnabled[3] = false;
                    this.m_RangeEnabled[4] = false;
                    this.m_RangeEndValue[0] = 300.0f;
                    this.m_RangeEndValue[1] = 400.0f;
                    this.m_RangeEndValue[2] = 0.0f;
                    this.m_RangeEndValue[3] = 0.0f;
                    this.m_RangeEndValue[4] = 0.0f;
                    this.m_RangeInnerRadius[0] = 10;
                    this.m_RangeInnerRadius[1] = 10;
                    this.m_RangeInnerRadius[2] = 10;
                    this.m_RangeInnerRadius[3] = 10;
                    this.m_RangeInnerRadius[4] = 10;
                    this.m_RangeOuterRadius[0] = 40;
                    this.m_RangeOuterRadius[1] = 40;
                    this.m_RangeOuterRadius[2] = 40;
                    this.m_RangeOuterRadius[3] = 40;
                    this.m_RangeOuterRadius[4] = 40;
                    this.m_RangeStartValue[0] = -100.0f;
                    this.m_RangeStartValue[1] = 300.0f;
                    this.m_RangeStartValue[2] = 0.0f;
                    this.m_RangeStartValue[3] = 0.0f;
                    this.m_RangeStartValue[4] = 0.0f;


                    this.m_ScaleLinesInterColor = Color.FromArgb(192, 192, 255);
                    this.m_ScaleLinesInterInnerRadius = 42;
                    this.m_ScaleLinesInterOuterRadius = 50;
                    this.m_ScaleLinesInterWidth = 1;

                    this.m_ScaleLinesMajorColor = Color.FromArgb(128, 128, 255);
                    this.m_ScaleLinesMajorInnerRadius = 40;
                    this.m_ScaleLinesMajorOuterRadius = 50;
                    this.m_ScaleLinesMajorStepValue = 1;
                    this.m_ScaleLinesMajorWidth = 2;

                    this.m_ScaleLinesMinorColor = Color.FromArgb(192, 192, 255);
                    this.m_ScaleLinesMinorInnerRadius = 43;
                    this.m_ScaleLinesMinorNumOf = 1;
                    this.m_ScaleLinesMinorOuterRadius = 50;
                    this.m_ScaleLinesMinorWidth = 1;

                    this.m_ScaleNumbersColor = Color.FromArgb(128, 128, 255);
                    this.m_ScaleNumbersFormat = "";
                    this.m_ScaleNumbersRadius = 62;
                    this.m_ScaleNumbersRotation = 0;
                    this.m_ScaleNumbersStartScaleLine = 2;
                    this.m_ScaleNumbersStepScaleLines = 2;

                    break;
                case GaugeType.Simple:
                    break;
                case GaugeType.Speedometer:
                    break;
                case GaugeType.FuelL:
                    break;
                case GaugeType.FuelR:
                    break;
                case GaugeType.SimpleHalf:
                    break;
                case GaugeType.SimpleHalfSpline:
                    break;
                case GaugeType.ThreeQuarter:
                    break;
                case GaugeType.TwoQuarter:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            drawGaugeBackground = true;
            Refresh();
        }
        #endregion

        #region Zeroit Gauge Types

        #region Variables
        /// <summary>
        /// The gauge type
        /// </summary>
        private GaugeType _gaugeType;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the gauge types.
        /// </summary>
        /// <value>The gauge types.</value>
        public GaugeType GaugeTypes
        {
            get { return _gaugeType; }
            set
            {
                _gaugeType = value;
                this.Invalidate();
            }
        }
        #endregion
        /// <summary>
        /// Enum representing the type of gauge
        /// </summary>
        public enum GaugeType
        {
            /// <summary>
            /// The default
            /// </summary>
            Default,
            /// <summary>
            /// The quarter normal
            /// </summary>
            QuarterNormal,
            /// <summary>
            /// The voltage
            /// </summary>
            Voltage,
            /// <summary>
            /// The compass
            /// </summary>
            Compass,
            /// <summary>
            /// The simple
            /// </summary>
            Simple,
            /// <summary>
            /// The speedometer
            /// </summary>
            Speedometer,
            /// <summary>
            /// The fuel l
            /// </summary>
            FuelL,
            /// <summary>
            /// The fuel r
            /// </summary>
            FuelR,
            /// <summary>
            /// The simple half
            /// </summary>
            SimpleHalf,
            /// <summary>
            /// The simple half spline
            /// </summary>
            SimpleHalfSpline,
            /// <summary>
            /// The three quarter
            /// </summary>
            ThreeQuarter,
            /// <summary>
            /// The two quarter
            /// </summary>
            TwoQuarter

        }

        #endregion

    }

    #endregion

    #region AGauge.Designer

    partial class ZeroitGaugeAll
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
    #endregion

    #endregion
}
