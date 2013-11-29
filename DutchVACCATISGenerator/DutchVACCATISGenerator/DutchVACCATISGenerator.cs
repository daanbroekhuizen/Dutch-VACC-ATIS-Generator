using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DutchVACCATISGenerator
{
    public partial class DutchVACCATISGenerator : Form
    {
        private String metar { get; set; }

        private MetarProcessor metarProcessor { get; set; }

        private List<String> phoneticAlphabet { get; set; }

        private int atisIndex { get; set; }

        //private DataTable dataTable;
        
        public DutchVACCATISGenerator()
        {
            InitializeComponent();

            metar = String.Empty;

            phoneticAlphabet = new List<String> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            buildDataTable();

            atisIndex = 25;

            atisLetterLabel.Text = phoneticAlphabet[0];

            //TransitionLevel tl = new TransitionLevel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getMetarButton_Click(object sender, EventArgs e)
        {
            String _ICAO = icaoTextBox.Text;

            getMetarButton.Enabled = false;

            metarBackgroundWorker.RunWorkerAsync(_ICAO);
        }

        /// <summary>
        /// 
        /// </summary>
        private void buildDataTable()
        {
            dataGridView1.Rows.Add("04", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("06", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("09", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("18L", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("18C", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("18R", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("22", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("24", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("27", "..", "..", "..", "..", "..");      
            dataGridView1.Rows.Add("36L", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("36C", "..", "..", "..", "..", "..");
            dataGridView1.Rows.Add("36R", "..", "..", "..", "..", "..");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metarBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WebRequest request = WebRequest.Create("http://metar.vatsim.net/metar.php?id=" + e.Argument);
            WebResponse response = request.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());

            metar = reader.ReadToEnd();
            metar = metar.Remove(metar.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metarBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metarTextBox.Text = metar;

            getMetarButton.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void processMetarButton_Click(object sender, EventArgs e)
        {
            if (metar.Equals(String.Empty) && metarTextBox.Text.Equals(String.Empty))
            {
                MessageBox.Show("No metar fetched or entered.", "Error"); return;
            }
            else metar = metarTextBox.Text;
              
            metarProcessor = new MetarProcessor(metar.Split(' '));

            if (metarProcessor.parseComplete)
            {
                if (metar.Length > 64) lastLabel.Text = "Last successful processed metar:\n" + metar.Substring(0, 64) + "\n" + metar.Substring(64);
                else lastLabel.Text = "Last successful processed metar:\n" + metar;

                if (atisIndex == 25) atisIndex = 0;
                else atisIndex++;

                atisLetterLabel.Text = phoneticAlphabet[atisIndex];

                generateATISButton.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousATISLetterButton_Click(object sender, EventArgs e)
        {
            if(atisIndex == 0) atisIndex = 25;
            else atisIndex--;
            
            atisLetterLabel.Text = phoneticAlphabet[atisIndex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextATISLetterButton_Click(object sender, EventArgs e)
        {
            if(atisIndex == 25) atisIndex = 0;
            else atisIndex++;
            
            atisLetterLabel.Text = phoneticAlphabet[atisIndex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainLandingRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(mainLandingRunwayCheckBox.Checked) mainLandingRunwayComboBox.Enabled = true;
            else mainLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secondaryLandingRunway_CheckedChanged(object sender, EventArgs e)
        {
            if (secondaryLandingRunwayCheckBox.Checked) secondaryLandingRunwayComboBox.Enabled = true;
            else secondaryLandingRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mainDepartureRunwayCheckBox.Checked) mainDepartureRunwayComboBox.Enabled = true;
            else mainDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secondaryDepartureRunwayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (secondaryDepartureRunwayCheckBox.Checked) secondaryDepartureRunwayComboBox.Enabled = true;
            else secondaryDepartureRunwayComboBox.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private String getRunwayMarker(String input)
        {
            switch (input)
            {
                case "L":
                    return "[left]";

                case "C":
                    return "[center]";

                case "R":
                    return "[right]";
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateATISButton_Click(object sender, EventArgs e)
        {
            //generateATISButton.Enabled = false;

            String output = String.Empty;

            #region ICAO
            switch(metarProcessor.metar.ICAO)
            {
                case "EHAM":
                    output += "[ehamatis]";
                    break;

                case "EHRD":

                    break;

            }
            #endregion

            #region ATIS LETTER
            //ATIS LETTER
            output += phoneticAlphabet[atisIndex];
            output += "[pause]";
            #endregion

            #region MAIN LANDING RUNWAY
            if (mainLandingRunwayCheckBox.Checked)
            {
                output += "[mlrwy]";

                if(mainLandingRunwayComboBox.SelectedItem.ToString().Length > 2)
                {
                    output += mainLandingRunwayComboBox.SelectedItem.ToString().Substring(0, 2);
                    output += getRunwayMarker(mainLandingRunwayComboBox.SelectedItem.ToString().Substring(2));
                }
                else output += mainLandingRunwayComboBox.SelectedItem.ToString();
            }
            #endregion

            #region SECONDARY LANDING RUNWAY
            if (secondaryLandingRunwayCheckBox.Checked)
            {
                output += "[slrwy]";

                if (secondaryLandingRunwayComboBox.SelectedItem.ToString().Length > 2)
                {
                    output += secondaryLandingRunwayComboBox.SelectedItem.ToString().Substring(0, 2);
                    output += getRunwayMarker(secondaryLandingRunwayComboBox.SelectedItem.ToString().Substring(2));
                }
                else output += mainLandingRunwayComboBox.SelectedItem.ToString();
            }
            #endregion

            #region MAIN DEPARTURE RUNWAY
            if (mainDepartureRunwayCheckBox.Checked)
            {
                output += "[mtrwy]";

                if (mainDepartureRunwayComboBox.SelectedItem.ToString().Length > 2)
                {
                    output += mainDepartureRunwayComboBox.SelectedItem.ToString().Substring(0, 2);
                    output += getRunwayMarker(mainDepartureRunwayComboBox.SelectedItem.ToString().Substring(2));
                }
                else output += mainDepartureRunwayComboBox.SelectedItem.ToString();
            }
            #endregion 

            #region SECONDARY DEPARTURE RUNWAY
            if (secondaryDepartureRunwayCheckBox.Checked)
            {
                output += "[strwy]";

                if (secondaryDepartureRunwayComboBox.SelectedItem.ToString().Length > 2)
                {
                    output += secondaryDepartureRunwayComboBox.SelectedItem.ToString().Substring(0, 2);
                    output += getRunwayMarker(secondaryDepartureRunwayComboBox.SelectedItem.ToString().Substring(2));
                }
                else output += secondaryDepartureRunwayComboBox.SelectedItem.ToString();
            }
            #endregion


            //TL
            output += "[trl]";

            
            output += "[pause]";

            #region WIND
            if (metarProcessor.metar.winds.First().Value.VRB) output += "[vrb]" + metarProcessor.metar.winds.First().Value.windKnots + "[kt]";
            else if (metarProcessor.metar.winds.First().Value.windGustMin != null) output += metarProcessor.metar.winds.First().Value.windHeading + "[deg]" + metarProcessor.metar.winds.First().Value.windGustMin + "[max]" + metarProcessor.metar.winds.First().Value.windGustMax + "[kt]";
            else output += metarProcessor.metar.winds.First().Value.windHeading + "[deg]" + metarProcessor.metar.winds.First().Value.windKnots + "[kt]";

            /*Variable wind*/
            if (metarProcessor.metar.winds.First().Value.windVariableLeft != null) output += "[vrbbtn]" + metarProcessor.metar.winds.First().Value.windVariableLeft + "[and]" + metarProcessor.metar.winds.First().Value.windVariableRight + "[deg]";
            #endregion

            if (metarProcessor.metar.CAVOK) output += "[cavok]";
 
            #region VISIBILITY          
            if(metarProcessor.metar.Visibility.Count > 0 && metarProcessor.metar.Visibility.First().Value > 0)
            {
                output += "[vis]";

                if(metarProcessor.metar.Visibility.First().Value < 1000)   output += "1";
                else if (metarProcessor.metar.Visibility.First().Value >= 9999) output += "10";
                else output += Convert.ToString(metarProcessor.metar.Visibility.First().Value / 1000);
               
                output += "[km]" ;
            }
            #endregion
            
            //TODO 4 CHAR OPTIONS
            #region PHENOMENA
            if(metarProcessor.metar.phenomena.Count > 0)
            {
                int _index = metarProcessor.metar.phenomena.Keys.ElementAt(0);

                foreach (KeyValuePair<int, MetarPhenoma> pair in metarProcessor.metar.phenomena)
                {
                    if (Math.Abs(_index - pair.Key) == 0)
                    {
                        if (pair.Value.hasIntensity)
                        {
                            output += "[-]";

                            if (pair.Value.phenoma.Count() > 2) output += "[" + pair.Value.phenoma.Substring(0, 2).ToLower() + "][" + pair.Value.phenoma.Substring(2).ToLower() + "]";
                            else output += "[" + pair.Value.phenoma.ToLower() + "]";

                        }
                        else output += "[" + pair.Value.phenoma.ToLower() + "]";
                    }

                    _index++;
                }
            }
            #endregion

            #region CLOUDS OPTIONS
            if (metarProcessor.metar.SKC) output += "[skc]";
            if (metarProcessor.metar.NSC) output += "[nsc]";
            #endregion

            #region CLOUDS
            if (metarProcessor.metar.clouds.Count > 0)
            {
                int _index = metarProcessor.metar.clouds.Keys.ElementAt(0);

                foreach (KeyValuePair<int, MetarCloud> pair in metarProcessor.metar.clouds)
                {
                    if(Math.Abs(_index - pair.Key) == 0)
                    {
                        output += "[" + pair.Value.cloudType.ToLower() + "]";

                        if (pair.Value.altitude / 100 > 0) output += "1" + pair.Value.altitude / 10 + "[thousand]";
                        
                        if (pair.Value.altitude / 10 > 0) output += pair.Value.altitude / 10 + "[thousand]";
                        
                        if (pair.Value.altitude % 10 > 0) output += pair.Value.altitude % 10 + "[hundred]";
                        
                        output += "[ft]";

                        if (pair.Value.addition != null) output += "[" + pair.Value.addition.ToLower() + "]";
                    }

                    _index++;
                }
            }
            #endregion

            #region Vertical visibility
            if(metarProcessor.metar.verticalVisibility != null)
            {
                output += "[vv]";
                
                int visibility = Convert.ToInt32(metarProcessor.metar.verticalVisibility.Substring(2));

                if(visibility / 100 > 0)
                {
                    output += "1" + metarProcessor.metar.verticalVisibility.Substring(2, 1) + "[thousand]";
                }

                
                //+ Convert.ToInt32(metarProcessor.metar.verticalVisibility.Substring(3))) + 
            }
            #endregion

            #region TEMPERATURE
            output += "[temp]";
            if (metarProcessor.metar.Temperature.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Temperature.ToString().Substring(1,2));
            else output += Convert.ToInt32(metarProcessor.metar.Temperature.ToString());
            #endregion

            #region DEWPOINT
            //DEWPOINT
            output += "[dp]";
            if (metarProcessor.metar.Dewpoint.StartsWith("M")) output += "[minus]" + Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString().Substring(1,2));
            else output += Convert.ToInt32(metarProcessor.metar.Dewpoint.ToString());
            #endregion

            #region QNH
            output += "[qnh]";
            output += metarProcessor.metar.QNH.ToString();
            output += "[hpa]";
            #endregion

            #region NOSIG
            if (metarProcessor.metar.NOSIG) output += "[nosig]";
            #endregion

            #region TEMPO || BECMG
            if (metarProcessor.metar.TEMPO || metarProcessor.metar.BECMG)
            {
                if (metarProcessor.metar.TEMPO && metarProcessor.metar.BECMG)
                {
                    if(metarProcessor.metar.trendTEMPOPosition < metarProcessor.metar.trendBECMGPosition)
                    {
                        
                    }
                    else
                    {

                    }
                }
                else if (metarProcessor.metar.TEMPO)
                {
                    output += "[tempo]";

                    int _index = 0;

                    foreach(KeyValuePair<int, MetarWind> metarWind in metarProcessor.metar.winds)
                    {
                        if(metarWind.Key == metarProcessor.metar.trendTEMPOPosition)
                        {
                            _index = metarWind.Key;
                        }
                    }

                }
                else
                {

                }
            }
            #endregion

            #region END
            output += "[end]";
            output += phoneticAlphabet[atisIndex];
            #endregion


            //Clipboard.SetText(output);


            System.Diagnostics.Debug.WriteLine(output);

               //[ehamatis]C[pause][mlrwy][trl]50[pause]180[deg]10[kt][vrbbtn]150[and]210[deg][cavok][few]2[thousand]7[hundred][ft][cb][bkn]4[thousand]4[hundred][ft][tcu][temp]7[dp]5[qnh]995[hpa][nosig][end]C

            //[trl]45    [pause]180[deg]9[kt][vrbbtn]150[and]210[deg]   [vis]10[km]    [few]9[hundred][ft][sct]1[thousand]3[hundred][ft][temp]10[dp]9[qnh]1007[hpa][becmg][bkn]1[thousand]3[hundred][ft][end]A

            //[trl]45[opr][independend][pause]250[deg]13[max]24[kt][vrbbtn]210[and]280[deg][vis]10[km][-][shra][few]2[thousand][ft][few]2[thousand]5[hundred][ft][cb][temp]10[dp]6[qnh]998[hpa][tempo][vis]7[km][end]D

            //[ehamatis]E[pause][mlrwy]18[right][slrwy]06[mtrwy]18[center][strwy]18[center][trl]50[pause]250[deg]13[max]24[kt][vrbbtn]210[and]280[deg][vis]10[km][-][shra][few]2[thousand][ft][few]2[thousand]5[hundred][ft][cb][temp][minus]10[dp]6[qnh]998[hpa][tempo][vis]7[km][end]E
        }
    }
}
