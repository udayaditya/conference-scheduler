using System;
using System.Collections.Generic;

namespace Scheduler.Conference
{
    public class Session
    {
        private DateTime _nextSessionAt;

        private readonly List<Talk> _talks = new List<Talk>();

        public Session(int startTime, int endTime)
        {
            if (startTime > endTime)
            {
                throw new Exception("Invalid session initialization");
            }

            var now = DateTime.Now;
            StartTime = new DateTime(now.Year, now.Month, now.Day, startTime, 0, 0);
            EndTime = StartTime.AddHours(endTime - startTime);

            _nextSessionAt = StartTime;
            DurationLeft = TimeSpan.FromMinutes((EndTime - StartTime).TotalMinutes);
        }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public TimeSpan DurationLeft { get; private set; }

        public IEnumerable<Talk> Talks { get { return this._talks; } }

        public void Add(Talk aTalk)
        {
            aTalk.StartTime = this._nextSessionAt;
            this._talks.Add(aTalk);

            this._nextSessionAt = this._nextSessionAt.Add(aTalk.Duration);
            this.UpdateDurationLeft(aTalk.Duration);
        }

        private void UpdateDurationLeft(TimeSpan aTalkDuration)
        {
            this.DurationLeft = TimeSpan.FromMinutes(this.DurationLeft.TotalMinutes - aTalkDuration.TotalMinutes);
        }
    }
}