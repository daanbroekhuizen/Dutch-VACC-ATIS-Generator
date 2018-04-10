using DutchVACCATISGenerator.Extensions;
using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class RunwayForm : Form
    {
        private readonly ApplicationVariables applicationVariables;
        private readonly IRunwayLogic runwayLogic;

        public RunwayForm(ApplicationVariables applicationVariables, IRunwayLogic runwayLogic)
        {
            this.applicationVariables = applicationVariables;
            this.runwayLogic = runwayLogic;

            InitializeComponent();

            //Register application events.
            ApplicationEvents.MainFormMovedEvent += MainFormMoved;
            ApplicationEvents.METARProcessedEvent += METARProcessed;

            SetVisibleDataGrid(this.applicationVariables.SelectedAirport);

            //Set runway friction combo box selection to first item.
            frictionComboBox.SelectedIndex = applicationVariables.FrictionIndex;
        }

        #region UI events
        private void Runway_Load(object sender, EventArgs e)
        {
            this.SetRelativeRight(this.applicationVariables.MainFormBounds);
        }

        private void FrictionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update friction index.
            applicationVariables.FrictionIndex = frictionComboBox.SelectedIndex;

            SetDataGrid();
        }
        #endregion

        #region Application events
        private void MainFormMoved(object sender, EventArgs e)
        {
            this.SetRelativeRight(this.applicationVariables.MainFormBounds);
        }

        private void METARProcessed(object sender, EventArgs e)
        {
            this.SetVisibleDataGrid(this.applicationVariables.SelectedAirport);
            this.SetDataGrid();
        }
        #endregion

        /// <summary>
        /// Sets runway info data grid to be visible depending on the selected ICAO tab.
        /// </summary>
        /// <param name="airport">ICAO selected airport</param>
        private void SetVisibleDataGrid(string airport)
        {
            switch (airport)
            {
                case "EHAM":
                    schipholDepartureDataGridView.Visible =
                        schipholLandingDataGridView.Visible =
                        schipholDepartureRunwaysGroupBox.Visible =
                        schipholLandingRunwaysGroupBox.Visible = true;

                    dataGridView.Visible = false;

                    //Set the form size to 414 by 645.
                    this.Size = new Size(414, 645);
                    break;

                case "EHBK":
                case "EHEH":
                case "EHGG":
                case "EHRD":
                    schipholDepartureDataGridView.Visible =
                        schipholLandingDataGridView.Visible =
                        schipholDepartureRunwaysGroupBox.Visible =
                        schipholLandingRunwaysGroupBox.Visible = false;

                    dataGridView.Visible = true;

                    //Set the form size to 414 by 373.
                    this.Size = new Size(414, 373);
                    break;
            }
        }

        /// <summary>
        /// Check which ICAO tab is selected in DutchVACCATISGenerator.
        /// </summary>
        public void SetDataGrid()
        {
            if (!(applicationVariables.SelectedAirport.Equals("EHAM")))
                SetDataGrid(applicationVariables.SelectedAirport);
            else
                SetSchipholDataGrids();
        }

        /// <summary>
        /// Selects a dictionary to process depending on the selected ICAO tab in DutchVACCATISGenerator.
        /// </summary>
        /// <param name="selectedAirport">ICAO tab selected.</param>
        private void SetDataGrid(string selectedAirport)
        {
            switch (selectedAirport)
            {
                case "EHBK":
                    SetDataGrid(Runways.Beek);
                    break;

                case "EHRD":
                    SetDataGrid(Runways.Rotterdam);
                    break;

                case "EHGG":
                    SetDataGrid(Runways.Eelde);
                    break;

                case "EHEH":
                    SetDataGrid(Runways.Eindhoven);
                    break;
            }
        }

        /// <summary>
        /// Fills the EHAM runway info data grids.
        /// </summary>
        private void SetSchipholDataGrids()
        {
            schipholLandingDataGridView.Rows.Clear();

            foreach (var pair in Runways.SchipholLanding)
                schipholLandingDataGridView.Rows.Add(GetDataRow(schipholLandingDataGridView, pair));

            schipholDepartureDataGridView.Rows.Clear();

            foreach (var pair in Runways.SchipholLanding)
                schipholDepartureDataGridView.Rows.Add(GetDataRow(schipholDepartureDataGridView, pair));

            AddToolTip();
        }

        /// <summary>
        /// Fills the runway info data grids.
        /// </summary>
        /// <param name="runways">Runways</param>
        private void SetDataGrid(Dictionary<string, Tuple<int, int, string>> runways)
        {
            dataGridView.Rows.Clear();

            foreach (var pair in runways)
                dataGridView.Rows.Add(GetDataRow(dataGridView, pair));
        }

        /// <summary>
        /// Get a data row for KeyValuePair<string, Tuple<int, int, string>> runway.
        /// </summary>
        /// <param name="dataGridView">Data grid view to add row to</param>
        /// <param name="runway">Runway KeyValuePair</param>
        /// <returns>DataGridViewRow to add data grid</returns>
        private DataGridViewRow GetDataRow(DataGridView dataGridView, KeyValuePair<string, Tuple<int, int, string>> runway)
        {
            var row = new DataGridViewRow();

            row.CreateCells(dataGridView);

            var crosswindComponent = runwayLogic.CalculateCrosswindComponent(runway.Value.Item1,
                applicationVariables.METAR.Wind.Heading,
                applicationVariables.METAR.Wind.Knots,
                applicationVariables.METAR.Wind.GustMin,
                applicationVariables.METAR.Wind.GustMax);

            var tailwindComponent = runwayLogic.CalculateTailwindComponent(runway.Value.Item2,
                applicationVariables.METAR.Wind.Heading,
                applicationVariables.METAR.Wind.Knots,
                applicationVariables.METAR.Wind.GustMin,
                applicationVariables.METAR.Wind.GustMax);

            row.Cells[0].Value = runway.Key;
            row.Cells[1].Value = crosswindComponent;
            row.Cells[2].Value = tailwindComponent * -1; //Q&D
            row.Cells[3].Value = runway.Value.Item3;
            row.Cells[4].Value = runwayLogic.RunwayComplies(applicationVariables.FrictionIndex,
                runway.Key,
                applicationVariables.METAR.RVR,
                applicationVariables.METAR.RVRValues,
                applicationVariables.METAR.Visibility,
                applicationVariables.METAR.Clouds,
                crosswindComponent,
                tailwindComponent);

            return row;
        }

        /// <summary>
        /// Get a data row for KeyValuePair<string, Tuple<int, int, string, string>> runway.
        /// </summary>
        /// <param name="dataGridView">Data grid view to add row to</param>
        /// <param name="runway">Runway KeyValuePair</param>
        /// <returns>DataGridViewRow to add data grid</returns>
        private DataGridViewRow GetDataRow(DataGridView dataGridView, KeyValuePair<string, Tuple<int, int, string, string>> runway)
        {
            var row = new DataGridViewRow();

            row.CreateCells(dataGridView);

            var crosswindComponent = runwayLogic.CalculateCrosswindComponent(runway.Value.Item1,
                applicationVariables.METAR.Wind.Heading,
                applicationVariables.METAR.Wind.Knots,
                applicationVariables.METAR.Wind.GustMin,
                applicationVariables.METAR.Wind.GustMax);

            var tailwindComponent = runwayLogic.CalculateTailwindComponent(runway.Value.Item2,
                applicationVariables.METAR.Wind.Heading,
                applicationVariables.METAR.Wind.Knots,
                applicationVariables.METAR.Wind.GustMin,
                applicationVariables.METAR.Wind.GustMax);

            row.Cells[0].Value = runway.Key;
            row.Cells[1].Value = crosswindComponent;
            row.Cells[2].Value = tailwindComponent * -1; //Q&D
            row.Cells[3].Value = runway.Value.Item3;
            row.Cells[4].Value = runway.Value.Item4;
            row.Cells[5].Value = runwayLogic.RunwayComplies(applicationVariables.FrictionIndex, 
                runway.Key, 
                applicationVariables.METAR.RVR,
                applicationVariables.METAR.RVRValues,
                applicationVariables.METAR.Visibility,
                applicationVariables.METAR.Clouds,
                crosswindComponent, 
                tailwindComponent);

            return row;
        }

        /// <summary>
        /// Adds a tool tip to the EHAM runway info grids if the value of a cell is --.
        /// </summary>
        private void AddToolTip()
        {
            //Add tool tip to EHAM landing- runway info DataGridView.
            foreach (DataGridViewRow row in schipholLandingDataGridView.Rows)
            {
                if (row.Cells[3].FormattedValue.Equals("--"))
                    row.Cells[3].ToolTipText = "-- = Not allowed during night hours.";

                if (row.Cells[4].FormattedValue.Equals("--"))
                    row.Cells[4].ToolTipText = "-- = Not allowed during night hours.";
            }

            //Add tool tip to EHAM departure runway info DataGridView.
            foreach (DataGridViewRow row in schipholDepartureDataGridView.Rows)
            {
                if (row.Cells[3].FormattedValue.Equals("--"))
                    row.Cells[3].ToolTipText = "-- = Not allowed during night hours.";

                if (row.Cells[4].FormattedValue.Equals("--"))
                    row.Cells[4].ToolTipText = "-- = Not allowed during night hours.";
            }
        }
    }
}
