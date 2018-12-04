using System;

namespace CSharpOOP
{
        public abstract class Vehicle
        {
            public string Name { get; set; }
            public string Color { get; set; }
            public int Id { get; set; }
            public bool IsRunning { get; private set; }

            public void Drive()
            {
                if (IsRunning)
                    Console.WriteLine("The engine is already running.");
                else
                    IsRunning = true;
            }

            public override string ToString()
            {
                return Name;
            }
        }
}
