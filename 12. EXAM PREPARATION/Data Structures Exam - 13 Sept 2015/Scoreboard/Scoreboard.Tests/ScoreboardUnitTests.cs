﻿using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scoreboard.Tests
{
    [TestClass]
    public class UnitTestsScoreboard
    {
        private void ExecuteTest(string inputFileName, string outputFileName)
        {
            var commandExecutor = new CommandExecutor();

            var inputCommands = File.ReadAllLines(
                @"..\..\..\Judge-Tests\" + inputFileName);
            var output = new StringBuilder();

            var expectedOutput = File.ReadAllText(@"..\..\..\Judge-Tests\" + outputFileName);

            foreach (var command in inputCommands)
            {
                if (command == "End")
                {
                    break;
                }
                if (command != "")
                {
                    var commandOutput = commandExecutor.ProcessCommand(command);
                    output.AppendLine(commandOutput);
                }
            }
            var actualOutput = output.ToString();

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        [Timeout(200)]
        public void Test000_SampleInput()
        {
            this.ExecuteTest("test.000.001.in.txt", "test.000.001.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test001_RegisterUser()
        {
            this.ExecuteTest("test.001.in.txt", "test.001.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test002_RegisterGame()
        {
            this.ExecuteTest("test.002.in.txt", "test.002.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test003_RegisterUser_RegisterGame_AddScore_Simple()
        {
            this.ExecuteTest("test.003.in.txt", "test.003.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test004_RegisterUser_RegisterGame_AddScore_Complex()
        {
            this.ExecuteTest("test.004.in.txt", "test.004.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test005_ShowScoreboard_Very_Simple()
        {
            this.ExecuteTest("test.005.in.txt", "test.005.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test006_ShowScoreboard_Simple()
        {
            this.ExecuteTest("test.006.in.txt", "test.006.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test007_ShowScoreboard_Empty()
        {
            this.ExecuteTest("test.007.in.txt", "test.007.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test008_ShowScoreboard_All_Cases()
        {
            this.ExecuteTest("test.008.in.txt", "test.008.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test009_ListGamesByPrefix()
        {
            this.ExecuteTest("test.009.in.txt", "test.009.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test010_RegisterGame_DeleteGame()
        {
            this.ExecuteTest("test.010.in.txt", "test.010.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test011_AddScore_DeleteGame()
        {
            this.ExecuteTest("test.011.in.txt", "test.011.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test012_DeleteGame_WithInvalidCases()
        {
            this.ExecuteTest("test.012.in.txt", "test.012.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test013_ShowScoreboard_DeleteGame_Complex()
        {
            this.ExecuteTest("test.013.in.txt", "test.013.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test014_ListGamesByPrefix_DeleteGame()
        {
            this.ExecuteTest("test.014.in.txt", "test.014.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test015_All_Commands_All_Cases()
        {
            this.ExecuteTest("test.015.in.txt", "test.015.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test016_Performance_RegisterUser()
        {
            this.ExecuteTest("test.016.in.txt", "test.016.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test017_Performance_RegisterGame()
        {
            this.ExecuteTest("test.017.in.txt", "test.017.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test018_Performance_AddScore()
        {
            this.ExecuteTest("test.018.in.txt", "test.018.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test019_Performance_ShowScoreboard()
        {
            this.ExecuteTest("test.019.in.txt", "test.019.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test020_Performance_ListGamesByPrefix()
        {
            this.ExecuteTest("test.020.in.txt", "test.020.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test021_Performance_DeleteGame()
        {
            this.ExecuteTest("test.021.in.txt", "test.021.out.txt");
        }

        [TestMethod]
        [Timeout(200)]
        public void Test022_Performance_AllCommands()
        {
            this.ExecuteTest("test.022.in.txt", "test.022.out.txt");
        }
    }
}
