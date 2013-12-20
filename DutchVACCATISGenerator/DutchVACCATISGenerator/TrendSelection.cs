using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// TrendSelection class.
    /// </summary>
    public partial class TrendSelection : Form
    {
        /// <summary>
        /// Constructor of TrendSelection. Initializes new instance of TrendSelection.
        /// </summary>
        public TrendSelection()
        {
            InitializeComponent();

            //Set trend combo box selection to first item.
            trendComboBox.SelectedIndex = 0;
        }
    }
}
