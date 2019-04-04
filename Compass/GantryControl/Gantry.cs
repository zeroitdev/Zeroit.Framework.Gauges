// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Gantry.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;

#endregion

namespace Zeroit.Framework.Gauges.Compass.GantryControl
{

    #region Control

    /// <summary>
    /// Class ZeroitAsanaGantry.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitAsanaGantryDesigner))]
    public partial class ZeroitAsanaGantry : UserControl
    {

        #region Private Fields

        /// <summary>
        /// The cc
        /// </summary>
        private ZeroitAsanaCompass cc = new ZeroitAsanaCompass();
        /// <summary>
        /// The tube ms
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet tubeMs;
        /// <summary>
        /// The tube marker
        /// </summary>
        private ZeroitAsanaCompass.Marker tubeMarker;
        /// <summary>
        /// The panel marker
        /// </summary>
        private ZeroitAsanaCompass.Marker panelMarker;
        /// <summary>
        /// The xray marker
        /// </summary>
        private ZeroitAsanaCompass.Marker xrayMarker;

        /// <summary>
        /// The stage ms
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet stageMs;
        /// <summary>
        /// The stage marker
        /// </summary>
        private ZeroitAsanaCompass.Marker stageMarker;


        /// <summary>
        /// The tick color
        /// </summary>
        private Color tickColor = Color.Gray;
        /// <summary>
        /// The ticks
        /// </summary>
        private int ticks = 12;
        /// <summary>
        /// The tick size
        /// </summary>
        private float tickSize = 0.2f;
        /// <summary>
        /// The tick start
        /// </summary>
        private float tickStart = 0.35f;
        /// <summary>
        /// The tick thickness
        /// </summary>
        private float tickThickness = 0.5f;
        /// <summary>
        /// The tick per tick
        /// </summary>
        private int tickPerTick = 0;

        /// <summary>
        /// The angle wraps
        /// </summary>
        private bool angleWraps = false;
        /// <summary>
        /// The angle maximum
        /// </summary>
        private float angleMax = 460.0f;
        /// <summary>
        /// The angle minimum
        /// </summary>
        private float angleMin = -100.0f;

        /// <summary>
        /// The text item
        /// </summary>
        private TextItem textItem = new TextItem();
        /// <summary>
        /// The ring
        /// </summary>
        private Ring ring = new Ring();
        /// <summary>
        /// The stage
        /// </summary>
        private Stage stage = new Stage();
        /// <summary>
        /// The x ray
        /// </summary>
        private Xray xRay = new Xray();
        /// <summary>
        /// The pan marker
        /// </summary>
        private PanelMarker panMarker = new PanelMarker();
        /// <summary>
        /// The tub marker
        /// </summary>
        private TubeMarker tubMarker = new TubeMarker();

        /// <summary>
        /// The marker set float angle
        /// </summary>
        private float markerSetFloatAngle = -90f;
        /// <summary>
        /// The tubemarker set float angle
        /// </summary>
        private float tubemarkerSetFloatAngle = 90f;

        #endregion

        #region Public Properties

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ZeroitAsanaCompass Compass
        {
            get { return cc; }
            set
            {
                cc = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [angle wraps].
        /// </summary>
        /// <value><c>true</c> if [angle wraps]; otherwise, <c>false</c>.</value>
        public bool AngleWraps
        {
            get { return angleWraps; }
            set
            {
                angleWraps = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public float Maximum
        {
            get { return angleMax; }
            set
            {
                angleMax = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public float Minimum
        {
            get { return angleMin; }
            set
            {
                angleMin = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the ticks.
        /// </summary>
        /// <value>The ticks.</value>
        public int Ticks
        {
            get { return ticks; }
            set
            {
                ticks = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the tick.
        /// </summary>
        /// <value>The size of the tick.</value>
        public float TickSize
        {
            get { return tickSize; }
            set
            {
                tickSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the tick start.
        /// </summary>
        /// <value>The tick start.</value>
        public float TickStart
        {
            get { return tickStart; }
            set
            {
                tickStart = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the tick thickness.
        /// </summary>
        /// <value>The tick thickness.</value>
        public float TickThickness
        {
            get { return tickThickness; }
            set
            {
                tickThickness = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the tick per tick.
        /// </summary>
        /// <value>The tick per tick.</value>
        public int TickPerTick
        {
            get { return tickPerTick; }
            set
            {
                tickPerTick = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the tick.
        /// </summary>
        /// <value>The color of the tick.</value>
        public Color TickColor
        {
            get { return tickColor; }
            set
            {
                tickColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the stage angle.
        /// </summary>
        /// <value>The stage angle.</value>
        public float StageAngle
        {
            get { return markerSetFloatAngle; }
            set
            {
                markerSetFloatAngle = value;
                Invalidate();
            }
        }

        public int Max { get; set; } = 100;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get { return tubemarkerSetFloatAngle; }
            set
            {
                tubemarkerSetFloatAngle = value;
                GantryParameters();
                this.OnValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text item.
        /// </summary>
        /// <value>The text item.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TextItem TextItem
        {
            get { return textItem; }
            set
            {
                textItem = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the ring.
        /// </summary>
        /// <value>The ring.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Ring Ring
        {
            get { return ring; }
            set
            {
                ring = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        /// <value>The stage.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Stage Stage
        {
            get { return stage; }
            set
            {
                stage = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the x ray.
        /// </summary>
        /// <value>The x ray.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Xray XRay
        {
            get { return xRay; }
            set
            {
                xRay = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the panel marker.
        /// </summary>
        /// <value>The panel marker.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PanelMarker PanelMarker
        {
            get { return panMarker; }
            set
            {
                panMarker = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the tube marker.
        /// </summary>
        /// <value>The tube marker.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TubeMarker TubeMarker
        {
            get { return tubMarker; }
            set
            {
                tubMarker = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The angle
        /// </summary>
        private float angle = 0;

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>The angle.</value>
        public float Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                this.OnValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaGantry"/> class.
        /// </summary>
        public ZeroitAsanaGantry()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

            // Initialize MainForm
            this.ClientSize = new System.Drawing.Size(200, 200);


            // Enable key preview
            //this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(OnKeyPress);

            GantryParameters();

            // Install angle change handler
            cc.AngleChanged += new ZeroitAsanaCompass.AngleChangedHandler(OnAngleChange);
            
            // Add to mainform
            this.Controls.Add(cc);

            UpdateTitle();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gantries the parameters.
        /// </summary>
        private void GantryParameters()
        {
            
            // Create and initialize CircleControl
            //cc = new ZeroitAsanaCompass();

            cc.Dock = System.Windows.Forms.DockStyle.Fill;
            cc.Location = new System.Drawing.Point(0, 0);
            cc.Size = new System.Drawing.Size(200, 200);
            cc.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            cc.TabIndex = 0;
            //cc.BackColor = Color.LightYellow;
            //cc.FixedBackground = true;

            // Major tick every 30 degrees
            // No minor ticks
            cc.MajorTickColor = TickColor;
            cc.MajorTicks = Ticks;
            cc.MajorTickSize = TickSize;
            cc.MajorTickStart = TickStart;
            cc.MajorTickThickness = TickThickness;
            cc.MinorTicksPerMajorTick = TickPerTick;

            // Angle wrapping disabled
            // 560 degrees of motion
            cc.AngleWraps = AngleWraps;
            cc.AngleMax = Maximum;
            cc.AngleMin = Minimum;

            cc.ShowText = true;
            cc.ShowAngle = false;
            cc.Font = Font;
            
            // Clear existing marker sets, text items, and rings
            cc.MarkerSets.Clear();
            cc.TextItems.Clear();
            cc.Rings.Clear();


            // Market set #1 - tube, panel and x-ray beam
            // Tube can be dragged, panel and x-ray beam cannot
            // Angle of marker set matches angle of tube

            

            tubeMarker = new ZeroitAsanaCompass.Marker(
                ZeroitAsanaCompass.MakeArgb(0.8f, TubeMarker.InsideColor),
                TubeMarker.BorderColor, // border color
                TubeMarker.BorderThickness, // border thickness
                TubeMarker.TubePoly.ToArray(),
                TubeMarker.OffsetAngle, // angle offset
                TubeMarker.DragButton);

            

            panelMarker = new ZeroitAsanaCompass.Marker(
                ZeroitAsanaCompass.MakeArgb(0.8f, PanelMarker.InsideColor), // inside color
                PanelMarker.BorderColor, // border color
                PanelMarker.BorderThickness, // border thickness
                PanelMarker.PanelPoly.ToArray(),
                PanelMarker.OffsetAngle, // 180 degrees offset from tube
                PanelMarker.DragButton); // canNOT drag

            

            Color xrayColorAtTube = ZeroitAsanaCompass.MakeArgb(0.8f, XRay.TubeColor);
            Color xrayColorAtPanel = ZeroitAsanaCompass.MakeArgb(0.3f, XRay.PanelColor);

            xrayMarker = new ZeroitAsanaCompass.Marker(
                XRay.Start, //Start Point
                XRay.End, //End Point
                xrayColorAtTube, //Tube Color (colors[5])
                xrayColorAtPanel,//Panel Color (colors[6])
                XRay.BorderColor, // border color
                XRay.BorderThickness, // border thickness (NONE)
                XRay.XRayPoly.ToArray(),
                XRay.OffsetAngle, // same angle as tube
                XRay.DragButton, // canNOT drag
                XRay.Visible); // initially NOT visible

            tubeMs = new ZeroitAsanaCompass.MarkerSet(Value);
            tubeMs.Add(tubeMarker);
            tubeMs.Add(panelMarker);
            tubeMs.Add(xrayMarker);
            cc.MarkerSets.Add(tubeMs);

            angle = tubeMs.Angle;
            // Marker set #2 - stage
            // Cannot be dragged
            // Does not move, so set IncludeInFixedBackground to true

            

            stageMarker = new ZeroitAsanaCompass.Marker(
                    Stage.Pattern, // hatched pattern in background
                    Stage.ForeColor, // foreground color
                    Stage.BackgroundColor, // background color
                    Stage.BorderColor, // border color
                    Stage.BorderThickness, // border thickness
                    Stage.StagePoly.ToArray(),
                    Stage.OffsetAngle,
                    Stage.DragButton, // canNOT drag
                    Stage.Visible); // visible

            stageMs = new ZeroitAsanaCompass.MarkerSet(StageAngle);
            stageMs.Add(stageMarker);
            stageMs.IncludeInFixedBackground = Stage.Fixed;
            cc.MarkerSets.Add(stageMs);

            // Text marker below stage
            cc.TextItems.Add(TextItem.Font,
                TextItem.Color,
                TextItem.Size, // size
                TextItem.Text,
                TextItem.Position, // position
                TextItem.Angle); // angle


            // Simple ring
            cc.Rings.Add(Ring.Size, Ring.BackgroundColor, Ring.BorderColor, Ring.BorderSize);

            
        }

        /// <summary>
        /// Updates the title.
        /// </summary>
        private void UpdateTitle()
        {
            Text = string.Format("Angle {0:F1} - x-ray is {1}", tubeMs.Angle, xrayMarker.Visible ? "ON" : "OFF");
        }

        #endregion

        #region Events and Overrides

        /// <summary>
        /// Handles the <see cref="E:KeyPress" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            xrayMarker.Visible = !xrayMarker.Visible;
            UpdateTitle();
            e.Handled = true;
        }

        /// <summary>
        /// Called when [angle change].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OnAngleChange(object sender, ZeroitAsanaCompass.AngleChangedArgs e)
        {
            UpdateTitle();
            //GantryParameters();
            this.OnValueChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GantryParameters();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            
            GantryParameters();

            
        }

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




        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GantryParameters();
        }

        #endregion

        #region Event Creation

        /////Implement this in the Property you want to trigger the event///////////////////////////
        // 
        //  For Example this will be triggered by the Value Property
        //
        //  public int Value
        //   { 
        //      get { return _value;}
        //      set
        //         {
        //          
        //              _value = value;
        //
        //              this.OnValueChanged(EventArgs.Empty);
        //              Invalidate();
        //          }
        //    }
        //
        ////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// The on value changed
        /// </summary>
        private EventHandler onValueChanged;

        /// <summary>
        /// Triggered when the Value changes
        /// </summary>

        public event EventHandler ValueChanged
        {
            add
            {
                this.onValueChanged = this.onValueChanged + value;
            }
            remove
            {
                this.onValueChanged = this.onValueChanged - value;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:ValueChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.onValueChanged == null)
                return;
            this.onValueChanged((object)this, e);
        }

        #endregion

    }

    #endregion

    /// <summary>
    /// Class TubeMarker.
    /// </summary>
    public class TubeMarker
    {

        /// <summary>
        /// The tube poly
        /// </summary>
        List<PointF> tubePoly = new List<PointF>()
        {
                new PointF(0.650f, 0.025f), new PointF(0.700f, 0.025f),
                new PointF(0.750f, 0.050f), new PointF(0.800f, 0.050f),
                new PointF(0.800f, 0.125f), new PointF(0.825f, 0.150f),
                new PointF(0.875f, 0.150f), new PointF(0.900f, 0.125f),
                new PointF(0.900f, -0.125f), new PointF(0.875f, -0.150f),
                new PointF(0.825f, -0.150f), new PointF(0.800f, -0.125f),
                new PointF(0.800f, -0.050f), new PointF(0.750f, -0.050f),
                new PointF(0.700f, -0.025f), new PointF(0.650f, -0.025f)
        };

        /// <summary>
        /// Gets or sets the tube poly.
        /// </summary>
        /// <value>The tube poly.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<PointF> TubePoly
        {
            get { return tubePoly; }
            set
            {
                tubePoly = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the inside.
        /// </summary>
        /// <value>The color of the inside.</value>
        public Color InsideColor { get; set; } = Color.Crimson;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Black;
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public float BorderThickness { get; set; } = 2f;
        /// <summary>
        /// Gets or sets the offset angle.
        /// </summary>
        /// <value>The offset angle.</value>
        public float OffsetAngle { get; set; } = 0f;
        /// <summary>
        /// Gets or sets the drag button.
        /// </summary>
        /// <value>The drag button.</value>
        public MouseButtons DragButton { get; set; } = MouseButtons.Left;

    }

    /// <summary>
    /// Class PanelMarker.
    /// </summary>
    public class PanelMarker
    {


        /// <summary>
        /// The panel poly
        /// </summary>
        List<PointF> panelPoly = new List<PointF>()
        {
                new PointF(0.800f, 0.400f), new PointF(0.825f, 0.425f),
                new PointF(0.850f, 0.425f), new PointF(0.850f, -0.425f),
                new PointF(0.825f, -0.425f), new PointF(0.800f, -0.400f)
        };

        /// <summary>
        /// Gets or sets the panel poly.
        /// </summary>
        /// <value>The panel poly.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<PointF> PanelPoly
        {
            get { return panelPoly; }
            set
            {
                panelPoly = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the inside.
        /// </summary>
        /// <value>The color of the inside.</value>
        public Color InsideColor { get; set; } = Color.Sienna;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Black;
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public float BorderThickness { get; set; } = 1.5f;
        /// <summary>
        /// Gets or sets the offset angle.
        /// </summary>
        /// <value>The offset angle.</value>
        public float OffsetAngle { get; set; } = 180f;
        /// <summary>
        /// Gets or sets the drag button.
        /// </summary>
        /// <value>The drag button.</value>
        public MouseButtons DragButton { get; set; } = MouseButtons.None;
        
    }

    /// <summary>
    /// Class Xray.
    /// </summary>
    public class Xray
    {
        /// <summary>
        /// The xray poly
        /// </summary>
        List<PointF> xrayPoly = new List<PointF>()
        {
                new PointF(0.650f, 0.025f), new PointF(-0.800f, 0.400f),
                new PointF(-0.800f, -0.400f), new PointF(0.650f, -0.025f)
        };

        /// <summary>
        /// Gets or sets the x ray poly.
        /// </summary>
        /// <value>The x ray poly.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<PointF> XRayPoly
        {
            get { return xrayPoly; }
            set
            {
                xrayPoly = value;
            }
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public PointF Start { get; set; } = new PointF(0.65f, 0.00f);
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public PointF End { get; set; } = new PointF(-0.80f, 0.00f);
        /// <summary>
        /// Gets or sets the color of the tube.
        /// </summary>
        /// <value>The color of the tube.</value>
        public Color TubeColor { get; set; } = Color.Red;
        /// <summary>
        /// Gets or sets the color of the panel.
        /// </summary>
        /// <value>The color of the panel.</value>
        public Color PanelColor { get; set; } = Color.Lime;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Black;
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public float BorderThickness { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets the offset angle.
        /// </summary>
        /// <value>The offset angle.</value>
        public float OffsetAngle { get; set; } = 0f;
        /// <summary>
        /// Gets or sets the drag button.
        /// </summary>
        /// <value>The drag button.</value>
        public MouseButtons DragButton { get; set; } = MouseButtons.None;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Xray"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; } = false;
        
    }

    /// <summary>
    /// Class Stage.
    /// </summary>
    public class Stage
    {
        /// <summary>
        /// The stage poly
        /// </summary>
        List<PointF> stagePoly = new List<PointF>()
        {
            new PointF(0.075f, 0.200f), new PointF(0.100f, 0.200f),
            new PointF(0.100f, 0.100f), new PointF(0.150f, 0.050f),
            new PointF(0.150f, -0.050f), new PointF(0.100f, -0.100f),
            new PointF(0.100f, -0.200f), new PointF(0.075f, -0.200f)
        };

        /// <summary>
        /// Gets or sets the stage poly.
        /// </summary>
        /// <value>The stage poly.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<PointF> StagePoly
        {
            get { return stagePoly;}
            set
            {
                stagePoly = value;
            }
        }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        public HatchStyle Pattern { get; set; } = HatchStyle.SmallGrid;
        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor { get; set; } = Color.Green;
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor { get; set; } = Color.LightBlue;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.DarkGreen;
        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public float BorderThickness { get; set; } = 1f;
        /// <summary>
        /// Gets or sets the offset angle.
        /// </summary>
        /// <value>The offset angle.</value>
        public float OffsetAngle { get; set; } = 0f;
        /// <summary>
        /// Gets or sets the drag button.
        /// </summary>
        /// <value>The drag button.</value>
        public MouseButtons DragButton { get; set; } = MouseButtons.None;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Stage"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Stage"/> is fixed.
        /// </summary>
        /// <value><c>true</c> if fixed; otherwise, <c>false</c>.</value>
        public bool Fixed { get; set; } = true;
        

    }

    /// <summary>
    /// Class Ring.
    /// </summary>
    public class Ring
    {

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public float Size { get; set; } = 0.980f;
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor { get; set; } = Color.Lavender;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Indigo;
        /// <summary>
        /// Gets or sets the size of the border.
        /// </summary>
        /// <value>The size of the border.</value>
        public float BorderSize { get; set; } = 2.0f;
        
    }

    /// <summary>
    /// Class TextItem.
    /// </summary>
    public class TextItem
    {

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font { get; set; } = new System.Drawing.Font("Arial", 12.0f);

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color { get; set; } = Color.Green;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public float Size { get; set; } = 0.125f;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; } = "Stage";

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public float Position { get; set; } = 0.225f;

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>The angle.</value>
        public float Angle { get; set; } = -90.0f;
    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitAsanaGantryDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitAsanaGantryDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitAsanaGantryDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitAsanaGantrySmartTagActionList(this.Component));
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
    /// Class ZeroitAsanaGantrySmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitAsanaGantrySmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAsanaGantry colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaGantrySmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitAsanaGantrySmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAsanaGantry;

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
        /// Gets or sets the color of the tick.
        /// </summary>
        /// <value>The color of the tick.</value>
        public Color TickColor
        {
            get
            {
                return colUserControl.TickColor;
            }
            set
            {
                GetPropertyByName("TickColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public float Maximum
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
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public float Minimum
        {
            get
            {
                return colUserControl.Minimum;
            }
            set
            {
                GetPropertyByName("Minimum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the ticks.
        /// </summary>
        /// <value>The ticks.</value>
        public int Ticks
        {
            get
            {
                return colUserControl.Ticks;
            }
            set
            {
                GetPropertyByName("Ticks").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the tick per tick.
        /// </summary>
        /// <value>The tick per tick.</value>
        public int TickPerTick
        {
            get
            {
                return colUserControl.TickPerTick;
            }
            set
            {
                GetPropertyByName("TickPerTick").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the tick.
        /// </summary>
        /// <value>The size of the tick.</value>
        public float TickSize
        {
            get
            {
                return colUserControl.TickSize;
            }
            set
            {
                GetPropertyByName("TickSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the tick start.
        /// </summary>
        /// <value>The tick start.</value>
        public float TickStart
        {
            get
            {
                return colUserControl.TickStart;
            }
            set
            {
                GetPropertyByName("TickStart").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the tick thickness.
        /// </summary>
        /// <value>The tick thickness.</value>
        public float TickThickness
        {
            get
            {
                return colUserControl.TickThickness;
            }
            set
            {
                GetPropertyByName("TickThickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the stage angle.
        /// </summary>
        /// <value>The stage angle.</value>
        public float StageAngle
        {
            get
            {
                return colUserControl.StageAngle;
            }
            set
            {
                GetPropertyByName("StageAngle").SetValue(colUserControl, value);
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
                "Angle Wraps", "Appearance",
                "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));
            
            items.Add(new DesignerActionPropertyItem("TickColor",
                                 "Tick Color", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("Maximum",
                "Maximum Angle", "Appearance",
                "Sets the maximum angle."));

            items.Add(new DesignerActionPropertyItem("Minimum",
                "Minimum Angle", "Appearance",
                "Sets the minimum angle."));

            items.Add(new DesignerActionPropertyItem("Ticks",
                "Ticks", "Appearance",
                "Sets the number of ticks."));

            items.Add(new DesignerActionPropertyItem("TickPerTick",
                "Sub Tick", "Appearance",
                "Sets the number of sub ticks."));

            items.Add(new DesignerActionPropertyItem("TickSize",
                "Tick Size", "Appearance",
                "Sets the size of the ticks."));

            items.Add(new DesignerActionPropertyItem("TickStart",
                "Tick Start", "Appearance",
                "Sets the start point of the ticks."));

            items.Add(new DesignerActionPropertyItem("TickThickness",
                "Tick Thickness", "Appearance",
                "Sets the thickness of the tickness."));

            items.Add(new DesignerActionPropertyItem("StageAngle",
                "Stage Angle", "Appearance",
                "Sets the angle of the stage."));

            items.Add(new DesignerActionPropertyItem("Value",
                "Tube Angle", "Appearance",
                "Sets the angle of the tube."));
            
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
