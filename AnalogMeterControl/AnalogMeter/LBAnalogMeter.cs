// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-26-2018
// ***********************************************************************
// <copyright file="LBAnalogMeter.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;

#endregion

namespace Zeroit.Framework.Gauges.AnalogMeterControl.AnalogMeter
{

    #region Control
    /// <summary>
    /// Class for the analog meter control
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitAnalogMeterDesigner))]
    public partial class ZeroitAnalogMeter : UserControl
    {
        #region Enumerator
        /// <summary>
        /// Enum AnalogMeterStyle
        /// </summary>
        public enum AnalogMeterStyle
        {
            /// <summary>
            /// The circular
            /// </summary>
            Circular = 0,
        };
        #endregion

        #region Private Fields
        /// <summary>
        /// The meter style
        /// </summary>
        private AnalogMeterStyle meterStyle;
        /// <summary>
        /// The body color
        /// </summary>
        private Color bodyColor;
        /// <summary>
        /// The needle color
        /// </summary>
        private Color needleColor;
        /// <summary>
        /// The scale color
        /// </summary>
        private Color scaleColor;
        /// <summary>
        /// The view glass
        /// </summary>
        private bool viewGlass;
        /// <summary>
        /// The curr value
        /// </summary>
        private double currValue;
        /// <summary>
        /// The minimum value
        /// </summary>
        private double minValue;
        /// <summary>
        /// The maximum value
        /// </summary>
        private double maxValue;
        /// <summary>
        /// The scale divisions
        /// </summary>
        private int scaleDivisions;
        /// <summary>
        /// The scale sub divisions
        /// </summary>
        private int scaleSubDivisions;
        /// <summary>
        /// The renderer
        /// </summary>
        private LBAnalogMeterRenderer renderer;
        /// <summary>
        /// Enum RendererType
        /// </summary>
        public enum RendererType { Default,Custom}
        /// <summary>
        /// The renderer type
        /// </summary>
        private RendererType rendererType = RendererType.Default;
        #endregion

        #region Class variables
        /// <summary>
        /// The needle center
        /// </summary>
        protected PointF needleCenter;
        /// <summary>
        /// The draw rect
        /// </summary>
        protected RectangleF drawRect;
        /// <summary>
        /// The glossy rect
        /// </summary>
        protected RectangleF glossyRect;
        /// <summary>
        /// The needle cover rect
        /// </summary>
        protected RectangleF needleCoverRect;
        /// <summary>
        /// The start angle
        /// </summary>
        protected float startAngle;
        /// <summary>
        /// The end angle
        /// </summary>
        protected float endAngle;
        /// <summary>
        /// The draw ratio
        /// </summary>
        protected float drawRatio;
        /// <summary>
        /// The default renderer
        /// </summary>
        protected LBAnalogMeterRenderer defaultRenderer;
        /// <summary>
        /// The custom renderer
        /// </summary>
        protected LBAnalogMeterRenderer customRenderer;
        #endregion

        #region Costructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAnalogMeter"/> class.
        /// </summary>
        public ZeroitAnalogMeter()
        {
            // Initialization
            InitializeComponent();

            // Properties initialization
            this.bodyColor = Color.Red;
            this.needleColor = Color.Yellow;
            this.scaleColor = Color.White;
            this.meterStyle = AnalogMeterStyle.Circular;
            this.viewGlass = false;
            this.startAngle = 135;
            this.endAngle = 405;
            this.minValue = 0;
            this.maxValue = 1;
            this.currValue = 0;
            this.scaleDivisions = 10;
            this.scaleSubDivisions = 10;
            this.renderer = null;

            // Set the styles for drawing
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            // Create the default renderer
            this.defaultRenderer = new LBDefaultAnalogMeterRenderer();
            this.defaultRenderer.AnalogMeter = this;

            // Create the default renderer
            this.customRenderer = new LBCustomAnalogMeterRenderer();
            this.customRenderer.AnalogMeter = this;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the renderers.
        /// </summary>
        /// <value>The renderers.</value>
        public RendererType Renderers
        {
            get {return rendererType; }
            set
            {
                rendererType = value;
                ScaleColor = Color.White;

                if (value == RendererType.Custom)
                {
                    Renderer = customRenderer;
                    MaxValue = 100;
                    MinValue = 0;
                    ScaleDivisions = 11;
                    ScaleSubDivisions = 5;
                    NeedleColor = Color.Red;
                    ScaleColor = Color.DarkRed;
                }
                

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the meter style.
        /// </summary>
        /// <value>The meter style.</value>
        [
            Category("Appearance"),
            Description("Style of the control")
        ]
        public AnalogMeterStyle MeterStyle
        {
            get { return meterStyle; }
            set
            {
                meterStyle = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the body.
        /// </summary>
        /// <value>The color of the body.</value>
        [
            Category("Appearance"),
            Description("Color of the body of the control")
        ]
        public Color BodyColor
        {
            get { return bodyColor; }
            set
            {
                bodyColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the needle.
        /// </summary>
        /// <value>The color of the needle.</value>
        [
            Category("Appearance"),
            Description("Color of the needle")
        ]
        public Color NeedleColor
        {
            get { return needleColor; }
            set
            {
                needleColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [view glass].
        /// </summary>
        /// <value><c>true</c> if [view glass]; otherwise, <c>false</c>.</value>
        [
            Category("Appearance"),
            Description("Show or hide the glass effect")
        ]
        public bool ViewGlass
        {
            get { return viewGlass; }
            set
            {
                viewGlass = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the scale.
        /// </summary>
        /// <value>The color of the scale.</value>
        [
            Category("Appearance"),
            Description("Color of the scale of the control")
        ]
        public Color ScaleColor
        {
            get { return scaleColor; }
            set
            {
                scaleColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [
            Category("Behavior"),
            Description("Value of the data")
        ]
        public double Value
        {
            get { return currValue; }
            set
            {
                double val = value;
                if (val > maxValue)
                    val = maxValue;

                if (val < minValue)
                    val = minValue;

                currValue = val;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [
            Category("Behavior"),
            Description("Minimum value of the data")
        ]
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [
            Category("Behavior"),
            Description("Maximum value of the data")
        ]
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the scale divisions.
        /// </summary>
        /// <value>The scale divisions.</value>
        [
            Category("Appearance"),
            Description("Number of the scale divisions")
        ]
        public int ScaleDivisions
        {
            get { return scaleDivisions; }
            set
            {

                scaleDivisions = value;
                CalculateDimensions();
                Invalidate();
                                
            }
        }


        /// <summary>
        /// Gets or sets the scale sub divisions.
        /// </summary>
        /// <value>The scale sub divisions.</value>
        [
            Category("Appearance"),
            Description("Number of the scale subdivisions")
        ]
        public int ScaleSubDivisions
        {
            get { return scaleSubDivisions; }
            set
            {
                scaleSubDivisions = value;
                CalculateDimensions();
                Invalidate();
                //switch (rendererType)
                //{
                //    case RendererType.Default:
                //        //this.Renderer = defaultRenderer;
                //        scaleSubDivisions = value;
                //        CalculateDimensions();
                //        Invalidate();
                //        break;
                //    case RendererType.Custom:
                //        //this.Renderer = customRenderer;
                //        scaleSubDivisions = 5;
                //        CalculateDimensions();
                //        Invalidate();
                //        break;
                //    default:
                //        break;
                //}
                
            }
        }

        /// <summary>
        /// Gets or sets the renderer.
        /// </summary>
        /// <value>The renderer.</value>
        [Browsable(false)]
        public LBAnalogMeterRenderer Renderer
        {
            get { return this.renderer; }
            set
            {
                this.renderer = value;
                if (this.renderer != null)
                    renderer.AnalogMeter = this;
                Invalidate();
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the draw ratio.
        /// </summary>
        /// <returns>System.Single.</returns>
        public float GetDrawRatio()
        {
            return this.drawRatio;
        }

        /// <summary>
        /// Gets the start angle.
        /// </summary>
        /// <returns>System.Single.</returns>
        public float GetStartAngle()
        {
            return this.startAngle;
        }

        /// <summary>
        /// Gets the end angle.
        /// </summary>
        /// <returns>System.Single.</returns>
        public float GetEndAngle()
        {
            return this.endAngle;
        }

        /// <summary>
        /// Gets the needle center.
        /// </summary>
        /// <returns>PointF.</returns>
        public PointF GetNeedleCenter()
        {
            return this.needleCenter;
        }

        //public bool DrawDivisions(Graphics Gr, RectangleF rc)
        //{
        //    ScaleSubDivisions = 5;

        //    if (this == null)
        //        return false;

        //    PointF needleCenter = this.GetNeedleCenter();
        //    float startAngle = this.GetStartAngle();
        //    float endAngle = this.GetEndAngle();
        //    float scaleDivisions = this.ScaleDivisions;
        //    float scaleSubDivisions = this.ScaleSubDivisions;
        //    float drawRatio = this.GetDrawRatio();
        //    double minValue = this.MinValue;
        //    double maxValue = this.MaxValue;
        //    Color scaleColor = this.ScaleColor;

        //    float cx = needleCenter.X;
        //    float cy = needleCenter.Y;
        //    float w = rc.Width;
        //    float h = rc.Height;

        //    float incr = GetRadian((endAngle - startAngle) / ((scaleDivisions - 1) * (scaleSubDivisions + 1)));
        //    float currentAngle = GetRadian(startAngle);
        //    float radius = (float)(w / 2 - (w * 0.08));
        //    float rulerValue = (float)minValue;

        //    Pen pen = new Pen(scaleColor, (2 * drawRatio));
        //    SolidBrush br = new SolidBrush(scaleColor);

        //    PointF ptStart = new PointF(0, 0);
        //    PointF ptEnd = new PointF(0, 0);
        //    PointF ptCenter = new PointF(0, 0);
        //    RectangleF rcTick = new RectangleF(0, 0, 0, 0);
        //    SizeF sizeMax = new SizeF(10 * drawRatio, 10 * drawRatio);
        //    SizeF sizeMin = new SizeF(4 * drawRatio, 4 * drawRatio);

        //    int n = 0;
        //    for (; n < scaleDivisions; n++)
        //    {
        //        //Draw Thick Line
        //        ptCenter.X = (float)(cx + (radius - w / 70) * Math.Cos(currentAngle));
        //        ptCenter.Y = (float)(cy + (radius - w / 70) * Math.Sin(currentAngle));
        //        ptStart.X = ptCenter.X - (5 * drawRatio);
        //        ptStart.Y = ptCenter.Y - (5 * drawRatio);
        //        rcTick.Location = ptStart;
        //        rcTick.Size = sizeMax;
        //        Gr.FillEllipse(br, rcTick);

        //        //Draw Strings
        //        Font font = new Font(this.Font.FontFamily, (float)(8F * drawRatio), FontStyle.Italic);

        //        float tx = (float)(cx + (radius - (20 * drawRatio)) * Math.Cos(currentAngle));
        //        float ty = (float)(cy + (radius - (20 * drawRatio)) * Math.Sin(currentAngle));
        //        double val = Math.Round(rulerValue);
        //        String str = String.Format("{0,0:D}", (int)val);

        //        SizeF size = Gr.MeasureString(str, font);
        //        Gr.DrawString(str,
        //                        font,
        //                        br,
        //                        tx - (float)(size.Width * 0.5),
        //                        ty - (float)(size.Height * 0.5));

        //        rulerValue += (float)((maxValue - minValue) / (scaleDivisions - 1));

        //        if (n == scaleDivisions - 1)
        //            break;

        //        if (scaleDivisions <= 0)
        //            currentAngle += incr;
        //        else
        //        {
        //            for (int j = 0; j <= scaleSubDivisions; j++)
        //            {
        //                currentAngle += incr;

        //                ptCenter.X = (float)(cx + (radius - w / 70) * Math.Cos(currentAngle));
        //                ptCenter.Y = (float)(cy + (radius - w / 70) * Math.Sin(currentAngle));
        //                ptStart.X = ptCenter.X - (2 * drawRatio);
        //                ptStart.Y = ptCenter.Y - (2 * drawRatio);
        //                rcTick.Location = ptStart;
        //                rcTick.Size = sizeMin;
        //                Gr.FillEllipse(br, rcTick);
        //            }
        //        }
        //    }

        //    return true;
        //}

        /// <summary>
        /// Gets the radian.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>System.Single.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private float GetRadian(float v)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Events delegates
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // Calculate dimensions
            CalculateDimensions();

            this.Invalidate();
        }

        

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            RectangleF _rc = new RectangleF(0, 0, this.Width, this.Height);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (/*this.renderer == null*/ rendererType == RendererType.Default)
            {

                this.defaultRenderer.DrawBackground(e.Graphics, _rc, AllowTransparency);
                this.defaultRenderer.DrawBody(e.Graphics, drawRect);
                this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);
                
                //switch (rendererType)
                //{
                //    case RendererType.Default:
                //        //this.Renderer = defaultRenderer;
                //        this.defaultRenderer.DrawBackground(e.Graphics, _rc);
                //        this.defaultRenderer.DrawBody(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                //        this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                //        this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);
                //        break;
                //    case RendererType.Custom:
                //        //this.Renderer = customRenderer;
                //        //e.Graphics.Clear(BackColor);
                //        this.customRenderer.DrawBackground(e.Graphics, _rc);
                //        this.customRenderer.DrawBody(e.Graphics, drawRect);
                //        this.customRenderer.DrawThresholds(e.Graphics, drawRect);
                //        this.customRenderer.DrawDivisions(e.Graphics, drawRect);
                //        this.customRenderer.DrawUM(e.Graphics, drawRect);
                //        this.customRenderer.DrawValue(e.Graphics, drawRect);
                //        this.customRenderer.DrawNeedle(e.Graphics, drawRect);
                //        this.customRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                //        this.customRenderer.DrawGlass(e.Graphics, this.glossyRect);
                //        break;
                //    default:
                //        break;
                //}
                
            }
            else
            {
                if (this.Renderer.DrawBackground(e.Graphics, _rc,AllowTransparency) == false)
                    this.defaultRenderer.DrawBackground(e.Graphics, _rc);
                if (this.Renderer.DrawBody(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawBody(e.Graphics, drawRect);
                if (this.Renderer.DrawThresholds(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                if (this.Renderer.DrawDivisions(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                if (this.Renderer.DrawUM(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                if (this.Renderer.DrawValue(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                if (this.Renderer.DrawNeedle(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                if (this.Renderer.DrawNeedleCover(e.Graphics, this.needleCoverRect) == false)
                    this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                if (this.Renderer.DrawGlass(e.Graphics, this.glossyRect) == false)
                    this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);


                //switch (rendererType)
                //{
                //    case RendererType.Default:
                //        //this.Renderer = defaultRenderer;
                //        if (this.Renderer.DrawBackground(e.Graphics, _rc) == false)
                //            this.defaultRenderer.DrawBackground(e.Graphics, _rc);
                //        if (this.Renderer.DrawBody(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawBody(e.Graphics, drawRect);
                //        if (this.Renderer.DrawThresholds(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                //        if (this.Renderer.DrawDivisions(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                //        if (this.Renderer.DrawUM(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                //        if (this.Renderer.DrawValue(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                //        if (this.Renderer.DrawNeedle(e.Graphics, drawRect) == false)
                //            this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                //        if (this.Renderer.DrawNeedleCover(e.Graphics, this.needleCoverRect) == false)
                //            this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                //        if (this.Renderer.DrawGlass(e.Graphics, this.glossyRect) == false)
                //            this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);
                //        break;
                //    case RendererType.Custom:
                //        //this.Renderer = customRenderer;
                //        //e.Graphics.Clear(BackColor);
                //        if (this.Renderer.DrawBackground(e.Graphics, _rc) == false)
                //            this.customRenderer.DrawBackground(e.Graphics, _rc);
                //        if (this.Renderer.DrawBody(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawBody(e.Graphics, drawRect);
                //        if (this.Renderer.DrawThresholds(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawThresholds(e.Graphics, drawRect);
                //        if (this.Renderer.DrawDivisions(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawDivisions(e.Graphics, drawRect);
                //        if (this.Renderer.DrawUM(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawUM(e.Graphics, drawRect);
                //        if (this.Renderer.DrawValue(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawValue(e.Graphics, drawRect);
                //        if (this.Renderer.DrawNeedle(e.Graphics, drawRect) == false)
                //            this.customRenderer.DrawNeedle(e.Graphics, drawRect);
                //        if (this.Renderer.DrawNeedleCover(e.Graphics, this.needleCoverRect) == false)
                //            this.customRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                //        if (this.Renderer.DrawGlass(e.Graphics, this.glossyRect) == false)
                //            this.customRenderer.DrawGlass(e.Graphics, this.glossyRect);
                //        break;
                //    default:
                //        break;
                //}
                
            }
        }
        #endregion

        #region Virtual functions		
        /// <summary>
        /// Calculates the dimensions.
        /// </summary>
        protected virtual void CalculateDimensions()
        {
            // Rectangle
            float x, y, w, h;
            x = 0;
            y = 0;
            w = this.Size.Width;
            h = this.Size.Height;

            // Calculate ratio
            drawRatio = (Math.Min(w, h)) / 200;
            if (drawRatio == 0.0)
                drawRatio = 1;

            // Draw rectangle
            drawRect.X = x;
            drawRect.Y = y;
            drawRect.Width = w - 2;
            drawRect.Height = h - 2;

            if (w < h)
                drawRect.Height = w;
            else if (w > h)
                drawRect.Width = h;

            if (drawRect.Width < 10)
                drawRect.Width = 10;
            if (drawRect.Height < 10)
                drawRect.Height = 10;

            // Calculate needle center
            needleCenter.X = drawRect.X + (drawRect.Width / 2);
            needleCenter.Y = drawRect.Y + (drawRect.Height / 2);

            // Needle cover rect
            needleCoverRect.X = needleCenter.X - (20 * drawRatio);
            needleCoverRect.Y = needleCenter.Y - (20 * drawRatio);
            needleCoverRect.Width = 40 * drawRatio;
            needleCoverRect.Height = 40 * drawRatio;

            // Glass effect rect
            glossyRect.X = drawRect.X + (20 * drawRatio);
            glossyRect.Y = drawRect.Y + (10 * drawRatio);
            glossyRect.Width = drawRect.Width - (40 * drawRatio);
            glossyRect.Height = needleCenter.Y + (30 * drawRatio);
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

    #region Designer Generated Code

    public partial class ZeroitAnalogMeter
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // UserControl1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ZeroitAnalogMeter";
        }
    }

    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitAnalogMeterDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitAnalogMeterDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitAnalogMeterDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitAnalogMeterSmartTagActionList(this.Component));
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
    /// Class ZeroitAnalogMeterSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitAnalogMeterSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAnalogMeter colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAnalogMeterSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitAnalogMeterSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAnalogMeter;

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
        /// Gets or sets the renderers.
        /// </summary>
        /// <value>The renderers.</value>
        public ZeroitAnalogMeter.RendererType Renderers
        {
            get
            {
                return colUserControl.Renderers;
            }
            set
            {
                GetPropertyByName("Renderers").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the meter style.
        /// </summary>
        /// <value>The meter style.</value>
        public ZeroitAnalogMeter.AnalogMeterStyle MeterStyle
        {
            get
            {
                return colUserControl.MeterStyle;
            }
            set
            {
                GetPropertyByName("MeterStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the body.
        /// </summary>
        /// <value>The color of the body.</value>
        public Color BodyColor
        {
            get
            {
                return colUserControl.BodyColor;
            }
            set
            {
                GetPropertyByName("BodyColor").SetValue(colUserControl, value);
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
        /// Gets or sets a value indicating whether [view glass].
        /// </summary>
        /// <value><c>true</c> if [view glass]; otherwise, <c>false</c>.</value>
        public bool ViewGlass
        {
            get
            {
                return colUserControl.ViewGlass;
            }
            set
            {
                GetPropertyByName("ViewGlass").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the scale.
        /// </summary>
        /// <value>The color of the scale.</value>
        public Color ScaleColor
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public double Value
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
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public double MinValue
        {
            get
            {
                return colUserControl.MinValue;
            }
            set
            {
                GetPropertyByName("MinValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public double MaxValue
        {
            get
            {
                return colUserControl.MaxValue;
            }
            set
            {
                GetPropertyByName("MaxValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale divisions.
        /// </summary>
        /// <value>The scale divisions.</value>
        public int ScaleDivisions
        {
            get
            {
                return colUserControl.ScaleDivisions;
            }
            set
            {
                GetPropertyByName("ScaleDivisions").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale sub divisions.
        /// </summary>
        /// <value>The scale sub divisions.</value>
        public int ScaleSubDivisions
        {
            get
            {
                return colUserControl.ScaleSubDivisions;
            }
            set
            {
                GetPropertyByName("ScaleSubDivisions").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("ViewGlass",
                "View Glass", "Appearance",
                "Sets the view glass."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("Renderers",
                                 "Renderers", "Appearance",
                                 "Sets the type of renderer."));

            items.Add(new DesignerActionPropertyItem("MeterStyle",
                                 "Meter Style", "Appearance",
                                 "Sets the style of the meter."));

            items.Add(new DesignerActionPropertyItem("BodyColor",
                "Body Color", "Appearance",
                "Sets the main color."));

            items.Add(new DesignerActionPropertyItem("NeedleColor",
                "Needle Color", "Appearance",
                "Sets the color of the needle."));

            
            items.Add(new DesignerActionPropertyItem("ScaleColor",
                "Scale Color", "Appearance",
                "Sets the scale color."));

            items.Add(new DesignerActionPropertyItem("Value",
                "Value", "Appearance",
                "Sets the value."));

            items.Add(new DesignerActionPropertyItem("MinValue",
                "Minimum Value", "Appearance",
                "Sets the minimum value."));

            items.Add(new DesignerActionPropertyItem("MaxValue",
                "Maximum Value", "Appearance",
                "Sets the Maximum value."));

            items.Add(new DesignerActionPropertyItem("ScaleDivisions",
                "Scale Divisions", "Appearance",
                "Sets the divisions."));

            items.Add(new DesignerActionPropertyItem("ScaleSubDivisions",
                "Scale Sub-Divisions", "Appearance",
                "Sets the sub-divisions."));
            
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
