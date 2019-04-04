// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="VuMeter.cs" company="Zeroit Dev Technologies">
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
#region Imports

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;

#endregion

namespace Zeroit.Framework.Gauges.ZeroitVuMeter
{
    #region Control


    /// <summary>
    /// Enum MeterScale
    /// </summary>
    public enum MeterScale { Analog, Log10 };

    /// <summary>
    /// Class ZeroitVuMeter.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitVuMeterDesigner))]
    public partial class ZeroitVuMeter : UserControl
    {

        #region Private Fields
        /// <summary>
        /// The current value
        /// </summary>
        int CurrentValue;
        /// <summary>
        /// The peak value
        /// </summary>
        int PeakValue;
        /// <summary>
        /// The led count1
        /// </summary>
        int LedCount1 = 6, LedCount2 = 6, LedCount3 = 4;
        /// <summary>
        /// The calculate value
        /// </summary>
        int calcValue, calcPeak;
        /// <summary>
        /// The led color on1
        /// </summary>
        Color LedColorOn1 = Color.LimeGreen, LedColorOn2 = Color.Yellow, LedColorOn3 = Color.Red;
        /// <summary>
        /// The led color off1
        /// </summary>
        Color LedColorOff1 = Color.DarkGreen, LedColorOff2 = Color.Olive, LedColorOff3 = Color.Maroon;
        /// <summary>
        /// The border color
        /// </summary>
        Color BorderColor = Color.DimGray;
        /// <summary>
        /// The dial back color
        /// </summary>
        Color DialBackColor = Color.White;
        /// <summary>
        /// The dial text low
        /// </summary>
        Color DialTextLow = Color.Red;
        /// <summary>
        /// The dial text neutral
        /// </summary>
        Color DialTextNeutral = Color.DarkGreen;
        /// <summary>
        /// The dial text high
        /// </summary>
        Color DialTextHigh = Color.Black;
        /// <summary>
        /// The dial needle
        /// </summary>
        Color DialNeedle = Color.Black;
        /// <summary>
        /// The dial peak
        /// </summary>
        Color DialPeak = Color.Red;
        /// <summary>
        /// The minimum
        /// </summary>
        int Min = 0, Max = 65535;
        /// <summary>
        /// The peak hold time
        /// </summary>
        int PeakHoldTime = 1000;
        /// <summary>
        /// The show peak
        /// </summary>
        bool ShowPeak = true;
        /// <summary>
        /// The vertical
        /// </summary>
        bool Vertical = false;
        /// <summary>
        /// The meter analog
        /// </summary>
        bool MeterAnalog = false;
        /// <summary>
        /// The meter text
        /// </summary>
        string MeterText = "VU";
        /// <summary>
        /// The dial text
        /// </summary>
        string[] DialText = { "-40", "-20", "-10", "-5", "0", "+6" };
        /// <summary>
        /// The show dial text
        /// </summary>
        bool ShowDialText = false;
        /// <summary>
        /// The analog dial region only
        /// </summary>
        bool AnalogDialRegionOnly = false;
        /// <summary>
        /// The use led light in analog
        /// </summary>
        bool UseLedLightInAnalog = false;
        /// <summary>
        /// The show led peak in analog
        /// </summary>
        bool ShowLedPeakInAnalog = false;

        /// <summary>
        /// The deg low
        /// </summary>
        private double DegLow = Math.PI * 0.8, DegHigh = Math.PI * 1.2;

        /// <summary>
        /// The timer1
        /// </summary>
        protected System.Windows.Forms.Timer timer1;
        /// <summary>
        /// The led
        /// </summary>
        Size Led = new Size(6, 14);
        /// <summary>
        /// The led spacing
        /// </summary>
        int LedSpacing = 3;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [use led light].
        /// </summary>
        /// <value><c>true</c> if [use led light]; otherwise, <c>false</c>.</value>
        [Category("Analog Meter")]
        [Description("Show textvalues in dial")]
        public bool UseLedLight
        {
            get
            {
                return UseLedLightInAnalog;
            }
            set
            {
                UseLedLightInAnalog = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show led peak].
        /// </summary>
        /// <value><c>true</c> if [show led peak]; otherwise, <c>false</c>.</value>
        [Category("Analog Meter")]
        [Description("Show textvalues in dial")]
        public bool ShowLedPeak
        {
            get
            {
                return ShowLedPeakInAnalog;
            }
            set
            {
                ShowLedPeakInAnalog = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [analog meter].
        /// </summary>
        /// <value><c>true</c> if [analog meter]; otherwise, <c>false</c>.</value>
        [Category("Analog Meter")]
        [Description("Analog meter layout")]
        public bool AnalogMeter
        {
            get
            {
                return MeterAnalog;
            }
            set
            {
                if (value & !MeterAnalog) this.Size = new Size(100, 80);
                MeterAnalog = value;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the vu text.
        /// </summary>
        /// <value>The vu text.</value>
        [Category("Analog Meter")]
        [Description("Text (max 10 letters)")]
        public string VuText
        {
            get
            {
                return MeterText;
            }
            set
            {
                if (value.Length < 11) MeterText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text in dial.
        /// </summary>
        /// <value>The text in dial.</value>
        [Category("Analog Meter")]
        [Description("Text in dial")]
        public string[] TextInDial
        {
            get
            {
                return DialText;
            }
            set
            {
                DialText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show text in dial].
        /// </summary>
        /// <value><c>true</c> if [show text in dial]; otherwise, <c>false</c>.</value>
        [Category("Analog Meter")]
        [Description("Show textvalues in dial")]
        public bool ShowTextInDial
        {
            get
            {
                return ShowDialText;
            }
            set
            {
                ShowDialText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show dial only].
        /// </summary>
        /// <value><c>true</c> if [show dial only]; otherwise, <c>false</c>.</value>
        [Category("Analog Meter")]
        [Description("Only show the Analog Dial Panel (Sets BackColor to DialBackColor so antialias won't look bad)")]
        public bool ShowDialOnly
        {
            get
            {
                return AnalogDialRegionOnly;
            }
            set
            {
                AnalogDialRegionOnly = value;
                if (AnalogDialRegionOnly) this.BackColor = DialBackColor;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dial background.
        /// </summary>
        /// <value>The dial background.</value>
        [Category("Analog Meter")]
        [Description("Color on dial background")]
        public Color DialBackground
        {
            get
            {
                return DialBackColor;
            }
            set
            {
                DialBackColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dial text negative.
        /// </summary>
        /// <value>The dial text negative.</value>
        [Category("Analog Meter")]
        [Description("Color on Value < 0")]
        public Color DialTextNegative
        {
            get
            {
                return DialTextLow;
            }
            set
            {
                DialTextLow = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dial text zero.
        /// </summary>
        /// <value>The dial text zero.</value>
        [Category("Analog Meter")]
        [Description("Color on Value = 0")]
        public Color DialTextZero
        {
            get
            {
                return DialTextNeutral;
            }
            set
            {
                DialTextNeutral = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dial text positive.
        /// </summary>
        /// <value>The dial text positive.</value>
        [Category("Analog Meter")]
        [Description("Color on Value > 0")]
        public Color DialTextPositive
        {
            get
            {
                return DialTextHigh;
            }
            set
            {
                DialTextHigh = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the needle.
        /// </summary>
        /// <value>The color of the needle.</value>
        [Category("Analog Meter")]
        [Description("Color on needle")]
        public Color NeedleColor
        {
            get
            {
                return DialNeedle;
            }
            set
            {
                DialNeedle = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the peak needle.
        /// </summary>
        /// <value>The color of the peak needle.</value>
        [Category("Analog Meter")]
        [Description("Color on Peak needle")]
        public Color PeakNeedleColor
        {
            get
            {
                return DialPeak;
            }
            set
            {
                DialPeak = value;
                this.Invalidate();
            }
        }


        /// <summary>
        /// The form type
        /// </summary>
        private MeterScale FormType = MeterScale.Log10;

        /// <summary>
        /// Gets or sets the meter scale.
        /// </summary>
        /// <value>The meter scale.</value>
        [Category("VU Meter")]
        [Description("Display value in analog or logarithmic scale")]
        public MeterScale MeterScale
        {
            get
            {
                return FormType;
            }
            set
            {
                FormType = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the led.
        /// </summary>
        /// <value>The size of the led.</value>
        [Category("VU Meter")]
        [Description("Led size (1 to 72 pixels)")]
        public Size LedSize
        {
            get
            {
                return Led;
            }
            set
            {
                if (value.Height < 1) Led.Height = 1;
                else if (value.Height > 72) Led.Height = 72;
                else Led.Height = value.Height;

                if (value.Width < 1) Led.Width = 1;
                else if (value.Width > 72) Led.Width = 72;
                else Led.Width = value.Width;

                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led space.
        /// </summary>
        /// <value>The led space.</value>
        [Category("VU Meter")]
        [Description("Led spacing (0 to 72 pixels)")]
        public int LedSpace
        {
            get
            {
                return LedSpacing;
            }
            set
            {
                if (value < 0) LedSpacing = 0;
                else if (value > 72) LedSpacing = 72;
                else LedSpacing = value;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vertical bar].
        /// </summary>
        /// <value><c>true</c> if [vertical bar]; otherwise, <c>false</c>.</value>
        [Category("VU Meter")]
        [Description("Led bar is vertical")]
        public bool VerticalBar
        {
            get
            {
                return Vertical;
            }
            set
            {
                Vertical = value;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [Category("VU Meter")]
        [Description("Max value from total LedCount to 65535")]
        public int Maximum
        {
            get
            {
                return Max;
            }
            set
            {
                if (value < (Led1Count + Led2Count + Led3Count)) Max = (Led1Count + Led2Count + Led3Count);
                else if (value > 65535) Max = 65535;
                else Max = value;

                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Category("VU Meter")]
        [Description("The level shown (between Min and Max)")]
        public int Value
        {
            get
            {
                return CurrentValue;
            }

            set
            {
                if (value != CurrentValue)
                {
                    if (value < Min) CurrentValue = Min;
                    else if (value > Max) CurrentValue = Max;
                    else CurrentValue = value;

                    if ((CurrentValue > PeakValue) & (ShowPeak | ShowLedPeakInAnalog))
                    {
                        PeakValue = CurrentValue;
                        timer1.Stop();
                        timer1.Start();
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the peakms.
        /// </summary>
        /// <value>The peakms.</value>
        [Category("VU Meter")]
        [Description("How many mS to hold peak indicator (50 to 10000mS)")]
        public int Peakms
        {
            get
            {
                return PeakHoldTime;
            }
            set
            {
                if (value < 50) PeakHoldTime = 50;
                else if (value > 10000) PeakHoldTime = 10000;
                else PeakHoldTime = value;
                timer1.Interval = PeakHoldTime;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [peak hold].
        /// </summary>
        /// <value><c>true</c> if [peak hold]; otherwise, <c>false</c>.</value>
        [Category("VU Meter")]
        [Description("Use peak indicator")]
        public bool PeakHold
        {
            get
            {
                return ShowPeak;
            }
            set
            {
                ShowPeak = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led1 count.
        /// </summary>
        /// <value>The led1 count.</value>
        [Category("VU Meter")]
        [Description("Number of Leds in first area (0 to 32, default 6) Total Led count must be at least 1")]
        public int Led1Count
        {
            get
            {
                return LedCount1;
            }

            set
            {
                if (value < 0) LedCount1 = 0;
                else if (value > 32) LedCount1 = 32;
                else LedCount1 = value;
                if ((LedCount1 + LedCount2 + LedCount3) < 1) LedCount1 = 1;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led2 count.
        /// </summary>
        /// <value>The led2 count.</value>
        [Category("VU Meter")]
        [Description("Number of Leds in middle area (0 to 32, default 6) Total Led count must be at least 1")]
        public int Led2Count
        {
            get
            {
                return LedCount2;
            }

            set
            {
                if (value < 0) LedCount2 = 0;
                else if (value > 32) LedCount2 = 32;
                else LedCount2 = value;
                if ((LedCount1 + LedCount2 + LedCount3) < 1) LedCount2 = 1;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led3 count.
        /// </summary>
        /// <value>The led3 count.</value>
        [Category("VU Meter")]
        [Description("Number of Leds in last area (0 to 32, default 4) Total Led count must be at least 1")]
        public int Led3Count
        {
            get
            {
                return LedCount3;
            }

            set
            {
                if (value < 0) LedCount3 = 0;
                else if (value > 32) LedCount3 = 32;
                else LedCount3 = value;
                if ((LedCount1 + LedCount2 + LedCount3) < 1) LedCount3 = 1;
                CalcSize();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led1 color on.
        /// </summary>
        /// <value>The led1 color on.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in first area (Led on)")]
        public Color Led1ColorOn
        {
            get
            {
                return LedColorOn1;
            }
            set
            {
                LedColorOn1 = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led2 color on.
        /// </summary>
        /// <value>The led2 color on.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in middle area (Led on)")]
        public Color Led2ColorOn
        {
            get
            {
                return LedColorOn2;
            }
            set
            {
                LedColorOn2 = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led3 color on.
        /// </summary>
        /// <value>The led3 color on.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in last area (Led on)")]
        public Color Led3ColorOn
        {
            get
            {
                return LedColorOn3;
            }
            set
            {
                LedColorOn3 = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led1 color off.
        /// </summary>
        /// <value>The led1 color off.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in first area (Led off)")]
        public Color Led1ColorOff
        {
            get
            {
                return LedColorOff1;
            }
            set
            {
                LedColorOff1 = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led2 color off.
        /// </summary>
        /// <value>The led2 color off.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in middle area (Led off)")]
        public Color Led2ColorOff
        {
            get
            {
                return LedColorOff2;
            }
            set
            {
                LedColorOff2 = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the led3 color off.
        /// </summary>
        /// <value>The led3 color off.</value>
        [Category("VU Meter - Colors")]
        [Description("Color of Leds in last area (Led off)")]
        public Color Led3ColorOff
        {
            get
            {
                return LedColorOff3;
            }
            set
            {
                LedColorOff3 = value;
                this.Invalidate();
            }
        }


        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// The textrendering
        /// </summary>
        private TextRenderingHint textrendering = TextRenderingHint.AntiAlias;

        /// <summary>
        /// Gets or sets the text rendering.
        /// </summary>
        /// <value>The text rendering.</value>
        public TextRenderingHint TextRendering
        {
            get { return textrendering; }
            set
            {
                textrendering = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitVuMeter"/> class.
        /// </summary>
        public ZeroitVuMeter()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            this.Name = "ZeroitVuMeter";
            CalcSize();
            
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = PeakHoldTime;
            timer1.Enabled = false;
            timer1.Tick += new EventHandler(timer1_Tick);
            
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            g.SmoothingMode = Smoothing;
            g.Clear(Parent.BackColor);
            if (MeterAnalog)
            {
                
                g.SmoothingMode = smoothing;
                g.TextRenderingHint = textrendering;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                DrawAnalogBorder(g);
                DrawAnalogDial(g);
            }
            else
            {
                DrawBorder(g);
                DrawLeds(g);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (MeterAnalog)
            {
                base.OnResize(e);
            }
            CalcSize();
            base.OnResize(e);
            this.Invalidate();
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            PeakValue = CurrentValue;
            this.Invalidate();
            timer1.Start();
        }

        /// <summary>
        /// Calculates the size.
        /// </summary>
        private void CalcSize()
        {
            if (MeterAnalog)
            {
                this.Size = new Size(this.Width, (int)(this.Width * 0.8));
            }
            else if (Vertical)
            {
                this.Size = new Size(Led.Width + LedSpacing * 2, (LedCount1 + LedCount2 + LedCount3) * (Led.Height + LedSpacing) + LedSpacing);
            }
            else
            {
                this.Size = new Size((LedCount1 + LedCount2 + LedCount3) * (Led.Width + LedSpacing) + LedSpacing, Led.Height + LedSpacing * 2);
            }
        }


        /// <summary>
        /// Draws the analog dial.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawAnalogDial(Graphics g)
        {
            //Add code to draw "LED:s" by color in Dial (Analog and LED)
            if (UseLedLightInAnalog)
            {
                if (FormType == MeterScale.Log10)
                {
                    calcValue = (int)(Math.Log10((double)CurrentValue / (Max / 10) + 1) * (LedCount1 + LedCount2 + LedCount3));
                    if (ShowLedPeakInAnalog) calcPeak = (int)(Math.Log10((double)PeakValue / (Max / 10) + 1) * (LedCount1 + LedCount2 + LedCount3));
                }

                if (FormType == MeterScale.Analog)
                {
                    calcValue = (int)(((double)CurrentValue / Max) * (LedCount1 + LedCount2 + LedCount3) + 0.5);
                    if (ShowLedPeakInAnalog) calcPeak = (int)(((double)PeakValue / Max) * (LedCount1 + LedCount2 + LedCount3) + 0.5);
                }

                Double DegStep = (DegHigh - DegLow) / (LedCount1 + LedCount2 + LedCount3 - 1);
                double i;
                double SinI, CosI;
                Pen scalePen;
                int lc = 0;
                int LedRadiusStart = (int)(this.Width * 0.6);
                if (!ShowTextInDial) LedRadiusStart = (int)(this.Width * 0.65);
                for (i = DegHigh; i > DegLow - DegStep / 2; i = i - DegStep)
                {
                    if ((lc < calcValue) | (((lc + 1) == calcPeak) & ShowLedPeakInAnalog))
                    {
                        scalePen = new Pen(Led3ColorOn, Led.Width);
                        if (lc < LedCount1 + LedCount2) scalePen = new Pen(Led2ColorOn, Led.Width);
                        if (lc < LedCount1) scalePen = new Pen(Led1ColorOn, Led.Width);
                    }
                    else
                    {
                        scalePen = new Pen(Led3ColorOff, Led.Width);
                        if (lc < LedCount1 + LedCount2) scalePen = new Pen(Led2ColorOff, Led.Width);
                        if (lc < LedCount1) scalePen = new Pen(Led1ColorOff, Led.Width);
                    }

                    lc++;
                    SinI = Math.Sin(i);
                    CosI = Math.Cos(i);
                    g.DrawLine(scalePen, (int)((LedRadiusStart - Led.Height) * SinI + this.Width / 2),
                        (int)((LedRadiusStart - Led.Height) * CosI + this.Height * 0.9),
                        (int)(LedRadiusStart * SinI + this.Width / 2), (int)(LedRadiusStart * CosI + this.Height * 0.9));
                }
            }
            //End of code addition

            if (FormType == MeterScale.Log10)
            {
                calcValue = (int)(Math.Log10((double)CurrentValue / (Max / 10) + 1) * Max);
                if (ShowPeak) calcPeak = (int)(Math.Log10((double)PeakValue / (Max / 10) + 1) * Max);
            }

            if (FormType == MeterScale.Analog)
            {
                calcValue = CurrentValue;
                if (ShowPeak) calcPeak = PeakValue;
            }
            int DialRadiusLow = (int)(this.Width * 0.3f), DialRadiusHigh = (int)(this.Width * 0.65f);

            Pen DialPen = new Pen(DialNeedle, this.Width * 0.01f);
            double DialPos;
            if (calcValue > 0) DialPos = DegHigh - (((double)calcValue / Max) * (DegHigh - DegLow));
            else DialPos = DegHigh;
            Double SinD = Math.Sin(DialPos), CosD = Math.Cos(DialPos);
            g.DrawLine(DialPen, (int)(DialRadiusLow * SinD + this.Width * 0.5),
                (int)(DialRadiusLow * CosD + this.Height * 0.9),
                (int)(DialRadiusHigh * SinD + this.Width * 0.5),
                (int)(DialRadiusHigh * CosD + this.Height * 0.9));

            if (ShowPeak)
            {
                Pen PeakPen = new Pen(DialPeak, this.Width * 0.01f);
                if (calcPeak > 0) DialPos = DegHigh - (((double)calcPeak / Max) * (DegHigh - DegLow));
                else DialPos = DegHigh;
                Double SinP = Math.Sin(DialPos), CosP = Math.Cos(DialPos);
                g.DrawLine(PeakPen, (int)(DialRadiusLow * SinP + this.Width * 0.5),
                    (int)(DialRadiusLow * CosP + this.Height * 0.9),
                    (int)(DialRadiusHigh * SinP + this.Width * 0.5),
                    (int)(DialRadiusHigh * CosP + this.Height * 0.9));
            }
            DialPen.Dispose();
        }

        /// <summary>
        /// Draws the leds.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawLeds(Graphics g)
        {
            if (FormType == MeterScale.Log10)
            {
                calcValue = (int)(Math.Log10((double)CurrentValue / (Max / 10) + 1) * (LedCount1 + LedCount2 + LedCount3));
                if (ShowPeak) calcPeak = (int)(Math.Log10((double)PeakValue / (Max / 10) + 1) * (LedCount1 + LedCount2 + LedCount3));
            }

            if (FormType == MeterScale.Analog)
            {
                calcValue = (int)(((double)CurrentValue / Max) * (LedCount1 + LedCount2 + LedCount3) + 0.5);
                if (ShowPeak) calcPeak = (int)(((double)PeakValue / Max) * (LedCount1 + LedCount2 + LedCount3) + 0.5);
            }


            for (int i = 0; i < (LedCount1 + LedCount2 + LedCount3); i++)
            {

                if (Vertical)
                {
                    Rectangle current = new Rectangle(this.ClientRectangle.X + LedSpacing,
                        this.ClientRectangle.Height - ((i + 1) * (Led.Height + LedSpacing)),
                        Led.Width, Led.Height);

                    if ((i < calcValue) | (((i + 1) == calcPeak) & ShowPeak))
                    {
                        if (i < LedCount1)
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn1), current);
                        }
                        else if (i < (LedCount1 + LedCount2))
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn2), current);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn3), current);
                        }
                    }
                    else
                    {
                        if (i < LedCount1)
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff1), current);
                        }
                        else if (i < (LedCount1 + LedCount2))
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff2), current);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff3), current);
                        }
                    }

                }
                else
                {
                    Rectangle current = new Rectangle(this.ClientRectangle.X + (i * (Led.Width + LedSpacing)) + LedSpacing,
                        this.ClientRectangle.Y + LedSpacing, Led.Width, Led.Height);

                    if ((i) < calcValue | (((i + 1) == calcPeak) & ShowPeak))
                    {
                        if (i < LedCount1)
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn1), current);
                        }
                        else if (i < (LedCount1 + LedCount2))
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn2), current);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(LedColorOn3), current);
                        }
                    }
                    else
                    {
                        if (i < LedCount1)
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff1), current);
                        }
                        else if (i < (LedCount1 + LedCount2))
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff2), current);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(LedColorOff3), current);
                        }
                    }

                }

            }
        }

        /// <summary>
        /// Draws the border.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawBorder(Graphics g)
        {
            Rectangle Border = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
            g.FillRectangle(new SolidBrush(this.BackColor), Border);
        }

        /// <summary>
        /// Draws the analog border.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawAnalogBorder(Graphics g)
        {
            if (!AnalogDialRegionOnly) g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);

            double DegStep = (DegHigh * 1.05 - DegLow / 1.05) / 19;
            double i = DegHigh * 1.05;
            double SinI, CosI;

            PointF[] curvePoints = new PointF[40];
            for (int cp = 0; cp < 20; cp++)
            {
                i = i - DegStep;
                SinI = Math.Sin(i);
                CosI = Math.Cos(i);
                curvePoints[cp] = new PointF((float)(SinI * this.Width * 0.7 + this.Width / 2), (float)(CosI * this.Width * 0.7 + this.Height * 0.9));
                curvePoints[38 - cp] = new PointF((float)(SinI * this.Width * 0.3 + this.Width / 2), (float)(CosI * this.Width * 0.3 + this.Height * 0.9));
            }
            curvePoints[39] = curvePoints[0];
            System.Drawing.Drawing2D.GraphicsPath dialPath = new System.Drawing.Drawing2D.GraphicsPath();
            if (AnalogDialRegionOnly) dialPath.AddPolygon(curvePoints);
            else dialPath.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            this.Region = new System.Drawing.Region(dialPath);
            g.FillPolygon(new SolidBrush(DialBackColor), curvePoints);

            // Test moving this block
            if (!UseLedLightInAnalog)
            {
                DegStep = (DegHigh - DegLow) / (LedCount1 + LedCount2 + LedCount3 - 1);
                int lc = 0;
                int LedRadiusStart = (int)(this.Width * 0.6);
                if (!ShowTextInDial) LedRadiusStart = (int)(this.Width * 0.65);
                for (i = DegHigh; i > DegLow - DegStep / 2; i = i - DegStep)
                {
                    //Graphics scale = g.Graphics;
                    Pen scalePen = new Pen(Led3ColorOn, Led.Width);
                    if (lc < LedCount1 + LedCount2) scalePen = new Pen(Led2ColorOn, Led.Width);
                    if (lc < LedCount1) scalePen = new Pen(Led1ColorOn, Led.Width);
                    lc++;
                    SinI = Math.Sin(i);
                    CosI = Math.Cos(i);
                    g.DrawLine(scalePen, (int)((LedRadiusStart - Led.Height) * SinI + this.Width / 2),
                        (int)((LedRadiusStart - Led.Height) * CosI + this.Height * 0.9),
                        (int)(LedRadiusStart * SinI + this.Width / 2), (int)(LedRadiusStart * CosI + this.Height * 0.9));
                    scalePen.Dispose();
                }
            }
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            float MeterFontSize = this.Font.SizeInPoints;
            if (this.Width > 0) MeterFontSize = MeterFontSize * (float)(this.Width / 100f);
            if (MeterFontSize < 4) MeterFontSize = 4;
            if (MeterFontSize > 72) MeterFontSize = 72;
            Font MeterFont = new Font(this.Font.FontFamily, MeterFontSize);
            g.DrawString(this.MeterText, MeterFont, new SolidBrush(this.ForeColor), this.Width / 2, this.Height * 0.43f, format);

            if (ShowDialText)
            {
                double DialTextStep = (DegHigh - DegLow) / (DialText.Length - 1);
                int dt = 0;
                MeterFontSize = MeterFontSize * 0.6f;
                int TextRadiusStart = (int)(this.Width * 0.64);
                for (i = DegHigh; i > DegLow - DialTextStep / 2; i = i - DialTextStep)
                {
                    //Graphics scale = g.Graphics;
                    Brush dtColor = new SolidBrush(DialTextHigh);
                    StringFormat dtformat = new StringFormat();
                    dtformat.Alignment = StringAlignment.Center;
                    dtformat.LineAlignment = StringAlignment.Center;
                    try
                    {
                        if (int.Parse(DialText[dt]) < 0) dtColor = new SolidBrush(DialTextLow);
                        if (int.Parse(DialText[dt]) == 0) dtColor = new SolidBrush(DialTextNeutral);
                    }
                    catch
                    {
                        dtColor = new SolidBrush(DialTextHigh);
                    }
                    Font dtfont = new Font(this.Font.FontFamily, MeterFontSize);
                    SinI = Math.Sin(i);
                    CosI = Math.Cos(i);
                    g.DrawString(DialText[dt++], dtfont, dtColor, (int)(TextRadiusStart * SinI + this.Width / 2), (int)(TextRadiusStart * CosI + this.Height * 0.9), dtformat);

                }
            }


        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
        #endregion

        

    }

    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitVuMeterDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitVuMeterDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitVuMeterDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitVuMeterSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="properties">The properties for the class of the component.</param>
        protected override void PostFilterProperties(System.Collections.IDictionary properties)
        {
            properties.Remove("AccessibleDescription");
            properties.Remove("AccessibleName");
            properties.Remove("AccessibleRole");
            properties.Remove("BackgroundImage");
            //properties.Remove("BackgroundImageLayout");
            properties.Remove("BorderStyle");
            properties.Remove("Cursor");
            properties.Remove("RightToLeft");
            properties.Remove("UseWaitCursor");
            properties.Remove("AllowDrop");
            properties.Remove("AutoValidate");
            properties.Remove("ContextMenuStrip");
            properties.Remove("Enabled");
            properties.Remove("ImeMode");
            //properties.Remove("TabIndex"); // Don't remove this one or the designer will break
            properties.Remove("TabStop");
            //properties.Remove("Visible");
            properties.Remove("ApplicationSettings");
            properties.Remove("DataBindings");
            properties.Remove("Tag");
            properties.Remove("GenerateMember");
            properties.Remove("Locked");
            //properties.Remove("Modifiers");
            properties.Remove("CausesValidation");
            properties.Remove("Anchor");
            properties.Remove("AutoSize");
            properties.Remove("AutoSizeMode");
            //properties.Remove("Location");
            properties.Remove("Dock");
            properties.Remove("Margin");
            properties.Remove("MaximumSize");
            properties.Remove("MinimumSize");
            properties.Remove("Padding");
            //properties.Remove("Size");
            properties.Remove("DockPadding");
            properties.Remove("AutoScrollMargin");
            properties.Remove("AutoScrollMinSize");
            properties.Remove("AutoScroll");
            properties.Remove("ForeColor");
            //properties.Remove("BackColor");
            properties.Remove("Text");
            //properties.Remove("Font");
        }

        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitVuMeterSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitVuMeterSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitVuMeter colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitVuMeterSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitVuMeterSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitVuMeter;

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
        /// Gets or sets a value indicating whether [use led light].
        /// </summary>
        /// <value><c>true</c> if [use led light]; otherwise, <c>false</c>.</value>
        public bool UseLedLight
        {
            get
            {
                return colUserControl.UseLedLight;
            }
            set
            {
                GetPropertyByName("UseLedLight").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show led peak].
        /// </summary>
        /// <value><c>true</c> if [show led peak]; otherwise, <c>false</c>.</value>
        public bool ShowLedPeak
        {
            get
            {
                return colUserControl.ShowLedPeak;
            }
            set
            {
                GetPropertyByName("ShowLedPeak").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [analog meter].
        /// </summary>
        /// <value><c>true</c> if [analog meter]; otherwise, <c>false</c>.</value>
        public bool AnalogMeter
        {
            get
            {
                return colUserControl.AnalogMeter;
            }
            set
            {
                GetPropertyByName("AnalogMeter").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show text in dial].
        /// </summary>
        /// <value><c>true</c> if [show text in dial]; otherwise, <c>false</c>.</value>
        public bool ShowTextInDial
        {
            get
            {
                return colUserControl.ShowTextInDial;
            }
            set
            {
                GetPropertyByName("ShowTextInDial").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show dial only].
        /// </summary>
        /// <value><c>true</c> if [show dial only]; otherwise, <c>false</c>.</value>
        public bool ShowDialOnly
        {
            get
            {
                return colUserControl.ShowDialOnly;
            }
            set
            {
                GetPropertyByName("ShowDialOnly").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vertical bar].
        /// </summary>
        /// <value><c>true</c> if [vertical bar]; otherwise, <c>false</c>.</value>
        public bool VerticalBar
        {
            get
            {
                return colUserControl.VerticalBar;
            }
            set
            {
                GetPropertyByName("VerticalBar").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [peak hold].
        /// </summary>
        /// <value><c>true</c> if [peak hold]; otherwise, <c>false</c>.</value>
        public bool PeakHold
        {
            get
            {
                return colUserControl.PeakHold;
            }
            set
            {
                GetPropertyByName("PeakHold").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the vu text.
        /// </summary>
        /// <value>The vu text.</value>
        public string VuText
        {
            get
            {
                return colUserControl.VuText;
            }
            set
            {
                GetPropertyByName("VuText").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text in dial.
        /// </summary>
        /// <value>The text in dial.</value>
        public string[] TextInDial
        {
            get
            {
                return colUserControl.TextInDial;
            }
            set
            {
                GetPropertyByName("TextInDial").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dial background.
        /// </summary>
        /// <value>The dial background.</value>
        public Color DialBackground
        {
            get
            {
                return colUserControl.DialBackground;
            }
            set
            {
                GetPropertyByName("DialBackground").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dial text negative.
        /// </summary>
        /// <value>The dial text negative.</value>
        public Color DialTextNegative
        {
            get
            {
                return colUserControl.DialTextNegative;
            }
            set
            {
                GetPropertyByName("DialTextNegative").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dial text zero.
        /// </summary>
        /// <value>The dial text zero.</value>
        public Color DialTextZero
        {
            get
            {
                return colUserControl.DialTextZero;
            }
            set
            {
                GetPropertyByName("DialTextZero").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dial text positive.
        /// </summary>
        /// <value>The dial text positive.</value>
        public Color DialTextPositive
        {
            get
            {
                return colUserControl.DialTextPositive;
            }
            set
            {
                GetPropertyByName("DialTextPositive").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the needle.
        /// </summary>
        /// <value>The color of the needle.</value>
        public Color NeedleColor
        {
            get
            {
                return colUserControl.NeedleColor;
            }
            set
            {
                GetPropertyByName("NeedleColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the peak needle.
        /// </summary>
        /// <value>The color of the peak needle.</value>
        public Color PeakNeedleColor
        {
            get
            {
                return colUserControl.PeakNeedleColor;
            }
            set
            {
                GetPropertyByName("PeakNeedleColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the meter scale.
        /// </summary>
        /// <value>The meter scale.</value>
        public MeterScale MeterScale
        {
            get
            {
                return colUserControl.MeterScale;
            }
            set
            {
                GetPropertyByName("MeterScale").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the led.
        /// </summary>
        /// <value>The size of the led.</value>
        public Size LedSize
        {
            get
            {
                return colUserControl.LedSize;
            }
            set
            {
                GetPropertyByName("LedSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led space.
        /// </summary>
        /// <value>The led space.</value>
        public int LedSpace
        {
            get
            {
                return colUserControl.LedSpace;
            }
            set
            {
                GetPropertyByName("LedSpace").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return colUserControl.Maximum;
            }
            set
            {
                GetPropertyByName("Maximum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
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
        /// Gets or sets the peakms.
        /// </summary>
        /// <value>The peakms.</value>
        public int Peakms
        {
            get
            {
                return colUserControl.Peakms;
            }
            set
            {
                GetPropertyByName("Peakms").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led1 count.
        /// </summary>
        /// <value>The led1 count.</value>
        public int Led1Count
        {
            get
            {
                return colUserControl.Led1Count;
            }
            set
            {
                GetPropertyByName("Led1Count").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led2 count.
        /// </summary>
        /// <value>The led2 count.</value>
        public int Led2Count
        {
            get
            {
                return colUserControl.Led2Count;
            }
            set
            {
                GetPropertyByName("Led2Count").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led3 count.
        /// </summary>
        /// <value>The led3 count.</value>
        public int Led3Count
        {
            get
            {
                return colUserControl.Led3Count;
            }
            set
            {
                GetPropertyByName("Led3Count").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led1 color on.
        /// </summary>
        /// <value>The led1 color on.</value>
        public Color Led1ColorOn
        {
            get
            {
                return colUserControl.Led1ColorOn;
            }
            set
            {
                GetPropertyByName("Led1ColorOn").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led2 color on.
        /// </summary>
        /// <value>The led2 color on.</value>
        public Color Led2ColorOn
        {
            get
            {
                return colUserControl.Led2ColorOn;
            }
            set
            {
                GetPropertyByName("Led2ColorOn").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led3 color on.
        /// </summary>
        /// <value>The led3 color on.</value>
        public Color Led3ColorOn
        {
            get
            {
                return colUserControl.Led3ColorOn;
            }
            set
            {
                GetPropertyByName("Led3ColorOn").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led1 color off.
        /// </summary>
        /// <value>The led1 color off.</value>
        public Color Led1ColorOff
        {
            get
            {
                return colUserControl.Led1ColorOff;
            }
            set
            {
                GetPropertyByName("Led1ColorOff").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led2 color off.
        /// </summary>
        /// <value>The led2 color off.</value>
        public Color Led2ColorOff
        {
            get
            {
                return colUserControl.Led2ColorOff;
            }
            set
            {
                GetPropertyByName("Led2ColorOff").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the led3 color off.
        /// </summary>
        /// <value>The led3 color off.</value>
        public Color Led3ColorOff
        {
            get
            {
                return colUserControl.Led3ColorOff;
            }
            set
            {
                GetPropertyByName("Led3ColorOff").SetValue(colUserControl, value);
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
            
            items.Add(new DesignerActionPropertyItem("AnalogMeter",
                "Analog Meter", "Behaviour",
                "Set to use analog meter."));
            
            items.Add(new DesignerActionPropertyItem("ShowTextInDial",
                "Show Text InDial", "Behaviour",
                "Set to show text in dial."));

            items.Add(new DesignerActionPropertyItem("ShowLedPeak",
                "Show Led Peak", "Behaviour",
                "Set to show led peak."));

            items.Add(new DesignerActionPropertyItem("PeakHold",
                "Peak Hold", "Behaviour",
                "Set to enable peak hold."));

            items.Add(new DesignerActionPropertyItem("VerticalBar",
                "Vertical Bar", "Behaviour",
                "Set to use either vertical/horizontal orientation."));
            
            items.Add(new DesignerActionPropertyItem("UseLedLight",
                "Use Led Light", "Behaviour",
                "Set to use led light."));

            items.Add(new DesignerActionPropertyItem("ShowDialOnly",
                "Show Dial Only", "Behaviour",
                "Set to show dial only."));

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                "Back Color", "Appearance",
                "Set the background color."));

            items.Add(new DesignerActionPropertyItem("NeedleColor",
                "Needle Color", "Appearance",
                "Set Needle color."));

            items.Add(new DesignerActionPropertyItem("PeakNeedleColor",
                "Peak Needle Color", "Appearance",
                "Set Peak Needle color."));

            items.Add(new DesignerActionPropertyItem("DialBackground",
                "Dial Background", "Appearance",
                "Sets the dial background."));
            

            items.Add(new DesignerActionPropertyItem("Led1ColorOn",
                "Led1 On", "Appearance",
                "Set Led-1 color when on."));

            items.Add(new DesignerActionPropertyItem("Led1ColorOff",
                "Led1 Off", "Appearance",
                "Set Led-1 color when off."));

            items.Add(new DesignerActionPropertyItem("Led2ColorOn",
                "Led2 On", "Appearance",
                "Set Led-2 color when on."));

            items.Add(new DesignerActionPropertyItem("Led2ColorOff",
                "Led2 Off", "Appearance",
                "Set Led-2 color when off."));

            items.Add(new DesignerActionPropertyItem("Led3ColorOn",
                "Led3 On", "Appearance",
                "Set Led-3 color when on."));

            items.Add(new DesignerActionPropertyItem("Led3ColorOff",
                "Led3 Off", "Appearance",
                "Set Led-3 color when off."));

            items.Add(new DesignerActionPropertyItem("MeterScale",
                "Meter Scale", "Appearance",
                "Sets the meter scale."));

            items.Add(new DesignerActionPropertyItem("LedSize",
                "Led Size", "Appearance",
                "Sets the led size."));


            items.Add(new DesignerActionPropertyItem("LedSpace",
                "Led Space", "Appearance",
                "Sets the led space."));
            
            items.Add(new DesignerActionPropertyItem("Led1Count",
                "Led1Count", "Appearance",
                "Sets the number of led for Led-1."));


            items.Add(new DesignerActionPropertyItem("Led2Count",
                "Led2Count", "Appearance",
                "Sets the number of led for Led-2."));

            items.Add(new DesignerActionPropertyItem("Led3Count",
                "Led3Count", "Appearance",
                "Sets the number of led for Led-3."));

            items.Add(new DesignerActionPropertyItem("Maximum",
                "Maximum", "Appearance",
                "Sets the maximum value."));
            
            items.Add(new DesignerActionPropertyItem("Value",
                "Value", "Appearance",
                "Sets the value."));


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
