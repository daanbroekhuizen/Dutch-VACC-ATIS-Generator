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
    /// 
    /// </summary>
    public partial class TrendSelection : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public TrendSelection()
        {
            InitializeComponent();

            trendComboBox.SelectedIndex = 0;
        }
    }
}
