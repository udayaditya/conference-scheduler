using Scheduler.Conference.Parser.CustomException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Scheduler.Conference;
using System.Linq;

namespace Scheduler.Conference.Parser
{
    public class TextParser
    {
        // private const string REGEX_RECOGNIZE_A_TALK = @"^([a-zA-Z\s]+)(\d+min|lightning)+$";
        // private const string REGEX_RECOGNIZE_A_TALK = @"^([a-zA-Z\s]+)((\d+)min|lightning)+$";
        private const string REGEX_RECOGNIZE_A_TALK = @"^([-.:()a-zA-Z\s]+)((\d+)min|lightning)+$";
        private const string LIGHTNING_DURATION_CODE = "lightning";

        public IEnumerable<Talk> Parse(string fileName)
        {
            var talks = new List<Talk>();
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("Invalid file name input");
            }

            foreach(var aTalkInputLineToParse in File.ReadAllLines(fileName))
            {
                if (!Regex.IsMatch(aTalkInputLineToParse, REGEX_RECOGNIZE_A_TALK))
                {
                    throw new InvalidParserInputException();
                }

                var talkAttributes = Regex.Match(aTalkInputLineToParse, REGEX_RECOGNIZE_A_TALK).Groups;
                talks.Add(new Talk(name: talkAttributes[1].ToString(), duration: this.ExtractTimeDuration(talkAttributes)));
            }

            return talks;
        }

        private TimeSpan ExtractTimeDuration(GroupCollection talkAttributes)
        {
            return talkAttributes[2].ToString().Equals(LIGHTNING_DURATION_CODE, StringComparison.OrdinalIgnoreCase)
                ? TimeSpan.FromMinutes(5)
                : TimeSpan.FromMinutes(Convert.ToInt16(talkAttributes[3].ToString()));
        }
    }
}
