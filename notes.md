# ls

* currently, ls has an if/else statement to determine if there are any arguments present. It might make more sense to get a list from a function that grabs all of the files and folders in the directory specified (no directory specified will be transformed into current directory as the  first argument. This way, if there are arguments that are not specifying a different directory than current, an argument list that is < 0 will not get confused when there is not directory argument but has an argument with a switch, ie -a)

* do I want to transfor the array into a list or transform the array to take one more arg ( see above ).

* use the following for transforming array

```
Array.Resize(ref array, newsize);
array[newsize - 1] = "newvalue"
```

* for multiple arguments, use a foreach to iterate through the array and do one argument at a time with a switch.
