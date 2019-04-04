// ***********************************************************************
// Assembly         : Zeroit.Framework.Gauges
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-25-2018
// ***********************************************************************
// <copyright file="MeterRenderer.cs" company="Zeroit Dev Technologies">
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Zeroit.Framework.Gauges.AnalogMeterControl.Utils;

namespace Zeroit.Framework.Gauges.AnalogMeterControl.AnalogMeter
{
    /// <summary>
    /// Base class for the renderers of the analog meter
    /// </summary>
    public class LBAnalogMeterRenderer
	{
        #region Variables
        /// <summary>
        /// Control to render
        /// </summary>
        private ZeroitAnalogMeter meter = null;
        #endregion

        #region Properies
        /// <summary>
        /// Gets or sets the analog meter.
        /// </summary>
        /// <value>The analog meter.</value>
        public ZeroitAnalogMeter AnalogMeter
		{
			set { this.meter = value; }
			get { return this.meter; }
		}
        #endregion

        #region Virtual method
        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawBackground( Graphics gr, RectangleF rc , bool allowtransparency = true)
		{
			return false;
		}

        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawBody( Graphics Gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawDivisions( Graphics Gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the thresholds
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawThresholds( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Drawt the unit measure of the control
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawUM( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the current value in numerical form
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawValue( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the needle
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawNeedle( Graphics Gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the needle cover at the center
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawNeedleCover( Graphics Gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the glass effect
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DrawGlass( Graphics Gr, RectangleF rc )
		{
			return false;
		}
		#endregion
	}


    /// <summary>
    /// Default renderer class for the analog meter
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Gauges.AnalogMeterControl.AnalogMeter.LBAnalogMeterRenderer" />
    public class LBDefaultAnalogMeterRenderer : LBAnalogMeterRenderer
	{
        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawBackground( Graphics gr, RectangleF rc, bool allowTransparency = true )
		{
		    if (!allowTransparency)
		    {
		        if (this.AnalogMeter == null)
		            return false;

		        Color c = this.AnalogMeter.Parent.BackColor;
		        SolidBrush br = new SolidBrush(c);
		        Pen pen = new Pen(c);

		        Rectangle _rcTmp = new Rectangle(0, 0, this.AnalogMeter.Width, this.AnalogMeter.Height);
		        gr.DrawRectangle(pen, _rcTmp);
		        gr.FillRectangle(br, rc);
            }
		    
            return true;
		}

	    

        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawBody( Graphics Gr, RectangleF rc )
		{
			if ( this.AnalogMeter == null )
				return false;
			
			Color bodyColor = this.AnalogMeter.BodyColor;
			Color cDark = LBColorManager.StepColor ( bodyColor, 20 );
			
			LinearGradientBrush br1 = new LinearGradientBrush ( rc, 
			                                                   bodyColor,
			                                                   cDark,
			                                                   45 );
			Gr.FillEllipse ( br1, rc );
			
			float drawRatio = this.AnalogMeter.GetDrawRatio();
			
			RectangleF _rc = rc;
			_rc.X += 3 * drawRatio;
			_rc.Y += 3 * drawRatio;
			_rc.Width -= 6 * drawRatio;
			_rc.Height -= 6 * drawRatio;

			LinearGradientBrush br2 = new LinearGradientBrush ( _rc,
			                                                   cDark,
			                                                   bodyColor,
			                                                   45 );
			Gr.FillEllipse ( br2, _rc );
			
			return true;
		}

        /// <summary>
        /// Draw the thresholds
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawThresholds( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawDivisions( Graphics Gr, RectangleF rc )
		{
			if ( this.AnalogMeter == null )
				return false;
			
			PointF needleCenter = this.AnalogMeter.GetNeedleCenter();
			float startAngle = this.AnalogMeter.GetStartAngle();
			float endAngle = this.AnalogMeter.GetEndAngle();
			float scaleDivisions = this.AnalogMeter.ScaleDivisions;
			float scaleSubDivisions = this.AnalogMeter.ScaleSubDivisions;
			float drawRatio = this.AnalogMeter.GetDrawRatio();
			double minValue = this.AnalogMeter.MinValue;
			double maxValue = this.AnalogMeter.MaxValue;
			Color scaleColor = this.AnalogMeter.ScaleColor;
			
			float cx = needleCenter.X;
			float cy = needleCenter.Y;
			float w = rc.Width;
			float h = rc.Height;

			float incr = LBMath.GetRadian(( endAngle - startAngle ) / (( scaleDivisions - 1 )* (scaleSubDivisions + 1)));
			float currentAngle = LBMath.GetRadian( startAngle );
			float radius = (float)(w / 2 - ( w * 0.08));
			float rulerValue = (float)minValue;

			Pen pen = new Pen ( scaleColor, ( 2 * drawRatio ) );
			SolidBrush br = new SolidBrush ( scaleColor );
			
			PointF ptStart = new PointF(0,0);
			PointF ptEnd = new PointF(0,0);
			int n = 0;
			for( ; n < scaleDivisions; n++ )
			{
					//Draw Thick Line
				ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
				ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
				ptEnd.X = (float)(cx + (radius - w/20) * Math.Cos(currentAngle));
				ptEnd.Y = (float)(cy + (radius - w/20) * Math.Sin(currentAngle));
				Gr.DrawLine( pen, ptStart, ptEnd );
				
       				//Draw Strings
       			Font font = new Font ( this.AnalogMeter.Font.FontFamily, (float)( 6F * drawRatio ) );
		
				float tx = (float)(cx + (radius - ( 20 * drawRatio )) * Math.Cos(currentAngle));
		        float ty = (float)(cy + (radius - ( 20 * drawRatio )) * Math.Sin(currentAngle));
		        double val = Math.Round ( rulerValue );
		        String str = String.Format( "{0,0:D}", (int)val );
		
				SizeF size = Gr.MeasureString ( str, font );
				Gr.DrawString ( str, 
				                font, 
				                br, 
				                tx - (float)( size.Width * 0.5 ), 
				                ty - (float)( size.Height * 0.5 ) );

				rulerValue += (float)(( maxValue - minValue) / (scaleDivisions - 1));
		
				if ( n == scaleDivisions -1)
					break;
		
				if ( scaleDivisions <= 0 )
					currentAngle += incr;
				else
				{
			        for (int j = 0; j <= scaleSubDivisions; j++)
			        {
						currentAngle += incr;
						ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
						ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
						ptEnd.X = (float)(cx + (radius - w/50) * Math.Cos(currentAngle));
						ptEnd.Y = (float)(cy + (radius - w/50) * Math.Sin(currentAngle));
						Gr.DrawLine( pen, ptStart, ptEnd );
			        }
				}
			}
			
			return true;
		}

        /// <summary>
        /// Drawt the unit measure of the control
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawUM( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the current value in numerical form
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawValue( Graphics gr, RectangleF rc )
		{
			return false;
		}

        /// <summary>
        /// Draw the needle
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawNeedle( Graphics Gr, RectangleF rc )
		{
			if ( this.AnalogMeter == null )
				return false;
			
			float w, h ;		
			w = rc.Width;
			h = rc.Height;
		
			double minValue = this.AnalogMeter.MinValue;
			double maxValue = this.AnalogMeter.MaxValue;
			double currValue = this.AnalogMeter.Value;
			float startAngle = this.AnalogMeter.GetStartAngle();
			float endAngle = this.AnalogMeter.GetEndAngle();
			PointF needleCenter = this.AnalogMeter.GetNeedleCenter();
			
			float radius = (float)(w / 2 - ( w * 0.12));
			float val = (float)(maxValue - minValue);
		
			val = (float)((100 * ( currValue - minValue )) / val);
			val = (( endAngle - startAngle ) * val) / 100;
		    val += startAngle;
			
		    float angle = LBMath.GetRadian ( val );
		    
		    float cx = needleCenter.X;
		    float cy = needleCenter.Y;
		    
		    PointF ptStart = new PointF(0,0);
		    PointF ptEnd = new PointF(0,0);

		    GraphicsPath pth1 = new GraphicsPath();
				    
		    ptStart.X = cx;
		    ptStart.Y = cy;		    
		    angle = LBMath.GetRadian(val + 10);
			ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
		    ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
		    pth1.AddLine ( ptStart, ptEnd );
		    
		    ptStart = ptEnd;
		    angle = LBMath.GetRadian(val);
		    ptEnd.X = (float)(cx + radius * Math.Cos(angle));
		    ptEnd.Y = (float)(cy + radius * Math.Sin(angle));
			pth1.AddLine ( ptStart, ptEnd );

		    ptStart = ptEnd;
		    angle = LBMath.GetRadian(val - 10);
			ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
		    ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
		    pth1.AddLine ( ptStart, ptEnd );
			
		    pth1.CloseFigure();
		    
			SolidBrush br = new SolidBrush( this.AnalogMeter.NeedleColor );
		    Pen pen = new Pen ( this.AnalogMeter.NeedleColor );
			Gr.DrawPath ( pen, pth1 );
			Gr.FillPath ( br, pth1 );
			
			return true;
		}

        /// <summary>
        /// Draw the needle cover at the center
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawNeedleCover( Graphics Gr, RectangleF rc )
		{
			if ( this.AnalogMeter == null )
				return false;
			
			Color clr = this.AnalogMeter.NeedleColor;
			RectangleF _rc = rc;
			float drawRatio = this.AnalogMeter.GetDrawRatio();
			
			Color clr1 = Color.FromArgb( 70, clr );
			
			_rc.Inflate ( 5 * drawRatio, 5 * drawRatio );
		
			SolidBrush brTransp = new SolidBrush ( clr1 );
			Gr.FillEllipse ( brTransp, _rc );
			
			clr1 = clr;
			Color clr2 = LBColorManager.StepColor ( clr, 75 );
			LinearGradientBrush br1 = new LinearGradientBrush( rc,
			                                                   clr1,
			                                                   clr2,
			                                                   45 );
			Gr.FillEllipse ( br1, rc );
			return true;
		}

        /// <summary>
        /// Draw the glass effect
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawGlass( Graphics Gr, RectangleF rc )
		{
			if ( this.AnalogMeter == null )
				return false;
			
			if ( this.AnalogMeter.ViewGlass == false )
				return true;
			
			Color clr1 = Color.FromArgb( 40, 200, 200, 200 );
			
			Color clr2 = Color.FromArgb( 0, 200, 200, 200 );
			LinearGradientBrush br1 = new LinearGradientBrush( rc,
			                                                   clr1,
			                                                   clr2,
			                                                   45 );
			Gr.FillEllipse ( br1, rc );
			
			return true;
		}
	}

    /// <summary>
    /// Custom renderer class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Gauges.AnalogMeterControl.AnalogMeter.LBAnalogMeterRenderer" />
	public class LBCustomAnalogMeterRenderer : LBAnalogMeterRenderer
    {
        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawBody(Graphics Gr, RectangleF rc)
        {
            return true;
        }

        /// <summary>
        /// Draw the glass effect
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            return true;
        }

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr">The gr.</param>
        /// <param name="rc">The rc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;
            
            PointF needleCenter = this.AnalogMeter.GetNeedleCenter();
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            float scaleDivisions = this.AnalogMeter.ScaleDivisions;
            float scaleSubDivisions = this.AnalogMeter.ScaleSubDivisions;
            float drawRatio = this.AnalogMeter.GetDrawRatio();
            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            Color scaleColor = this.AnalogMeter.ScaleColor;

            float cx = needleCenter.X;
            float cy = needleCenter.Y;
            float w = rc.Width;
            float h = rc.Height;

            float incr = LBMath.GetRadian((endAngle - startAngle) / ((scaleDivisions - 1) * (scaleSubDivisions + 1)));
            float currentAngle = LBMath.GetRadian(startAngle);
            float radius = (float)(w / 2 - (w * 0.08));
            float rulerValue = (float)minValue;

            Pen pen = new Pen(scaleColor, (2 * drawRatio));
            SolidBrush br = new SolidBrush(scaleColor);

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);
            PointF ptCenter = new PointF(0, 0);
            RectangleF rcTick = new RectangleF(0, 0, 0, 0);
            SizeF sizeMax = new SizeF(10 * drawRatio, 10 * drawRatio);
            SizeF sizeMin = new SizeF(4 * drawRatio, 4 * drawRatio);

            int n = 0;
            for (; n < scaleDivisions; n++)
            {
                //Draw Thick Line
                ptCenter.X = (float)(cx + (radius - w / 70) * Math.Cos(currentAngle));
                ptCenter.Y = (float)(cy + (radius - w / 70) * Math.Sin(currentAngle));
                ptStart.X = ptCenter.X - (5 * drawRatio);
                ptStart.Y = ptCenter.Y - (5 * drawRatio);
                rcTick.Location = ptStart;
                rcTick.Size = sizeMax;
                Gr.FillEllipse(br, rcTick);

                //Draw Strings
                Font font = new Font(this.AnalogMeter.Font.FontFamily, (float)(8F * drawRatio), FontStyle.Italic);

                float tx = (float)(cx + (radius - (20 * drawRatio)) * Math.Cos(currentAngle));
                float ty = (float)(cy + (radius - (20 * drawRatio)) * Math.Sin(currentAngle));
                double val = Math.Round(rulerValue);
                String str = String.Format("{0,0:D}", (int)val);

                SizeF size = Gr.MeasureString(str, font);
                Gr.DrawString(str,
                                font,
                                br,
                                tx - (float)(size.Width * 0.5),
                                ty - (float)(size.Height * 0.5));

                rulerValue += (float)((maxValue - minValue) / (scaleDivisions - 1));

                if (n == scaleDivisions - 1)
                    break;

                if (scaleDivisions <= 0)
                    currentAngle += incr;
                else
                {
                    for (int j = 0; j <= scaleSubDivisions; j++)
                    {
                        currentAngle += incr;

                        ptCenter.X = (float)(cx + (radius - w / 70) * Math.Cos(currentAngle));
                        ptCenter.Y = (float)(cy + (radius - w / 70) * Math.Sin(currentAngle));
                        ptStart.X = ptCenter.X - (2 * drawRatio);
                        ptStart.Y = ptCenter.Y - (2 * drawRatio);
                        rcTick.Location = ptStart;
                        rcTick.Size = sizeMin;
                        Gr.FillEllipse(br, rcTick);
                    }
                }
            }

            return true;
        }
    }
}
