using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myPosSizer
{
    public partial class myPosSizerControl : UserControl
    {
        public myPosSizerControl()
        {
            InitializeComponent();
        }

        public double MaxRisk
        {
            get
            {
                return (double)this.numericUpDown1.Value;
            }
            set
            {
                this.numericUpDown1.Value = Decimal.Parse(value.ToString());
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
