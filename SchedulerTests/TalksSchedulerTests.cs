using Scheduler.Conference;
using Scheduler.Conference.Parser;
using Scheduler.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SchedulerTests
{
    public class TalksSchedulerTests
    {
        [Fact(DisplayName = "Successfully schedules a talk into a track")]
        public void UnitTest_1()
        {
            TextParser parser = new TextParser();
            var talks = parser.Parse("input.txt");

            TalksScheduler scheduler = new TalksScheduler();
            var tracks = scheduler.Schedule(talks);

            Assert.NotEmpty(tracks);
            Assert.Equal(talks.Count(), (int)tracks.Sum(track => track.MorningSession.Talks.Count() + track.AfternoonSession.Talks.Count()));
        }
    }
}
