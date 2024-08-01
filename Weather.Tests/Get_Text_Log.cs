using NUnit.Framework;
using System;
using System.IO;
using Weather;

namespace TestProject_Weather
{
    [TestFixture]
    public class ConsoleLoggerTest
    {
        private ConsoleLogger _logger;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _logger = new ConsoleLogger();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter); 
        }

        [TearDown]
        public void Teardown()
        {
            _stringWriter.Dispose(); 
            Console.SetOut(Console.Out); 
        }

        [Test]
        public void Get_Text_Log()
        {
            var message = "Test message";
            var dateTimePattern = @"\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}";
            
            _logger.Log(message);
            
            var output = _stringWriter.ToString();
            Assert.That(output, Does.Match(dateTimePattern).And.Contain(message));
        }


    }
}