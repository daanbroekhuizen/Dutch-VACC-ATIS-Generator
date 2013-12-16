using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// RunwayInfo class.
    /// </summary>
    public partial class RunwayInfo : Form
    {
        private DutchVACCATISGenerator dutchVACCATISGenerator { get; set; }
        public Metar metar { get; set; }

        //Tuple<RunwayHeading, OpositeRunwayHeading, Day Preference, Night Preference>
        private Dictionary<String, Tuple<int, int, String, String>> EHAMlandingRunways = new Dictionary<String, Tuple<int, int, String, String>>()
        {
            {"04", new Tuple<int, int, String, String>(041, 221,  "9" , "--")},
            {"06", new Tuple<int, int, String, String>(058, 238, "1", "1")},
            {"09", new Tuple<int, int, String, String>(087, 267, "8", "--")},
            {"18C", new Tuple<int, int, String, String>(183, 003, "4", "--")},
            {"18R", new Tuple<int, int, String, String>(183, 003, "2", "2")},
            {"22", new Tuple<int, int, String, String>(221, 041, "7", "--")},
            {"24", new Tuple<int, int, String, String>(238, 058, "7", "--")},
            {"27", new Tuple<int, int, String, String>(267, 087, "6", "--")},
            {"36C", new Tuple<int, int, String, String>(003, 183, "5", "3")},
            {"36R", new Tuple<int, int, String, String>(003, 183, "3", "--")}
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Day Preference, Night Preference>
        private Dictionary<String, Tuple<int, int, String, String>> EHAMdepartureRunways = new Dictionary<String, Tuple<int, int, String, String>>()
        {
            {"04", new Tuple<int, int, String, String>(041, 221, "10", "--")},
            {"06", new Tuple<int, int, String, String>(058, 238, "8", "4")},
            {"09", new Tuple<int, int, String, String>(087, 267, "6", "--")},
            {"18L", new Tuple<int, int, String, String>(183, 003, "4", "--")},
            {"18C", new Tuple<int, int, String, String>(183, 003, "5", "3")},
            {"22", new Tuple<int, int, String, String>(221, 041, "9", "--")},
            {"24", new Tuple<int, int, String, String>(238, 058, "2", "2")},
            {"27", new Tuple<int, int, String, String>(267, 087, "7", "--")},
            {"36L", new Tuple<int, int, String, String>(003, 183, "1", "1")},
            {"36C", new Tuple<int, int, String, String>(003, 183, "3", "--")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        private Dictionary<String, Tuple<int, int, String>> EHBKRunways = new Dictionary<String, Tuple<int, int, String>>()
        {
            {"03", new Tuple<int, int, String>(032, 212, "2")},
            {"21", new Tuple<int, int, String>(212, 032, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        private Dictionary<String, Tuple<int, int, String>> EHRDRunways = new Dictionary<String, Tuple<int, int, String>>()
        {
            {"06", new Tuple<int, int, String>(057, 257, "2")},
            {"24", new Tuple<int, int, String>(237, 037, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        private Dictionary<String, Tuple<int, int, String>> EHEHRunways = new Dictionary<String, Tuple<int, int, String>>()
        {
            {"03", new Tuple<int, int, String>(034, 214, "2")},
            {"21", new Tuple<int, int, String>(214, 034, "1")},
        };

        //Tuple<RunwayHeading, OpositeRunwayHeading, Preference>
        private Dictionary<String, Tuple<int, int, String>> EHGGRunways = new Dictionary<String, Tuple<int, int, String>>()
        {
            {"01", new Tuple<int, int, String>(008, 214, "4")},
            {"05", new Tuple<int, int, String>(051, 231, "2")},
            {"19", new Tuple<int, int, String>(188, 008, "3")},
            {"23", new Tuple<int, int, String>(231, 051, "1")},
        };

        /// <summary>
        /// Constructor of RunwayInfo class. Initializes new instance of RunwayInfo.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        /// <param name="metar">METAR processed in DutchVACCATISGenerator.</param>
        public RunwayInfo(DutchVACCATISGenerator dutchVACCATISGenerator, Metar metar)
        {
            InitializeComponent();
           
            this.dutchVACCATISGenerator = dutchVACCATISGenerator;
            this.metar = metar;

            setVisibleRunwayInfoDataGrid(this.dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Text);
            
            runwayFrictionComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets runway info data grid to be visible depending on the selected ICAO tab in DutchVACCATISGenerator.
        /// </summary>
        /// <param name="icaoTab">ICAO tab selected.</param>
        public void setVisibleRunwayInfoDataGrid(String icaoTab)
        {
            switch (icaoTab)
            {
                case "EHAM":
                    EHAMdepartureRunwayInfoDataGridView.Visible = EHAMlandingRunwayInfoDataGridView.Visible = EHAMdepartureRunwaysGroupBox.Visible = EHAMLandingRunwaysGroupBox.Visible = true;
                    runwayInfoDataGridView.Visible = false;
                    this.Size = new Size(414, 645);                    
                    break;

                case "EHBK": case "EHRD": case "EHGG": case "EHEH":
                    EHAMdepartureRunwayInfoDataGridView.Visible = EHAMlandingRunwayInfoDataGridView.Visible = EHAMdepartureRunwaysGroupBox.Visible = EHAMLandingRunwaysGroupBox.Visible = false;
                    runwayInfoDataGridView.Visible = true;
                    this.Size = new Size(414, 373);
                    break;
            }
        }

        /// <summary>
        /// Selects a dictionary to process depending on the selected ICAO tab in DutchVACCATISGenerator.
        /// </summary>
        /// <param name="icaoTab">ICAO tab selected.</param>
        private void ICAODirectoryToProcess(String icaoTab)
        {
            switch (icaoTab)
            {
                case "EHBK":
                    fillRunwayInfoDataGrid(EHBKRunways);
                    break;

                case "EHRD":
                    fillRunwayInfoDataGrid(EHRDRunways);
                    break;

                case "EHGG":
                    fillRunwayInfoDataGrid(EHGGRunways);
                    break;

                case "EHEH":
                    fillRunwayInfoDataGrid(EHEHRunways);
                    break;
            }
        }

        /// <summary>
        /// Fills the EHAM runway info data grids.
        /// </summary>
        public void fillEHAMRunwayInfoDataGrids()
        {
            EHAMlandingRunwayInfoDataGridView.Rows.Clear();

            foreach (KeyValuePair<string, Tuple<int, int, String, String>> pair in EHAMlandingRunways)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(EHAMlandingRunwayInfoDataGridView);
                row.Cells[0].Value = pair.Key;
                row.Cells[1].Value = calculateCrosswindComponent(pair.Value.Item1);
                row.Cells[2].Value = calculateTailwindComponent(pair.Value.Item2);
                row.Cells[3].Value = pair.Value.Item3;
                row.Cells[4].Value = pair.Value.Item4;
                row.Cells[5].Value = checkRunwayComply(pair.Key, calculateCrosswindComponent(pair.Value.Item1), calculateTailwindComponent(pair.Value.Item2));

                EHAMlandingRunwayInfoDataGridView.Rows.Add(row);
            }

            EHAMdepartureRunwayInfoDataGridView.Rows.Clear();

            foreach (KeyValuePair<string, Tuple<int, int, String, String>> pair in EHAMdepartureRunways)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(EHAMdepartureRunwayInfoDataGridView);
                row.Cells[0].Value = pair.Key;
                row.Cells[1].Value = calculateCrosswindComponent(pair.Value.Item1);
                row.Cells[2].Value = calculateTailwindComponent(pair.Value.Item2);
                row.Cells[3].Value = pair.Value.Item3;
                row.Cells[4].Value = pair.Value.Item4;
                row.Cells[5].Value = checkRunwayComply(pair.Key, calculateCrosswindComponent(pair.Value.Item1), calculateTailwindComponent(pair.Value.Item2));

                EHAMdepartureRunwayInfoDataGridView.Rows.Add(row);
            }

            addToolTipToEHAMRunwayInfoGridViews();
        }

        /// <summary>
        /// Fills the runway info data grids.
        /// </summary>
        /// <param name="runways">Dictionary to process.</param>
        public void fillRunwayInfoDataGrid(Dictionary<String, Tuple<int, int, String>> runways)
        {
            runwayInfoDataGridView.Rows.Clear();

            foreach (KeyValuePair<string, Tuple<int, int, String>> pair in runways)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(runwayInfoDataGridView);
                row.Cells[0].Value = pair.Key;
                row.Cells[1].Value = calculateCrosswindComponent(pair.Value.Item1);
                row.Cells[2].Value = calculateTailwindComponent(pair.Value.Item2);
                row.Cells[3].Value = pair.Value.Item3;
                row.Cells[4].Value = checkRunwayComply(pair.Key, calculateCrosswindComponent(pair.Value.Item1), calculateTailwindComponent(pair.Value.Item2));

                runwayInfoDataGridView.Rows.Add(row);
            }
        }

        /// <summary>
        /// Adds a tool tip to the EHAM runway info grids if the value of a cell is --.
        /// </summary>
        private void addToolTipToEHAMRunwayInfoGridViews()
        {
            foreach (DataGridViewRow row in EHAMlandingRunwayInfoDataGridView.Rows)
            {
                if(row.Cells[3].FormattedValue.Equals("--")) row.Cells[3].ToolTipText = "-- = Not allowed during night hours.";
                if (row.Cells[4].FormattedValue.Equals("--")) row.Cells[4].ToolTipText = "-- = Not allowed during night hours.";
            }

            foreach (DataGridViewRow row in EHAMdepartureRunwayInfoDataGridView.Rows)
            {
                if (row.Cells[3].FormattedValue.Equals("--")) row.Cells[3].ToolTipText = "-- = Not allowed during night hours.";
                if (row.Cells[4].FormattedValue.Equals("--")) row.Cells[4].ToolTipText = "-- = Not allowed during night hours.";
            }
        }

        /// <summary>
        /// Calculate the tail wind component of a runway.
        /// </summary>
        /// <param name="runwayHeading">Opposite runway heading.</param>
        /// <returns>Calculated tail wind component of a runway.</returns>
        private int calculateTailwindComponent(int runwayHeading)
        {
            if(metar.Wind.windGustMin != null)
            {
                if (Math.Abs(Convert.ToInt32(metar.Wind.windGustMax) - Convert.ToInt32(metar.Wind.windGustMin)) >= 10) return Convert.ToInt32(Math.Cos(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windGustMax));
                else return Convert.ToInt32(Math.Cos(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windGustMin)); 
            }
            else return Convert.ToInt32(Math.Cos(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windKnots));
        }

        /// <summary>
        /// Calculate the cross wind component of a runway.
        /// </summary>
        /// <param name="runwayHeading">Runway heading.</param>
        /// <returns>Calculated cross wind component of a runway.</returns>
        private int calculateCrosswindComponent(int runwayHeading)
        {
            int crosswind;

            if(metar.Wind.windGustMin != null)
            {
                if (Math.Abs(Convert.ToInt32(metar.Wind.windGustMax) - Convert.ToInt32(metar.Wind.windGustMin)) >= 10) crosswind = Convert.ToInt32(Math.Sin(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windGustMax));
                else crosswind = Convert.ToInt32(Math.Sin(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windGustMin));
            }
            else crosswind = Convert.ToInt32(Math.Sin(degreeToRadian(Math.Abs(Convert.ToInt32(metar.Wind.windHeading) - runwayHeading))) * Convert.ToInt32(metar.Wind.windKnots));

            if (crosswind < -0) return crosswind * -1;
            else return crosswind;
        }

        /// <summary>
        /// Convert degree to radian.
        /// </summary>
        /// <param name="input">Angle in degree.</param>
        /// <returns>Angle in radian.</returns>
        private double degreeToRadian(int input)
        {
            return input * (Math.PI / 180);
        }

        /// <summary>
        /// Set RunwayInform position relative to DutchVACCATISGenerator.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public void showRelativeToDutchVACCATISGenerator(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            this.Left = dutchVACCATISGenerator.Right;
            this.Top = dutchVACCATISGenerator.Top;
            this.Refresh();
        }
  
        /// <summary>
        /// Check if a runway complies with weather criteria for a runway.
        /// </summary>
        /// <param name="crosswind">Runway cross wind component.</param>
        /// <param name="tailwind">Runway tail wind component.</param>
        /// <returns>Indication if the a runway complies with the weather criteria for that runway.</returns>
        private String checkRunwayComply(String rwy, int crosswind, int tailwind)
        {
            switch(runwayFrictionComboBox.SelectedIndex)
            {
                case 0:
                    return checkRunwayVisbility(rwy) ? checkRunwayComplyWithWind(20, 7, crosswind, tailwind) : checkRunwayComplyWithWind(15, 7, crosswind, tailwind);

                case 1: case 2:
                    return checkRunwayVisbility(rwy) ? checkRunwayComplyWithWind(10, 0, crosswind, tailwind) : checkRunwayComplyWithWind(10, 0, crosswind, tailwind);

                case 3: case 4:
                    return checkRunwayVisbility(rwy) ? checkRunwayComplyWithWind(5, 0, crosswind, tailwind) : checkRunwayComplyWithWind(5, 0, crosswind, tailwind);

                default: return "Error";
            }
        }

        /// <summary>
        /// Check if a runway visibility complies with the viability criteria for that runway.
        /// </summary>
        /// <returns>Boolean indicating if the runway visibility complies with the viability criteria for that runway.</returns>
        private Boolean checkRunwayVisbility(string rwy)
        {
            if(metar.RVR)
            {
                foreach (KeyValuePair<String, int> pair in metar.RVRValues)
                {
                    if (pair.Key.Equals(rwy)) return runwayCompliesWithRVR(pair.Value);
                }

                return runwayCompliesWithRVR(metar.Visibility);
            }
            else
            {
                if (metar.Clouds.Count != 0 ? metar.Clouds.First().altitude >= 2 : true) return true;
                else return false;
            }
        }

        /// <summary>
        /// Check if a runway visibility complies with the RVR criteria for that runway.
        /// </summary>
        /// <param name="rvr">RVR to check to.</param>
        /// <returns>Boolean indicating if the runway visibility complies with the RVR criteria for that runway.</returns>
        private Boolean runwayCompliesWithRVR(int rvr)
        {
            if (rvr >= 550 && (metar.Clouds.Count != 0 ? metar.Clouds.First().altitude >= 2 : true)) return true;
            else if (rvr < 550 || (metar.Clouds.Count != 0 && metar.Clouds.First().altitude < 2)) return false;
            else return false;
        }

        /// <summary>
        /// Check if a runway wind complies with the wind criteria for that runway.
        /// </summary>
        /// <param name="maxCrosswind">Maximum crosswind component.</param>
        /// <param name="maxTailWind">Maximum tailwind component.</param>
        /// <param name="crosswind">Actual crosswind component.</param>
        /// <param name="tailwind">Actual tailwind component.</param>
        /// <returns>Indication if the a runway complies with the weather criteria for that runway.</returns>
        private String checkRunwayComplyWithWind(int maxCrosswind, int maxTailWind, int crosswind, int tailwind)
        {
            if (crosswind <= maxCrosswind && tailwind <= maxTailWind) return "OK";

            else return "-";
        }

        /// <summary>
        /// Method called if runway info form is loaded.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void RunwayInfo_Load(object sender, EventArgs e)
        {
            showRelativeToDutchVACCATISGenerator(dutchVACCATISGenerator);
        }
        
        /// <summary>
        /// Method called if the selected index of runway friction combo box is changed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void runwayFrictionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkICAOTabSelected();
        }

        /// <summary>
        /// Check which ICAO tab is selected in DutchVACCATISGenerator.
        /// </summary>
        public void checkICAOTabSelected()
        {
            if(!(metar.ICAO.Equals(dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Text)))
            {
                MessageBox.Show(String.Format("Last processed METAR ICAO does not match the selected ICAO tab.\nRunway criteria will be calculated of the wrong METAR ({0})!", metar.ICAO), "Warning");
            }

            if (!(dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Text.Equals("EHAM"))) ICAODirectoryToProcess(dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Text);

            else fillEHAMRunwayInfoDataGrids();
        }

        /// <summary>
        /// Method called if runway info form is closed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void RunwayInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dutchVACCATISGenerator.runwayInfoButton.Text.Equals("<")) dutchVACCATISGenerator.runwayInfoButton.Text = ">";
        }
    }
}
