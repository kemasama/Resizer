Resizer
====

Image Resizer for Windows.

# About

Simple command line image resize tool.

For example:  
`$ Resizer.exe -target "C:\Users\User Account\Pictures\" -output "D:\ResizeOutput"`

# Functions

- Async resize (better sync)
- Single application (no need runtimes)
- Fast mode

# Arguments

## -target <target directory>

This option is required.  
Choose source images directory.


## -output <output directory>

This option is required.  
Choose output images directory.


## -width <width>

This option is unrequired. Numbers only.  
Choose resize image width.


## -height <height>

This option is unrequired. Numbers only.  
Choose resize image height.

## -mode <mode>

This option is unrequired. Numbers only.  
Choose graphics interpoloation mode.  

[Reference InterpolationMode](https://learn.microsoft.com/ja-jp/dotnet/api/system.drawing.drawing2d.interpolationmode)

## Target envirnoment

Build by .NET Core. However, .NET Core didn't support `System.Drawing.Image` except Windows.  
So, this project can run on Windows only.  

Build release for single application of x64 arch Windows.

# Resize too late

If you resize larger than source, also slow resize depends machine spec.


# Copyright

Copyright &copy; 2022 DevRas All Rights Reserved.
