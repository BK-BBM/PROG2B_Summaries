# PROG2B_LU1_Summary
Runnable version of code for all the concepts discussed in LU1 (LO1 - LO11) 


# NOTE

## Parallel Execution Demo (11 on the menu)
To "visually see the method **_RunParallelDemo()_** <br/>
Create a breakpoint at this line: 
```csharp
double result = 0;
```
<br/>
Then when execution stops, click on `Debug`>`Windows`>`Threads` <br/>
You should see the main thread and other threads that run parallel to it as output is displayed on the console window.<br/>

## Async Demo (12 on the menu)
To "visually see the method **_RunAsyncDemo()_** <br/>
Create a breakpoint at this line:
```csharp 
string[] pages = await Task.WhenAll(download1, download2, download3);
```
<br/>
Then when execution stops, click on `Debug`>`Windows`>`Threads` <br/>
You should see the main thread and other threads that are spawned as a result of asynchronous processing.<br/>
This is the goal, to demonstrate that processes don't lock/get block like they would if it was synchronous processes. 

## LO12: The main difference between Multi-threaded, Parallel and Async programming 
