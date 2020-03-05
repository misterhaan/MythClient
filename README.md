# MythClient
MythClient helps Windows clients play recordings from a separate MythTV server.  It provides an alternative to the MythWeb Recorded Programs page, with the streaming and downloading of recordings replaced with directly launching the recording from a network share.

Download the latest installer from [track7.org](http://www.track7.org/code/vs/mythclient).  Check the [wiki](https://github.com/misterhaan/MythClient/wiki/) for more information.

## Revision History

### (next)
* Check for updates and support update and restart
* Show title when cover art isn't available
* Remove vertical gap in delete options

### 2.0.5
* Fix unexpected jump into show when deleting an episode from the recordings view

### 2.0.4
* Fix refresh not updating display on show or season view
* Fix delete not updating title when it triggers going back

### 2.0.3
* Fix crash loading recordings if at least one didn't have an optional date
* Fix crash when clicking empty space below last show / season / episode
* Error message instead of crash when trying to play or download recording files that don't exist
* Fix contents scrolling to bottom and arrow keys not working

### 2.0.2
* Fix deletion still showing deleted item

### 2.0.1
* Fix startup without required settings

### 2.0.0
* Add Ctrl+Enter shortcut to play next instead of opening the show / season
* Fix vertical scroll stopping before the bottom
* Configuration file changed (previous configuration ignored)
* Major refactor into multiple assemblies
* Update to .NET 4.5
* Add unit tests

### 1.2.1
* Handle deleting last episode

### 1.2.0
* Add delete all to shows and seasons

### 1.1.0
* Include "Next up" episode in season and show information
* Add play with at season and show levels
* Handle movies differently than series

### 1.0.1
* Switch to Visual Studio 2017.
* Handle no cover art for a show.

### 1.0.0
* Switch to API instead of parsing MythWeb.
* New show-based user interface.
* Export (download) now shows progress window and doesn't block the main window.

### 0.3.0
* Add export feature.

### 0.2.1
* Handle blank original airdate.

### 0.2.0
* Added Play with... feature in case an alternate movie player is needed for some shows.

### 0.1.1
* Fix to TaskDialog for 64-bit systems.

### 0.1.0
* Initial release.