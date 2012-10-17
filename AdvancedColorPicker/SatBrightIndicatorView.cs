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
	public class SatBrightIndicatorView : UIView
	{
		public SaturationBrightnessPickerView satBrightPickerViewRef;

		public SatBrightIndicatorView ()
		{
			BackgroundColor = UIColor.Clear;
		}


		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			float margins = 4;
			RectangleF drawRect = new RectangleF(rect.X + margins, rect.Y + margins, rect.Width - margins*2, rect.Height - margins*2);

			CGContext context = UIGraphics.GetCurrentContext();
			context.AddEllipseInRect(drawRect);
			context.AddEllipseInRect(drawRect.Inset(4,4));
			context.SetFillColor(UIColor.Black.CGColor);
			context.SetStrokeColor(UIColor.White.CGColor);
			context.SetLineWidth(0.5f);
			context.ClosePath();
			context.SetShadow(new SizeF(1,2),4);
			context.DrawPath(CGPathDrawingMode.EOFillStroke);
		}
	}
}

