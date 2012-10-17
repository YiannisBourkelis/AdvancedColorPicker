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

namespace AdvancedColorPicker
{
	public class ColorPickerViewController : UIViewController
	{
		public event Action ColorPicked;

		SizeF satBrightIndicatorSize = new SizeF(28,28);
		HuePickerView huewView = new HuePickerView();
		SaturationBrightnessPickerView satbrightview = new SaturationBrightnessPickerView();
		SelectedColorPreviewView selPrevView = new SelectedColorPreviewView();
		HueIndicatorView huewIndicatorView = new HueIndicatorView();
		SatBrightIndicatorView satBrightIndicatorView = new SatBrightIndicatorView();

		public ColorPickerViewController ()
		{
			satbrightview.hue = .5984375f;
			satbrightview.saturation = .5f;
			satbrightview.brightness = .7482993f;
			huewView.Hue = satbrightview.hue;

			selPrevView.BackgroundColor = UIColor.FromHSB(satbrightview.hue,satbrightview.saturation,satbrightview.brightness);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			float selectedColorViewHeight = 60;

			float viewSpace = 1;

			selPrevView.Frame = new RectangleF(0,0,this.View.Bounds.Width,selectedColorViewHeight);
			selPrevView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			selPrevView.Layer.ShadowOpacity = 0.6f;
			selPrevView.Layer.ShadowOffset = new SizeF(0,7);
			selPrevView.Layer.ShadowColor = UIColor.Black.CGColor;


			//to megalo view epilogis apoxrwsis tou epilegmenou xrwmats
			satbrightview.Frame = new RectangleF(0,selectedColorViewHeight + viewSpace , this.View.Bounds.Width, this.View.Bounds.Height - selectedColorViewHeight - selectedColorViewHeight  - viewSpace - viewSpace);
			satbrightview.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			satbrightview.ColorPicked += HandleColorPicked;
			satbrightview.AutosizesSubviews = true;

			//to mikro view me ola ta xrwmata
			huewView.Frame = new RectangleF(0, this.View.Bounds.Bottom - selectedColorViewHeight, this.View.Bounds.Width, selectedColorViewHeight);
			huewView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin;
			huewView.HueChanged += HandleHueChanged;

			huewIndicatorView.huePickerViewRef = huewView;
			float pos = huewView.Frame.Width * huewView.Hue;
			huewIndicatorView.Frame = new RectangleF(pos - 10,huewView.Bounds.Y - 2,20,huewView.Bounds.Height + 2);
			huewIndicatorView.UserInteractionEnabled = false;
			huewIndicatorView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			huewView.AddSubview(huewIndicatorView);

			satBrightIndicatorView.satBrightPickerViewRef = satbrightview;
			PointF pos2 = new PointF(satbrightview.saturation * satbrightview.Frame.Size.Width, 
			                         satbrightview.Frame.Size.Height - (satbrightview.brightness * satbrightview.Frame.Size.Height));
			satBrightIndicatorView.Frame = new RectangleF(pos2.X - satBrightIndicatorSize.Width/2,pos2.Y-satBrightIndicatorSize.Height/2,satBrightIndicatorSize.Width,satBrightIndicatorSize.Height);
			satBrightIndicatorView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;
			satBrightIndicatorView.UserInteractionEnabled = false;
			satbrightview.AddSubview(satBrightIndicatorView);

			this.View.AddSubviews(new UIView[] {satbrightview, huewView, selPrevView});
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			PositionIndicators();
		}

		void PositionIndicators()
		{
			PositionHueIndicatorView();
			PositionSatBrightIndicatorView();
		}

		void PositionSatBrightIndicatorView ()
		{
			UIView.Animate(0.3f,0f,UIViewAnimationOptions.AllowUserInteraction, delegate() {
				PointF pos = new PointF(satbrightview.saturation * satbrightview.Frame.Size.Width, 
				                        satbrightview.Frame.Size.Height - (satbrightview.brightness * satbrightview.Frame.Size.Height));
				satBrightIndicatorView.Frame = new RectangleF(pos.X - satBrightIndicatorSize.Width/2,pos.Y-satBrightIndicatorSize.Height/2,satBrightIndicatorSize.Width,satBrightIndicatorSize.Height);
			}, delegate() {
			});
		}

		void PositionHueIndicatorView ()
		{
			UIView.Animate(0.3f,0f,UIViewAnimationOptions.AllowUserInteraction, delegate() {
				float pos = huewView.Frame.Width * huewView.Hue;
				huewIndicatorView.Frame = new RectangleF(pos - 10,huewView.Bounds.Y - 2,20,huewView.Bounds.Height + 2);
			}, delegate() {
				huewIndicatorView.Hidden = false;
		});
		}

		void HandleColorPicked ()
		{
			PositionSatBrightIndicatorView ();
			selPrevView.BackgroundColor = UIColor.FromHSB (satbrightview.hue, satbrightview.saturation, satbrightview.brightness);

			if (ColorPicked != null) {
				ColorPicked();
			}
		}

		void HandleHueChanged ()
		{
			PositionHueIndicatorView();
			satbrightview.hue = huewView.Hue;
			satbrightview.SetNeedsDisplay();
			HandleColorPicked();
		}

		public UIColor SelectedColor {
			get {
				return UIColor.FromHSB(satbrightview.hue,satbrightview.saturation,satbrightview.brightness);
			}
			set {
				float hue = 0,brightness = 0,saturation = 0,alpha = 0;
				value.GetHSBA(out hue,out saturation,out brightness,out alpha);
				huewView.Hue = hue;
				satbrightview.hue = hue;
				satbrightview.brightness = brightness;
				satbrightview.saturation = saturation;
				selPrevView.BackgroundColor = value;

				PositionIndicators();

				satbrightview.SetNeedsDisplay();
				huewView.SetNeedsDisplay();
			}
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.All;
		}

		//gia symvatotita me ios 4/5
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			if (UIDevice.CurrentDevice.SystemVersion.StartsWith("4.")){
				PositionIndicators();
			}
		}

		public override bool ShouldAutorotate ()
		{
			return true;
		} 
	}
}

