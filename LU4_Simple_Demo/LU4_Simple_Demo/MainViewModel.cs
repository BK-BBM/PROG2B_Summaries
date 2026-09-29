using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LU4_Simple_Demo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string name = "";
        private int age;
        private string notificationMessage = "No input yet";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;

                    NotificationMessage = "Name change: " + name;

                    OnPropertyChanged(); //to update our name as we type


                }
            }
        }

 

        public string NotificationMessage
        {
            get => notificationMessage;

            set
            {
                notificationMessage = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => age;

            set 
            {
                age = value;
               
                OnPropertyChanged();
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
         * TRY IT YOURSELF (29/09/2026)
         * Add a Button that is only clickable if the form is valid (Valid age and name)
         * When the Button is clicked, show a message box with the provided form values
         * 
        */
    }
}
