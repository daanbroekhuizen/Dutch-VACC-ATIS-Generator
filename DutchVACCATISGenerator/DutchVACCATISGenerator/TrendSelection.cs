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
    /// TrendSelection class.
    /// </summary>
    public partial class TrendSelection : Form
    {
        /// <summary>
        /// Constructor of TrendSelection.
        /// </summary>
        public TrendSelection()
        {
            InitializeComponent();

            trendComboBox.SelectedIndex = 0;
        }
    }
}
