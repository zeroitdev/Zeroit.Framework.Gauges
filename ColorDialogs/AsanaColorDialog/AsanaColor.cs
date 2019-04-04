// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="AsanaColor.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;
using Zeroit.Framework.Gauges.Compass;


#endregion

namespace Zeroit.Framework.Gauges.ColorDialogs.AsanaColorDialog
{
    #region Form

    #region Control

    /// <summary>
    /// Class AsanaColorDialog.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AsanaColorDialog : Form
    {
        /// <summary>
        /// The last click
        /// </summary>
        public Point lastClick;
        /// <summary>
        /// The color border drag paint
        /// </summary>
        private Color ColorBorderDragPaint = Color.Green;
        /// <summary>
        /// Initializes a new instance of the <see cref="AsanaColorDialog"/> class.
        /// </summary>
        public AsanaColorDialog()
        {
            InitializeComponent();

            // Major ticks are counts on the lock
            cc.MajorTicks = 64;
            cc.MajorTickStart = 0.08f;
            cc.MajorTickSize = 0.80f;
            cc.MajorTickThickness = 0.6f;

            cc.MinorTicksPerMajorTick = 0; // no minor ticks

            // Text at end of ticks
            cc.TextItems.Clear();
            float pos = 0.92f;
            Font f = new Font("Arial", 7.0f);
            for (int i = 0; i < cc.MajorTicks; i++)
            {
                int val = i * (256 / cc.MajorTicks);
                cc.TextItems.Add(f, Color.Black, val.ToString(), pos, cc.GetMajorTickAngle(i));
            }

            // Rings line up with the locks
            cc.Rings.Clear();
            innerRing = new ZeroitAsanaCompass.Ring(0.08f, Color.White);
            cc.Rings.Add(innerRing);
            cc.Rings.Add(0.26f, Color.Gainsboro);
            cc.Rings.Add(0.26f, Color.LightGray);
            cc.Rings.Add(0.26f, Color.Gainsboro);
            cc.Rings.Add(0.12f, Color.LightGray);
            outerRing = new ZeroitAsanaCompass.Ring(9.99f, Color.White);
            cc.Rings.Add(outerRing);

            // Inner lock 
            cc.MarkerSets.Clear();

            ArrayList redBigPoints = new ArrayList();
            redBigPoints.Add(new PointF(0.10f, 0.00f));
            redBigPoints.Add(new PointF(0.32f, 0.10f));
            redBigPoints.Add(new PointF(0.32f, -0.10f));
            redBigPoints.Add(new PointF(0.10f, 0.00f));
            PointF[] redBigPoly = (PointF[])redBigPoints.ToArray(typeof(PointF));
            Color redBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Red);

            ArrayList redSmallPoints = new ArrayList();
            redSmallPoints.Add(new PointF(0.12f, 0.00f));
            redSmallPoints.Add(new PointF(0.30f, 0.05f));
            redSmallPoints.Add(new PointF(0.30f, -0.05f));
            redSmallPoints.Add(new PointF(0.12f, 0.00f));
            PointF[] redSmallPoly = (PointF[])redSmallPoints.ToArray(typeof(PointF));
            Color redSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Red);

            red = new ZeroitAsanaCompass.MarkerSet();
            red.Add(redBigColor, Color.Black, 3.0f, redBigPoly, 0.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 45.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 90.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 135.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 180.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 225.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 270.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 315.0f, MouseButtons.None);

            cc.MarkerSets.Add(red);

            // Middle lock
            ArrayList greenBigPoints = new ArrayList();
            greenBigPoints.Add(new PointF(0.36f, 0.00f));
            greenBigPoints.Add(new PointF(0.58f, 0.10f));
            greenBigPoints.Add(new PointF(0.58f, -0.10f));
            greenBigPoints.Add(new PointF(0.36f, 0.00f));
            PointF[] greenBigPoly = (PointF[])greenBigPoints.ToArray(typeof(PointF));
            Color greenBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Green);

            ArrayList greenSmallPoints = new ArrayList();
            greenSmallPoints.Add(new PointF(0.38f, 0.00f));
            greenSmallPoints.Add(new PointF(0.56f, 0.05f));
            greenSmallPoints.Add(new PointF(0.56f, -0.05f));
            greenSmallPoints.Add(new PointF(0.38f, 0.00f));
            PointF[] greenSmallPoly = (PointF[])greenSmallPoints.ToArray(typeof(PointF));
            Color greenSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Green);

            green = new ZeroitAsanaCompass.MarkerSet();
            green.Add(greenBigColor, Color.Black, 3.0f, greenBigPoly, 0.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 30.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 60.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 90.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 120.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 150.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 180.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 210.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 240.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 270.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 300.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 330.0f, MouseButtons.None);

            cc.MarkerSets.Add(green);

            // Middle lock
            ArrayList blueBigPoints = new ArrayList();
            blueBigPoints.Add(new PointF(0.62f, 0.00f));
            blueBigPoints.Add(new PointF(0.84f, 0.10f));
            blueBigPoints.Add(new PointF(0.84f, -0.10f));
            blueBigPoints.Add(new PointF(0.62f, 0.00f));
            PointF[] blueBigPoly = (PointF[])blueBigPoints.ToArray(typeof(PointF));
            Color blueBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Blue);

            ArrayList blueSmallPoints = new ArrayList();
            blueSmallPoints.Add(new PointF(0.64f, 0.00f));
            blueSmallPoints.Add(new PointF(0.82f, 0.05f));
            blueSmallPoints.Add(new PointF(0.82f, -0.05f));
            blueSmallPoints.Add(new PointF(0.64f, 0.00f));
            PointF[] blueSmallPoly = (PointF[])blueSmallPoints.ToArray(typeof(PointF));
            Color blueSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Blue);

            blue = new ZeroitAsanaCompass.MarkerSet();
            blue.Add(blueBigColor, Color.Black, 3.0f, blueBigPoly, 0.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 22.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 45.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 67.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 90.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 112.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 135.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 157.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 180.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 202.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 225.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 247.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 270.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 292.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 315.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 337.5f, MouseButtons.None);

            cc.MarkerSets.Add(blue);

            UpdateColor();
        }

        /// <summary>
        /// The red
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet red;
        /// <summary>
        /// The green
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet green;
        /// <summary>
        /// The blue
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet blue;

        /// <summary>
        /// The inner ring
        /// </summary>
        private ZeroitAsanaCompass.Ring innerRing;
        /// <summary>
        /// The outer ring
        /// </summary>
        private ZeroitAsanaCompass.Ring outerRing;

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
            int r = (int)(256.0f * red.Angle / 360.0f);
            int g = (int)(256.0f * green.Angle / 360.0f);
            int b = (int)(256.0f * blue.Angle / 360.0f);

            innerRing.SolidColor = Color.FromArgb(255, r, g, b);
            outerRing.SolidColor = Color.FromArgb(255, r, g, b);

            this.Text = string.Format("ColorDialer [{0}, {1}, {2}]", r, g, b);
        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// Handles the Paint event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        public void DragForm_Paint(object sender, PaintEventArgs e)
        {
            //Draws a border to make the Form stand out
            //Just done for appearance, not necessary


            Pen p = new Pen(ColorBorderDragPaint, 1);
            e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            p.Dispose();
        }



        /// <summary>
        /// Handles the MouseDown event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseDown(object sender, MouseEventArgs e)
        {

            lastClick = new Point(e.X, e.Y); //We'll need this for when the Form starts to move
        }

        /// <summary>
        /// Handles the MouseMove event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseMove(object sender, MouseEventArgs e)
        {

            //Point newLocation = new Point(e.X - lastE.X, e.Y - lastE.Y);
            if (e.Button == MouseButtons.Left) //Only when mouse is clicked
            {
                //Move the Form the same difference the mouse cursor moved;
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            DoubleBuffered = true;
            //this.StartPosition = FormStartPosition.CenterParent;

            base.OnLoad(e);
        }

    }

    #endregion
    
    #region Designer Generated Code
    partial class AsanaColorDialog
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
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsanaColorDialog));
            this.cc = new ZeroitAsanaCompass();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cc
            // 
            this.cc.AngleMax = 360F;
            this.cc.AngleMin = 0F;
            this.cc.AngleWraps = true;
            this.cc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cc.CursorCanDrag = System.Windows.Forms.Cursors.Hand;
            this.cc.CursorCannotDrag = System.Windows.Forms.Cursors.Arrow;
            this.cc.CursorDragging = System.Windows.Forms.Cursors.SizeAll;
            this.cc.FixedBackground = false;
            this.cc.Location = new System.Drawing.Point(0, 37);
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
            this.cc.PrimaryMarkerSolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cc.Size = new System.Drawing.Size(522, 462);
            this.cc.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.cc.TabIndex = 0;
            this.cc.AngleChanged += new ZeroitAsanaCompass.AngleChangedHandler(this.cc_AngleChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(115, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(250, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 36);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 498);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(522, 53);
            this.panel2.TabIndex = 4;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(489, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 27);
            this.button3.TabIndex = 3;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(522, 551);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "ColorDialer";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The cc
        /// </summary>
        private ZeroitAsanaCompass cc;
        /// <summary>
        /// The button1
        /// </summary>
        private System.Windows.Forms.Button button1;
        /// <summary>
        /// The button2
        /// </summary>
        private System.Windows.Forms.Button button2;
        /// <summary>
        /// The panel1
        /// </summary>
        private System.Windows.Forms.Panel panel1;
        /// <summary>
        /// The button3
        /// </summary>
        private System.Windows.Forms.Button button3;
        /// <summary>
        /// The panel2
        /// </summary>
        private System.Windows.Forms.Panel panel2;
    }
    #endregion

    #endregion

    #region Toolbox

    #region Control

    /// <summary>
    /// Class ZeroitAsanaColor.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ZeroitAsanaColor : UserControl
    {
        /// <summary>
        /// The last click
        /// </summary>
        public Point lastClick;
        /// <summary>
        /// The color border drag paint
        /// </summary>
        private Color ColorBorderDragPaint = Color.Green;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaColor"/> class.
        /// </summary>
        public ZeroitAsanaColor()
        {
            InitializeComponent();

            // Major ticks are counts on the lock
            cc.MajorTicks = 64;
            cc.MajorTickStart = 0.08f;
            cc.MajorTickSize = 0.80f;
            cc.MajorTickThickness = 0.6f;

            cc.MinorTicksPerMajorTick = 0; // no minor ticks

            // Text at end of ticks
            cc.TextItems.Clear();
            float pos = 0.92f;
            Font f = new Font("Arial", 7.0f);
            for (int i = 0; i < cc.MajorTicks; i++)
            {
                int val = i * (256 / cc.MajorTicks);
                cc.TextItems.Add(f, Color.Black, val.ToString(), pos, cc.GetMajorTickAngle(i));
            }

            // Rings line up with the locks
            cc.Rings.Clear();
            innerRing = new ZeroitAsanaCompass.Ring(0.08f, Color.White);
            cc.Rings.Add(innerRing);
            cc.Rings.Add(0.26f, Color.Gainsboro);
            cc.Rings.Add(0.26f, Color.LightGray);
            cc.Rings.Add(0.26f, Color.Gainsboro);
            cc.Rings.Add(0.12f, Color.LightGray);
            outerRing = new ZeroitAsanaCompass.Ring(9.99f, Color.White);
            cc.Rings.Add(outerRing);

            // Inner lock 
            cc.MarkerSets.Clear();

            ArrayList redBigPoints = new ArrayList();
            redBigPoints.Add(new PointF(0.10f, 0.00f));
            redBigPoints.Add(new PointF(0.32f, 0.10f));
            redBigPoints.Add(new PointF(0.32f, -0.10f));
            redBigPoints.Add(new PointF(0.10f, 0.00f));
            PointF[] redBigPoly = (PointF[])redBigPoints.ToArray(typeof(PointF));
            Color redBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Red);

            ArrayList redSmallPoints = new ArrayList();
            redSmallPoints.Add(new PointF(0.12f, 0.00f));
            redSmallPoints.Add(new PointF(0.30f, 0.05f));
            redSmallPoints.Add(new PointF(0.30f, -0.05f));
            redSmallPoints.Add(new PointF(0.12f, 0.00f));
            PointF[] redSmallPoly = (PointF[])redSmallPoints.ToArray(typeof(PointF));
            Color redSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Red);

            red = new ZeroitAsanaCompass.MarkerSet();
            red.Add(redBigColor, Color.Black, 3.0f, redBigPoly, 0.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 45.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 90.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 135.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 180.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 225.0f, MouseButtons.None);
            red.Add(redBigColor, Color.Black, 1.5f, redBigPoly, 270.0f, MouseButtons.Left);
            red.Add(redSmallColor, Color.Black, 1.0f, redSmallPoly, 315.0f, MouseButtons.None);

            cc.MarkerSets.Add(red);

            // Middle lock
            ArrayList greenBigPoints = new ArrayList();
            greenBigPoints.Add(new PointF(0.36f, 0.00f));
            greenBigPoints.Add(new PointF(0.58f, 0.10f));
            greenBigPoints.Add(new PointF(0.58f, -0.10f));
            greenBigPoints.Add(new PointF(0.36f, 0.00f));
            PointF[] greenBigPoly = (PointF[])greenBigPoints.ToArray(typeof(PointF));
            Color greenBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Green);

            ArrayList greenSmallPoints = new ArrayList();
            greenSmallPoints.Add(new PointF(0.38f, 0.00f));
            greenSmallPoints.Add(new PointF(0.56f, 0.05f));
            greenSmallPoints.Add(new PointF(0.56f, -0.05f));
            greenSmallPoints.Add(new PointF(0.38f, 0.00f));
            PointF[] greenSmallPoly = (PointF[])greenSmallPoints.ToArray(typeof(PointF));
            Color greenSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Green);

            green = new ZeroitAsanaCompass.MarkerSet();
            green.Add(greenBigColor, Color.Black, 3.0f, greenBigPoly, 0.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 30.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 60.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 90.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 120.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 150.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 180.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 210.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 240.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 270.0f, MouseButtons.None);
            green.Add(greenBigColor, Color.Black, 1.5f, greenBigPoly, 300.0f, MouseButtons.Left);
            green.Add(greenSmallColor, Color.Black, 1.0f, greenSmallPoly, 330.0f, MouseButtons.None);

            cc.MarkerSets.Add(green);

            // Middle lock
            ArrayList blueBigPoints = new ArrayList();
            blueBigPoints.Add(new PointF(0.62f, 0.00f));
            blueBigPoints.Add(new PointF(0.84f, 0.10f));
            blueBigPoints.Add(new PointF(0.84f, -0.10f));
            blueBigPoints.Add(new PointF(0.62f, 0.00f));
            PointF[] blueBigPoly = (PointF[])blueBigPoints.ToArray(typeof(PointF));
            Color blueBigColor = ZeroitAsanaCompass.MakeArgb(0.8f, Color.Blue);

            ArrayList blueSmallPoints = new ArrayList();
            blueSmallPoints.Add(new PointF(0.64f, 0.00f));
            blueSmallPoints.Add(new PointF(0.82f, 0.05f));
            blueSmallPoints.Add(new PointF(0.82f, -0.05f));
            blueSmallPoints.Add(new PointF(0.64f, 0.00f));
            PointF[] blueSmallPoly = (PointF[])blueSmallPoints.ToArray(typeof(PointF));
            Color blueSmallColor = ZeroitAsanaCompass.MakeArgb(0.6f, Color.Blue);

            blue = new ZeroitAsanaCompass.MarkerSet();
            blue.Add(blueBigColor, Color.Black, 3.0f, blueBigPoly, 0.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 22.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 45.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 67.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 90.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 112.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 135.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 157.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 180.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 202.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 225.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 247.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 270.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 292.5f, MouseButtons.None);
            blue.Add(blueBigColor, Color.Black, 1.5f, blueBigPoly, 315.0f, MouseButtons.Left);
            blue.Add(blueSmallColor, Color.Black, 1.0f, blueSmallPoly, 337.5f, MouseButtons.None);

            cc.MarkerSets.Add(blue);

            UpdateColor();
        }

        /// <summary>
        /// The red
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet red;
        /// <summary>
        /// The green
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet green;
        /// <summary>
        /// The blue
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet blue;

        /// <summary>
        /// The inner ring
        /// </summary>
        private ZeroitAsanaCompass.Ring innerRing;
        /// <summary>
        /// The outer ring
        /// </summary>
        private ZeroitAsanaCompass.Ring outerRing;

        /// <summary>
        /// The get color
        /// </summary>
        private Color getColor;

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get { return GetColor(); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Browsable(true)]
        public new string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Ccs the angle changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void cc_AngleChanged(object sender, ZeroitAsanaCompass.AngleChangedArgs e)
        {
            UpdateColor();

            this.OnColorChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        private void UpdateColor()
        {
            // Calc RGB value from R/G/B markers
            int r = (int)(256.0f * red.Angle / 360.0f);
            int g = (int)(256.0f * green.Angle / 360.0f);
            int b = (int)(256.0f * blue.Angle / 360.0f);

            innerRing.SolidColor = Color.FromArgb(255, r, g, b);
            outerRing.SolidColor = Color.FromArgb(255, r, g, b);

            this.Text = string.Format("ColorDialer [{0}, {1}, {2}]", r, g, b);

            getColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Handles the Paint event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        public void DragForm_Paint(object sender, PaintEventArgs e)
        {
            //Draws a border to make the Form stand out
            //Just done for appearance, not necessary


            Pen p = new Pen(ColorBorderDragPaint, 1);
            e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            p.Dispose();
        }

        /// <summary>
        /// Handles the MouseDown event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseDown(object sender, MouseEventArgs e)
        {

            lastClick = new Point(e.X, e.Y); //We'll need this for when the Form starts to move
        }

        /// <summary>
        /// Handles the MouseMove event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseMove(object sender, MouseEventArgs e)
        {

            //Point newLocation = new Point(e.X - lastE.X, e.Y - lastE.Y);
            if (e.Button == MouseButtons.Left) //Only when mouse is clicked
            {
                //Move the Form the same difference the mouse cursor moved;
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            DoubleBuffered = true;
            //this.StartPosition = FormStartPosition.CenterParent;

            base.OnLoad(e);
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetColor()
        {
            return getColor;
        }

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

        public event EventHandler ColorChanged
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
        /// Handles the <see cref="E:ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            if (this.onValueChanged == null)
                return;
            this.onValueChanged((object)this, e);
        }

        #endregion
        

    }

    #endregion

    #region Designer Generated Code
    partial class ZeroitAsanaColor
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
            getColor = BackColor;
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsanaColorDialog));
            this.cc = new ZeroitAsanaCompass();
            //this.button1 = new System.Windows.Forms.Button();
            //this.button2 = new System.Windows.Forms.Button();
            //this.panel1 = new System.Windows.Forms.Panel();
            //this.panel2 = new System.Windows.Forms.Panel();
            //this.button3 = new System.Windows.Forms.Button();
            //this.panel1.SuspendLayout();
            //this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cc
            // 
            this.cc.AngleMax = 360F;
            this.cc.AngleMin = 0F;
            this.cc.AngleWraps = true;
            this.cc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cc.CursorCanDrag = System.Windows.Forms.Cursors.Hand;
            this.cc.CursorCannotDrag = System.Windows.Forms.Cursors.Arrow;
            this.cc.CursorDragging = System.Windows.Forms.Cursors.SizeAll;
            this.cc.FixedBackground = false;
            this.cc.Location = new System.Drawing.Point(0, 37);
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
            this.cc.PrimaryMarkerSolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cc.Size = new System.Drawing.Size(522, 462);
            this.cc.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.cc.TabIndex = 0;
            this.cc.AngleChanged += new ZeroitAsanaCompass.AngleChangedHandler(this.cc_AngleChanged);
            cc.Dock = DockStyle.Fill;
            // 
            // button1
            // 
            //this.button1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            //this.button1.FlatAppearance.BorderSize = 0;
            //this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.button1.Location = new System.Drawing.Point(115, 7);
            //this.button1.Name = "button1";
            //this.button1.Size = new System.Drawing.Size(108, 38);
            //this.button1.TabIndex = 1;
            //this.button1.Text = "OK";
            //this.button1.UseVisualStyleBackColor = false;
            //this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            //this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            //this.button2.FlatAppearance.BorderSize = 0;
            //this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.button2.Location = new System.Drawing.Point(250, 7);
            //this.button2.Name = "button2";
            //this.button2.Size = new System.Drawing.Size(108, 38);
            //this.button2.TabIndex = 2;
            //this.button2.Text = "Cancel";
            //this.button2.UseVisualStyleBackColor = false;
            //this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            //this.panel1.Controls.Add(this.button3);
            //this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            //this.panel1.Location = new System.Drawing.Point(0, 0);
            //this.panel1.Name = "panel1";
            //this.panel1.Size = new System.Drawing.Size(522, 36);
            //this.panel1.TabIndex = 3;
            //this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            //this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            // 
            // panel2
            // 
            //this.panel2.Controls.Add(this.button1);
            //this.panel2.Controls.Add(this.button2);
            //this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            //this.panel2.Location = new System.Drawing.Point(0, 498);
            //this.panel2.Name = "panel2";
            //this.panel2.Size = new System.Drawing.Size(522, 53);
            //this.panel2.TabIndex = 4;
            //this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            //this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            // 
            // button3
            // 
            //this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            //this.button3.FlatAppearance.BorderSize = 0;
            //this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.button3.Location = new System.Drawing.Point(489, 3);
            //this.button3.Name = "button3";
            //this.button3.Size = new System.Drawing.Size(29, 27);
            //this.button3.TabIndex = 3;
            //this.button3.Text = "X";
            //this.button3.UseVisualStyleBackColor = false;
            //this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(522, 551);
            //this.Controls.Add(this.panel2);
            //this.Controls.Add(this.panel1);
            this.Controls.Add(this.cc);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Name = "MainForm";
            this.Text = "ColorDialer";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseMove);
            //this.Dock = DockStyle.Fill;
            //this.panel1.ResumeLayout(false);
            //this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The cc
        /// </summary>
        private ZeroitAsanaCompass cc;
        //private System.Windows.Forms.Button button1;
        //private System.Windows.Forms.Button button2;
        //private System.Windows.Forms.Panel panel1;
        //private System.Windows.Forms.Button button3;
        //private System.Windows.Forms.Panel panel2;
    }
    #endregion

    #endregion
}
