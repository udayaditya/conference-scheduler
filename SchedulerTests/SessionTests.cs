using Scheduler.Conference;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SchedulerTests
{
    public class SessionTests
    {
        [Fact(DisplayName = "Throws exception if start time exceeds end time")]
        public void UnitTest_1()
        {
            Assert.Throws<Exception>(() => { Session session = new Session(14, 12); });
        }

        [Fact(DisplayName = "Initialises StartTime, EndTime, DurationLeft properties to expected values")]
        public void UnitTest_2()
        {
            Session session = new Session(9, 12);

            Assert.True(session.StartTime.Equals(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0)));
            Assert.True(session.EndTime.Equals(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)));
            Assert.Equal(180, session.DurationLeft.TotalMinutes);
        }

        [Fact(DisplayName = "Successfully adds a talk to a session, sets a start time on talk")]
        public void UnitTest_3()
        {
            var aTalk = new Talk("SampleTalk", TimeSpan.FromMinutes(30));
            var talkStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

            Session session = new Session(9, 12);
            session.Add(aTalk);

            Assert.NotEmpty(session.Talks);
            Assert.True(talkStartTime.Equals(aTalk.StartTime));
        }
    }
}
