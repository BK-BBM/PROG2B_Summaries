# PROG2B_Summaries
Runnable version of code for all the concepts discussed in class


# NOTE

## Parallel Execution Demo (11 on the menu)
To "visually see the method **_RunParallelDemo()_** <br/>
Create a breakpoint at this line: 
```csharp
double result = 0;
```
<br/>
Then when execution stops, click on <code>Debug</code>><code>Windows</code>><code>Threads</code> <br/>
You should see the main thread and other threads that run parallel to it as output is displayed on the console window.<br/>

## Async Demo (12 on the menu)
To "visually see the method **_RunAsyncDemo()_** <br/>
Create a breakpoint at this line:
```csharp 
string[] pages = await Task.WhenAll(download1, download2, download3);
```
<br/>
Then when execution stops, click on <code>Debug</code>><code>Windows</code>><code>Threads</code> <br/>
You should see the main thread and other threads that are spawned as a result of asynchronous processing.<br/>
This is the goal, to demonstrate that processes don't lock/get block like they would if it was synchronous processes. 

## LO12: The main difference between Multi-threaded, Parallel and Async programming 

### What is Multithreading? <br/>
Multithreading is the ability of a program to execute multiple threads.<br/>
Different threads can perform different tasks simultaneously.<br/>
On a multi-core CPU:
* Threads can run on different cores at the same time.
Benefits:
Faster execution
Better CPU utilisation
Improved application responsiveness
### Concurrency
Concurrency means multiple tasks make progress during overlapping time periods.
Tasks do not have to execute simultaneously.
A single-core processor achieves concurrency using time slicing.
The CPU rapidly switches between tasks, giving the appearance that they run together.
Key Point

Concurrency is about managing multiple tasks efficiently.
### Parallelism
Parallelism means multiple tasks execute at exactly the same time.
Requires multiple CPU cores (or processors).
Each core executes a different thread simultaneously.
Key Point

Parallelism is about doing multiple tasks at the same time.
### Concurrency vs Parallelism
#### Concurrency

Tasks overlap.
Can occur on a single CPU core.
Achieved through time slicing.
#### Parallelism

Tasks execute simultaneously.
Requires multiple CPU cores.
Improves performance by sharing work across cores.
