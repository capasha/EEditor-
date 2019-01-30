using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class CoinsOptions : Form
    {
        public NumericUpDown NumericUpdown1 { get { return numericUpDown1; } set { numericUpDown1 = value; } }
        public NumericUpDown NumericUpdown2 { get { return numericUpDown2; } set { numericUpDown2 = value; } }
        public NumericUpDown NumericUpdown3 { get { return numericUpDown3; } set { numericUpDown3 = value; } }

        public CoinsOptions()
        {
            InitializeComponent();
        }

        private void CoinsOptions_Load(object sender, EventArgs e)
        {
        }

        private void CoinsOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
