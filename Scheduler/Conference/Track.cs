using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Conference
{
    public class Track
    {
        public int Id;

        public Session MorningSession { get; set; }

        public Session Lunch { get; set; }

        public Session AfternoonSession { get; set; }

        public Session NetworkingSession { get; set; }

        internal bool HasEnoughTimeLeftFor(Talk talk)
        {
            return MorningSession.DurationLeft.TotalMinutes >= talk.Duration.TotalMinutes || AfternoonSession.DurationLeft.TotalMinutes >= talk.Duration.TotalMinutes;
        }
    }
}
