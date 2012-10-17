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
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace AdvancedColorPicker
{
	public class HueIndicatorView : UIView
	{
		public HueIndicatorView ()
		{
			BackgroundColor = UIColor.Clear;
		}

		public HuePickerView huePickerViewRef;

		public override void Draw (System.Drawing.RectangleF rect)
		{
			base.Draw (rect);

			CGContext context = UIGraphics.GetCurrentContext();

			float indicatorLength = rect.Size.Height / 3;

			context.SetFillColor(UIColor.Black.CGColor);
			context.SetStrokeColor(UIColor.White.CGColor);
			context.SetLineWidth(0.5f);
			context.SetShadow(new SizeF(0,0),4);

			float pos = rect.Width / 2;

			context.MoveTo(pos - (indicatorLength/2), -1);
			context.AddLineToPoint(pos+(indicatorLength/2), -1);
			context.AddLineToPoint(pos, indicatorLength);
			context.AddLineToPoint(pos-(indicatorLength/2), -1);

			context.MoveTo(pos-(indicatorLength/2), rect.Size.Height+1);
			context.AddLineToPoint(pos+(indicatorLength/2), rect.Size.Height+1);
			context.AddLineToPoint(pos, rect.Size.Height-indicatorLength);
			context.AddLineToPoint(pos-(indicatorLength/2), rect.Size.Height+1);

			context.ClosePath();
			context.DrawPath(CGPathDrawingMode.FillStroke); 
		}


	}
}

