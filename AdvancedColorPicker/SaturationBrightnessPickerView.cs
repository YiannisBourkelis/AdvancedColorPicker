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
//using System.Diagnostics;

namespace AdvancedColorPicker
{
	public class SaturationBrightnessPickerView : UIView
	{
		public event Action ColorPicked;

		public SaturationBrightnessPickerView ()
		{
		}

		public float hue { get; set; }
		public float saturation { get; set; }
		public float brightness { get; set; }

		public override void Draw (RectangleF rect)
		{
			//Stopwatch s = new Stopwatch();
			//s.Start();

			//Console.WriteLine (" ----- SatBrightPickerView Draw");

			CGContext context = UIGraphics.GetCurrentContext ();

			CGColor[] gradColors = new CGColor[] {UIColor.FromHSBA(hue,1,1,1).CGColor,new CGColor(1,1,1,1)};
			float[] gradLocations = new float[] { 0.0f, 1.0f };

			var colorSpace = CGColorSpace.CreateDeviceRGB ();

			CGGradient gradient = new CGGradient (colorSpace, gradColors, gradLocations);
			context.DrawLinearGradient(gradient,new PointF(rect.Size.Width,0),new PointF(0,0),CGGradientDrawingOptions.DrawsBeforeStartLocation);
	
			gradColors = new CGColor[] {new CGColor(0,0,0,0), new CGColor(0,0,0,1)};
	
			gradient = new CGGradient(colorSpace,gradColors, gradLocations);
			context.DrawLinearGradient(gradient,new PointF(0,0),new PointF(0,rect.Size.Height),CGGradientDrawingOptions.DrawsBeforeStartLocation);

			gradient.Dispose();
			colorSpace.Dispose();
		
			//s.Stop();
			//Console.WriteLine("-----> SatBright Draw time: " + s.Elapsed.ToString());
		} //draw

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

			float w = Frame.Size.Width;
			float h = Frame.Size.Height;
			
			if (pos.X < 0)
				saturation = 0;
			else if (pos.X > w)
				saturation = 1;
			else
				saturation = pos.X / w;
			
			if (pos.Y < 0)
				brightness = 1;
			else if (pos.Y > h)
				brightness = 0;
			else
				brightness = 1 - (pos.Y / h);

			if (ColorPicked != null) {
				ColorPicked ();
			}
		}
	}
}

