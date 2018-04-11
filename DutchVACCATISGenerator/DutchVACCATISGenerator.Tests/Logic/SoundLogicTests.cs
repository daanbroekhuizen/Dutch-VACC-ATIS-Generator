using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class SoundLogicTests
    {
        private const string ATISEHAM = @"C:\Users\Virtual\Documents\EuroScope\atis\atiseham.txt";

        private readonly ApplicationVariables applicationVariables;
        private readonly IATISLogic ATISLogic;
        private readonly ISoundLogic soundLogic;

        public SoundLogicTests()
        {
            applicationVariables = new ApplicationVariables();
            var METARLogic = new METARLogic();

            ATISLogic = new ATISLogic(applicationVariables, METARLogic);

            soundLogic = new SoundLogic();
        }

        [TestMethod]
        public void Build_IndividualMETAR_Passes()
        {
            //Arrange
            var METAR = new METAR(METARHelper.METAR);
            applicationVariables.SelectedAirport = METAR.ICAO;
            ATISLogic.SetPhoneticAlphabet(false, false, true, false);
            var eventTriggerd = false;

            ApplicationEvents.BuildAITSCompletedEvent += (sender, args) =>
            {
                eventTriggerd = true;
            };

            //Act
            var output = ATISLogic.Generate(METAR, "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false);
            soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();

            //Assert
            Assert.IsNotNull(output);
            Assert.AreNotEqual(output, "");
            Assert.IsTrue(eventTriggerd);
        }

        [TestMethod]
        public void Build_EindhovenMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHEH";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHEHMETARs.Take(100))
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHEHMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHEHMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }

        [TestMethod]
        public void Build_RotterdamMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHRD";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHRDMETARs.Take(100))
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHRDMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHRDMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }

        [TestMethod]
        public void Build_SchipholMETARs_Passes()
        {
            //Arrange
            applicationVariables.SelectedAirport = "EHAM";
            var ATISBuilds = new List<string>();
            var soundBuilds = new List<bool>();

            ApplicationEvents.BuildAITSCompletedEvent += (e, args) =>
            {
                soundBuilds.Add(true);
            };

            //Act
            foreach (var METAR in METARHelper.EHAMMETARs.Take(100))
            {
                ATISLogic.SetPhoneticAlphabet(false, false, true, false);
                ATISBuilds.Add(ATISLogic.Generate(new METAR(METAR), "18R", "24", true, true, "18C", "18L", "24", true, false, false, true, false));
                soundLogic.Build(ATISEHAM, applicationVariables.ATISSamples).Wait();
            }

            //Assert
            Assert.AreEqual(ATISBuilds.Count, METARHelper.EHAMMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, METARHelper.EHAMMETARs.Take(100).ToList().Count);
            Assert.AreEqual(soundBuilds.Count, ATISBuilds.Count);
        }

        [TestMethod]
        public void Play_StartsPlayback_IsTrue()
        {
            //Arrange
            bool playbackStarted = false;

            ApplicationEvents.PlaybackStartedEvent += (sender, args) =>
            {
                playbackStarted = true;
            };

            //Act
            soundLogic.Play(ATISEHAM);

            //Assert
            Assert.IsTrue(playbackStarted);
        }

        [TestMethod]
        public void Stop_StopsPlayback_IsTrue()
        {
            //Arrange
            bool playbackStopped = false;

            ApplicationEvents.PlaybackStoppedEvent += (sender, args) =>
            {
                playbackStopped = true;
            };

            //Act
            soundLogic.Play(ATISEHAM);
            soundLogic.Stop();

            //Assert
            Assert.IsTrue(playbackStopped);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Play_NonExisitingFile_ThrowsException()
        {
            //Act
            soundLogic.Play("NonExisitingPath");
        }

        [TestMethod]
        public void Play_IsPlaying_NoPlayback()
        {
            //Arrange
            bool playbackStarted = false;
            bool playbackStopped = false;

            ApplicationEvents.PlaybackStartedEvent += (sender, args) =>
            {
                playbackStarted = true;
            };

            ApplicationEvents.PlaybackStoppedEvent += (sender, args) =>
            {
                playbackStopped = true;
                playbackStarted = false;
            };

            //Act
            soundLogic.Play(ATISEHAM);
            soundLogic.Play(ATISEHAM);

            //Assert
            Assert.IsTrue(playbackStopped);
            Assert.IsFalse(playbackStarted);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Build_NonExisitingFile_ThrowsException()
        {
            //Act
            soundLogic.Build("NonExisitingPath", new List<string>());
        }
    }
}
