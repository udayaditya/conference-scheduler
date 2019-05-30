using Scheduler.Conference.Parser;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace SchedulerTests
{
    public class TextParserTests
    {
        [Fact(DisplayName = "Throws exception if empty file name is input")]
        public void UnitTest_1()
        {
            var parser = new TextParser();
            Assert.Throws<Exception>(() => parser.Parse(""));
        }

        [Fact(DisplayName = "Throws file not found exception")]
        public void UnitTest_2()
        {
            var parser = new TextParser();
            Assert.Throws<FileNotFoundException>(() => parser.Parse("sample"));
        }

        [Fact(DisplayName = "Successfully parses lines to talks from input text file")]
        public void UnitTest_3()
        {
            var parser = new TextParser();
            var talks = parser.Parse("input.txt");

            Assert.NotEmpty(talks);
            Assert.Equal<int>(19, talks.Count());
        }
    }
}
