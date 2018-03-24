== Description ==

Dutch VACC ATIS Generator.

Utility to generate a text output used to setup a voice ATIS (Automatic Terminal Information Service) in EuroScope.

Created by Daan Broekhuizen
Samples by Robert van der Leije


== Requirements ==

.NET 4.5.1 or higher.


== Installation ==

Run the Dutch VACC ATIS Generator - Setup.exe and follow the setup steps. 

The ATIS audio samples will be installed in your "*\Documents\EuroScope\atis" folder. After setup has completed,
setup the "ATIS files descriptor" path in your EuroScope "Voice ATIS Setup Dialog" by pointing EuroScope to the
ATIS descriptor file installed in the "*\Documents\EuroScope\atis" folder.


== Changelog ==

= 1.0.0.0 =
* Initial version.

= 1.0.0.1 =
* Checked a couple of check boxes by default (saves clicks).
* Automatic set runway combo boxes based on the best preferred runway and weather.

= 1.0.0.2 =
* Fixed bug when closing the runway weather criteria dialog and reopening it, would cause a disposed exception.
* Added safe check to selecting best preferred runway.

= 1.0.0.3 =
* Added some checks to the code which prevent some .NET errors from occurring.
* Disabled the option for auto selecting the best preferred runway. (Still have to finish that code...)
* Added a system which will give a pop-up if a newer version is available (pulls the latest version number from my website).
* Military METAR processing has been optimized.
* Tailwind component changed to headwind component.
* Fixed some issues regarding the generation of the runway info DataGridView.

= 1.0.0.4 =
* Fixed minor bugs.
* Added the ability to get real runway combination from Schiphol using the LVNL actual runway use website (http://www.lvnl.nl/nl/airtraffic).
* Added the ability to auto selected the best runway conform the weather (except EHAM).

= 1.0.0.5 =
* New Dutch VACC icon.
* Rewritten real runway parser for new LVNL site code.
* Added error checking to the real runway HTML parser.

= 1.0.0.6 =
* Added auto updater ability.
* Fixed bugs in processing of auto METARs.
* Refurbished the LVNL getter, now including the user agent header.

= 1.0.0.7 =
* Fixed bug caused by closing the runway info form.
* Multiple code optimizations.
* Added menu items for all forms.
* Replaced the best/real runway checkbox with a button (requires user action now).
* Added TAF form.
* Added link to Amsterdam Info and Dutch VACC website.

= 1.0.0.8 =
* Added settings to ATIS generator, the ability to auto fetch and auto process a METAR.
* Added the ability to add an user defined wave to the output (userwave.wav).
* METAR gets automatically pulled when starting the program or switching the ICAO tab.
* Fixed bug in TAF form. Supports asynchronous pulling of TAF now, prevents freezing of program.
* Fixed bug in last processed label if the input METAR is too long.
* Fixed bug in processing of the clouds.

= 1.0.0.9 =
* Added settings to ATIS generator, the ability to auto load the EHAM runways and auto generate the ATIS at start up (last needs auto process METAR and auto load EHAM runways to be enabled).
* Added support for TAF AMD and TAF COR.
* Added support for all runway combinations.
* Fixed bug in METAR loader (background worker).
* Fixed the ATIS letter not being changed when changing the ICAO tab is switched.
* Moved extra to the end of the ATIS. (output is now ...[end]A[extra])
* Fail checking on INI file.

= 1.0.1.0 =
* Swapped positions of the EHAM landing and departing runways combo boxes.
* Utility will flash in task bar when a new METAR is fetched (requires “Auto fetch METAR” option to be enabled).
* Utility will play a sound when a new METAR is fetched (requires “Play sound when METAR is fetched” option to be enabled) (sound can be changed by replacing the [installation folder]/sounds/alert.wav file).
* Added option for ATIS letter A – M for EHAM.
* Added option for ATIS letter N – Z for EHRD.
* Added the ability to random generate an ATIS letter on startup and on switching the ICAO tab.
* Added all converging approaches combinations.
* Skips phenomena for which there are no samples available.
* Fixed some minor bugs.

= 2.0.0.0 =
* Full text ATIS.
* New URL for TAF.
* Added more samples.
* Fixed bug in SNOWTAM.
* Changed "main runway" sample to "main landing runway" for regional airfields.
* Fixed bug in flashing window method.

