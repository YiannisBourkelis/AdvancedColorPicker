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
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AdvancedColorPicker;

namespace AdvancedColorPickerDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		ColorPickerViewController picker;
		ContainerController container;
		UIButton pickAColorBtn;
		UINavigationController nav;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			container = new ContainerController();
			nav = new UINavigationController(container);

			pickAColorBtn = UIButton.FromType(UIButtonType.RoundedRect);
			pickAColorBtn.Frame = new System.Drawing.RectangleF(UIScreen.MainScreen.Bounds.Width/2 - 50,UIScreen.MainScreen.Bounds.Height/2 - 22,100,44);
			pickAColorBtn.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			pickAColorBtn.SetTitle("Pick a color!",UIControlState.Normal);
			pickAColorBtn.TouchUpInside += pickAColorBtn_HandleTouchUpInside;
			container.View.AddSubview(pickAColorBtn);

			window.RootViewController = nav;
	
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

		UIBarButtonItem doneBtn;
		void pickAColorBtn_HandleTouchUpInside (object sender, EventArgs e)
		{
			picker = new ColorPickerViewController();
			picker.ColorPicked += HandleColorPicked;
			picker.Title = "Pick a color!";
			UINavigationController pickerNav = new UINavigationController(picker);
			pickerNav.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

			doneBtn = new UIBarButtonItem(UIBarButtonSystemItem.Done);
			picker.NavigationItem.RightBarButtonItem = doneBtn;
			doneBtn.Clicked += doneBtn_HandleClicked;
			nav.PresentModalViewController(pickerNav,true);
		}

		void doneBtn_HandleClicked (object sender, EventArgs e)
		{
			HandleColorPicked();
			nav.DismissModalViewControllerAnimated(true);
		}

		void HandleColorPicked ()
		{
			container.View.BackgroundColor = picker.SelectedColor;
		}
	}
}

