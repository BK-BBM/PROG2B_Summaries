using System;
using System.Collections.Generic;
using System.Text;

namespace LU1_Summary
{
    class ResourceManager : IDisposable
    {
        private StreamWriter _writer;

        private bool _disposed = false;

        public ResourceManager(string filePath)
        {
            _writer = new StreamWriter(filePath);
        }

        //need a method to write to files 
        public void WritetoFile(string message)
        {

            if (_disposed) return;

            _writer.WriteLine(message);
        }


        public void Dispose() //create a flag for this method called _disposed
        {
            //within the Dispose method, you need to dispose! and then
            //tell your program not to run a desctructor if it exists

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_writer != null)
                {
                    _writer.Dispose();
                    Console.WriteLine("Streamwriter closed");
                }
            }

            _disposed = true;
        }
        ~ResourceManager()
        {

            Dispose(false);
        }
    }
}
