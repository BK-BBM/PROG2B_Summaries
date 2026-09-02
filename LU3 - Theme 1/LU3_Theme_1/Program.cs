namespace LU3_Theme_1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //to "manipulate files -> need to know where to look

            string rootpath = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Path.Combine(rootpath, "LU3_Things");
            string actualFile = Path.Combine(projectDirectory, "LU3_log.txt");

            /* the code above is a safe way of writing the following path
             * C:\Users\tman\source\repos\LU3_Theme_1\LU3_Theme_1\bin\Debug\net10.0\LU3_Things\LU3_log.txt
             * 
             * rather than use a folder on my computer, I'm using the debug folder (which contains our .exe file)
             * 
             * AppDomain.Current.Domain.BaseDirectory -> "get the folder that contains the .exe file for this program"
             */


            DriveInfo[] drive = DriveInfo.GetDrives();
            //DriveInfo so I can get a list of hard drives on my machine (I only have one) 

            try
            {
                //writing the file
                if (!Directory.Exists(projectDirectory))
                {
                    Directory.CreateDirectory(projectDirectory);
                    //CreateDirectory to create our folder if it doesn't exist.

                    Console.WriteLine($"Folder {projectDirectory} created");
                }

                using (StreamWriter streamWriter = new StreamWriter(actualFile, append: false))
                {
                    streamWriter.WriteLine("LU3 Licence Terms");
                    streamWriter.WriteLine($"Date created: {DateTime.Now}");
                    streamWriter.WriteLine($"LU3 Installed Successfully on {drive[0]}");
                    /*because I only have one drive, I get whatever is at index 0 of my DriveInfo array.
                     * again, this checks the system drive (where the program "lives") 
                    */
                }
                Console.WriteLine("Installation completed");
                Console.WriteLine();
                Console.WriteLine();

                //reading the file
                Console.WriteLine("File contents");
                if (File.Exists(actualFile))
                {
                    using (StreamReader streamReader = new StreamReader(actualFile))
                    {
                        string line;

                        while ((line = streamReader.ReadLine()) != null)
                        {
                            
                            Console.WriteLine($"{line}");
                        }

                        /* in the while loop, we used an assignment expression as a condition (2 birds, 1 stone) 
                         * because streamReader.ReadLine() will return string, assign that the variable "line" and check if it's null
                         * 
                         * an alternative way of reading the file would be to use the File class 
                         * but we were using Stream (because of familiarity) 
                         * 
                         * this foreach loop will do the exact same thing. TRY IT OUT
                         * foreach (string textLine in File.ReadLines(actualFile))
                           {
                            Console.WriteLine(textLine);
                           }
                         */
                         
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                
                //get file info
                Console.WriteLine("File info");
                FileInfo fileInfo = new FileInfo(actualFile);

                Console.WriteLine($"File size: {fileInfo.Length} bytes");
                Console.WriteLine($"File name: {fileInfo.Name}" );
                Console.WriteLine($"File extension: {fileInfo.Extension}");

                /* I think the details we're getting above are self explanatory
                 * Let me know if you need clarity
                 */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
