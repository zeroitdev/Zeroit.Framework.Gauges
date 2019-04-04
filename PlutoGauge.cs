// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-27-2018
// ***********************************************************************
// <copyright file="PlutoGauge.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Gauges
{

    #region Control

    /// <summary>
    /// Class ZeroitPlutoGauge.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitPlutoGaugeDesigner))]
    public class ZeroitPlutoGauge : Control
    {

        #region Private Fields
        /// <summary>
        /// The gauge style
        /// </summary>
        private ZeroitPlutoGauge.Style gaugeStyle = ZeroitPlutoGauge.Style.Material;

        /// <summary>
        /// The thickness
        /// </summary>
        private int thickness = 8;

        /// <summary>
        /// The dial thickness
        /// </summary>
        private int dialThickness = 5;

        /// <summary>
        /// The percentage
        /// </summary>
        private float percentage = 75;

        /// <summary>
        /// The dial color
        /// </summary>
        private Color[] dialColor = new Color[]
        {
            Color.Gray,
            Color.Gray
        };

        /// <summary>
        /// The colors flat
        /// </summary>
        private FlatColors colorsFlat = new FlatColors();

        /// <summary>
        /// The colors material
        /// </summary>
        private MaterialColor colorsMaterial = new MaterialColor();

        /// <summary>
        /// The colors standard
        /// </summary>
        private StandardColors colorsStandard = new StandardColors();

        /// <summary>
        /// The show percentage
        /// </summary>
        private bool showPercentage = false;

        /// <summary>
        /// The symbol
        /// </summary>
        private string symbol = "%";


        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the color of the dial.
        /// </summary>
        /// <value>The color of the dial.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the dial")]
        public Color[] DialColor
        {
            get
            {
                return this.dialColor;
            }
            set
            {
                this.dialColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dial thickness.
        /// </summary>
        /// <value>The dial thickness.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The gauge dial thickness")]
        public int DialThickness
        {
            get
            {
                return this.dialThickness;
            }
            set
            {
                this.dialThickness = value;
                base.Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the gauge style.
        /// </summary>
        /// <value>The gauge style.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The gauge style")]
        public ZeroitPlutoGauge.Style GaugeStyle
        {
            get
            {
                return this.gaugeStyle;
            }
            set
            {
                this.gaugeStyle = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The gauge percentage")]
        public float Percentage
        {
            get
            {
                return this.percentage;
            }
            set
            {
                this.percentage = value;
                if (value < 0)
                {
                    this.percentage = 0;
                }
                if (value > 100)
                {
                    this.percentage = 100;
                }
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The gauge thickness")]
        public int Thickness
        {
            get
            {
                return this.thickness;
            }
            set
            {
                this.thickness = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the colors flat.
        /// </summary>
        /// <value>The colors flat.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FlatColors ColorsFlat
        {
            get { return colorsFlat; }
            set
            {
                colorsFlat = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the colors material.
        /// </summary>
        /// <value>The colors material.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MaterialColor ColorsMaterial
        {
            get { return colorsMaterial; }
            set
            {
                colorsMaterial = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the colors standard.
        /// </summary>
        /// <value>The colors standard.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public StandardColors ColorsStandard
        {
            get { return colorsStandard; }
            set
            {
                colorsStandard = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show percentage].
        /// </summary>
        /// <value><c>true</c> if [show percentage]; otherwise, <c>false</c>.</value>
        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                showPercentage = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public string Symbol
        {
            get { return symbol;}
            set
            {
                symbol = value;
                Invalidate();
            }

        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPlutoGauge" /> class.
        /// </summary>
        public ZeroitPlutoGauge()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            base.Size = new System.Drawing.Size(140, 70);
        }
        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (this.gaugeStyle == ZeroitPlutoGauge.Style.Standard)
            {
                Rectangle rectangle = new Rectangle(1 + this.thickness / 2, 1 + this.thickness / 2, base.Width - 2 - this.thickness, base.Height * 2 - this.thickness);
                e.Graphics.DrawArc(new Pen(ColorsStandard.ArcColors[0], (float)(this.thickness / 4)), rectangle, 180f, 44f);
                e.Graphics.DrawArc(new Pen(ColorsStandard.ArcColors[1], (float)(this.thickness / 4 * 2)), rectangle, 226f, 44f);
                e.Graphics.DrawArc(new Pen(ColorsStandard.ArcColors[2], (float)(this.thickness / 4 * 3)), rectangle, 272f, 44f);
                e.Graphics.DrawArc(new Pen(ColorsStandard.ArcColors[3], (float)this.thickness), rectangle, 318f, 44f);
                rectangle.Inflate(0 - this.thickness, 0 - this.thickness);
                e.Graphics.FillPie(new SolidBrush(dialColor[0]), new Rectangle(base.Width / 2 - this.thickness, base.Height - this.thickness, this.thickness * 2, this.thickness * 2), 0f, 360f);
                if (this.percentage < 5)
                {
                    e.Graphics.FillPie(new SolidBrush(ColorsStandard.DynamicColors[0]), rectangle, (float)(180 + this.dialThickness * 2 - 2), (float)this.dialThickness);
                }
                else if (this.percentage <= 95)
                {
                    e.Graphics.FillPie(new SolidBrush(dialColor[1]), rectangle, (float)(180 + (int)((double)this.percentage * 1.8) - this.dialThickness / 2), (float)this.dialThickness);
                }
                else
                {
                    e.Graphics.FillPie(new SolidBrush(ColorsStandard.DynamicColors[1]), rectangle, (float)(360 - this.dialThickness * 2), (float)this.dialThickness);
                }
            }

            if (this.gaugeStyle == ZeroitPlutoGauge.Style.Material)
            {
                Rectangle rectangle1 = new Rectangle(1 + this.thickness / 2, 1 + this.thickness / 2, base.Width - 2 - this.thickness, base.Height * 2 - this.thickness);
                e.Graphics.DrawArc(new Pen(new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), ColorsMaterial.FilledColor[0], ColorsMaterial.FilledColor[1], 1f), (float)this.thickness), rectangle1, 180f, (float)((int)((double)this.percentage * 1.8) - 1));
                e.Graphics.DrawArc(new Pen(ColorsMaterial.UnfilledColor, (float)this.thickness), rectangle1, (float)(180 + (int)((double)this.percentage * 1.8) + 1), (float)(180 - (int)((double)this.percentage * 1.8) + 5));
            }

            if (this.gaugeStyle == ZeroitPlutoGauge.Style.Flat)
            {
                Rectangle rectangle2 = new Rectangle(1 + this.thickness / 2, 1 + this.thickness / 2, base.Width - 2 - this.thickness, base.Height * 2 - this.thickness);
                e.Graphics.DrawArc(new Pen(ColorsFlat.FilledColor, (float)this.thickness), rectangle2, 180f, (float)((int)((double)this.percentage * 1.8) - 1));
                e.Graphics.DrawArc(new Pen(ColorsFlat.UnfilledColor, (float)this.thickness), rectangle2, (float)(180 + (int)((double)this.percentage * 1.8) + 1), (float)(180 - (int)((double)this.percentage * 1.8) + 5));
            }

            if (ShowPercentage)
            {
                CenterString(e.Graphics, Convert.ToInt32(Percentage).ToString() + Symbol, Font, ForeColor, this.ClientRectangle);

            }

            base.OnPaint(e);
        }


        /// <summary>
        /// Enum representing the Style
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// The standard
            /// </summary>
            Standard,
            /// <summary>
            /// The material
            /// </summary>
            Material,
            /// <summary>
            /// The flat
            /// </summary>
            Flat
        }

        /// <summary>
        /// Center String
        /// </summary>
        /// <param name="G">Set Graphics</param>
        /// <param name="T">Set string</param>
        /// <param name="F">Set Font</param>
        /// <param name="C">Set color</param>
        /// <param name="R">Set rectangle</param>
        public static void CenterString(System.Drawing.Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF TS = G.MeasureString(T, F);

            using (SolidBrush B = new SolidBrush(C))
            {
                G.DrawString(T, F, B, new Point((int)(R.Width / 2 - (TS.Width / 2)), (int)(R.Height / 2 - (TS.Height / 2))));
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

    /// <summary>
    /// Class FlatColors.
    /// </summary>
    public class FlatColors
    {
        /// <summary>
        /// The unfilled color
        /// </summary>
        private Color unfilledColor = Color.Gray;

        /// <summary>
        /// The filled color
        /// </summary>
        private Color filledColor = Color.FromArgb(0, 162, 250);

        /// <summary>
        /// Gets or sets the color of the unfilled.
        /// </summary>
        /// <value>The color of the unfilled.</value>
        public Color UnfilledColor
        {
            get { return unfilledColor; }
            set { unfilledColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the filled.
        /// </summary>
        /// <value>The color of the filled.</value>
        public Color FilledColor
        {
            get { return filledColor; }
            set { filledColor = value; }
        }
    }

    /// <summary>
    /// Class StandardColors.
    /// </summary>
    public class StandardColors
    {

        /// <summary>
        /// The arc colors
        /// </summary>
        private Color[] arcColors = new Color[]
        {
            Color.FromArgb(255, 220, 0),
            Color.FromArgb(255, 150, 0),
            Color.FromArgb(250, 90, 0),
            Color.FromArgb(255, 0, 0)
        };

        /// <summary>
        /// The dynamic colors
        /// </summary>
        private Color[] dynamicColors = new Color[]
        {
            Color.Black,
            Color.Black
        };

        /// <summary>
        /// Gets or sets the arc colors.
        /// </summary>
        /// <value>The arc colors.</value>
        public Color[] ArcColors
        {
            get { return arcColors; }
            set { arcColors = value; }
        }

        /// <summary>
        /// Gets or sets the dynamic colors.
        /// </summary>
        /// <value>The dynamic colors.</value>
        public Color[] DynamicColors
        {
            get { return dynamicColors; }
            set { dynamicColors = value; }
        }
    }

    /// <summary>
    /// Class MaterialColor.
    /// </summary>
    public class MaterialColor
    {
        /// <summary>
        /// The unfilled color
        /// </summary>
        private Color unfilledColor = Color.FromArgb(0, 162, 250);

        /// <summary>
        /// The filled color
        /// </summary>
        private Color[] filledColor = new Color[]
        {
            Color.FromArgb(249, 55, 98),
            Color.FromArgb(0, 162, 250)
        };

        /// <summary>
        /// Gets or sets the color of the unfilled.
        /// </summary>
        /// <value>The color of the unfilled.</value>
        public Color UnfilledColor
        {
            get { return unfilledColor; }
            set { unfilledColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the filled.
        /// </summary>
        /// <value>The color of the filled.</value>
        public Color[] FilledColor
        {
            get { return filledColor; }
            set { filledColor = value; }
        }
    }

    #endregion


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitPlutoGaugeDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitPlutoGaugeDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitPlutoGaugeDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitPlutoGaugeSmartTagActionList(this.Component));
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
    /// Class ZeroitPlutoGaugeSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitPlutoGaugeSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitPlutoGauge colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPlutoGaugeSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitPlutoGaugeSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitPlutoGauge;

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
        /// Gets or sets the color of the dial.
        /// </summary>
        /// <value>The color of the dial.</value>
        public Color[] DialColor
        {
            get
            {
                return colUserControl.DialColor;
            }
            set
            {
                GetPropertyByName("DialColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dial thickness.
        /// </summary>
        /// <value>The dial thickness.</value>
        public int DialThickness
        {
            get
            {
                return colUserControl.DialThickness;
            }
            set
            {
                GetPropertyByName("DialThickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the gauge style.
        /// </summary>
        /// <value>The gauge style.</value>
        public ZeroitPlutoGauge.Style GaugeStyle
        {
            get
            {
                return colUserControl.GaugeStyle;
            }
            set
            {
                GetPropertyByName("GaugeStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public float Percentage
        {
            get
            {
                return colUserControl.Percentage;
            }
            set
            {
                GetPropertyByName("Percentage").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        public int Thickness
        {
            get
            {
                return colUserControl.Thickness;
            }
            set
            {
                GetPropertyByName("Thickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show percentage].
        /// </summary>
        /// <value><c>true</c> if [show percentage]; otherwise, <c>false</c>.</value>
        public bool ShowPercentage
        {
            get
            {
                return colUserControl.ShowPercentage;
            }
            set
            {
                GetPropertyByName("ShowPercentage").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>The symbol.</value>
        public string Symbol
        {
            get
            {
                return colUserControl.Symbol;
            }
            set
            {
                GetPropertyByName("Symbol").SetValue(colUserControl, value);
            }

        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            get
            {
                return colUserControl.Font;
            }
            set
            {
                GetPropertyByName("Font").SetValue(colUserControl, value);
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


            items.Add(new DesignerActionPropertyItem("ShowPercentage",
                "Show Percentage", "Appearance",
                "Set to show the percentage value."));
            
            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("DialColor",
                                 "Dial Color", "Appearance",
                                 "Sets the dial color."));

            items.Add(new DesignerActionPropertyItem("GaugeStyle",
                "Gauge Style", "Appearance",
                "Sets the gauge style."));

            items.Add(new DesignerActionPropertyItem("DialThickness",
                                 "Dial Thickness", "Appearance",
                                 "Sets the dial thickness."));

            items.Add(new DesignerActionPropertyItem("Percentage",
                "Percentage", "Appearance",
                "Sets the percentage value."));

            items.Add(new DesignerActionPropertyItem("Thickness",
                "Thickness", "Appearance",
                "Sets the thickness."));

            items.Add(new DesignerActionPropertyItem("Symbol",
                "Symbol", "Appearance",
                "Sets the percentage symbol."));

            items.Add(new DesignerActionPropertyItem("Font",
                "Percentage Font", "Appearance",
                "Sets the percentage font."));


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