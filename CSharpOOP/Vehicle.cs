using System;

namespace CSharpOOP
{
        public abstract class Vehicle
        {
            public string Name { get; set; }
            public string Id { get; set; }
            // TODO: add IsRunning property
            public bool IsRunning { get; private set; }
            // TODO: Drive should check for IsRunning and set it to true/false
            public void Drive()
            {
                if (IsRunning)
                    Console.WriteLine("The engine is already running.");
                else
                    IsRunning = true;
            }
        }
}
