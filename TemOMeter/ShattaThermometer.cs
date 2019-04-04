// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShattaThermometer.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Drawing2D;
using System.Text;
//using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
//using Zeroit.Framework.Widgets;

#endregion

namespace Zeroit.Framework.Gauges.Thermometer
{

    #region ZeroitTemOMeter

    /// <summary>
    /// The TomOMeter will display values using a pleasant meter-style display.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitTemOMeterDesigner))]
    public class ZeroitTemOMeter : System.Windows.Forms.Control
    {

        #region Private Fields

        /// <summary>
        /// The start
        /// </summary>
        int start;
        /// <summary>
        /// The end
        /// </summary>
        int end = 5;
        /// <summary>
        /// The range
        /// </summary>
        int range;
        /// <summary>
        /// The r zone
        /// </summary>
        int rZone;
        /// <summary>
        /// The needle color
        /// </summary>
        Color needleColor = Color.Blue;
        /// <summary>
        /// The rz color
        /// </summary>
        Color rzColor = Color.Red;
        /// <summary>
        /// The hm color
        /// </summary>
        Color hmColor = Color.Yellow;
        /// <summary>
        /// The deflection
        /// </summary>
        private float deflection, modifier, hSize;
        /// <summary>
        /// The label
        /// </summary>
        String label, units;
        /// <summary>
        /// The number font
        /// </summary>
        Font numFont;
        /// <summary>
        /// The end cap
        /// </summary>
        LineCap endCap = LineCap.Round;
        /// <summary>
        /// The start cap
        /// </summary>
        LineCap startCap = LineCap.ArrowAnchor;
        #endregion

        #region Properties

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
        /// Gets or sets the start cap.
        /// </summary>
        /// <value>The start cap.</value>
        public LineCap StartCap
        {
            get { return startCap; }
            set
            {
                startCap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        /// <value>The end cap.</value>
        public LineCap EndCap
        {
            get { return endCap; }
            set
            {
                endCap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the deflection.
        /// </summary>
        /// <value>The deflection.</value>
        public float Deflection
        {
            get { return deflection; }
            set
            {
                if (range != 0)
                    deflection = ((value - start) * (120 / (float)range)) + 30;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the width of the hash mark.
        /// </summary>
        /// <value>The width of the hash mark.</value>
        public float HashMarkWidth
        {
            get { return hSize; }
            set
            {
                hSize = value;
                this.Refresh();
            }
        }

        //Getter / Setter for Range
        /// <summary>
        /// Gets or sets the start value.
        /// </summary>
        /// <value>The start value.</value>
        public int StartValue
        {
            get { return start; }
            set
            {
                start = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the end value.
        /// </summary>
        /// <value>The end value.</value>
        public int EndValue
        {
            get { return end; }
            set
            {
                end = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the red zone.
        /// </summary>
        /// <value>The red zone.</value>
        public int RedZone
        {
            get { return rZone; }
            set
            {
                rZone = value;
                this.Refresh();
            }
        }

        //Getter / Setter for Needle Color
        /// <summary>
        /// Gets or sets the color of the needle.
        /// </summary>
        /// <value>The color of the needle.</value>
        public Color NeedleColor
        {
            get { return needleColor; }
            set
            {
                needleColor = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the red zone.
        /// </summary>
        /// <value>The color of the red zone.</value>
        public Color RedZoneColor
        {
            get { return rzColor; }
            set
            {
                rzColor = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the hash mark.
        /// </summary>
        /// <value>The color of the hash mark.</value>
        public Color HashMarkColor
        {
            get { return hmColor; }
            set
            {
                hmColor = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public String Label
        {
            get { return label; }
            set
            {
                label = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        public String Units
        {
            get { return units; }
            set
            {
                units = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the number font.
        /// </summary>
        /// <value>The number font.</value>
        public Font NumberFont
        {
            get { return numFont; }
            set
            {
                numFont = value;
                this.Refresh();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitTemOMeter"/> class.
        /// </summary>
        public ZeroitTemOMeter()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
            deflection = 30;
            modifier = 0;
            numFont = new Font(this.Font.FontFamily.Name, 10);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // UserControl1
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControl1_Paint);

        }
        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Handles the Paint event of the UserControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        public void UserControl1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            PointF center = new PointF(this.Width / 2, this.Height);
            Rectangle backing = new Rectangle(new Point(0, this.Height / 10), new Size(this.Width, (this.Height * 2) + (this.Height / 10)));
            Graphics g = e.Graphics;
            g.SmoothingMode = smoothing;

            Pen needlePen = new Pen(needleColor, 2);
            needlePen.EndCap = startCap;
            needlePen.StartCap = endCap;

            Pen textPen = new Pen(ForeColor);
            Pen hmPen = new Pen(hmColor, hSize);
            Pen rzPen = new Pen(rzColor, hSize);
            SolidBrush textBrush = new SolidBrush(ForeColor);
            Font labelFont = new Font(FontFamily.GenericSansSerif, 25);
            Font unitFont = new Font(FontFamily.GenericSerif, 15, FontStyle.Italic);
            int rzAngle = 150 * rZone / end;
            range = end - start;
            modifier = (float)range / 120;

            // Draw Label
            g.DrawString(label, labelFont, Brushes.Black, this.Width * 1 / 3, this.Height / 2);
            g.DrawString(units, unitFont, Brushes.Black, this.Width * 3 / 7, this.Height * 3 / 4);

            // Draw Numbers
            for (double i = 30f; i <= 150f; i += 12f)
            {
                PointF numPoint = new Point((int)(center.X - (float)(Math.Cos(i * Math.PI / 180) * center.X) - 7), (int)(center.Y - (float)(Math.Sin(i * Math.PI / 180) * center.X) - 20));
                g.DrawString((Math.Round((i - 30) * modifier) + start).ToString(), numFont, textBrush, numPoint);
            }

            // draw hash marks
            for (int i = 30; i <= 150; i += 12)
            {
                Matrix matrix = new Matrix();
                matrix.RotateAt(i, center);
                g.Transform = matrix;
                if (i >= rzAngle)
                    g.DrawLine(rzPen, this.Width / 8, this.Height, 0, this.Height);
                else
                    g.DrawLine(hmPen, this.Width / 8, this.Height, 0, this.Height);
            }



            //Draw Needle
            Matrix m = new Matrix();
            m.RotateAt(deflection, center);
            g.Transform = m;
            g.DrawLine(needlePen, this.Width / 2, this.Height, 0, this.Height);
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

    //--------------- [Designer(typeof(ZeroitTemOMeterDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitTemOMeterDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitTemOMeterDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitTemOMeterSmartTagActionList(this.Component));
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
    /// Class ZeroitTemOMeterSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitTemOMeterSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitTemOMeter colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitTemOMeterSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitTemOMeterSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitTemOMeter;

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
        /// Gets or sets the color of the red zone.
        /// </summary>
        /// <value>The color of the red zone.</value>
        public Color RedZoneColor
        {
            get
            {
                return colUserControl.RedZoneColor;
            }
            set
            {
                GetPropertyByName("RedZoneColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hash mark.
        /// </summary>
        /// <value>The color of the hash mark.</value>
        public Color HashMarkColor
        {
            get
            {
                return colUserControl.HashMarkColor;
            }
            set
            {
                GetPropertyByName("HashMarkColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the start cap.
        /// </summary>
        /// <value>The start cap.</value>
        public LineCap StartCap
        {
            get
            {
                return colUserControl.StartCap;
            }
            set
            {
                GetPropertyByName("StartCap").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        /// <value>The end cap.</value>
        public LineCap EndCap
        {
            get
            {
                return colUserControl.EndCap;
            }
            set
            {
                GetPropertyByName("EndCap").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the deflection.
        /// </summary>
        /// <value>The deflection.</value>
        public float Deflection
        {
            get
            {
                return colUserControl.Deflection;
            }
            set
            {
                GetPropertyByName("Deflection").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the hash mark.
        /// </summary>
        /// <value>The width of the hash mark.</value>
        public float HashMarkWidth
        {
            get
            {
                return colUserControl.HashMarkWidth;
            }
            set
            {
                GetPropertyByName("HashMarkWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the start value.
        /// </summary>
        /// <value>The start value.</value>
        public int StartValue
        {
            get
            {
                return colUserControl.StartValue;
            }
            set
            {
                GetPropertyByName("StartValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the end value.
        /// </summary>
        /// <value>The end value.</value>
        public int EndValue
        {
            get
            {
                return colUserControl.EndValue;
            }
            set
            {
                GetPropertyByName("EndValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the red zone.
        /// </summary>
        /// <value>The red zone.</value>
        public int RedZone
        {
            get
            {
                return colUserControl.RedZone;
            }
            set
            {
                GetPropertyByName("RedZone").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public String Label
        {
            get
            {
                return colUserControl.Label;
            }
            set
            {
                GetPropertyByName("Label").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        public String Units
        {
            get
            {
                return colUserControl.Units;
            }
            set
            {
                GetPropertyByName("Units").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the number font.
        /// </summary>
        /// <value>The number font.</value>
        public Font NumberFont
        {
            get
            {
                return colUserControl.NumberFont;
            }
            set
            {
                GetPropertyByName("NumberFont").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("NeedleColor",
                                 "Needle Color", "Appearance",
                                 "Set the needle color."));

            items.Add(new DesignerActionPropertyItem("RedZoneColor",
                                 "Red-Zone Color", "Appearance",
                                 "Set the red-zone color."));

            items.Add(new DesignerActionPropertyItem("HashMarkColor",
                "Hash Mark Color", "Appearance",
                "Set the hash mark color."));

            items.Add(new DesignerActionPropertyItem("StartCap",
                "Start Cap", "Appearance",
                "Set the start cap."));

            items.Add(new DesignerActionPropertyItem("EndCap",
                "End Cap", "Appearance",
                "Set the end cap."));

            items.Add(new DesignerActionPropertyItem("Deflection",
                "Deflection", "Appearance",
                "Set the deflection."));

            items.Add(new DesignerActionPropertyItem("HashMarkWidth",
                "Hash Mark Width", "Appearance",
                "Set the hash mark width."));

            items.Add(new DesignerActionPropertyItem("StartValue",
                "Start Value", "Appearance",
                "Set the end value."));

            items.Add(new DesignerActionPropertyItem("EndValue",
                "End Value", "Appearance",
                "Set the start value."));

            items.Add(new DesignerActionPropertyItem("RedZone",
                "Red-Zone", "Appearance",
                "Set the red zone."));

            items.Add(new DesignerActionPropertyItem("Label",
                "Label", "Appearance",
                "Sets the text label."));

            items.Add(new DesignerActionPropertyItem("Units",
                "Units", "Appearance",
                "Set the type of unit of measurement."));

            items.Add(new DesignerActionPropertyItem("NumberFont",
                "NumberFont", "Appearance",
                "Sets the number font."));

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
    
    #region Old Code

    //#region Control

    //public partial class ShattaThermometer : UserControl
    //{
    //    public ShattaThermometer()
    //    {
    //        InitializeComponent();
    //    }
    //    //public delegate void Mydel();
    //    System.Windows.Forms.Timer t1;
    //    double temp;
    //    System.IO.Ports.SerialPort port;
    //    double voltage;
    //    public string portname;
    //    public Parity parity;
    //    public int BaudRate;
    //    public StopBits stopbits;
    //    public int databits;
    //    int count;
    //    double sum;
    //    byte[] bt;
    //    PaintEventArgs args;
    //    double CurrentAngle;
    //    Rectangle DrawingRectangle;
    //    bool AnimationAllowed;
    //    public string PortName
    //    {
    //        get { return portname; }
    //        set { portname = value; }
    //    }
    //    System.Drawing.BufferedGraphics buff;
    //    Graphics surface;
    //    bool firsttime;
    //    ContextMenu menu;
    //    Point initialPoint;
    //    double InitialTemp;
    //    double IncreaseFactor;
    //    double displaytemp;
    //    private void Form1_Load(object sender, EventArgs e)
    //    {
    //        //this.GotFocus += new EventHandler(Form1_GotFocus);
    //        InitialTemp = 0;
    //        temp = 0;
    //        initialPoint = new Point();
    //        this.Click += new EventHandler(Thermometer_Click);
    //        menu = new ContextMenu();
    //        MenuItem item = new MenuItem("Close");
    //        item.Click += new EventHandler(item_Click);
    //        menu.MenuItems.Add(item);
    //        this.ContextMenu = menu;
    //        firsttime = true;
    //        AnimationAllowed = true;
    //        DrawingRectangle = new Rectangle(0, 0, this.Width, this.Height);
    //        buff = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), DrawingRectangle);
    //        //this.Opacity = 0.5;
    //        //this.TransparencyKey = Color.Black;
    //        surface = buff.Graphics;
    //        surface.PixelOffsetMode = PixelOffsetMode.HighQuality;
    //        surface.SmoothingMode = SmoothingMode.HighQuality;
    //        CurrentAngle = 235;
    //        voltage = 4.65;
    //        args = new PaintEventArgs(surface, this.Bounds);
    //        bt = new byte[1];
    //        count = 0;
    //        sum = 0;
    //        portname = "COM4";
    //        parity = Parity.None;
    //        BaudRate = 9600;
    //        stopbits = StopBits.Two;
    //        databits = 8;

    //        port = new System.IO.Ports.SerialPort(portname);
    //        port.Parity = parity;
    //        port.BaudRate = BaudRate;
    //        port.StopBits = stopbits;
    //        port.DataBits = databits;
    //        port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
    //        port.Open();
    //        count = 0;
    //        //t1 = new Timer();
    //        //t1.Interval = 200;
    //        //t1.Tick += new EventHandler(t1_Tick);
    //        //t1.Start();
    //    }

    //    void Thermometer_Click(object sender, EventArgs e)
    //    {
    //        //throw new NotImplementedException();

    //        //initialPoint = PointToClient(initialPoint);
    //    }



    //    void item_Click(object sender, EventArgs e)
    //    {
    //        //throw new NotImplementedException();
    //        //this.Close();
    //    }

    //    //void t1_Tick(object sender, EventArgs e)
    //    //{
    //    //throw new NotImplementedException();
    //    //t1.Enabled = false;

    //    //temp += 5;
    //    //double angle = 0;
    //    //if ((temp > 30 && temp <= 40) || (temp < 70 && temp > 60))
    //    //{
    //    //    if(temp <=40)
    //    //    angle = -65 + 3 * ((temp-1) % 10) +3;
    //    //    else
    //    //    {
    //    //        angle =35 + 3 * ((temp - 1) % 10) + 3;
    //    //    }

    //    //}
    //    //else if ((temp > 40 && temp <= 50) || (temp <= 60 && temp >= 50))
    //    //{
    //    //    //if (temp == 60) System.Diagnostics.Debugger.Break();
    //    //    if (temp <= 50)
    //    //        angle = -35 + 35 * ((temp - 1) % 10) / 10 + 3.5;
    //    //    else
    //    //    {
    //    //        //if (temp == 50)
    //    //        //    System.Diagnostics.Debugger.Break();
    //    //        angle = 35 * ((temp - 1) % 10) / 10 + 3.5;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    if(temp <= 30)
    //    //    angle = -125 + 2 * temp;
    //    //    else
    //    //    {
    //    //        angle = 65 + 2 *(temp-70);
    //    //    }
    //    //}
    //    //if (AnimationAllowed)
    //    //    Animate(CurrentAngle, angle);
    //    //Form1_Paint(sender, args);
    //    //MessageBox.Show(temp.ToString() + "," + angle.ToString());
    //    ////CurrentAngle += 5;
    //    //t1.Enabled = true;
    //    //}

    //    void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    //    {
    //        port.Read(bt, 0, 1);
    //        sum += Convert.ToDouble(bt[0]) * voltage * 100 / 255;

    //        count++;
    //        if (count == 100)
    //        {
    //            InitialTemp = temp;
    //            temp = sum / 100;
    //            sum = 0;
    //            count = 0;
    //            double angle = 0;
    //            if ((Math.Floor(temp) > 30 && Math.Floor(temp) <= 40) || (Math.Floor(temp) < 70 && Math.Floor(temp) > 60))
    //            {
    //                if (Math.Floor(temp) <= 40)
    //                    angle = -65 + 3 * ((temp - 1) % 10) + 3;
    //                else
    //                {
    //                    angle = 35 + 3 * ((temp - 1) % 10) + 3;
    //                }

    //            }
    //            else if ((Math.Floor(temp) > 40 && Math.Floor(temp) <= 50) || (Math.Floor(temp) <= 60 && Math.Floor(temp) >= 50))
    //            {
    //                //if (temp == 60) System.Diagnostics.Debugger.Break();
    //                if (Math.Floor(temp) <= 50)
    //                    angle = -35 + 35 * ((temp - 1) % 10) / 10 + 3.5;
    //                else
    //                {
    //                    //if (temp == 50)
    //                    //    System.Diagnostics.Debugger.Break();
    //                    angle = 35 * ((temp - 1) % 10) / 10 + 3.5;
    //                }
    //            }
    //            else
    //            {
    //                if (Math.Floor(temp) <= 30)
    //                    angle = -125 + 2 * temp;
    //                else
    //                {
    //                    angle = 65 + 2 * (temp - 70);
    //                }
    //            }
    //            if (AnimationAllowed)
    //                Animate(CurrentAngle, angle);

    //        }
    //        //throw new NotImplementedException();

    //    }



    //    private void Form1_Paint(object sender, PaintEventArgs e)
    //    {
    //        if (firsttime)
    //            if (AnimationAllowed)
    //                AnimationAllowed = false;
    //        //t1.Enabled = false;
    //        try
    //        {
    //            surface.FillRectangle(Brushes.LightSteelBlue, DrawingRectangle);
    //            Image img = new Bitmap(Properties.Resources.speedometer, this.Size);
    //            //Graphics ImageGraphics = Graphics.FromImage(img);
    //            Image hand = new Bitmap(Properties.Resources.MinuteHand);

    //            //handgraphics.TranslateTransform(hand.Width / 2, hand.Height / 2);

    //            //ImageGraphics.DrawImage(hand, new Point(this.Width / 2 -10, 40));
    //            surface.DrawImageUnscaled(img, new Point(0, 0));
    //            //e.Graphics.TranslateTransform(0,0);
    //            surface.TranslateTransform(this.Width / 2f, this.Height / 2f);
    //            surface.RotateTransform((float)CurrentAngle);

    //            surface.DrawImage(hand, new Point(-10, -this.Height / 2 + 40));
    //            string stringtemp = displaytemp.ToString(); //InitialTemp.ToString(); //temp.ToString();
    //            stringtemp = stringtemp.Length > 5 ? stringtemp.Remove(5, stringtemp.Length - 5) : stringtemp;
    //            Font fnt = new Font("Arial", 20);
    //            SizeF siz = surface.MeasureString(stringtemp, fnt);
    //            surface.ResetTransform();
    //            LinearGradientBrush gd = new LinearGradientBrush(new Point(0, (int)siz.Height + 20), new Point((int)siz.Width, 0), Color.Red, Color.Lavender);
    //            surface.DrawString(stringtemp, fnt, gd, new PointF(DrawingRectangle.Width / 2 - siz.Width / 2, 70));
    //            surface.DrawEllipse(Pens.LightGray, DrawingRectangle);

    //            buff.Render();
    //            if (firsttime)
    //            {
    //                firsttime = false;
    //                AnimationAllowed = true;
    //            }
    //        }
    //        catch (InvalidOperationException)
    //        {


    //        }

    //        //MessageBox.Show(count.ToString());
    //        //t1.Enabled = true;
    //    }

    //    private void Animate(double Currentangle, double FinalAngle)
    //    {
    //        //if (FinalAngle == 17.5)
    //        //    System.Diagnostics.Debugger.Break();
    //        if (AnimationAllowed)
    //            AnimationAllowed = false;
    //        if (FinalAngle < 0)
    //            FinalAngle += 360;
    //        if (Currentangle >= 360)
    //        {

    //            Currentangle = Currentangle - 360;
    //        }
    //        if (IncreaseFactor == 0)
    //        {
    //            IncreaseFactor = (temp - InitialTemp) / (FinalAngle - Currentangle);
    //            if (firsttime)
    //                displaytemp = 0;
    //            else
    //                displaytemp = InitialTemp;
    //        }
    //        if (!(Currentangle > FinalAngle - 0.5 && Currentangle < FinalAngle + 0.5))
    //        {

    //            if (Currentangle > FinalAngle)
    //            {
    //                Currentangle -= 1;
    //                displaytemp -= IncreaseFactor;
    //            }
    //            else
    //            {
    //                Currentangle += 1;
    //                displaytemp += IncreaseFactor;
    //            }
    //            CurrentAngle = Currentangle;
    //            Form1_Paint(this, new PaintEventArgs(surface, DrawingRectangle));
    //            //System.Threading.Thread.Sleep(10);
    //            Animate(Currentangle, FinalAngle);
    //        }
    //        else
    //        {
    //            AnimationAllowed = true;
    //            IncreaseFactor = 0;
    //        }
    //    }

    //    private void Form1_Enter(object sender, EventArgs e)
    //    {

    //    }






    //}

    //#endregion

    //#region Designer Generated Code

    //partial class ShattaThermometer
    //{
    //    /// <summary>
    //    /// Required designer variable.
    //    /// </summary>
    //    private System.ComponentModel.IContainer components = null;

    //    /// <summary>
    //    /// Clean up any resources being used.
    //    /// </summary>
    //    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null))
    //        {
    //            components.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }

    //    #region Windows Form Designer generated code

    //    /// <summary>
    //    /// Required method for Designer support - do not modify
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent()
    //    {
    //        this.SuspendLayout();
    //        // 
    //        // Thermometer
    //        // 
    //        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //        this.BackColor = System.Drawing.Color.LightSteelBlue;
    //        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
    //        this.ClientSize = new System.Drawing.Size(389, 392);
    //        this.DoubleBuffered = true;
    //        this.BorderStyle = System.Windows.Forms.BorderStyle.None;
    //        this.Name = "Thermometer";
    //        this.Text = "Form1";
    //        //this.TransparencyKey = System.Drawing.Color.LightSteelBlue;
    //        this.Load += new System.EventHandler(this.Form1_Load);
    //        this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
    //        this.Enter += new System.EventHandler(this.Form1_Enter);
    //        //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Thermometer_MouseDown);
    //        //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Thermometer_MouseMove);
    //        this.ResumeLayout(false);

    //    }

    //    #endregion


    //}

    //#endregion

    #endregion
    
}
