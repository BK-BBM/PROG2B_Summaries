# LU3 Theme 1:  Manipulating Files and Directories using System I/O Operations

## Key System.IO methods (excerpt from: Andrew Troelsen & Phil Japikse (Pro C#10)
### System.IO Namespace methods 

<table style="width: 542px;">
<tbody>
<tr>
<td style="width: 79.65px;">Directory<br />DirectoryInfo</td>
<td style="width: 448.35px;">You use these classes to manipulate a machine&rsquo;s directory structure. The Directory type exposes functionality using static members, while the DirectoryInfo type exposes similar functionality from a valid object reference.</td>
</tr>
<tr>
<td style="width: 79.65px;">DriveInfo</td>
<td style="width: 448.35px;">This class provides detailed information regarding the drives that a given machine uses.</td>
</tr>
<tr>
<td style="width: 79.65px;">File<br />FileInfo</td>
<td style="width: 448.35px;">You use these classes to manipulate a machine&rsquo;s set of files. The File type exposes functionality using static members, while the FileInfo type exposes similar functionality from a valid object reference.</td>
</tr>
<tr>
<td style="width: 79.65px;">FileStream</td>
<td style="width: 448.35px;">This class gives you random file access (e.g., seeking capabilities) with data represented as a stream of bytes.</td>
</tr>
<tr>
<td style="width: 79.65px;">Path</td>
<td style="width: 448.35px;">This class performs operations on System.String types that contain file or directory path information in a platform-neutral manner.</td>
</tr>
<tr>
<td style="width: 79.65px;">StreamWriter<br />StreamReader</td>
<td style="width: 448.35px;">You use these classes to store (and retrieve) textual information to (or from) a file. These types do not support random file access.</td>
</tr>
</tbody>
</table>

### FileSystemInfo Properties 
<table style="height: 114px; width: 479px;">
<tbody>
<tr>
<td style="width: 114.3px;">Attributes</td>
<td style="width: 349.7px;">Gets or sets the attributes associated with the current file that are represented by the FileAttributes enumeration (e.g., is the file or directory read-only, encrypted, hidden, or compressed?)</td>
</tr>
<tr>
<td style="width: 114.3px;">Exists</td>
<td style="width: 349.7px;">Determines whether a given file or directory exists</td>
</tr>
<tr>
<td style="width: 114.3px;">Name</td>
<td style="width: 349.7px;">Obtains the name of the current file or directory</td>
</tr>
</tbody>
</table>

### DirectoryInfo 
<table style="height: 62px; width: 480px;">
<tbody>
<tr style="height: 56px;">
<td style="width: 85.8px; height: 56px;">
<p>Create()</p>
<p>CreateSubdirectory()</p>
</td>
<td style="width: 380.2px; height: 56px;">Creates a directory (or set of subdirectories) when given a path name</td>
</tr>
<tr style="height: 13px;">
<td style="width: 85.8px; height: 13px;">Root</td>
<td style="width: 380.2px; height: 13px;">Gets the root portion of a path</td>
</tr>
<tr style="height: 13.8px;">
<td style="width: 85.8px; height: 13.8px;">GetDirectories()</td>
<td style="width: 380.2px; height: 13.8px;">Returns an array of DirectoryInfo objects that represent all subdirectories in the current directory</td>
</tr>
<tr style="height: 13px;">
<td style="width: 85.8px; height: 13px;">GetFiles()</td>
<td style="width: 380.2px; height: 13px;">Retrieves an array of FileInfo objects that represent a set of files in the given directory</td>
</tr>
</tbody>
</table>

### FileInfo
<table style="height: 73px; width: 476px;">
<tbody>
<tr style="height: 14px;">
<td style="width: 99.8px; height: 14px;">AppendText()</td>
<td style="width: 362.2px; height: 14px;">Creates a StreamWriter object that appends text to a file</td>
</tr>
<tr style="height: 14px;">
<td style="width: 99.8px; height: 14px;">Create()</td>
<td style="width: 362.2px; height: 14px;">Creates a new file and returns a FileStream object to interact with the newly created file</td>
</tr>
<tr style="height: 14.3625px;">
<td style="width: 99.8px; height: 14.3625px;">CreateText()</td>
<td style="width: 362.2px; height: 14.3625px;">Creates a StreamWriter object that writes a new text file</td>
</tr>
</tbody>
</table>
