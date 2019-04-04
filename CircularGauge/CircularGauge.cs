// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-27-2018
// ***********************************************************************
// <copyright file="CircularGauge.cs" company="Zeroit Dev Technologies">
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
using Zeroit.Framework.Gauges.Utils;
using Zeroit.Framework.Gauges.Drawable;

namespace Zeroit.Framework.Gauges.Gauges
{
    /// <summary>
    /// A circular gauge for pressure, speed, etc.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Gauges.BaseWControl" />
    [ToolboxItem(true)]
    [Description("A circular gauge for pressure, speed, etc.")]
    [Designer(typeof(ZeroitCircularGaugeDesigner))]
    public partial class ZeroitCircularGauge : BaseWControl
    {

        #region Private Fields

        /// <summary>
        /// The m axis
        /// </summary>
        private CircularAxis m_axis;
        /// <summary>
        /// The m needle
        /// </summary>
        private Needle m_needle;

        /// <summary>
        /// The m ease function
        /// </summary>
        private EaseFunction m_easeFunction;
        /// <summary>
        /// The m animate start
        /// </summary>
        private DateTime m_animateStart;

        /// <summary>
        /// The m value
        /// </summary>
        private double m_value;


        #endregion

        #region Public Properties

        /// <summary>
        /// The value of the gauge
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        [DefaultValue(0.0d)]
        [Description("The value of the gauge")]
        [Category("Appearance")]
        public double Value
        {
            get { return m_value; }
            set
            {
                if ((value < Axis.MinValue) || (value > Axis.MaxValue))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                m_value = value;
                HandleValueChanged();
            }
        }

        /// <summary>
        /// Whether or not to animate value changes
        /// </summary>
        /// <value><c>true</c> if animate; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        [Description("Whether or not to animate value changes")]
        [Category("Behavior")]
        public bool Animate { get; set; }

        /// <summary>
        /// The total amount of time (ms) to get between two different values
        /// </summary>
        /// <value>The length of the animation.</value>
        [DefaultValue(1000)]
        [Description("The total amount of time (ms) to get between two different values")]
        [Category("Behavior")]
        public int AnimationLength { get; set; }

        /// <summary>
        /// What type of interpolation to do when animating the needle
        /// </summary>
        /// <value>The ease function.</value>
        [DefaultValue(typeof(EaseFunctionType), "Linear")]
        [Description("What type of interpolation to do when animating the needle")]
        [Category("Behavior")]
        public EaseFunctionType EaseFunction { get; set; }

        /// <summary>
        /// What mode of easing to use when animating the needle
        /// </summary>
        /// <value>The ease mode.</value>
        [DefaultValue(typeof(EaseMode), "InOut")]
        [Description("What mode of easing to use when animating the needle")]
        [Category("Behavior")]
        public EaseMode EaseMode { get; set; }

        /// <summary>
        /// Controls the granularity of the animation, smaller values look better but are
        /// more CPU-intensive
        /// </summary>
        /// <value>The animation interval.</value>
        [DefaultValue(100)]
        [Description("The amount of time (ms) between animation frames")]
        [Category("Behavior")]
        public int AnimationInterval
        {
            get { return m_animationTimer.Interval; }
            set { m_animationTimer.Interval = value; }
        }

        /// <summary>
        /// The shape of the control
        /// </summary>
        /// <value>The shape.</value>
        [Browsable(false)]
        public override ControlShape Shape
        {
            get
            {
                return ControlShape.Circular;
            }
            set { }
        }

        /// <summary>
        /// The needle associated with this control
        /// </summary>
        /// <value>The needle.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Needle Needle
        {
            get { return m_needle; }
        }

        /// <summary>
        /// The axis associated with this control
        /// </summary>
        /// <value>The axis.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CircularAxis Axis
        {
            get { return m_axis; }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCircularGauge"/> class.
        /// </summary>
        public ZeroitCircularGauge()
            : base(false)
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;


            m_axis = new CircularAxis();
            m_needle = new Needle();

            m_axis.AppearanceChanged += new EventHandler(OnItemAppearanceChanged);
            m_axis.LayoutChanged += new EventHandler(OnItemLayoutChanged);

            m_needle.AppearanceChanged += new EventHandler(OnItemAppearanceChanged);
            m_needle.LayoutChanged += new EventHandler(OnItemLayoutChanged);

            m_needle.CalculatePaths(ClientRectangle);
            m_axis.CalculatePaths(ClientRectangle);

            InitializeComponent();

            Value = 0;
            Animate = true;
            EaseFunction = EaseFunctionType.Linear;
            AnimationLength = 1000;
            AnimationInterval = 100;
            EaseMode = EaseMode.InOut;
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Handles the value changed.
        /// </summary>
        protected void HandleValueChanged()
        {
            StopAnimation();

            if (m_axis != null && m_needle != null)
            {
                double percent = (m_value - Axis.MinValue) / (double)(Axis.MaxValue - Axis.MinValue);
                double degrees = Axis.AxisStartDegrees - (percent * Axis.AxisLengthDegrees);

                if (Animate && !DesignMode)
                {
                    StartAnimation(m_needle.Orientation, degrees);
                }
                else
                {
                    m_needle.Orientation = degrees;
                }
            }
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        /// <param name="dFrom">The d from.</param>
        /// <param name="dTo">The d to.</param>
        private void StartAnimation(double dFrom, double dTo)
        {
            m_easeFunction = new EaseFunction(EaseFunction, EaseMode, AnimationLength, dFrom, dTo);
            m_animateStart = DateTime.Now;
            m_animationTimer.Start();
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        private void StopAnimation()
        {
            m_animationTimer.Stop();
        }

        /// <summary>
        /// Handles the Tick event of the m_animationTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void m_animationTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - m_animateStart;

            if (span.TotalMilliseconds >= AnimationLength)
            {
                m_needle.Orientation = m_easeFunction.ToValue;
                StopAnimation();
            }
            else
            {
                double dVal = m_easeFunction.GetValue(span.TotalMilliseconds);
                m_needle.Orientation = dVal;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //paint axis and needle, then gloss
            if (m_axis != null)
            {
                
                m_axis.Draw(e.Graphics);
            }
            if (m_needle != null)
            {
                
                m_needle.Draw(e.Graphics);
            }
            
            OnPaintGloss(e.Graphics);
            
        }

        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (m_axis != null)
            {
                m_axis.CalculatePaths(ClientRectangle);
            }
            if (m_needle != null)
            {
                m_needle.CalculatePaths(ClientRectangle);
            }

            base.OnResize(e);
        }

        /// <summary>
        /// Handles the <see cref="E:ItemAppearanceChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnItemAppearanceChanged(object sender, EventArgs e)
        {
            IDrawable drawable = sender as IDrawable;
            if (drawable != null)
            {
                this.Invalidate(drawable);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:ItemLayoutChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnItemLayoutChanged(object sender, EventArgs e)
        {
            IDrawable drawable = sender as IDrawable;
            if (drawable != null)
            {
                drawable.CalculatePaths(ClientRectangle);
                this.Invalidate(drawable);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
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

                if (m_axis != null)
                {
                    m_axis.Dispose();
                }

                if (m_needle != null)
                {
                    m_needle.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #endregion

        

    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitCircularGaugeDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitCircularGaugeDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitCircularGaugeDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitCircularGaugeSmartTagActionList(this.Component));
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
    /// Class ZeroitCircularGaugeSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitCircularGaugeSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitCircularGauge colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitCircularGaugeSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitCircularGaugeSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitCircularGauge;

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
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color GradientColor
        {
            get
            {
                return colUserControl.GradientColor;
            }
            set
            {
                GetPropertyByName("GradientColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color GlossColor
        {
            get
            {
                return colUserControl.GlossColor;
            }
            set
            {
                GetPropertyByName("GlossColor").SetValue(colUserControl, value);
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
        /// Gets or sets a value indicating whether this <see cref="ZeroitCircularGaugeSmartTagActionList"/> is animate.
        /// </summary>
        /// <value><c>true</c> if animate; otherwise, <c>false</c>.</value>
        public bool Animate
        {
            get
            {
                return colUserControl.Animate;
            }
            set
            {
                GetPropertyByName("Animate").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the length of the animation.
        /// </summary>
        /// <value>The length of the animation.</value>
        public int AnimationLength
        {
            get
            {
                return colUserControl.AnimationLength;
            }
            set
            {
                GetPropertyByName("AnimationLength").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the ease function.
        /// </summary>
        /// <value>The ease function.</value>
        public EaseFunctionType EaseFunction
        {
            get
            {
                return colUserControl.EaseFunction;
            }
            set
            {
                GetPropertyByName("EaseFunction").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the ease mode.
        /// </summary>
        /// <value>The ease mode.</value>
        public EaseMode EaseMode
        {
            get
            {
                return colUserControl.EaseMode;
            }
            set
            {
                GetPropertyByName("EaseMode").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the animation interval.
        /// </summary>
        /// <value>The animation interval.</value>
        public int AnimationInterval
        {
            get
            {
                return colUserControl.AnimationInterval;
            }
            set
            {
                GetPropertyByName("AnimationInterval").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Animate",
                "Animate", "Appearance",
                "Set to enable animation."));
            
            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("GradientColor",
                "Gradient Color", "Appearance",
                "Sets the gradient color."));

            items.Add(new DesignerActionPropertyItem("GlossColor",
                "Gloss Color", "Appearance",
                "Sets the gloss color."));

            items.Add(new DesignerActionPropertyItem("EaseFunction",
                "Ease Function", "Appearance",
                "Sets the easing function."));
            
            items.Add(new DesignerActionPropertyItem("EaseMode",
                "Ease Mode", "Appearance",
                "Sets the easing mode."));
            
            items.Add(new DesignerActionPropertyItem("AnimationLength",
                "Animation Length", "Appearance",
                "Sets the animation length."));

            items.Add(new DesignerActionPropertyItem("AnimationInterval",
                "Animation Interval", "Appearance",
                "Sets the animation interval."));

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
