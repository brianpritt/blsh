## Classes
* built-ins.cs
* initialize.cs
* session.cs
* blsh.cs

## Flow
* Look for _blsh.ini_ (`Initialize.checkIni()`), if it does not exist create one spcific to OS
  * if does exist continue to next step

* Initialize program `Initialize newInit = new Initialize()`
  * Create environment variables, most importantly `_home` and `_binaries`. These are the home path and binaries paths respecitvely

* Set arbitrary env variables such as color `.ConfigEnv()`

* Create the session object `Session newSession = new Session (newInit.GetPath(), newInit.GetBinaries());` pulling from initialize class

* Set current Directory

* begin loop that awaits user input

* after input, split command from arguments
  * add command to history

* pass session object, command, and argument into Promulgate method `Promulgate(newSession, command, args);`

* Promulgate method
  * if command = _exit_, exit program
  * check if command exists in built-ins dictionary `else if (BuiltIns.builtins.ContainsKey(command))`
  * if not in built-ins, attempt to run as a system process `process.StartInfo = new ProcessStartInfo(bin + external, args );`
  * pass exception to shell if there is one, currently does not pass exception message because it is checking an array of binary paths and will send several exceptions because file only exists in one.

## Built-ins Flow
#### built-ins uses a delegate so command can call a method using a dictionary or KVP instead of branching.

* Create delegate
* Create dictionary linking methods to possible input
* `runBuiltIns(Session thisSession, string com, string args)` is called from the Promugate method in blsh.cs
* run method assocated with input
* return to blsh.cs

