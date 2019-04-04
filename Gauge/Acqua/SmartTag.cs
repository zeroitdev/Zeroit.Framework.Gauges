// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-28-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="SmartTag.cs" company="Zeroit Dev Technologies">
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
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace Zeroit.Framework.MiscControls
{

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitMachoGaugeDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitMachoGaugeDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitMachoGaugeDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitMachoGaugeSmartTagActionList(this.Component));
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
    /// Class ZeroitMachoGaugeSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitMachoGaugeSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitMachoGauge colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMachoGaugeSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitMachoGaugeSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitMachoGauge;

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

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Single Value
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


        #region << Gauge Base >>

        /// <summary>
        /// Gets or sets the color of the base arc.
        /// </summary>
        /// <value>The color of the base arc.</value>
        public Color BaseArcColor
        {
            get
            {
                return colUserControl.BaseArcColor;
            }
            set
            {
                GetPropertyByName("BaseArcColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the base arc radius.
        /// </summary>
        /// <value>The base arc radius.</value>
        public Int32 BaseArcRadius
        {
            get
            {
                return colUserControl.BaseArcRadius;
            }
            set
            {
                GetPropertyByName("BaseArcRadius").SetValue(colUserControl, value);
            }
        }



        /// <summary>
        /// Gets or sets the base arc sweep.
        /// </summary>
        /// <value>The base arc sweep.</value>
        public Int32 BaseArcSweep
        {
            get
            {
                return colUserControl.BaseArcSweep;
            }
            set
            {
                GetPropertyByName("BaseArcSweep").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the base arc.
        /// </summary>
        /// <value>The width of the base arc.</value>
        public Int32 BaseArcWidth
        {
            get
            {
                return colUserControl.BaseArcWidth;
            }
            set
            {
                GetPropertyByName("BaseArcWidth").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region << Gauge Scale >>

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public Single MinValue
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
        public Single MaxValue
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
        /// Gets or sets the color of the scale lines inter.
        /// </summary>
        /// <value>The color of the scale lines inter.</value>
        public Color ScaleLinesInterColor
        {
            get
            {
                return colUserControl.ScaleLinesInterColor;
            }
            set
            {
                GetPropertyByName("ScaleLinesInterColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines inter inner radius.
        /// </summary>
        /// <value>The scale lines inter inner radius.</value>
        public Int32 ScaleLinesInterInnerRadius
        {
            get
            {
                return colUserControl.ScaleLinesInterInnerRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesInterInnerRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines inter outer radius.
        /// </summary>
        /// <value>The scale lines inter outer radius.</value>
        public Int32 ScaleLinesInterOuterRadius
        {
            get
            {
                return colUserControl.ScaleLinesInterOuterRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesInterOuterRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the scale lines inter.
        /// </summary>
        /// <value>The width of the scale lines inter.</value>
        public Int32 ScaleLinesInterWidth
        {
            get
            {
                return colUserControl.ScaleLinesInterWidth;
            }
            set
            {
                GetPropertyByName("ScaleLinesInterWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines minor ticks.
        /// </summary>
        /// <value>The scale lines minor ticks.</value>
        public Int32 ScaleLinesMinorTicks
        {
            get
            {
                return colUserControl.ScaleLinesMinorTicks;
            }
            set
            {
                GetPropertyByName("ScaleLinesMinorTicks").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the scale lines minor.
        /// </summary>
        /// <value>The color of the scale lines minor.</value>
        public Color ScaleLinesMinorColor
        {
            get
            {
                return colUserControl.ScaleLinesMinorColor;
            }
            set
            {
                GetPropertyByName("ScaleLinesMinorColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines minor inner radius.
        /// </summary>
        /// <value>The scale lines minor inner radius.</value>
        public Int32 ScaleLinesMinorInnerRadius
        {
            get
            {
                return colUserControl.ScaleLinesMinorInnerRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesMinorInnerRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines minor outer radius.
        /// </summary>
        /// <value>The scale lines minor outer radius.</value>
        public Int32 ScaleLinesMinorOuterRadius
        {
            get
            {
                return colUserControl.ScaleLinesMinorOuterRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesMinorOuterRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the scale lines minor.
        /// </summary>
        /// <value>The width of the scale lines minor.</value>
        public Int32 ScaleLinesMinorWidth
        {
            get
            {
                return colUserControl.ScaleLinesMinorWidth;
            }
            set
            {
                GetPropertyByName("ScaleLinesMinorWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines major step value.
        /// </summary>
        /// <value>The scale lines major step value.</value>
        public Single ScaleLinesMajorStepValue
        {
            get
            {
                return colUserControl.ScaleLinesMajorStepValue;
            }
            set
            {
                GetPropertyByName("ScaleLinesMajorStepValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the scale lines major.
        /// </summary>
        /// <value>The color of the scale lines major.</value>
        public Color ScaleLinesMajorColor
        {
            get
            {
                return colUserControl.ScaleLinesMajorColor;
            }
            set
            {
                GetPropertyByName("ScaleLinesMajorColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines major inner radius.
        /// </summary>
        /// <value>The scale lines major inner radius.</value>
        public Int32 ScaleLinesMajorInnerRadius
        {
            get
            {
                return colUserControl.ScaleLinesMajorInnerRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesMajorInnerRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale lines major outer radius.
        /// </summary>
        /// <value>The scale lines major outer radius.</value>
        public Int32 ScaleLinesMajorOuterRadius
        {
            get
            {
                return colUserControl.ScaleLinesMajorOuterRadius;
            }
            set
            {
                GetPropertyByName("ScaleLinesMajorOuterRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the scale lines major.
        /// </summary>
        /// <value>The width of the scale lines major.</value>
        public Int32 ScaleLinesMajorWidth
        {
            get
            {
                return colUserControl.ScaleLinesMajorWidth;
            }
            set
            {
                GetPropertyByName("ScaleLinesMajorWidth").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region << Gauge Scale Numbers >>

        /// <summary>
        /// Gets or sets the scale numbers radius.
        /// </summary>
        /// <value>The scale numbers radius.</value>
        public Int32 ScaleNumbersRadius
        {
            get
            {
                return colUserControl.ScaleNumbersRadius;
            }
            set
            {
                GetPropertyByName("ScaleNumbersRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the scale numbers.
        /// </summary>
        /// <value>The color of the scale numbers.</value>
        public Color ScaleNumbersColor
        {
            get
            {
                return colUserControl.ScaleNumbersColor;
            }
            set
            {
                GetPropertyByName("ScaleNumbersColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers format.
        /// </summary>
        /// <value>The scale numbers format.</value>
        public String ScaleNumbersFormat
        {
            get
            {
                return colUserControl.ScaleNumbersFormat;
            }
            set
            {
                GetPropertyByName("ScaleNumbersFormat").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers start scale line.
        /// </summary>
        /// <value>The scale numbers start scale line.</value>
        public Int32 ScaleNumbersStartScaleLine
        {
            get
            {
                return colUserControl.ScaleNumbersStartScaleLine;
            }
            set
            {
                GetPropertyByName("ScaleNumbersStartScaleLine").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers step scale lines.
        /// </summary>
        /// <value>The scale numbers step scale lines.</value>
        public Int32 ScaleNumbersStepScaleLines
        {
            get
            {
                return colUserControl.ScaleNumbersStepScaleLines;
            }
            set
            {
                GetPropertyByName("ScaleNumbersStepScaleLines").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the scale numbers rotation.
        /// </summary>
        /// <value>The scale numbers rotation.</value>
        public Int32 ScaleNumbersRotation
        {
            get
            {
                return colUserControl.ScaleNumbersRotation;
            }
            set
            {
                GetPropertyByName("ScaleNumbersRotation").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region << Gauge Needle >>

        /// <summary>
        /// Gets or sets the type of the needle.
        /// </summary>
        /// <value>The type of the needle.</value>
        public NeedleType NeedleType
        {
            get
            {
                return colUserControl.NeedleType;
            }
            set
            {
                GetPropertyByName("NeedleType").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the needle radius.
        /// </summary>
        /// <value>The needle radius.</value>
        public Int32 NeedleRadius
        {
            get
            {
                return colUserControl.NeedleRadius;
            }
            set
            {
                GetPropertyByName("NeedleRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the needle color1.
        /// </summary>
        /// <value>The needle color1.</value>
        public Color[] NeedleColor1
        {
            get
            {
                return colUserControl.NeedleColor1;
            }
            set
            {
                GetPropertyByName("NeedleColor1").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the needle color2.
        /// </summary>
        /// <value>The needle color2.</value>
        public Color NeedleColor2
        {
            get
            {
                return colUserControl.NeedleColor2;
            }
            set
            {
                GetPropertyByName("NeedleColor2").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the needle.
        /// </summary>
        /// <value>The width of the needle.</value>
        public Int32 NeedleWidth
        {
            get
            {
                return colUserControl.NeedleWidth;
            }
            set
            {
                GetPropertyByName("NeedleWidth").SetValue(colUserControl, value);
            }
        }

        #endregion

        #endregion

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


            items.Add(new DesignerActionPropertyItem("BaseArcColor",
                "Base-Arc Color", "Appearance",
                "Sets the base arc color."));

            items.Add(new DesignerActionPropertyItem("NeedleColor1",
                "Needle", "Appearance",
                "Sets the needle color color."));

            items.Add(new DesignerActionPropertyItem("NeedleColor2",
                "Needle Circle", "Appearance",
                "Sets the cirle of the needl's color."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesInterColor",
                "Scale Inter Color", "Appearance",
                "Sets the inter scale color."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMajorColor",
                "Scale Major Color", "Appearance",
                "Sets the major scale color."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMinorColor",
                "Scale Minor Color", "Appearance",
                "Sets the minor scale color."));

            items.Add(new DesignerActionPropertyItem("ScaleNumbersColor",
                "Scale Numbers Color", "Appearance",
                "Sets the number scale color."));


            items.Add(new DesignerActionPropertyItem("BaseArcRadius",
                "Base-Arc Radius", "Appearance",
                "Sets base arc radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMinorInnerRadius",
                                 "Scale Minor Radius", "Appearance",
                                 "Sets minor-inner scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMajorInnerRadius",
                                 "Scale Major Inner Radius", "Appearance",
                                 "Sets major-inner scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMinorOuterRadius",
                "Scale Minor Outer Radius", "Appearance",
                "Sets minor-outer scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMajorOuterRadius",
                "Scale Major Outer Radius", "Appearance",
                "Sets major-outer scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesInterInnerRadius",
                "Scale Inter Inner Radius", "Appearance",
                "Sets inter-inner scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesInterOuterRadius",
                "Scale Inter Outer Radius", "Appearance",
                "Sets inter-outer scale radius."));

            items.Add(new DesignerActionPropertyItem("ScaleNumbersRadius",
                "Scale Numbers Radius", "Appearance",
                "Sets number scale radius."));

            items.Add(new DesignerActionPropertyItem("BaseArcWidth",
                "Base-Arc Width", "Appearance",
                "Sets base arc width."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesInterWidth",
                "Scale Inter Width", "Appearance",
                "Sets the inter scale width."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMinorWidth",
                "Scale Minor Width", "Appearance",
                "Sets the minor scale width."));

            items.Add(new DesignerActionPropertyItem("ScaleLinesMajorWidth",
                "Scale Major Width", "Appearance",
                "Sets the major scale width."));


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
