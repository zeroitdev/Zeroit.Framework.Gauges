// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-28-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Gauge.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Zeroit.Framework.Gauges.Anidaso.Helper;

namespace Zeroit.Framework.Gauges.Anidaso
{
    /// <summary>
    /// A class collection for rendering a gauge control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public class ZeroitAnidasoGauge : UserControl
	{

        #region Private Fields
        /// <summary>
        /// The color 0
        /// </summary>
        private Color color_0 = Color.Gray;

        /// <summary>
        /// The color 1
        /// </summary>
        private Color color_1 = Color.SeaGreen;

        /// <summary>
        /// The int 0
        /// </summary>
        private int int_0;

        /// <summary>
        /// The int 1
        /// </summary>
        private int int_1 = 30;

        /// <summary>
        /// The color 2
        /// </summary>
        private Color color_2 = Color.SeaGreen;

        /// <summary>
        /// The color 3
        /// </summary>
        private Color color_3 = Color.Tomato;

        /// <summary>
        /// The string 0
        /// </summary>
        private string string_0 = "";

        /// <summary>
        /// The icontainer 0
        /// </summary>
        private IContainer icontainer_0;

        /// <summary>
        /// The lblpass
        /// </summary>
        private Label lblpass;

        /// <summary>
        /// The lblmin
        /// </summary>
        private Label lblmin;

        /// <summary>
        /// The lblmax
        /// </summary>
        private Label lblmax;

        /// <summary>
        /// The anidaso color transition
        /// </summary>
        private GaugeColorTransition anidasoColorTransition;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the progress background.
        /// </summary>
        /// <value>The color of the progress bg.</value>
        public Color ProgressBgColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
                this.method_0(this.int_0);
            }
        }

        /// <summary>
        /// Gets or sets the progress color.
        /// </summary>
        /// <value>The progress color1.</value>
        public Color ProgressColor1
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
                this.anidasoColorTransition.Color1 = this.color_2;
                this.method_0(this.int_0);
            }
        }

        /// <summary>
        /// Gets or sets the progress color.
        /// </summary>
        /// <value>The progress color2.</value>
        public Color ProgressColor2
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
                this.anidasoColorTransition.Color2 = this.color_3;
                this.method_0(this.int_0);
            }
        }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>The suffix.</value>
        public string Suffix
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                this.lblpass.Text = string.Concat(this.Value, this.string_0);
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
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
                this.method_0(this.int_0);
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
                return this.int_0;
            }
            set
            {
                int num = 0;
                int num1 = 0;
                int num2;
                if (value > 100)
                {
                    do
                    {
                        if (num != num1)
                        {
                            break;
                        }
                        num1 = 1;
                        num2 = num;
                        num = 1;
                    }
                    while (1 <= num2);
                }
                else
                {
                    this.int_0 = value;
                    this.anidasoColorTransition.ProgessValue = this.int_0;
                    this.method_0(this.int_0);
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitAnidasoGauge" /> class.
        /// </summary>
        public ZeroitAnidasoGauge()
        {
            this.InitializeComponent();
            base.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, true, null);
            this.AnidasoGauge_Resize(this, new EventArgs());
            this.method_0(this.int_0);
        }
        #endregion

        #region Methods and Overrides
        /// <summary>
        /// Handles the OnValueChange event of the anidasoColorTransition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void anidasoColorTransition_OnValueChange(object sender, EventArgs e)
        {
            this.color_1 = this.anidasoColorTransition.Value;
        }

        /// <summary>
        /// Handles the FontChanged event of the AnidasoGauge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoGauge_FontChanged(object sender, EventArgs e)
        {
            this.lblpass.Font = this.Font;
            this.method_0(this.int_0);
        }

        /// <summary>
        /// Handles the ForeColorChanged event of the AnidasoGauge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoGauge_ForeColorChanged(object sender, EventArgs e)
        {
            Label label = this.lblpass;
            Label label1 = this.lblmin;
            Label label2 = this.lblmax;
            Color foreColor = this.ForeColor;
            Color color = foreColor;
            label2.ForeColor = foreColor;
            Color color1 = color;
            Color color2 = color1;
            label1.ForeColor = color1;
            label.ForeColor = color2;
            this.method_0(this.int_0);
        }

        /// <summary>
        /// Handles the Load event of the AnidasoGauge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoGauge_Load(object sender, EventArgs e)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            if (!base.DesignMode)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                CustomControl.initializeComponent(this);
            }
        }

        /// <summary>
        /// Handles the Resize event of the AnidasoGauge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnidasoGauge_Resize(object sender, EventArgs e)
        {
            this.method_0(this.int_0);
            this.lblpass.Top = base.Height - this.lblpass.Height - 30;
            Label label = this.lblmin;
            int height = base.Height - this.lblmax.Height - 10;
            int num = height;
            this.lblmax.Top = height;
            label.Top = num;
            this.lblmin.Left = 20;
            Label width = this.lblmax;
            System.Drawing.Size size = base.Size;
            width.Left = size.Width - this.lblmax.Width - 20;
            this.lblpass.Left = base.Width / 2 - this.lblpass.Width / 2;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.icontainer_0 != null)
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.icontainer_0 = new System.ComponentModel.Container();
            this.lblpass = new Label();
            this.lblmin = new Label();
            this.lblmax = new Label();
            this.anidasoColorTransition = new GaugeColorTransition(this.icontainer_0);
            base.SuspendLayout();
            this.lblpass.AutoSize = true;
            this.lblpass.Font = new System.Drawing.Font("Century Gothic", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblpass.Location = new Point(83, 34);
            this.lblpass.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblpass.Name = "lblpass";
            this.lblpass.Size = new System.Drawing.Size(22, 24);
            this.lblpass.TabIndex = 1;
            this.lblpass.Text = "0";
            this.lblmin.AutoSize = true;
            this.lblmin.Font = new System.Drawing.Font("Century Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblmin.Location = new Point(26, 86);
            this.lblmin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(15, 17);
            this.lblmin.TabIndex = 2;
            this.lblmin.Text = "0";
            this.lblmax.AutoSize = true;
            this.lblmax.Font = new System.Drawing.Font("Century Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblmax.Location = new Point(145, 86);
            this.lblmax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblmax.Name = "lblmax";
            this.lblmax.Size = new System.Drawing.Size(29, 17);
            this.lblmax.TabIndex = 3;
            this.lblmax.Text = "100";
            this.anidasoColorTransition.Color1 = Color.SeaGreen;
            this.anidasoColorTransition.Color2 = Color.Tomato;
            this.anidasoColorTransition.ProgessValue = 0;
            this.anidasoColorTransition.OnValueChange += new EventHandler(this.anidasoColorTransition_OnValueChange);
            base.AutoScaleDimensions = new SizeF(12f, 24f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.lblmax);
            base.Controls.Add(this.lblmin);
            base.Controls.Add(this.lblpass);
            this.Font = new System.Drawing.Font("Century Gothic", 15.75f);
            base.Margin = new System.Windows.Forms.Padding(6);
            base.Name = "ZeroitAnidasoGauge";
            base.Size = new System.Drawing.Size(174, 117);
            base.Load += new EventHandler(this.AnidasoGauge_Load);
            base.FontChanged += new EventHandler(this.AnidasoGauge_FontChanged);
            base.ForeColorChanged += new EventHandler(this.AnidasoGauge_ForeColorChanged);
            base.Resize += new EventHandler(this.AnidasoGauge_Resize);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        /// <summary>
        /// Methods the 0.
        /// </summary>
        /// <param name="int_2">The int 2.</param>
        private void method_0(int int_2)
        {
            int width = base.Size.Width;
            System.Drawing.Size size = base.Size;
            Bitmap bitmap = new Bitmap(width, size.Height);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.Clear(Color.Transparent);
            Pen pen = new Pen(this.color_0, (float)this.int_1);
            size = base.Size;
            int num = size.Width - this.int_1 * 2;
            int int1 = this.int_1;
            size = base.Size;
            Rectangle rectangle = new Rectangle(int1, size.Height / 4, num, num);
            Pen pen1 = new Pen(this.color_1, (float)this.int_1);
            System.Drawing.Graphics graphic1 = System.Drawing.Graphics.FromImage(bitmap);
            graphic1.SmoothingMode = SmoothingMode.HighQuality;
            graphic1.DrawArc(pen, rectangle, 180f, 180f);
            this.lblpass.Text = string.Concat(int_2, this.Suffix);
            graphic1.DrawArc(pen1, rectangle, 180f, (float)this.method_1(int_2));
            this.BackgroundImage = bitmap;
        }

        /// <summary>
        /// Methods the 1.
        /// </summary>
        /// <param name="int_2">The int 2.</param>
        /// <returns>System.Int32.</returns>
        private int method_1(int int_2)
        {
            double num = Math.Round((double)int_2 * 180 / 100, 0);
            return int.Parse(num.ToString());
        } 
        #endregion
    }
}