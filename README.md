# HoloLens-2-Relaxation
Relaxation game for HoloLens 2

How to setup Unity and Visual Studio for development, building and deployment:
Set Up:
1. Open Visual Studio Installer and add:
  Universal Windows Platform Development:
    - USB Device Connectivity
    - C++ (v142) Universal Windows Platform tools
    - Windows 10 SDK (10.0.18362.0)
2. Open Unity Hub and download Unity version 2020.3.26f1
  Make sure that Universal Windows Platform Build Support is part of your Unity installation
3. Clone this repository to your local machine
4. Locate the project folder from Unity Hub and open project

Build:
1. Access build settings in Unity
2. Ensure that Universal Windows Platform has been selected as target platform
3. In project settings, under the XR Plug-in Management->Project Validation tab, make sure that no fixable issues. Easily solved using the "Fix All" button
4. Return to build settings, specify a build folder
5. Build

Deployment:
1. Plug in your Hololens headset using USB
2. 

Attributions for free-to-use assets can be found in the Attributions.txt file
