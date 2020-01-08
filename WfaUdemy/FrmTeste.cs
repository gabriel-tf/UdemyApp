using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WfaUdemy.lib;

namespace WfaUdemy
{
    public partial class FrmTeste : Form
    {
        private Calculator _calculator;
        private int _param1;
        private int _param2;
        
        public FrmTeste()
        {
            InitializeComponent();
            _calculator = new Calculator();
        }

        private void btnSomar_Click(object sender, EventArgs e)
        {
            _param1 = Convert.ToInt32(txtParam1.Text);
            _param2 = Convert.ToInt32(txtParam2.Text);
            
            Console.WriteLine(_calculator.Sum(_param1, _param2));
        }

        private void btnSubtrair_Click(object sender, EventArgs e)
        {
            Console.WriteLine("result");
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("result");
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            Console.WriteLine("result");
        }
    }
}
