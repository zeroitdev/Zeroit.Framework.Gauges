// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-30-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ThermometerSlider.cs" company="Zeroit Dev Technologies">
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
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Gauges.Meter
{
    #region Slider Control

    #region About
    // *************************************************** class About

    /// <summary>
    /// Class SliderAbout.
    /// </summary>
    public class SliderAbout
    {

        // ******************************************* AssemblyCompany

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes;
                attributes = Assembly.
                             GetExecutingAssembly().
                             GetCustomAttributes(
                                 typeof(AssemblyCompanyAttribute),
                                 false);
                if (attributes.Length == 0)
                {
                    return (String.Empty);
                }
                return (((AssemblyCompanyAttribute)
                           attributes[0]).Company);
            }
        }

        // ***************************************** AssemblyCopyright

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes;

                attributes = Assembly.
                             GetExecutingAssembly().
                             GetCustomAttributes(
                                 typeof(
                                     AssemblyCopyrightAttribute),
                                 false);
                if (attributes.Length == 0)
                {
                    return (String.Empty);
                }
                return (((AssemblyCopyrightAttribute)
                           attributes[0]).Copyright);
            }
        }

        // *************************************** AssemblyDescription

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        /// <value>The assembly description.</value>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes;

                attributes = Assembly.
                             GetExecutingAssembly().
                             GetCustomAttributes(
                               typeof(
                                   AssemblyDescriptionAttribute),
                               false);
                if (attributes.Length == 0)
                {
                    return (String.Empty);
                }
                return (((AssemblyDescriptionAttribute)
                           attributes[0]).Description);
            }
        }

        // ****************************************** AssemblyFilename

        /// <summary>
        /// Gets the assembly filename.
        /// </summary>
        /// <value>The assembly filename.</value>
        public static string AssemblyFilename
        {

            get
            {
                string filename = String.Empty;

                filename = Application.ExecutablePath;


                return (filename);
            }
        }

        // ********************************************** AssemblyPath

        /// <summary>
        /// Gets the assembly path.
        /// </summary>
        /// <value>The assembly path.</value>
        public static string AssemblyPath
        {

            get
            {
                string path = String.Empty;

                path = Path.GetDirectoryName(AssemblyFilename);

                return (path);
            }
        }

        // ******************************************* AssemblyProduct

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes;

                attributes = Assembly.GetExecutingAssembly().
                             GetCustomAttributes(
                                typeof(AssemblyProductAttribute),
                                false);
                if (attributes.Length == 0)
                {
                    return (String.Empty);
                }
                return (((AssemblyProductAttribute)
                           attributes[0]).Product);
            }
        }

        // ********************************************* AssemblyTitle

        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes;

                attributes = Assembly.
                             GetExecutingAssembly().
                             GetCustomAttributes(
                                 typeof(AssemblyTitleAttribute),
                                 false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute;

                    titleAttribute =
                        (AssemblyTitleAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(
                                      titleAttribute.Title))
                    {
                        return (titleAttribute.Title);
                    }
                }
                return (Path.GetFileNameWithoutExtension(
                                  Assembly.GetExecutingAssembly().
                                           CodeBase));
            }
        }

        // ******************************************* AssemblyVersion

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public static string AssemblyVersion
        {
            get
            {
                return (Assembly.GetExecutingAssembly().
                                  GetName().
                                  Version.ToString());
            }
        }

    } // class About
    #endregion

    #region Graphics Buffer


    // ****************************************** class GraphicsBuffer

    /// <summary>
    /// Class SliderGraphicsBuffer.
    /// </summary>
    public class SliderGraphicsBuffer
    {
        /// <summary>
        /// The bitmap
        /// </summary>
        Bitmap bitmap;
        /// <summary>
        /// The graphics
        /// </summary>
        Graphics graphics;
        /// <summary>
        /// The height
        /// </summary>
        int height;
        /// <summary>
        /// The width
        /// </summary>
        int width;

        // ******************************************** GraphicsBuffer

        /// <summary>
        /// constructor for the GraphicsBuffer
        /// </summary>
        public SliderGraphicsBuffer()
        {

            width = 0;
            height = 0;
        }

        // ************************************** CreateGraphicsBuffer

        /// <summary>
        /// completes the creation of the GraphicsBuffer object
        /// </summary>
        /// <param name="width">width of the bitmap</param>
        /// <param name="height">height of the bitmap</param>
        /// <returns>true, if GraphicsBuffer created; otherwise, false</returns>
        public bool CreateGraphicsBuffer(int width,
                                           int height)
        {
            bool success = true;

            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }

            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }

            this.width = 0;
            this.height = 0;

            if ((width == 0) || (height == 0))
            {
                success = false;
            }
            else
            {
                this.width = width;
                this.height = height;

                bitmap = new Bitmap(this.width, this.height);
                graphics = Graphics.FromImage(bitmap);

                success = true;
            }

            return (success);
        }

        // ************************************** DeleteGraphicsBuffer

        /// <summary>
        /// deletes the current GraphicsBuffer
        /// </summary>
        /// <returns>null, always</returns>
        public SliderGraphicsBuffer DeleteGraphicsBuffer()
        {

            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }

            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }

            width = 0;
            height = 0;

            return (null);
        }

        // *************************************************** Graphic

        /// <summary>
        /// returns the current Graphic Graphics object
        /// </summary>
        /// <value>The graphic.</value>
        public Graphics Graphic
        {

            get
            {
                return (graphics);
            }
        }

        // ************************************** GraphicsBufferExists

        /// <summary>
        /// returns true if the grapgics object exists; false,
        /// otherwise
        /// </summary>
        /// <value><c>true</c> if [graphics buffer exists]; otherwise, <c>false</c>.</value>
        public bool GraphicsBufferExists
        {

            get
            {
                return (graphics != null);
            }
        }

        // ******************************************* ColorAtLocation

        /// <summary>
        /// given a point in the graphics bitmap, returns the GDI
        /// Color at that point
        /// </summary>
        /// <param name="location">location in the bitmap from which the color is to be
        /// returned</param>
        /// <returns>if the location is within the bitmap, the color at the
        /// location; otherwise, Black</returns>
        public Color ColorAtLocation(Point location)
        {
            Color color = Color.Black;

            if (((location.X > 0) &&
                   (location.X <= this.width)) &&
                 ((location.Y > 0) &&
                   (location.Y <= this.height)))
            {
                color = this.bitmap.GetPixel(location.X,
                                               location.Y);
            }

            return (color);
        }

        // ************************************** RenderGraphicsBuffer

        /// <summary>
        /// Renders the buffer to the graphics object identified by
        /// graphic
        /// </summary>
        /// <param name="graphic">target graphics object (e.g., PaintEventArgs e.Graphics)</param>
        public void RenderGraphicsBuffer(Graphics graphic)
        {

            if (bitmap != null)
            {
                graphic.DrawImage(
                            bitmap,
                            new Rectangle(0, 0, width, height),
                            new Rectangle(0, 0, width, height),
                            GraphicsUnit.Pixel);
            }
        }

        // ********************************************* ClearGraphics

        /// <summary>
        /// clears the graphics object identified by graphics
        /// </summary>
        /// <param name="background_color">background color to be used to clear graphics</param>
        public void ClearGraphics(Color background_color)
        {

            Graphic.Clear(background_color);
        }

    } // class GraphicsBuffer

    #endregion

    #region SliderControl

    // ******************************************* class SliderControl    
    /// <summary>
    /// A class collection for rendering a slider control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitMeterDesigner))]
    public class ZeroitMeter : Control
    {

        // ******************************** control delegate and event

        /// <summary>
        /// Delegate SliderValueChangedHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SliderValueChangedEventArgs"/> instance containing the event data.</param>
        public delegate void SliderValueChangedHandler(
                                Object sender,
                                SliderValueChangedEventArgs e);

        /// <summary>
        /// Occurs when [slider value changed].
        /// </summary>
        public event SliderValueChangedHandler SliderValueChanged;

        // *********************************** constants and variables

        /// <summary>
        /// The minimum delta
        /// </summary>
        const int MINIMUM_DELTA = 25;
        // default control properties
        /// <summary>
        /// The background color
        /// </summary>
        static Color BACKGROUND_COLOR = SystemColors.Control;
        /// <summary>
        /// The current value
        /// </summary>
        const int CURRENT_VALUE = 90;
        /// <summary>
        /// The font family
        /// </summary>
        const string FONT_FAMILY = "Calibri Light";
        /// <summary>
        /// The increment
        /// </summary>
        const int INCREMENT = 10;
        /// <summary>
        /// The interior angle
        /// </summary>
        const double INTERIOR_ANGLE = 20.0;
        /// <summary>
        /// The maximum color
        /// </summary>
        static Color MAXIMUM_COLOR = Color.Tomato;
        /// <summary>
        /// The maximum value
        /// </summary>
        const int MAXIMUM_VALUE = 100;
        /// <summary>
        /// The midpoint color
        /// </summary>
        static Color MIDPOINT_COLOR = Color.White;
        /// <summary>
        /// The minimum color
        /// </summary>
        static Color MINIMUM_COLOR = Color.SteelBlue;
        /// <summary>
        /// The minimum value
        /// </summary>
        const int MINIMUM_VALUE = 0;
        // initial control dimensions
        /// <summary>
        /// The arrow width
        /// </summary>
        const int ARROW_WIDTH = 23;
        /// <summary>
        /// The offset
        /// </summary>
        const int OFFSET = CONTROL_HEIGHT / 100;
        /// <summary>
        /// The control height
        /// </summary>
        const int CONTROL_HEIGHT = 500;
        /// <summary>
        /// The control width
        /// </summary>
        const int CONTROL_WIDTH = CONTROL_HEIGHT / 4;
        /// <summary>
        /// The font size
        /// </summary>
        const int FONT_SIZE = 8;
        /// <summary>
        /// The height to width
        /// </summary>
        const double HEIGHT_TO_WIDTH = 0.030612244897959;
        /// <summary>
        /// The label width
        /// </summary>
        const int LABEL_WIDTH = 25;
        /// <summary>
        /// The tube height
        /// </summary>
        const int TUBE_HEIGHT = CONTROL_HEIGHT - 2 * OFFSET;
        /// <summary>
        /// The tube width
        /// </summary>
        const int TUBE_WIDTH = 15;


        // ***********************************************************

        /// <summary>
        /// The arrow being dragged
        /// </summary>
        bool arrow_being_dragged = false;
        /// <summary>
        /// The arrow region
        /// </summary>
        Region arrow_region = null;
        /// <summary>
        /// The arrow width
        /// </summary>
        int arrow_width = 0;
        /// <summary>
        /// The background
        /// </summary>
        SliderGraphicsBuffer background = null;
        /// <summary>
        /// The background color
        /// </summary>
        Color background_color = BACKGROUND_COLOR;
        /// <summary>
        /// The control height
        /// </summary>
        int control_height = CONTROL_HEIGHT;
        /// <summary>
        /// The control width
        /// </summary>
        int control_width = CONTROL_WIDTH;
        /// <summary>
        /// The current value
        /// </summary>
        int current_value = CURRENT_VALUE;
        /// <summary>
        /// The force tube width
        /// </summary>
        bool force_tube_width = false;
        /// <summary>
        /// The increment
        /// </summary>
        int increment = INCREMENT;
        /// <summary>
        /// The indicator
        /// </summary>
        SliderGraphicsBuffer indicator = null;
        /// <summary>
        /// The label font
        /// </summary>
        Font label_font = new Font(FONT_FAMILY,
                                                FONT_SIZE);
        /// <summary>
        /// The label size
        /// </summary>
        Size label_size = new Size(0, 0);
        /// <summary>
        /// The labels
        /// </summary>
        string[] labels = null;
        /// <summary>
        /// The maximum color
        /// </summary>
        Color maximum_color = MAXIMUM_COLOR;
        /// <summary>
        /// The maximum value
        /// </summary>
        int maximum_value = MAXIMUM_VALUE;
        /// <summary>
        /// The midpoint color
        /// </summary>
        Color midpoint_color = MIDPOINT_COLOR;
        /// <summary>
        /// The minimum color
        /// </summary>
        Color minimum_color = MINIMUM_COLOR;
        /// <summary>
        /// The minimum value
        /// </summary>
        int minimum_value = MINIMUM_VALUE;
        /// <summary>
        /// The offset
        /// </summary>
        int offset = OFFSET;
        /// <summary>
        /// The p0
        /// </summary>
        Point P0 = Point.Empty;
        /// <summary>
        /// The p1
        /// </summary>
        Point P1 = Point.Empty;
        /// <summary>
        /// The p2
        /// </summary>
        Point P2 = Point.Empty;
        /// <summary>
        /// The p3
        /// </summary>
        Point P3 = Point.Empty;
        /// <summary>
        /// The p4
        /// </summary>
        Point P4 = Point.Empty;
        /// <summary>
        /// The p5
        /// </summary>
        Point P5 = Point.Empty;
        /// <summary>
        /// The p6
        /// </summary>
        Point P6 = Point.Empty;
        /// <summary>
        /// The p7
        /// </summary>
        Point P7 = Point.Empty;
        /// <summary>
        /// The revise background graphic
        /// </summary>
        bool revise_background_graphic = true;
        /// <summary>
        /// The tube height
        /// </summary>
        int tube_height = TUBE_HEIGHT;
        /// <summary>
        /// The tube width
        /// </summary>
        int tube_width = TUBE_WIDTH;

        // ******************************************** memory_cleanup

        /// <summary>
        /// Handles the cleanup event of the memory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void memory_cleanup(object sender,
                              EventArgs e)
        {

            if (arrow_region != null)
            {
                arrow_region.Dispose();
                arrow_region = null;
            }
            // DeleteGraphicsBuffer 
            // returns null
            if (background != null)
            {
                background = background.DeleteGraphicsBuffer();
            }

            if (indicator != null)
            {
                indicator = indicator.DeleteGraphicsBuffer();
            }
        }

        // ***************************************************** round

        // http://en.wikipedia.org/wiki/Rounding

        /// <summary>
        /// Rounds the specified control value.
        /// </summary>
        /// <param name="control_value">The control value.</param>
        /// <returns>System.Int32.</returns>
        int round(double control_value)
        {

            return ((int)(control_value + 0.5));
        }

        // ***************************************************** round

        // http://en.wikipedia.org/wiki/Rounding

        /// <summary>
        /// Rounds the specified control value.
        /// </summary>
        /// <param name="control_value">The control value.</param>
        /// <returns>System.Int32.</returns>
        int round(float control_value)
        {

            return ((int)(control_value + 0.5F));
        }

        // ************************************ update_tube_dimensions

        /// <summary>
        /// Updates the tube dimensions.
        /// </summary>
        void update_tube_dimensions()
        {

            offset = round((float)control_height / 100.0F);

            tube_height = control_height - 2 * offset;
            if (!force_tube_width)
            {
                tube_width = round((double)tube_height *
                                     HEIGHT_TO_WIDTH);
            }
            arrow_width = round(1.5 * (double)tube_width);
        }

        // ********************************** create_background_labels

        /// <summary>
        /// Creates the background labels.
        /// </summary>
        /// <param name="maximum_value">The maximum value.</param>
        /// <param name="minimum_value">The minimum value.</param>
        /// <param name="increment">The increment.</param>
        /// <returns>System.String[].</returns>
        string[] create_background_labels(int maximum_value,
                                              int minimum_value,
                                              int increment)
        {
            int count = 0;
            int i = 0;
            float incr = (float)increment;
            float range;
            string[] label_strings = null;
            int label_value = maximum_value;

            range = (float)maximum_value - (float)minimum_value;
            count = round(range / incr) + 1;

            label_strings = new string[count];
            while (label_value >= minimum_value)
            {
                label_strings[i++] = label_value.ToString();
                label_value -= increment;
            }

            return (label_strings);
        }

        // ****************************** determine_maximum_label_size

        /// <summary>
        /// Determines the maximum size of the label.
        /// </summary>
        /// <param name="labels">The labels.</param>
        /// <param name="font">The font.</param>
        /// <returns>Size.</returns>
        Size determine_maximum_label_size(string[] labels,
                                            Font font)
        {
            Size maximum_size = new Size(0, 0);

            foreach (string label in labels)
            {
                Size size = TextRenderer.MeasureText(label, font);

                if (size.Width > maximum_size.Width)
                {
                    maximum_size.Width = size.Width;
                }
                if (size.Height > maximum_size.Height)
                {
                    maximum_size.Height = size.Height;
                }
            }

            return (maximum_size);
        }

        // ********************************** update_background_labels

        /// <summary>
        /// Updates the background labels.
        /// </summary>
        void update_background_labels()
        {
            int available_height = 0;
            float font_size = FONT_SIZE;
            Font new_font = new Font(FONT_FAMILY, FONT_SIZE);
            int needed_height = 0;

            new_font = new Font(FONT_FAMILY, font_size);
            labels = create_background_labels(MaximumValue,
                                                MinimumValue,
                                                Increment);
            label_size = determine_maximum_label_size(labels,
                                                        new_font);
            // force vertical fit
            update_tube_dimensions();
            available_height = control_height -
                               (2 * offset) - tube_width;
            needed_height = labels.Length * label_size.Height;
            while (needed_height > available_height)
            {
                font_size -= 0.1F;
                new_font = new Font(FONT_FAMILY, font_size);
                label_size = determine_maximum_label_size(
                                                        labels,
                                                        new_font);
                needed_height = labels.Length * label_size.Height;
            }
            label_font = new_font;
        }

        // ******************************** update_background_geometry

        /// <summary>
        /// Updates the background geometry.
        /// </summary>
        void update_background_geometry()
        {

            update_tube_dimensions();

            P0.X = offset + label_size.Width + offset;
            P0.Y = offset;

            P1.X = P0.X;
            P1.Y = control_height - offset;

            P2.X = P0.X + round((double)tube_width / 2.0);
            P2.Y = P0.Y + round((double)tube_width / 2.0);

            P3.X = P2.X;
            P3.Y = P1.Y - round((double)tube_width / 2.0);

            P4.X = P0.X;
            P4.Y = P1.Y - tube_width;
        }

        // ********************************* update_indicator_geometry

        /// <summary>
        /// Updates the indicator geometry.
        /// </summary>
        void update_indicator_geometry()
        {
            int dy = 0;
            float percent = 0.0F;
            float percent_down = 0.0F;
            int pixels = 0;
            int pixels_down = 0;
            double theta = INTERIOR_ANGLE * Math.PI / 180.0;

            update_tube_dimensions();
            // solve initialization problem
            if (CurrentValue < MinimumValue)
            {
                CurrentValue = MaximumValue - Increment;
            }

            P5.X = P2.X + round((double)tube_width / 2.0) +
                          offset;
            pixels = round((float)(P3.Y - P2.Y));
            percent = Math.Abs((float)(current_value -
                                             MinimumValue) /
                                 (float)(MaximumValue -
                                             MinimumValue));
            percent_down = 1.0F - percent;
            pixels_down = round((percent_down *
                                  (float)pixels));
            P5.Y = P2.Y + pixels_down;

            dy = round((double)arrow_width *
                         Math.Sin(theta));

            P6.X = P5.X + arrow_width;
            P6.Y = P5.Y - dy;

            P7.X = P6.X;
            P7.Y = P5.Y + dy;
        }

        // ******************************************* update_geometry

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        void update_geometry()
        {

            update_tube_dimensions();
            update_background_labels();
            update_background_geometry();
            update_indicator_geometry();
        }

        // ********************************* create_background_graphic

        /// <summary>
        /// Creates the background graphic.
        /// </summary>
        void create_background_graphic()
        {

            if (background != null)
            {
                background = background.DeleteGraphicsBuffer();
            }
            background = new SliderGraphicsBuffer();
            background.CreateGraphicsBuffer(control_width,
                                              control_height);
            background.Graphic.SmoothingMode = SmoothingMode.
                                               HighQuality;
            background.ClearGraphics(ControlBackgroundColor);
        }

        // ********************************** create_indicator_graphic

        /// <summary>
        /// Creates the indicator graphic.
        /// </summary>
        void create_indicator_graphic()
        {

            if (indicator != null)
            {
                indicator = indicator.DeleteGraphicsBuffer();
            }
            indicator = new SliderGraphicsBuffer();
            indicator.CreateGraphicsBuffer(control_width,
                                             control_height);
            indicator.Graphic.SmoothingMode = SmoothingMode.
                                              HighQuality;
        }

        // ********************************************* SliderControl

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMeter" /> class.
        /// </summary>
        public ZeroitMeter()
        {

            Application.ApplicationExit +=
                            new EventHandler(memory_cleanup);
            memory_cleanup(this, EventArgs.Empty);

            control_height = CONTROL_HEIGHT;
            control_width = round((float)control_height / 4.0F);

            this.Width = control_width;
            this.Height = control_height;
            // order important here!!
            this.SetStyle((ControlStyles.DoubleBuffer |
                              ControlStyles.UserPaint |
                              ControlStyles.AllPaintingInWmPaint),
                            true);
            this.UpdateStyles();

            current_value = (this.MaximumValue -
                              this.MinimumValue) / 2 +
                            this.MinimumValue;

            revise_background_graphic = true;

            update_tube_dimensions();
            update_background_labels();
            update_background_geometry();
            create_background_graphic();

            update_indicator_geometry();
            create_indicator_graphic();
        }

        // ****************************** create_linear_gradient_brush

        /// <summary>
        /// Creates the linear gradient brush.
        /// </summary>
        /// <param name="maximum_color">The maximum color.</param>
        /// <param name="midpoint_color">Color of the midpoint.</param>
        /// <param name="minimum_color">The minimum color.</param>
        /// <returns>LinearGradientBrush.</returns>
        LinearGradientBrush create_linear_gradient_brush(
                                            Color maximum_color,
                                            Color midpoint_color,
                                            Color minimum_color)
        {
            LinearGradientBrush brush;
            ColorBlend color_blend = new ColorBlend();
            Rectangle rectangle;

            rectangle = new Rectangle(new Point(P0.X, P2.Y),
                                        new Size(tube_width,
                                                   tube_height));
            rectangle.Inflate(1, 1);
            brush = new LinearGradientBrush(
                                    this.ClientRectangle,
                                    maximum_color,
                                    minimum_color,
                                    LinearGradientMode.Vertical);

            color_blend.Positions = new float[]
                                        {
                                        0.0F,
                                        0.5F,
                                        1.0F
                                        };

            color_blend.Colors = new Color[]
                                    {
                                    maximum_color,
                                    midpoint_color,
                                    minimum_color
                                    };

            brush.InterpolationColors = color_blend;

            return (brush);
        }

        // ************************************ draw_background_labels

        /// <summary>
        /// Draws the background labels.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_background_labels(Graphics graphics)
        {
            Point location = new Point(0, 0);
            TextFormatFlags text_format_flags;
            int vertical_offset = 0;

            text_format_flags = (TextFormatFlags.Right |
                                  TextFormatFlags.VerticalCenter);

            vertical_offset = (P3.Y - P2.Y) / (labels.Length - 1);

            location.X = P0.X - (offset + label_size.Width);
            location.Y = P2.Y - (label_size.Height / 2);

            foreach (string label in labels)
            {
                Rectangle rectangle = new Rectangle(location,
                                                        label_size);
                //TextRenderer.DrawText(graphics,
                //                        label,
                //                        label_font,
                //                        rectangle,
                //                        Color.Black,
                //                        text_format_flags);

                graphics.DrawString(label, label_font, new SolidBrush(ForeColor), location);

                location.Y += vertical_offset;
            }
        }

        // *********************************** draw_background_graphic

        /// <summary>
        /// Draws the background graphic.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_background_graphic(Graphics graphics)
        {
            Brush brush;
            Rectangle end_point_rectangle =
                                    new Rectangle();
            LinearGradientBrush linear_gradient_brush;
            Rectangle rectangle;
            // background labels
            draw_background_labels(graphics);
            // endpoints
            end_point_rectangle.Size = new Size(tube_width,
                                                  tube_width);
            // maximum endpoint
            brush = new SolidBrush(MaximumColor);
            end_point_rectangle.Location = P0;
            graphics.FillEllipse(brush, end_point_rectangle);
            brush.Dispose();
            // minimum endpoint
            brush = new SolidBrush(MinimumColor);
            end_point_rectangle.Location = P4;
            graphics.FillEllipse(brush, end_point_rectangle);
            brush.Dispose();
            // gradient tube
            rectangle = new Rectangle(new Point(P0.X, P2.Y),
                                        new Size(tube_width,
                                                   P3.Y - P2.Y));
            // inflate to account for the 
            // right and bottom off by one 
            // value in Rectangles
            rectangle.Inflate(1, 1);
            linear_gradient_brush = create_linear_gradient_brush(
                                            MaximumColor,
                                            MidpointColor,
                                            MinimumColor);
            graphics.FillRectangle(linear_gradient_brush,
                                     rectangle);
            linear_gradient_brush.Dispose();
        }

        // ********************************* draw_current_value_string

        /// <summary>
        /// Draws the current value string.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_current_value_string(Graphics graphics)
        {
            Brush brush;
            string current_value_string;
            TextFormatFlags flags;
            Point location;
            Rectangle rectangle;

            brush = new SolidBrush(Color.Black);
            current_value_string = current_value.ToString();
            flags = (TextFormatFlags.Left |
                      TextFormatFlags.VerticalCenter);
            location = new Point(P6.X + offset,
                                   P5.Y - (label_size.Height / 2));

            rectangle = new Rectangle(location, label_size);
            //TextRenderer.DrawText(graphics,
            //                        current_value_string,
            //                        label_font,
            //                        rectangle,
            //                        Color.Black,
            //                        flags);

            graphics.DrawString(current_value_string, label_font, new SolidBrush(ForeColor), location);

        }

        // ************************************************ draw_arrow

        /// <summary>
        /// Draws the arrow.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_arrow(Graphics graphics)
        {
            Point[] arrow_outline = new Point[3];
            GraphicsPath arrow_path = null;
            Brush brush;
            int i = 0;
            Pen pen;
            Color value_color;

            update_geometry();

            value_color = background.ColorAtLocation(
                              new Point(P5.X - (tube_width / 2),
                                          P5.Y));
            brush = new SolidBrush(value_color);
            pen = new Pen(Color.Black, 1.0F);

            arrow_outline[i++] = P5;
            arrow_outline[i++] = P6;
            arrow_outline[i++] = P7;

            arrow_path = new GraphicsPath(FillMode.Alternate);
            arrow_path.AddLines(arrow_outline);
            arrow_path.CloseFigure();
            // arrow_region is used for 
            // hit testing
            arrow_region = new Region(arrow_path);
            // draw arrow outline
            graphics.DrawPolygon(pen, arrow_outline);
            // fill arrow outline
            graphics.FillPolygon(brush, arrow_outline);

            arrow_path.Dispose();
            brush.Dispose();
            pen.Dispose();
        }

        // ************************************ draw_indicator_graphic

        /// <summary>
        /// Draws the indicator graphic.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void draw_indicator_graphic(Graphics graphics)
        {

            update_indicator_geometry();

            draw_current_value_string(graphics);
            draw_arrow(graphics);
        }

        // ************************************ ControlBackgroundColor        
        /// <summary>
        /// Gets or sets the color of the control background.
        /// </summary>
        /// <value>The color of the control background.</value>
        [Category("Appearance"),
         Description("Gets/Sets control background color"),
         DefaultValue(typeof(SystemColors), "Control"),
         Bindable(true)]
        public Color ControlBackgroundColor
        {

            get
            {
                return (background_color);
            }
            set
            {
                Color old_background_color = background_color;

                background_color = value;
                if (old_background_color != background_color)
                {
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************** CurrentValue        
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The current value.</value>
        [Category("Appearance"),
         Description("Gets/Sets control current value"),
         DefaultValue(90),
         Bindable(true)]
        public int CurrentValue
        {

            get
            {
                return (current_value);
            }
            set
            {
                if ((value >= MinimumValue) &&
                     (value <= MaximumValue))
                {
                    int old_current_value = current_value;

                    current_value = value;
                    if (old_current_value != current_value)
                    {
                        update_geometry();
                        revise_background_graphic = true;
                        this.Invalidate();
                    }
                }
            }
        }

        // ******************************************** ForceTubeWidth        
        /// <summary>
        /// Gets or sets a value indicating whether the user may force the tube width.
        /// </summary>
        /// <value><c>true</c> if force tube width; otherwise, <c>false</c>.</value>
        [Category("Appearance"),
         Description("Gets/Sets if user may force the tube width"),
         DefaultValue(false),
         Bindable(true)]
        public bool ForceTubeWidth
        {

            get
            {
                return (force_tube_width);
            }
            set
            {
                bool old_force_tube_width = force_tube_width;

                force_tube_width = value;
                if (old_force_tube_width != force_tube_width)
                {
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ************************************************* Increment        
        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>The increment.</value>
        [Category("Appearance"),
         Description("Gets/Sets increment between label values"),
         DefaultValue(10),
         Bindable(true)]
        public int Increment

        {
            get
            {
                return (increment);
            }
            set
            {
                int old_increment = increment;

                increment = value;
                if (old_increment != increment)
                {
                    update_geometry();
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************** MaximumColor        
        /// <summary>
        /// Gets or sets the maximum color.
        /// </summary>
        /// <value>The maximum color.</value>
        [Category("Appearance"),
         Description("Gets/Sets control color at Maximum Value"),
         DefaultValue(typeof(Color), "Tomato"),
         Bindable(true)]
        public Color MaximumColor
        {

            get
            {
                return (maximum_color);
            }
            set
            {
                Color old_maximum_color = maximum_color;

                maximum_color = value;
                if (old_maximum_color != maximum_color)
                {
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************** MaximumValue        
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [Category("Appearance"),
         Description("Gets/Sets control maximum value"),
         DefaultValue(100),
         Bindable(true)]
        public int MaximumValue

        {
            get
            {
                return (maximum_value);
            }
            set
            {
                int old_maximum_value = maximum_value;

                maximum_value = value;
                if (old_maximum_value != maximum_value)
                {
                    update_geometry();
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************* MidpointColor        
        /// <summary>
        /// Gets or sets the color of the midpoint.
        /// </summary>
        /// <value>The color of the midpoint.</value>
        [Category("Appearance"),
         Description("Gets/Sets color at the control midpoint"),
         DefaultValue(typeof(Color), "White"),
         Bindable(true)]
        public Color MidpointColor
        {

            get
            {
                return (midpoint_color);
            }
            set
            {
                Color old_midpoint_color = midpoint_color;

                midpoint_color = value;
                if (old_midpoint_color != midpoint_color)
                {
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************** MinimumColor        
        /// <summary>
        /// Gets or sets the minimum color.
        /// </summary>
        /// <value>The minimum color.</value>
        [Category("Appearance"),
         Description("Gets/Sets control color at Minimum Value"),
         DefaultValue(typeof(Color), "SteelBlue"),
         Bindable(true)]
        public Color MinimumColor
        {

            get
            {
                return (minimum_color);
            }
            set
            {
                Color old_minimum_color = minimum_color;

                minimum_color = value;
                if (old_minimum_color != minimum_color)
                {
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ********************************************** MinimumValue        
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [Category("Appearance"),
         Description("Gets/Sets control minimum value"),
         DefaultValue(0),
         Bindable(true)]
        public int MinimumValue

        {
            get
            {
                return (minimum_value);
            }
            set
            {
                int old_minimum_value = minimum_value;

                minimum_value = value;
                if (minimum_value > maximum_value)
                {
                    minimum_value = maximum_value - MINIMUM_DELTA;
                }
                if (old_minimum_value != minimum_value)
                {
                    update_geometry();
                    revise_background_graphic = true;
                    this.Invalidate();
                }
            }
        }

        // ************************************************* TubeWidth        
        /// <summary>
        /// Gets or sets the width of the tube.
        /// </summary>
        /// <value>The width of the tube.</value>
        [Category("Appearance"),
         Description("Gets/Sets tube width if force_tube_width is true"),
         DefaultValue(15),
         Bindable(true)]
        public int TubeWidth
        {

            get
            {
                return (tube_width);
            }
            set
            {
                if (ForceTubeWidth)
                {
                    int old_tube_width = tube_width;

                    tube_width = value;
                    if (old_tube_width != tube_width)
                    {
                        update_geometry();
                        revise_background_graphic = true;
                        this.Invalidate();
                    }
                }
            }
        }

        // ************************************* current_value_changed

        /// <summary>
        /// Currents the value changed.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool current_value_changed(int y)
        {
            int old_current_value = current_value;
            float percent;
            float value_down;
            int pixels;
            int pixels_down;
            bool value_changed = false;

            if (P5.Y != y)
            {
                if (y > P3.Y)
                {
                    y = P3.Y;
                }
                if (y < P2.Y)
                {
                    y = P2.Y;
                }
                pixels = P3.Y - P2.Y;
                pixels_down = y - P2.Y;
                percent = (float)pixels_down / (float)pixels;
                value_down = percent * (float)(MaximumValue -
                                                   MinimumValue);
                current_value = round((float)MaximumValue -
                                        value_down);
                value_changed = (old_current_value !=
                                  current_value);
            }

            return (value_changed);
        }

        // ************************ trigger_slider_value_changed_event

        /// <summary>
        /// Triggers the slider value changed event.
        /// </summary>
        /// <param name="current_value">The current value.</param>
        void trigger_slider_value_changed_event(int current_value)
        {

            if (SliderValueChanged != null)
            {
                SliderValueChanged(this,
                                     new SliderValueChangedEventArgs(
                                             current_value));
            }
        }

        // *********************************************** OnMouseDown

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);

            if (arrow_region.IsVisible(new Point(e.X, e.Y)))
            {
                // cursor is in the arrow
                arrow_being_dragged = true;
                if (current_value_changed(e.Y))
                {
                    trigger_slider_value_changed_event(
                                                    current_value);
                    this.Invalidate();
                }
            }
        }

        // *********************************************** OnMouseMove

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {

            base.OnMouseMove(e);

            if (arrow_being_dragged)
            {
                if (current_value_changed(e.Y))
                {
                    trigger_slider_value_changed_event(
                                                    current_value);
                    this.Invalidate();
                }
            }
        }

        // ************************************************* OnMouseUp

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {

            base.OnMouseUp(e);

            arrow_being_dragged = false;
        }

        // *************************************************** OnPaint

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.Clear(ControlBackgroundColor);

            if ((this.Height != control_height) ||
                 (this.Width != control_width))
            {
                int width;

                revise_background_graphic = true;
                control_height = this.Height;
                control_width = round((float)control_height /
                                        4.0F);
                update_geometry();
                width = offset + label_size.Width + offset +
                        tube_width + offset + arrow_width + offset +
                        label_size.Width + offset;
                if (width > control_width)
                {
                    control_width = width;
                }
                this.Size = new Size(control_width,
                                       control_height);
            }

            if ((background == null) || revise_background_graphic)
            {
                if (revise_background_graphic)
                {
                    revise_background_graphic = false;
                }
                create_background_graphic();
                draw_background_graphic(background.Graphic);
            }
            background.RenderGraphicsBuffer(e.Graphics);

            create_indicator_graphic();
            draw_indicator_graphic(indicator.Graphic);

            indicator.RenderGraphicsBuffer(e.Graphics);
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

    } // class SliderControl

    #endregion

    #region SliderValueChangedEventArgs

    // ***************************** class SliderValueChangedEventArgs

    /// <summary>
    /// Class SliderValueChangedEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SliderValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The slider value
        /// </summary>
        public int SliderValue;

        // ******************************* SliderValueChangedEventArgs

        /// <summary>
        /// saves the supplied value in SliderValue
        /// </summary>
        /// <param name="slider_value">value to be saved</param>
        public SliderValueChangedEventArgs(int slider_value)
        {

            SliderValue = slider_value;
        }

    } // class SliderValueChangedEventArgs


    #endregion



    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitMeterDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitMeterDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitMeterDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitMeterSmartTagActionList(this.Component));
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
    /// Class ZeroitMeterSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitMeterSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitMeter colUserControl;


        /// <summary>
        /// Gets the designer action UI service.
        /// </summary>
        /// <value>The designer action UI service.</value>
        private DesignerActionUIService DesignerActionUIService
        {
            get { return GetService(typeof(DesignerActionUIService)) as DesignerActionUIService; }
        }


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMeterSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitMeterSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitMeter;

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
        /// Gets or sets the color of the control background.
        /// </summary>
        /// <value>The color of the control background.</value>
        public Color ControlBackgroundColor
        {

            get
            {
                return colUserControl.ControlBackgroundColor;
            }
            set
            {
                GetPropertyByName("ControlBackgroundColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The current value.</value>
        public int CurrentValue
        {

            get
            {
                return colUserControl.CurrentValue;
            }
            set
            {
                GetPropertyByName("CurrentValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [force tube width].
        /// </summary>
        /// <value><c>true</c> if [force tube width]; otherwise, <c>false</c>.</value>
        public bool ForceTubeWidth
        {

            get
            {
                return colUserControl.ForceTubeWidth;
            }
            set
            {
                GetPropertyByName("ForceTubeWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>The increment.</value>
        public int Increment

        {
            get
            {
                return colUserControl.Increment;
            }
            set
            {
                GetPropertyByName("Increment").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum color.
        /// </summary>
        /// <value>The maximum color.</value>
        public Color MaximumColor
        {

            get
            {
                return colUserControl.MaximumColor;
            }
            set
            {
                GetPropertyByName("MaximumColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public int MaximumValue

        {
            get
            {
                return colUserControl.MaximumValue;
            }
            set
            {
                GetPropertyByName("MaximumValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the midpoint.
        /// </summary>
        /// <value>The color of the midpoint.</value>
        public Color MidpointColor
        {

            get
            {
                return colUserControl.MidpointColor;
            }
            set
            {
                GetPropertyByName("MidpointColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum color.
        /// </summary>
        /// <value>The minimum color.</value>
        public Color MinimumColor
        {

            get
            {
                return colUserControl.MinimumColor;
            }
            set
            {
                GetPropertyByName("MinimumColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public int MinimumValue

        {
            get
            {
                return colUserControl.MinimumValue;
            }
            set
            {
                GetPropertyByName("MinimumValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the tube.
        /// </summary>
        /// <value>The width of the tube.</value>
        public int TubeWidth
        {

            get
            {
                return colUserControl.TubeWidth;
            }
            set
            {
                GetPropertyByName("TubeWidth").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        #region Template Methods
        /// <summary>
        /// Refreshes the component.
        /// </summary>
        internal void RefreshComponent()
        {
            if (DesignerActionUIService != null)
                DesignerActionUIService.Refresh(colUserControl);
        }


        #region Template Code
        //protected virtual void Export()
        //{
        //    var editor = new System.Windows.Forms.Form();
        //    editor.ShowDialog();
        //}

        //protected virtual void Import()
        //{
        //    var editor = new System.Windows.Forms.Form();
        //    editor.ShowDialog();
        //}

        //protected virtual void ShowBorders()
        //{
        //    colUserControl.ShowBorders = !colUserControl.ShowBorders;
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}


        //protected virtual void AddButton()
        //{

        //    var item = "Added";
        //    colUserControl.Items.Add(item);
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}

        //protected virtual void ClearButtons()
        //{
        //    colUserControl.Items.Clear();
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}

        //protected virtual void DeleteItem()
        //{
        //    colUserControl.Items.Remove("Added");
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //} 
        #endregion


        #endregion

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            #region Add Private Methods

            #region Template Code
            //items.Add(new DesignerActionHeaderItem("Template"));

            //items.Add(new DesignerActionMethodItem(this, "Export",
            //                     "Export Template", "Template",
            //                     "Selects the template to display"));

            //items.Add(new DesignerActionMethodItem(this, "Import",
            //                     "Import Template", "Template", true)); //Alternative Method



            //items.Add(new DesignerActionHeaderItem("Visuals"));

            //if (!colUserControl.ShowBorders)
            //    items.Add(new DesignerActionMethodItem(this, "ShowBorders", "Show Borders", "Visuals", true));
            //else
            //    items.Add(new DesignerActionMethodItem(this, "ShowBorders", "Hide Borders", "Visuals", true));

            ////List or Collections
            //items.Add(new DesignerActionHeaderItem("Collection"));
            //if (colUserControl.Items.Count > 0)
            //    items.Add(new DesignerActionMethodItem(this, "ClearButtons", "Clear Buttons", "Collection", true));
            //items.Add(new DesignerActionMethodItem(this, "AddButton", "Add Button", "Collection", true));
            //if (colUserControl.Items.Count > 0)
            //    items.Add(new DesignerActionMethodItem(this, "DeleteButton", "Delete Button", "Collection", true));

            #endregion


            #endregion

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("ForceTubeWidth",
                "Force Tube Width", "Appearance",
                "Set to force tube width."));

            items.Add(new DesignerActionPropertyItem("ControlBackgroundColor",
                "Back Color", "Appearance",
                "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                "Fore Color", "Appearance",
                "Sets the fore color."));


            items.Add(new DesignerActionPropertyItem("MidpointColor",
                "Midpoint Color", "Appearance",
                "Sets the mid-point color."));


            items.Add(new DesignerActionPropertyItem("MaximumColor",
                "Maximum Color", "Appearance",
                "Sets the maximum color."));
            
            items.Add(new DesignerActionPropertyItem("Increment",
                "Increment", "Appearance",
                "Sets the increment value."));

            items.Add(new DesignerActionPropertyItem("MaximumValue",
                "Maximum Value", "Appearance",
                "Sets the maximum value."));

            items.Add(new DesignerActionPropertyItem("MinimumValue",
                "Minimum Value", "Appearance",
                "Sets the minimum value."));


            items.Add(new DesignerActionPropertyItem("TubeWidth",
                "Tube Width", "Appearance",
                "Sets the tube width."));
            
            items.Add(new DesignerActionPropertyItem("CurrentValue",
                "Value", "Appearance",
                "Sets the current value."));


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


    #endregion
}
