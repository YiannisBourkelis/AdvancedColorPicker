/*
 * This code is licensed under the terms of the MIT license
 *
 * Copyright (C) 2012 Yiannis Bourkelis
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights 
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished
 * to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace AdvancedColorPicker
{
	public class HuePickerView : UIView
	{
		public event Action HueChanged;

		public HuePickerView ()
		{
		}

		public float Hue { get; set; }

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			CGContext context = UIGraphics.GetCurrentContext();

			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();

			float step=0.166666666666667f;

			float[] locations = new float[] {
				0.00f, 
				step, 
				step*2, 
				step*3, 
				step*4, 
				step*5, 
				1.0f
			};

			CGColor c1 = new CGColor(1,0,1,1);
			CGColor c2 = new CGColor(1,1,0,1);
			CGColor c3 = new CGColor(0,1,1,1);

			CGColor[] colors = new CGColor[] {
				UIColor.Red.CGColor,
				c1,
				UIColor.Blue.CGColor,
				c3,
				UIColor.Green.CGColor,
				c2,
				UIColor.Red.CGColor
			};


			CGGradient gradiend = new CGGradient(colorSpace,colors, locations);
			context.DrawLinearGradient(gradiend,new PointF(rect.Size.Width,0),new PointF(0,0),CGGradientDrawingOptions.DrawsBeforeStartLocation);
			gradiend.Dispose();
			colorSpace.Dispose();
		} // draw

		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			HandleTouches(touches,evt);
		}
		public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			HandleTouches(touches,evt);
		}

		private void HandleTouches (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			var touch = (UITouch)evt.TouchesForView (this).AnyObject;
			PointF pos;
			pos = touch.LocationInView (this);

			float p = pos.X;

			float b = Frame.Size.Width;
	
			if (p < 0)
				Hue = 0;
			else if (p > b)
				Hue = 1;
			else
				Hue = p / b;


			if (HueChanged != null) {
				HueChanged();
			}
		}
	}
}

