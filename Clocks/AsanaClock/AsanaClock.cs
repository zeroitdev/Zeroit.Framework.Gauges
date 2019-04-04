// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="AsanaClock.cs" company="Zeroit Dev Technologies">
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
using Zeroit.Framework.Gauges.Compass;


#endregion

namespace Zeroit.Framework.Gauges.Clocks.ZeroitAsanaClock
{

    #region Control

    /// <summary>
    /// Class ZeroitAsanaClock.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [Designer(typeof(ZeroitAsanaClockDesigner))]
    public partial class ZeroitAsanaClock : UserControl
    {

        #region Private Fields
        // Hour ticks
        /// <summary>
        /// The hour ticks color
        /// </summary>
        private Color hourTicksColor = Color.DarkBlue;

        // Minute ticks
        /// <summary>
        /// The minute ticks color
        /// </summary>
        private Color minuteTicksColor = Color.DarkGray;

        // Hour hand
        /// <summary>
        /// The hour hand color
        /// </summary>
        private Color hourHandColor = Color.Blue;

        // Minute hand
        /// <summary>
        /// The minute hand color
        /// </summary>
        private Color minuteHandColor = Color.DodgerBlue;

        // Second hand
        /// <summary>
        /// The second hand color
        /// </summary>
        private Color secondHandColor = Color.SlateBlue;

        // Hour text
        /// <summary>
        /// The hour text color
        /// </summary>
        private Color hourTextColor = Color.DarkSlateGray;

        // Time is only a 12-hour loop (count seconds 0 .. 12*60*60 = 43199)
        /// <summary>
        /// The clock length
        /// </summary>
        private const double CLOCK_LENGTH = 12 * 60.0 * 60.0;

        /// <summary>
        /// The hour
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet hour;
        /// <summary>
        /// The minute
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet minute;
        /// <summary>
        /// The second
        /// </summary>
        private ZeroitAsanaCompass.MarkerSet second;

        /// <summary>
        /// The dragging
        /// </summary>
        private bool dragging;
        /// <summary>
        /// The dt last
        /// </summary>
        private int dtLast;
        /// <summary>
        /// The time
        /// </summary>
        private double time;

        /// <summary>
        /// The show time
        /// </summary>
        private bool showTime = true;

        /// <summary>
        /// The degrees per hour
        /// </summary>
        private const float DEGREES_PER_HOUR = 360.0f / 12.0f;
        /// <summary>
        /// The degrees per minute
        /// </summary>
        private const float DEGREES_PER_MINUTE = 360.0f / 60.0f;
        /// <summary>
        /// The degrees per second
        /// </summary>
        private const float DEGREES_PER_SECOND = 360.0f / 60.0f;

        /// <summary>
        /// The digital time font
        /// </summary>
        private Font digitalTimeFont = new Font("Tahoma", 10);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the digital font.
        /// </summary>
        /// <value>The digital font.</value>
        public Font DigitalFont
        {
            get { return digitalTimeFont; }
            set
            {
                digitalTimeFont = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour ticks.
        /// </summary>
        /// <value>The color of the hour ticks.</value>
        public Color HourTicksColor
        {
            get { return hourTicksColor; }
            set
            {
                hourTicksColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the minute ticks.
        /// </summary>
        /// <value>The color of the minute ticks.</value>
        public Color MinuteTicksColor
        {
            get { return minuteTicksColor; }
            set
            {
                minuteTicksColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour hand.
        /// </summary>
        /// <value>The color of the hour hand.</value>
        public Color HourHandColor
        {
            get { return hourHandColor; }
            set
            {
                hourHandColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the minute hand.
        /// </summary>
        /// <value>The color of the minute hand.</value>
        public Color MinuteHandColor
        {
            get { return minuteHandColor; }
            set
            {
                minuteHandColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the second hand.
        /// </summary>
        /// <value>The color of the second hand.</value>
        public Color SecondHandColor
        {
            get { return secondHandColor; }
            set
            {
                secondHandColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour text.
        /// </summary>
        /// <value>The color of the hour text.</value>
        public Color HourTextColor
        {
            get { return hourTextColor; }
            set
            {
                hourTextColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show label time].
        /// </summary>
        /// <value><c>true</c> if [show label time]; otherwise, <c>false</c>.</value>
        public bool ShowLabelTime
        {
            get { return showTime; }
            set
            {
                if (value == false)
                {
                    //Controls.Remove(timeLabel);
                    timeLabel.Visible = false;

                }

                else
                {
                    //this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
                    //this.timeLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //this.timeLabel.Location = new System.Drawing.Point(0, 0);
                    //this.timeLabel.Name = "timeLabel";
                    //this.timeLabel.Size = new System.Drawing.Size(331, 62);
                    //this.timeLabel.TabIndex = 0;
                    //this.timeLabel.Text = "HH:MM:SS";     
                    //this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    //Controls.Add(timeLabel);
                    timeLabel.Visible = true;
                }

                showTime = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaClock"/> class.
        /// </summary>
        public ZeroitAsanaClock()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            
            //switch (_numberType)
            //{
            //    case numberType.Roman:
            //        cc.TextItems.Add(hourFont, hourBrush, size, "XII", pos, 90.0f, 0.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "I", pos, 60.0f, -30.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "II", pos, 30.0f, -60.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "III", pos, 0.0f, -90.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "IV", pos, -30.0f, -120.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "V", pos, -60.0f, -150.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "VI", pos, -90.0f, -180.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "VII", pos, -120.0f, -210.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "VIII", pos, -150.0f, -240.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "IX", pos, -180.0f, -270.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "X", pos, -210.0f, -300.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "XI", pos, -240.0f, -330.0f);
            //        Invalidate();
            //        break;
            //    case numberType.Number:
            //        cc.TextItems.Add(hourFont, hourBrush, size, "12", pos, 90.0f, 0.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "1", pos, 60.0f, -30.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "2", pos, 30.0f, -60.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "3", pos, 0.0f, -90.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "4", pos, -30.0f, -120.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "5", pos, -60.0f, -150.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "6", pos, -90.0f, -180.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "7", pos, -120.0f, -210.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "8", pos, -150.0f, -240.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "9", pos, -180.0f, -270.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "10", pos, -210.0f, -300.0f);
            //        cc.TextItems.Add(hourFont, hourBrush, size, "11", pos, -240.0f, -330.0f);
            //        break;
            //    default:
            //        break;
            //}


            dragging = false;
            dtLast = DateTimeNowToSeconds;
            SyncTime();
            ShowTime();
            timer.Start();


        }

        /// <summary>
        /// Clocks the parameters.
        /// </summary>
        private void ClockParameters()
        {
            cc.MarkerSets.Clear();

            // Hour ticks
            cc.MajorTicks = 12;
            cc.MajorTickStart = 0.30f;
            cc.MajorTickSize = 0.50f;
            cc.MajorTickThickness = 2.0f;
            cc.MajorTickColor = hourTicksColor;


            // Minute ticks
            cc.MinorTicksPerMajorTick = 4;
            cc.MinorTickStart = 0.30f;
            cc.MinorTickSize = 0.40f;
            cc.MinorTickThickness = 1.0f;
            cc.MinorTickColor = minuteTicksColor;

            
            // Hour hand
            ArrayList hourPoly = new ArrayList();
            hourPoly.Add(new PointF(-0.0250f, 0.0000f));
            hourPoly.Add(new PointF(-0.0250f, 0.0125f));
            hourPoly.Add(new PointF(-0.0125f, 0.0250f));
            hourPoly.Add(new PointF(0.0125f, 0.0250f));
            hourPoly.Add(new PointF(0.0250f, 0.0125f));
            hourPoly.Add(new PointF(0.0500f, 0.0125f));
            hourPoly.Add(new PointF(0.0500f, 0.0250f));
            hourPoly.Add(new PointF(0.0750f, 0.0500f));
            hourPoly.Add(new PointF(0.1500f, 0.0500f));
            hourPoly.Add(new PointF(0.5000f, 0.0000f));
            hourPoly.Add(new PointF(0.1500f, -0.0500f));
            hourPoly.Add(new PointF(0.0750f, -0.0500f));
            hourPoly.Add(new PointF(0.0500f, -0.0250f));
            hourPoly.Add(new PointF(0.0500f, -0.0125f));
            hourPoly.Add(new PointF(0.0250f, -0.0125f));
            hourPoly.Add(new PointF(0.0125f, -0.0250f));
            hourPoly.Add(new PointF(-0.0125f, -0.0250f));
            hourPoly.Add(new PointF(-0.0250f, -0.0125f));
            hourPoly.Add(new PointF(-0.0250f, 0.0000f));
            hour = new ZeroitAsanaCompass.MarkerSet();
            hour.Add(ZeroitAsanaCompass.MakeArgb(0.8f, hourHandColor), Color.Black, 1.0f,
                (PointF[]) hourPoly.ToArray(typeof(PointF)));
            cc.MarkerSets.Add(hour);

            // Minute hand
            ArrayList minutePoly = new ArrayList();
            minutePoly.Add(new PointF(-0.0250f, 0.0000f));
            minutePoly.Add(new PointF(-0.0250f, 0.0125f));
            minutePoly.Add(new PointF(-0.0125f, 0.0250f));
            minutePoly.Add(new PointF(0.0125f, 0.0250f));
            minutePoly.Add(new PointF(0.0250f, 0.0125f));
            minutePoly.Add(new PointF(0.0500f, 0.0125f));
            minutePoly.Add(new PointF(0.0500f, 0.0250f));
            minutePoly.Add(new PointF(0.0750f, 0.0500f));
            minutePoly.Add(new PointF(0.1500f, 0.0500f));
            minutePoly.Add(new PointF(0.7000f, 0.0000f));
            minutePoly.Add(new PointF(0.1500f, -0.0500f));
            minutePoly.Add(new PointF(0.0750f, -0.0500f));
            minutePoly.Add(new PointF(0.0500f, -0.0250f));
            minutePoly.Add(new PointF(0.0500f, -0.0125f));
            minutePoly.Add(new PointF(0.0250f, -0.0125f));
            minutePoly.Add(new PointF(0.0125f, -0.0250f));
            minutePoly.Add(new PointF(-0.0125f, -0.0250f));
            minutePoly.Add(new PointF(-0.0250f, -0.0125f));
            minutePoly.Add(new PointF(-0.0250f, 0.0000f));
            minute = new ZeroitAsanaCompass.MarkerSet();
            minute.Add(ZeroitAsanaCompass.MakeArgb(0.8f, minuteHandColor), Color.Black, 1.0f,
                (PointF[]) minutePoly.ToArray(typeof(PointF)));
            cc.MarkerSets.Add(minute);

            // Second hand
            ArrayList secondPoly = new ArrayList();
            secondPoly.Add(new PointF(-0.0250f, 0.0000f));
            secondPoly.Add(new PointF(-0.0250f, 0.0125f));
            secondPoly.Add(new PointF(-0.0125f, 0.0250f));
            secondPoly.Add(new PointF(0.0125f, 0.0250f));
            secondPoly.Add(new PointF(0.0250f, 0.0125f));
            secondPoly.Add(new PointF(0.1500f, 0.0125f));
            secondPoly.Add(new PointF(0.6000f, 0.0000f));
            secondPoly.Add(new PointF(0.1500f, -0.0125f));
            secondPoly.Add(new PointF(0.0250f, -0.0125f));
            secondPoly.Add(new PointF(0.0125f, -0.0250f));
            secondPoly.Add(new PointF(-0.0125f, -0.0250f));
            secondPoly.Add(new PointF(-0.0250f, -0.0125f));
            secondPoly.Add(new PointF(-0.0250f, 0.0000f));
            second = new ZeroitAsanaCompass.MarkerSet();
            second.Add(ZeroitAsanaCompass.MakeArgb(0.8f, secondHandColor), Color.Black, 1.0f,
                (PointF[]) secondPoly.ToArray(typeof(PointF)), 0.0f, MouseButtons.None, true);
            cc.MarkerSets.Add(second);

            // Hour text - roman numerals
            Font hourFont = new Font("Times New Roman", 10.0f, FontStyle.Bold);
            Brush hourBrush = new SolidBrush(hourTextColor);
            float size = 0.18f;
            float pos = 0.90f;

            cc.TextItems.Add(hourFont, hourBrush, size, "12", pos, 90.0f, 0.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "1", pos, 60.0f, -30.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "2", pos, 30.0f, -60.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "3", pos, 0.0f, -90.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "4", pos, -30.0f, -120.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "5", pos, -60.0f, -150.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "6", pos, -90.0f, -180.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "7", pos, -120.0f, -210.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "8", pos, -150.0f, -240.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "9", pos, -180.0f, -270.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "10", pos, -210.0f, -300.0f);
            cc.TextItems.Add(hourFont, hourBrush, size, "11", pos, -240.0f, -330.0f);

            
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Synchronizes the time.
        /// </summary>
        private void SyncTime() // match clock time current time
        {
            time = DateTimeNowToSeconds;
            AdjustTime();
        }

        /// <summary>
        /// Gets the date time now to seconds.
        /// </summary>
        /// <value>The date time now to seconds.</value>
        private int DateTimeNowToSeconds
        {
            get
            {
                DateTime now = DateTime.Now;
                int t = (3600 * now.Hour) + (60 * now.Minute) + now.Second;
                return t;
            }
        }

        /// <summary>
        /// Adjusts the time.
        /// </summary>
        private void AdjustTime()
        {
            while (time < 0.0)
            {
                time += CLOCK_LENGTH;
            }
            while (time >= CLOCK_LENGTH)
            {
                time -= CLOCK_LENGTH;
            }
        }

        /// <summary>
        /// Ccs the angle changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void cc_AngleChanged(object sender, ZeroitAsanaCompass.AngleChangedArgs e)
        {
            if (e.Mouse == ZeroitAsanaCompass.AngleChangedArgs.MouseState.Down)
            {
                dragging = true;
            }
            else if (e.Mouse == ZeroitAsanaCompass.AngleChangedArgs.MouseState.Dragging)
            {
                double changeInSeconds = 0.0;
                if (e.Ms == hour)
                {
                    double changeInHours = -e.AngleChange / DEGREES_PER_HOUR;
                    changeInSeconds = changeInHours * 60.0 * 60.0;
                }
                else if (e.Ms == minute)
                {
                    double changeInMinutes = -e.AngleChange / DEGREES_PER_MINUTE;
                    changeInSeconds = changeInMinutes * 60.0;
                }
                time += changeInSeconds;
                AdjustTime();
                ShowTime();
            }
            else if (e.Mouse == ZeroitAsanaCompass.AngleChangedArgs.MouseState.Up)
            {
                dtLast = DateTimeNowToSeconds;
                dragging = false;
            }
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!dragging)
            {
                int dt = DateTimeNowToSeconds;
                if (dt != dtLast)
                {
                    time++;
                    ShowTime();
                    dtLast = dt;
                }
            }
        }

        /// <summary>
        /// Shows the time.
        /// </summary>
        private void ShowTime()
        {
            ClockParameters();
            //cc.MarkerSets.Add(second);

            int t = (int)time;
            int hours = t / 3600;
            int minutes = (t % 3600) / 60;
            int seconds = t % 60;

            int hoursShow = (hours == 0) ? 12 : hours;
            
            timeLabel.Text = string.Format("{0}:{1}:{2}", hoursShow.ToString("##"), minutes.ToString("00"), seconds.ToString("00"));
            timeLabel.Font = DigitalFont;

            // Convert time to angle of hour hand
            float th = (float)hours + (float)minutes / 60.0f + (float)seconds / 3600.0f;
            float h = (15.0f - th) % 12.0f;
            hour.Angle = DEGREES_PER_HOUR * h;

            // Convert time to angle of minute hand
            float tm = (float)minutes + (float)seconds / 60.0f;
            float m = (75.0f - tm) % 60.0f;
            minute.Angle = DEGREES_PER_MINUTE * m;

            // Convert time to angle of second hand
            float s = (75.0f - (float)seconds) % 60.0f;
            second.Angle = DEGREES_PER_SECOND * s;
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            TransInPaint(e.Graphics);
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

    }

    #endregion

    #region Designer Generated Code
    partial class ZeroitAsanaClock
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZeroitAsanaClock));
            this.cc = new ZeroitAsanaCompass();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.split = new System.Windows.Forms.SplitContainer();
            this.timeLabel = new System.Windows.Forms.Label();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // cc
            // 
            this.cc.AngleMax = 360F;
            this.cc.AngleMin = 0F;
            this.cc.AngleWraps = true;
            this.cc.BorderStyle = this.BorderStyle;
            this.cc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc.Location = new System.Drawing.Point(0, 20);
            this.cc.PrimaryMarkerAngle = 0F;
            this.cc.PrimaryMarkerBorderColor = System.Drawing.Color.Black;
            this.cc.PrimaryMarkerBorderSize = 2F;
            this.cc.PrimaryMarkerPoints = new System.Drawing.PointF[4];
            cc.PrimaryMarkerPoints[0] = new PointF ( 0.35f, 0f );
            cc.PrimaryMarkerPoints[1] = new PointF(0.8f, 0.2f);
            cc.PrimaryMarkerPoints[2] = new PointF(0.7f, 0f);
            cc.PrimaryMarkerPoints[3] = new PointF(0.8f, 0.2f);
            //((System.Drawing.PointF)(resources.GetObject("cc.MarkerPoints"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.MarkerPoints1"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.MarkerPoints2"))),
            //((System.Drawing.PointF)(resources.GetObject("cc.MarkerPoints3")))};
            this.cc.PrimaryMarkerSolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(331, 271);
            this.cc.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.cc.TabIndex = 0;
            this.cc.MajorTickColor = System.Drawing.Color.Black;
            this.cc.MajorTickSize = 0.45F;
            this.cc.MajorTickStart = 0.4F;
            this.cc.AngleChanged += new ZeroitAsanaCompass.AngleChangedHandler(this.cc_AngleChanged);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.split.IsSplitterFixed = true;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.timeLabel);
            this.split.Panel1MinSize = 60;
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.cc);
            this.split.Size = new System.Drawing.Size(331, 337);
            this.split.SplitterDistance = 62;
            this.split.TabIndex = 1;
            // 
            // timeLabel
            // 
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(331, 62);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "HH:MM:SS";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // 
            // ZeroitAsanaClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 337);
            this.Controls.Add(this.split);
            this.Name = "AsanClock";
            this.Text = "Asana:Analog Clock";
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The cc
        /// </summary>
        private Zeroit.Framework.Gauges.Compass.ZeroitAsanaCompass cc;
        /// <summary>
        /// The timer
        /// </summary>
        private System.Windows.Forms.Timer timer;
        /// <summary>
        /// The split
        /// </summary>
        private System.Windows.Forms.SplitContainer split;
        /// <summary>
        /// The time label
        /// </summary>
        private System.Windows.Forms.Label timeLabel;
    }
    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitAsanaClockDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitAsanaClockDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitAsanaClockDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitAsanaClockSmartTagActionList(this.Component));
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
    /// Class ZeroitAsanaClockSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitAsanaClockSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAsanaClock colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAsanaClockSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitAsanaClockSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAsanaClock;

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
        /// Gets or sets a value indicating whether [show label time].
        /// </summary>
        /// <value><c>true</c> if [show label time]; otherwise, <c>false</c>.</value>
        public bool ShowLabelTime
        {
            get
            {
                return colUserControl.ShowLabelTime;
            }
            set
            {
                GetPropertyByName("ShowLabelTime").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour ticks.
        /// </summary>
        /// <value>The color of the hour ticks.</value>
        public Color HourTicksColor
        {
            get
            {
                return colUserControl.HourTicksColor;
            }
            set
            {
                GetPropertyByName("HourTicksColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the minute ticks.
        /// </summary>
        /// <value>The color of the minute ticks.</value>
        public Color MinuteTicksColor
        {
            get
            {
                return colUserControl.MinuteTicksColor;
            }
            set
            {
                GetPropertyByName("MinuteTicksColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour hand.
        /// </summary>
        /// <value>The color of the hour hand.</value>
        public Color HourHandColor
        {
            get
            {
                return colUserControl.HourHandColor;
            }
            set
            {
                GetPropertyByName("HourHandColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the minute hand.
        /// </summary>
        /// <value>The color of the minute hand.</value>
        public Color MinuteHandColor
        {
            get
            {
                return colUserControl.MinuteHandColor;
            }
            set
            {
                GetPropertyByName("MinuteHandColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the second hand.
        /// </summary>
        /// <value>The color of the second hand.</value>
        public Color SecondHandColor
        {
            get
            {
                return colUserControl.SecondHandColor;
            }
            set
            {
                GetPropertyByName("SecondHandColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hour text.
        /// </summary>
        /// <value>The color of the hour text.</value>
        public Color HourTextColor
        {
            get
            {
                return colUserControl.HourTextColor;
            }
            set
            {
                GetPropertyByName("HourTextColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the digital font.
        /// </summary>
        /// <value>The digital font.</value>
        public Font DigitalFont
        {
            get
            {
                return colUserControl.DigitalFont;
            }
            set
            {
                GetPropertyByName("DigitalFont").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("ShowLabelTime",
                "Show Label Time", "Appearance",
                "Set to show text."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Digital Time Color", "Appearance",
                                 "Selects the foreground color."));
            
            items.Add(new DesignerActionPropertyItem("HourTicksColor",
                                 "Hour Ticks Color", "Appearance",
                                 "Sets the color of the hour-tick handle."));
            
            items.Add(new DesignerActionPropertyItem("MinuteTicksColor",
                "Minute Ticks Color", "Appearance",
                "Sets the color of the minute-tick handle."));

            items.Add(new DesignerActionPropertyItem("HourHandColor",
                "Hour Hand Color", "Appearance",
                "Sets the color of the hour handle."));


            items.Add(new DesignerActionPropertyItem("MinuteHandColor",
                "Minute Hand Color", "Appearance",
                "Sets the color of the minute handle."));

            items.Add(new DesignerActionPropertyItem("SecondHandColor",
                "Second Hand Color", "Appearance",
                "Sets the color of the seconds handle."));


            items.Add(new DesignerActionPropertyItem("HourTextColor",
                "Hour Text Color", "Appearance",
                "Sets the color of the hour text handle."));

            items.Add(new DesignerActionPropertyItem("DigitalFont",
                "Digital Font", "Appearance",
                "Sets the font the digital time."));


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
