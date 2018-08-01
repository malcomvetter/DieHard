# DIE HARD(er)!

Build in Visual Studio (.net 3.5 so it runs on Win 7+). 

This is a proof of concept, example project that shows two patterns for how to protect userland processes on Windows written in C#:
* Alpha watches Bravo - two binaries watching each other
* Charlie watching itself - a single binary that spawns another instance of itself and watches the other instance

Both cases will respawn their twin process if they observe it dying. All spawns are passed through WMI to "launder" the parent process (making kill process tree a little more difficult since you have to kill the WMI process as well).

These are PROOF OF CONCEPT only. They're not perfect, but they're certainly a nuisance. 

Oh, and there are plenty of DIE HARD references for Bruce Willis fans.

![logo](periscope.png)

