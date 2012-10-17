AdvancedColorPicker
===================

A color picker component for Monotouch



Usage
=====
AdvancedColorPicker is very simple and easy to use:

1. Create an instance of ColorPickerViewController
2. Show ColorPickerViewController.View
3. Subscribe to ColorPickerViewController.ColorPicked event, to be notified when the user picks a color

You can also use the ColorPickerViewController.SelectedColor property to get/set the selected color. 
When you change the value of this property and the ColorPickerViewController.View is visible, the color
selection indicators will pick the color using an animation.



Compatibility
==============
AdvancedColorPicker is tested on iOS 4.3, 5.1, 6.0, both on iPhone and iPad.

All devices, screen sizes and orientations are supported.
AdvancedColorPicker does not use images, but custom drawing to display everything.



License
========
AdvancedColorPicker is licensed under the terms of the MIT license.

If you use this component in your projects consider adding the following in you app about screen:
"This app uses AdvancedColorPicker developed by Yiannis Bourkelis"