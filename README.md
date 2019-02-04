# _blsh - The Brian Lee shell v0.01_

#### _a small proof of concept shell written in c#, Winter/Spring 2019_

### The initial goal of _blsh_ is to make a basic shell that would be capable of reading commands and executing them with arguments.  The goal of this project is to scope out if it is a simple enough task to assign my c# students.

### Notes and questions
* Shell is currently passing an object all over the place. _Main()_ -> _DoesItExist(Session)_ -> _BuiltIns.runBuiltIns(Session)_ -> _anyBuiltInMethod(Session)_ and all the way back to main. I don't like this design, but it was a quick and dirty way of doing it.  
### External applications
* blsh will look for extenal applications in the /bin folder. 

## ToDo
* _Create ini file_
  *
* _Read MacOS native bash commands and .exe through mono. currently only runs .exe(simple fix)_
* _Change the name of the method DoesItExist_
*


## Finished
* _Basic Loop in Main()_
* _Simple ls application to test blsh_
* _handles builtins using delegates_

## Current Built-Ins
* clear
* pwd
* whoami

## Known Bugs

## Copyleft
(c) 2019 Brian Pritt GPLv3
