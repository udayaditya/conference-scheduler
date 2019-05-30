using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler.Conference;

namespace Scheduler.Scheduler
{
    public class TalksScheduler
    {
        private const int MAX_AVAILABLE_MINUTES_IN_A_TRACK = 420;

        public List<Track> Schedule(IEnumerable<Talk> talks)
        {
            var tracks = this.GetTracks(talks);

            foreach(var aTalk in talks)
            {
                var availableTrack = tracks.FirstOrDefault(track => track.HasEnoughTimeLeftFor(aTalk));
                if (object.ReferenceEquals(null, availableTrack))
                {
                    throw new Exception("No more room left for scheduling talks");
                }

                this.Add(aTalk, availableTrack);
            }

            return tracks;
        }

        private void Add(Talk aTalk, Track aTrack)
        {
            if (aTrack.MorningSession.DurationLeft.TotalMinutes >= aTalk.Duration.TotalMinutes)
            {
                aTrack.MorningSession.Add(aTalk);
                return;
            }

            if (aTrack.AfternoonSession.DurationLeft.TotalMinutes >= aTalk.Duration.TotalMinutes)
            {
                aTrack.AfternoonSession.Add(aTalk);
            }
        }

        private List<Track> GetTracks(IEnumerable<Talk> talks)
        {
            var tracksCount = (int)Math.Ceiling(talks.Sum(talk => talk.Duration.TotalMinutes) / MAX_AVAILABLE_MINUTES_IN_A_TRACK);
            var tracks = new List<Track>();

            for (var i=1; i<=tracksCount; i++)
            {
                tracks.Add(new Track
                {
                    Id = i,
                    MorningSession = new Session(9, 12),
                    Lunch = new Session(12, 13),
                    AfternoonSession = new Session(13, 17),
                    NetworkingSession = new Session(17, 18)
                });
            }

            return tracks;
        }
    }
}
