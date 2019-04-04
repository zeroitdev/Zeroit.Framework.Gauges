// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-27-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="CompassRing.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Gauges.Compass.CompassRing
{

    #region Control

    /// <summary>
    /// Class ZeroitCompassRing.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitCompassRingDesigner))]
    public partial class ZeroitCompassRing : UserControl
    {
        #region Private Fields
        /// <summary>
        /// The cc
        /// </summary>
        private ZeroitAsanaCompass cc = new ZeroitAsanaCompass();
        /// <summary>
        /// The markset
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet markset = new ZeroitAsanaCompass.MarkerSet();
        /// <summary>
        /// The marker
        /// </summary>
        private ZeroitAsanaCompass.Marker marker;
        /// <summary>
        /// The line marker
        /// </summary>
        private LineMarker lineMarker = new LineMarker();
        /// <summary>
        /// The listed rings
        /// </summary>
        private List<ListedRings> listedRings = new List<ListedRings>();

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets or sets the listed rings.
        /// </summary>
        /// <value>The listed rings.</value>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ListedRings> ListedRings
        {
            get { return listedRings; }
            set
            {
                listedRings = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the marker.
        /// </summary>
        /// <value>The marker.</value>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ZeroitAsanaCompass.Marker Marker
        {
            get { return marker; }
            set
            {
                marker = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [Browsable(false)]
        [Category("Line")]
        public Color LineColor
        {
            get { return LineMarker.InsideColor; }
            set
            {
                LineMarker.InsideColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the line border.
        /// </summary>
        /// <value>The color of the line border.</value>
        [Browsable(false)]
        [Category("Line")]
        public Color LineBorderColor
        {
            get { return LineMarker.BorderColor; }
            set
            {
                LineMarker.BorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line border thickness.
        /// </summary>
        /// <value>The line border thickness.</value>
        [Browsable(false)]
        [Category("Line")]
        public float LineBorderThickness
        {
            get { return LineMarker.BorderThickness; }
            set
            {
                LineMarker.BorderThickness = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line button.
        /// </summary>
        /// <value>The line button.</value>
        [Browsable(false)]
        [Category("Line")]
        public MouseButtons LineButton
        {
            get { return LineMarker.DragButton; }
            set
            {
                LineMarker.DragButton = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line polygon.
        /// </summary>
        /// <value>The line polygon.</value>
        [Browsable(false)]
        [Category("Line")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PointF> LinePolygon
        {
            get { return LineMarker.LinePoly; }
            set
            {
                LineMarker.LinePoly = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line offset.
        /// </summary>
        /// <value>The line offset.</value>
        [Browsable(false)]
        [Category("Line")]
        public float LineOffset
        {
            get { return LineMarker.OffsetAngle; }
            set
            {
                LineMarker.OffsetAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line start angle.
        /// </summary>
        /// <value>The line start angle.</value>
        [Browsable(false)]
        [Category("Line")]
        public float LineStartAngle
        {
            get { return LineMarker.StartAngle; }
            set
            {
                LineMarker.StartAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line transparency.
        /// </summary>
        /// <value>The line transparency.</value>
        [Browsable(false)]
        [Category("Line")]
        public float LineTransparency
        {
            get { return LineMarker.Transparency; }
            set
            {
                LineMarker.Transparency = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [line visible].
        /// </summary>
        /// <value><c>true</c> if [line visible]; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        [Category("Line")]
        public bool LineVisible
        {
            get { return LineMarker.Visible; }
            set
            {
                LineMarker.Visible = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the line marker.
        /// </summary>
        /// <value>The line marker.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Browsable(true)]
        [Category("Line")]
        public LineMarker LineMarker
        {
            get { return lineMarker; }
            set
            {
                lineMarker = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the functionalities.
        /// </summary>
        /// <value>The functionalities.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ZeroitAsanaCompass MainCompass
        {
            get { return cc; }
            set
            {
                cc = value;
                Invalidate();
            }
        }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCompassRing"/> class.
        /// </summary>
        public ZeroitCompassRing()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

            InitializeComponent();


            // Example of complex rings...

            //cc.BackColor = Color.Bisque;

            this.Controls.Add(cc);

            MainFunctionality();
        }

        /// <summary>
        /// Mains the functionality.
        /// </summary>
        private void MainFunctionality()
        {
            
            cc.MarkerSets.Clear();
            cc.Rings.Clear();

            for (int i = 0; i < ListedRings.Count; i++)
            {
                switch (ListedRings[i].Type)
                {
                    case Compass.CompassRing.ListedRings.RingType.Default:
                        cc.Rings.Add(ListedRings[i].Distance, ListedRings[i].Color1, ListedRings[i].Color2, ListedRings[i].BorderSize);

                        break;
                    case Compass.CompassRing.ListedRings.RingType.Gradient:
                        cc.Rings.Add(ListedRings[i].Distance, ListedRings[i].GradientMode, ListedRings[i].Color1,
                            ListedRings[i].Color2, ListedRings[i].BorderColor, ListedRings[i].BorderSize);
                        break;
                    case Compass.CompassRing.ListedRings.RingType.HatchStyle:
                        cc.Rings.Add(ListedRings[i].Distance, ListedRings[i].HatchStyle, ListedRings[i].Color1,
                            ListedRings[i].Color2, ListedRings[i].BorderColor, ListedRings[i].BorderSize);
                        break;

                }
            }

            #region Line Tick
            marker = new ZeroitAsanaCompass.Marker(
                ZeroitAsanaCompass.MakeArgb(0.8f, LineMarker.InsideColor),
                LineMarker.BorderColor, // border color
                LineMarker.BorderThickness, // border thickness
                LineMarker.LinePoly.ToArray(),
                LineMarker.OffsetAngle, // angle offset
                LineMarker.DragButton,
                LineMarker.Visible);
            markset = new ZeroitAsanaCompass.MarkerSet(90f);

            markset.Add(marker);
            cc.MarkerSets.Add(markset);

            //markset.Add(marker);

            #endregion


            cc.FixedBackground = false;
        }

        /// <summary>
        /// Ccs the angle changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void cc_AngleChanged(object sender, ZeroitAsanaCompass.AngleChangedArgs e)
        {
            UpdateColor();
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        private void UpdateColor()
        {
            // Calc RGB value from R/G/B markers
            //int r = (int)(256.0f * markset.Angle / 360.0f);
            //int g = (int)(256.0f * green.Angle / 360.0f);
            //int b = (int)(256.0f * blue.Angle / 360.0f);

            //innerRing.SolidColor = Color.FromArgb(255, r, g, b);
            //outerRing.SolidColor = Color.FromArgb(255, r, g, b);

            //this.Text = string.Format("ColorDialer [{0}, {1}, {2}]", r, g, b);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the fixedBackgroundCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void fixedBackgroundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //cc.FixedBackground = fixedBackgroundCheckBox.Checked;
            cc.ResetPaintTimeCounters();
        }

        /// <summary>
        /// Handles the Click event of the paintTimesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void paintTimesButton_Click(object sender, EventArgs e)
        {
            //paintTimesLabel.Text = string.Format("   Mean {0:F3} ms; {1} paints", cc.PaintTimeMean, cc.PaintTimeCount);
            cc.ResetPaintTimeCounters();
        }

        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            MainFunctionality();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            MainFunctionality();

            CenterString(e.Graphics, MainCompass.Value.ToString(), Font, ForeColor, ClientRectangle);

        }



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
            MainFunctionality();
        }

    }

    /// <summary>
    /// Class Rings.
    /// </summary>
    [Serializable]
    public class Rings
    {

        //--Ring1
        /// <summary>
        /// Gets or sets the ring1 colors.
        /// </summary>
        /// <value>The ring1 colors.</value>
        public Color[] Ring1Colors { get; set; } = new Color[] { Color.LightBlue, Color.Black };

        /// <summary>
        /// Gets or sets the size of the ring1.
        /// </summary>
        /// <value>The size of the ring1.</value>
        public float Ring1Size { get; set; } = 0.1f;

        /// <summary>
        /// Gets or sets the size of the ring1 border.
        /// </summary>
        /// <value>The size of the ring1 border.</value>
        public float Ring1BorderSize { get; set; } = 0.1f;


        //--Ring2
        /// <summary>
        /// Gets or sets the ring2 colors.
        /// </summary>
        /// <value>The ring2 colors.</value>
        public Color[] Ring2Colors { get; set; } = new Color[] { Color.Red, Color.Green };

        /// <summary>
        /// Gets or sets the size of the ring2.
        /// </summary>
        /// <value>The size of the ring2.</value>
        public float Ring2Size { get; set; } = 0.2f;

        /// <summary>
        /// Gets or sets the ring2 gradient mode.
        /// </summary>
        /// <value>The ring2 gradient mode.</value>
        public LinearGradientMode Ring2GradientMode { get; set; } = LinearGradientMode.Vertical;


        //--Ring3
        /// <summary>
        /// Gets or sets the ring3 colors.
        /// </summary>
        /// <value>The ring3 colors.</value>
        public Color[] Ring3Colors { get; set; } = new Color[] { Color.Purple, Color.PeachPuff };

        /// <summary>
        /// Gets or sets the size of the ring3.
        /// </summary>
        /// <value>The size of the ring3.</value>
        public float Ring3Size { get; set; } = 0.3f;

        /// <summary>
        /// Gets or sets the ring3 hatch style.
        /// </summary>
        /// <value>The ring3 hatch style.</value>
        public HatchStyle Ring3HatchStyle { get; set; } = HatchStyle.DottedGrid;


        //--Ring4
        /// <summary>
        /// Gets or sets the ring4 colors.
        /// </summary>
        /// <value>The ring4 colors.</value>
        public Color[] Ring4Colors { get; set; } = new Color[] { Color.Black, Color.White };

        /// <summary>
        /// Gets or sets the size of the ring4.
        /// </summary>
        /// <value>The size of the ring4.</value>
        public float Ring4Size { get; set; } = 0.2f;

        /// <summary>
        /// Gets or sets the ring4 gradient mode.
        /// </summary>
        /// <value>The ring4 gradient mode.</value>
        public LinearGradientMode Ring4GradientMode { get; set; } = LinearGradientMode.Horizontal;

        //--Ring1
        /// <summary>
        /// Gets or sets the ring5 colors.
        /// </summary>
        /// <value>The ring5 colors.</value>
        public Color[] Ring5Colors { get; set; } = new Color[] { Color.SandyBrown, Color.Navy };

        /// <summary>
        /// Gets or sets the size of the ring5.
        /// </summary>
        /// <value>The size of the ring5.</value>
        public float Ring5Size { get; set; } = 0.1f;

        /// <summary>
        /// Gets or sets the size of the ring5 border.
        /// </summary>
        /// <value>The size of the ring5 border.</value>
        public float Ring5BorderSize { get; set; } = 2f;

    }

    /// <summary>
    /// Class ListedRings.
    /// </summary>
    [Serializable]
    public class ListedRings
    {
        /// <summary>
        /// Enum RingType
        /// </summary>
        public enum RingType
        {
            /// <summary>
            /// The default
            /// </summary>
            Default,
            /// <summary>
            /// The gradient
            /// </summary>
            Gradient,
            /// <summary>
            /// The hatch style
            /// </summary>
            HatchStyle
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public RingType Type { get; set; } = RingType.Gradient;
        /// <summary>
        /// Gets or sets the color1.
        /// </summary>
        /// <value>The color1.</value>
        public Color Color1 { get; set; } = Color.DarkSlateBlue;
        /// <summary>
        /// Gets or sets the color2.
        /// </summary>
        /// <value>The color2.</value>
        public Color Color2 { get; set; } = Color.Red;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Black;
        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public float Distance { get; set; } = 0.1f;
        /// <summary>
        /// Gets or sets the size of the border.
        /// </summary>
        /// <value>The size of the border.</value>
        public float BorderSize { get; set; } = 0.1f;
        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        /// <value>The gradient mode.</value>
        public LinearGradientMode GradientMode { get; set; } = LinearGradientMode.Vertical;
        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        public HatchStyle HatchStyle { get; set; } = HatchStyle.DottedGrid;

    }

    /// <summary>
    /// Class LineMarker.
    /// </summary>
    [Serializable]
    public class LineMarker
    {


        /// <summary>
        /// The line poly
        /// </summary>
        List<PointF> linePoly = new List<PointF>()
        {
            new PointF(-0.0250f, 0.0000f),
            new PointF(-0.0250f, 0.0125f),
            new PointF(-0.0125f, 0.0250f),
            new PointF(0.0125f, 0.0250f),
            new PointF(0.0250f, 0.0125f),
            new PointF(0.6000f, 0.0000f),
            new PointF(0.1500f, -0.0125f),
            new PointF(0.0250f, -0.0125f),
            new PointF(0.0125f, -0.0250f),
            new PointF(-0.0125f, -0.0250f),
            new PointF(-0.0250f, -0.0125f),
            new PointF(-0.0250f, 0.0000f)
        };

        /// <summary>
        /// Gets or sets the line poly.
        /// </summary>
        /// <value>The line poly.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<PointF> LinePoly
        {
            get { return linePoly; }
            set
            {
                linePoly = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the inside.
        /// </summary>
        /// <value>The color of the inside.</value>
        public Color InsideColor { get; set; } = Color.Blue;
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor { get; set; } = Color.Black;
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
        public MouseButtons DragButton { get; set; } = MouseButtons.Left;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LineMarker"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        /// <value>The start angle.</value>
        public float StartAngle { get; set; } = 90f;

        /// <summary>
        /// Gets or sets the transparency.
        /// </summary>
        /// <value>The transparency.</value>
        public float Transparency { get; set; } = 0.8f;
    }

    #endregion

    #region Designer Generated Code

    partial class ZeroitCompassRing
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            //this.splitter = new System.Windows.Forms.SplitContainer();
            //this.upperSplit = new System.Windows.Forms.SplitContainer();
            //this.paintTimesButton = new System.Windows.Forms.Button();
            //this.fixedBackgroundCheckBox = new System.Windows.Forms.CheckBox();
            //this.paintTimesLabel = new System.Windows.Forms.Label();
            //this.infoTextBox = new System.Windows.Forms.TextBox();
            this.cc = new ZeroitAsanaCompass();
            //this.splitter.Panel1.SuspendLayout();
            //this.splitter.Panel2.SuspendLayout();
            //this.splitter.SuspendLayout();
            //this.upperSplit.Panel1.SuspendLayout();
            //this.upperSplit.Panel2.SuspendLayout();
            //this.upperSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            //this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            //this.splitter.Location = new System.Drawing.Point(0, 0);
            //this.splitter.Name = "splitter";
            //this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            //this.splitter.Panel1.Controls.Add(this.upperSplit);
            // 
            // splitter.Panel2
            // 
            //this.splitter.Panel2.Controls.Add(this.cc);
            //this.splitter.Size = new System.Drawing.Size(411, 516); /*new Size(Width, Height);*/
            //splitter.Dock = DockStyle.Fill;
            //this.splitter.SplitterDistance = 131;
            //this.splitter.TabIndex = 1;
            // 
            // upperSplit
            // 
            //this.upperSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.upperSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            //this.upperSplit.Location = new System.Drawing.Point(0, 0);
            //this.upperSplit.Name = "upperSplit";
            //this.upperSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // upperSplit.Panel1
            // 
            //this.upperSplit.Panel1.Controls.Add(this.infoTextBox);
            //this.upperSplit.Panel1.Padding = new System.Windows.Forms.Padding(8);
            // 
            // upperSplit.Panel2
            // 
            //this.upperSplit.Panel2.Controls.Add(this.paintTimesButton);
            //this.upperSplit.Panel2.Controls.Add(this.fixedBackgroundCheckBox);
            //this.upperSplit.Panel2.Controls.Add(this.paintTimesLabel);
            //this.upperSplit.Panel2.Padding = new System.Windows.Forms.Padding(8, 2, 2, 2);
            //this.upperSplit.Size = new System.Drawing.Size(411, 131);
            //this.upperSplit.SplitterDistance = 94;
            //this.upperSplit.TabIndex = 0;
            //this.upperSplit.TabStop = false;
            // 
            // paintTimesButton
            // 
            //this.paintTimesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.paintTimesButton.Location = new System.Drawing.Point(132, 2);
            //this.paintTimesButton.Name = "paintTimesButton";
            //this.paintTimesButton.Size = new System.Drawing.Size(86, 29);
            //this.paintTimesButton.TabIndex = 1;
            //this.paintTimesButton.Text = "Paint Times";
            //this.paintTimesButton.UseVisualStyleBackColor = true;
            //this.paintTimesButton.Click += new System.EventHandler(this.paintTimesButton_Click);
            // 
            // fixedBackgroundCheckBox
            // 
            //this.fixedBackgroundCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            //this.fixedBackgroundCheckBox.Location = new System.Drawing.Point(8, 2);
            //this.fixedBackgroundCheckBox.Name = "fixedBackgroundCheckBox";
            //this.fixedBackgroundCheckBox.Size = new System.Drawing.Size(124, 29);
            //this.fixedBackgroundCheckBox.TabIndex = 0;
            //this.fixedBackgroundCheckBox.Text = "Fixed Background";
            //this.fixedBackgroundCheckBox.UseVisualStyleBackColor = true;
            //this.fixedBackgroundCheckBox.CheckedChanged += new System.EventHandler(this.fixedBackgroundCheckBox_CheckedChanged);
            // 
            // paintTimesLabel
            // 
            //this.paintTimesLabel.Dock = System.Windows.Forms.DockStyle.Right;
            //this.paintTimesLabel.Location = new System.Drawing.Point(218, 2);
            //this.paintTimesLabel.Name = "paintTimesLabel";
            //this.paintTimesLabel.Size = new System.Drawing.Size(191, 29);
            //this.paintTimesLabel.TabIndex = 2;
            //this.paintTimesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoTextBox
            // 
            //this.infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.infoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.infoTextBox.Location = new System.Drawing.Point(8, 8);
            //this.infoTextBox.Multiline = true;
            //this.infoTextBox.Name = "infoTextBox";
            //this.infoTextBox.ReadOnly = true;
            //this.infoTextBox.Size = new System.Drawing.Size(395, 78);
            //this.infoTextBox.TabIndex = 0;
            //this.infoTextBox.TabStop = false;
            //this.infoTextBox.Text = resources.GetString("infoTextBox.Text");
            // 
            // cc
            // 
            this.cc.AngleMax = 360F;
            this.cc.AngleMin = 0F;
            this.cc.AngleWraps = true;
            this.cc.Cursor = System.Windows.Forms.Cursors.Default;
            this.cc.CursorCanDrag = System.Windows.Forms.Cursors.Hand;
            this.cc.CursorCannotDrag = System.Windows.Forms.Cursors.Default;
            this.cc.CursorDragging = null;
            this.cc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc.FixedBackground = false;
            this.cc.Location = new System.Drawing.Point(0, 0);
            this.cc.MajorTickColor = System.Drawing.Color.Black;
            this.cc.MajorTicks = 10;
            this.cc.MajorTickSize = 0.45F;
            this.cc.MajorTickStart = 0.4F;
            this.cc.MajorTickThickness = 0.45F;
            this.cc.MinorTickColor = System.Drawing.Color.Black;
            this.cc.MinorTickSize = 0.25F;
            this.cc.MinorTickStart = 0.5F;
            this.cc.MinorTickThickness = 0.45F;
            this.cc.Name = "cc";
            this.cc.PrimaryMarkerAngle = 0F;
            this.cc.PrimaryMarkerBorderColor = System.Drawing.Color.Black;
            this.cc.PrimaryMarkerBorderSize = 2F;
            this.cc.PrimaryMarkerPoints = new System.Drawing.PointF[4];
            cc.PrimaryMarkerPoints[0] = new PointF(0.35f, 0f);
            cc.PrimaryMarkerPoints[1] = new PointF(0.8f, 0.2f);
            cc.PrimaryMarkerPoints[2] = new PointF(0.7f, 0f);
            cc.PrimaryMarkerPoints[3] = new PointF(0.8f, 0.2f);
            //    this.cc.PrimaryMarkerPoints = new System.Drawing.PointF[] {
            //((System.Drawing.PointF)(resources.GetObject("cc.PrimaryMarkerPoints"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.PrimaryMarkerPoints1"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.PrimaryMarkerPoints2"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.PrimaryMarkerPoints3")))};
            this.cc.PrimaryMarkerSolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cc.Size = new System.Drawing.Size(411, 516);
            this.cc.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.cc.TabIndex = 0;
            this.cc.AngleChanged += new ZeroitAsanaCompass.AngleChangedHandler(this.cc_AngleChanged);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 516);
            this.Controls.Add(cc);
            this.Name = "MainForm";
            this.Text = "ManyRings";
            //this.splitter.Panel1.ResumeLayout(false);
            //this.splitter.Panel2.ResumeLayout(false);
            //this.splitter.ResumeLayout(false);
            //this.upperSplit.Panel1.ResumeLayout(false);
            //this.upperSplit.Panel1.PerformLayout();
            //this.upperSplit.Panel2.ResumeLayout(false);
            //this.upperSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        //private System.Windows.Forms.SplitContainer splitter;
        //private System.Windows.Forms.CheckBox fixedBackgroundCheckBox;
        //private System.Windows.Forms.Label paintTimesLabel;
        //private System.Windows.Forms.Button paintTimesButton;
        //private System.Windows.Forms.SplitContainer upperSplit;
        //private System.Windows.Forms.TextBox infoTextBox;
    }

    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitCompassRingDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitCompassRingDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitCompassRingDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitCompassRingSmartTagActionList(this.Component));
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
    /// Class ZeroitCompassRingSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitCompassRingSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitCompassRing colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCompassRingSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitCompassRingSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitCompassRing;

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
        /// Gets or sets a value indicating whether [line visible].
        /// </summary>
        /// <value><c>true</c> if [line visible]; otherwise, <c>false</c>.</value>
        public bool LineVisible
        {
            get
            {
                return colUserControl.LineVisible;
            }
            set
            {
                GetPropertyByName("LineVisible").SetValue(colUserControl, value);
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
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get
            {
                return colUserControl.LineColor;
            }
            set
            {
                GetPropertyByName("LineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the line border.
        /// </summary>
        /// <value>The color of the line border.</value>
        public Color LineBorderColor
        {
            get
            {
                return colUserControl.LineBorderColor;
            }
            set
            {
                GetPropertyByName("LineBorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line border thickness.
        /// </summary>
        /// <value>The line border thickness.</value>
        public float LineBorderThickness
        {
            get
            {
                return colUserControl.LineBorderThickness;
            }
            set
            {
                GetPropertyByName("LineBorderThickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line offset.
        /// </summary>
        /// <value>The line offset.</value>
        public float LineOffset
        {
            get
            {
                return colUserControl.LineOffset;
            }
            set
            {
                GetPropertyByName("LineOffset").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line start angle.
        /// </summary>
        /// <value>The line start angle.</value>
        public float LineStartAngle
        {
            get
            {
                return colUserControl.LineStartAngle;
            }
            set
            {
                GetPropertyByName("LineStartAngle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line transparency.
        /// </summary>
        /// <value>The line transparency.</value>
        public float LineTransparency
        {
            get
            {
                return colUserControl.LineTransparency;
            }
            set
            {
                GetPropertyByName("LineTransparency").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line button.
        /// </summary>
        /// <value>The line button.</value>
        public MouseButtons LineButton
        {
            get
            {
                return colUserControl.LineButton;
            }
            set
            {
                GetPropertyByName("LineButton").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the line polygon.
        /// </summary>
        /// <value>The line polygon.</value>
        public List<PointF> LinePolygon
        {
            get
            {
                return colUserControl.LinePolygon;
            }
            set
            {
                GetPropertyByName("LinePolygon").SetValue(colUserControl, value);
            }
        }

        public List<ListedRings> ListedRings
        {
            get
            {
                return colUserControl.ListedRings;
            }
            set
            {
                GetPropertyByName("ListedRings").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("LineVisible",
                "Visible", "Appearance",
                "Set to show the line."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));
            
            items.Add(new DesignerActionPropertyItem("LineColor",
                                 "Line Color", "Appearance",
                                 "Sets the line color."));

            items.Add(new DesignerActionPropertyItem("LineBorderColor",
                "Line Border Color", "Appearance",
                "Sets the color of the lines's border."));

            items.Add(new DesignerActionPropertyItem("LineBorderThickness",
                "Line Border Thickness", "Appearance",
                "Sets the line border thickness."));

            items.Add(new DesignerActionPropertyItem("LineOffset",
                "Line Offset", "Appearance",
                "Sets the line offset."));

            items.Add(new DesignerActionPropertyItem("LineStartAngle",
                "Line Start Angle", "Appearance",
                "Sets the start angle of the line."));

            items.Add(new DesignerActionPropertyItem("LineTransparency",
                "Line Transparency", "Appearance",
                "Sets the line transparency."));

            items.Add(new DesignerActionPropertyItem("LineButton",
                "Line Button", "Appearance",
                "Sets the type of button."));

            items.Add(new DesignerActionPropertyItem("LinePolygon",
                "Line Polygon", "Appearance",
                "Sets the polygon to use"));

            items.Add(new DesignerActionPropertyItem("ListedRings",
                "Rings", "Appearance",
                "Sets the rings to draw"));

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
