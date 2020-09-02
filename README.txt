****************
MATHS FOR GAMES PROJECT
****************

AUTHOR: Aidan Sakovits

README UPDATED: 21/08/2020

README: This project was made by Aidan Sakovits for assessment by the Academy of Interactive Entertainment.
It demonstrates the usage of Vector and Matrix transformations in graphics.

BUILD: Open the solution in Visual Studio 2019, and create a release build with "Any CPU" configuration. All
necessary files and should be handled automatically. Please note that a rebuild will replace all saved
inventory and entity data with the default.

EXECUTION: After build, execution is performed by running ./Project2D/bin/Release/GraphicalTest.exe

USE:
	Move your tank and turret with WASDQE, fire with Space.

EXTENSION FEATURES:
	Oriented bounding boxes, instead of aligned
	Firing bullets
	Interobject collision system
	Health/Damage/Destruction
	Noise alpha blended background

FILE MANIFEST:
	MathClasses
		App.config
		Colour.cs
		MathClassesAidan.csproj
		Matrix3.cs
		Matrix4.cs
		Program.cs
		Vector3.cs
		Vector4.cs
	Project2D
		Bindings
			Easings.cs
			Extensions.cs
			Physac.cs
			Raygui.cs
			Raylib.cs
			Raylb.projitems
			Raylib.shproj
			Raymath.cs
		Images
			Bullets
				(Bullet pngs)
			Environment
				(Environment pngs)
			Tanks
				(Tank and Barrel pngs)
		Properties
			AssemblyInfo.cs
		App.config
		Bullet.cs
		CollisionManager.cs
		Game.cs
		Global.cs
		GraphicalTest.csproj
		PlayerController.cs
		Program.cs
		SceneObject.cs
		Sprites.cs
		Tank.cs
		Turret.cs
	Raylib
		Bindings
			Easings.cs
			Extensions.cs
			Physac.cs
			Raygui.cs
			Raylib.cs
			Raylb.projitems
			Raylib.shproj
			Raymath.cs
		dll
			raylib.dll
		include
			raylib.h
		lib
			cmake
				raylib
					raylib-config.cmake
					raylib-config-version.cmake
			pkgconfig
				pralib.pc
			raylib.lib
			ralib_static.lib
	M4GGraphicalTest.sln
	README.txt

LICENSING: GNU General Public License
https://www.gnu.org/licenses/gpl-3.0.en.html

REQUIREMENTS: Windows 10 (insider) build 1826 or higher
Support not guaranteed on other platforms or earlier builds

KNOWN BUGS:
Bullets fired offscreen are immediately destroyed