using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Conference
{
    public class Talk
    {
        public DateTime StartTime;

        public Talk(string name, TimeSpan duration)
        {
            this.Name = name;
            this.Duration = duration;
        }

        public string Name { get; }
        public TimeSpan Duration { get; }
    }
}
