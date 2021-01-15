using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSAGL.Forms
{
    public partial class EdgeCreationForm : Form
    {
        public object returnWeight { get; private set; }
        public EdgeCreationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double weight;
            if (double.TryParse(this.edgeWeightTextbox.Text, out weight))
            {
                this.DialogResult = DialogResult.OK;
                this.returnWeight = weight;
                this.Close();
            }
            else
                MessageBox.Show("Введите корректный вес ребра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void edgeWeightTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.button1_Click(this.button1, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
