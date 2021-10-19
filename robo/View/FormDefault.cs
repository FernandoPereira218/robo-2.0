using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.View
{
    public partial class FormDefault : Form
    {
        List<Point> pontosComboBox = new List<Point>() { new Point(20, 140), new Point(20, 224) };
        int posicaoLabel, posicaoCombo = 0;
        public FormDefault(TOMenus menuSelecionado, string tipoFies)
        {
            InitializeComponent();

            LimparForm();
            foreach (System.Windows.Forms.Control item in flowLayoutPanel1.Controls)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString().Contains(menuSelecionado.Item))
                    {
                        item.Visible = true;
                        //if (item.GetType() == typeof(GroupBox))
                        //{
                        //    item.Location = pontosComboBox[posicaoCombo];
                        //    posicaoCombo++;
                        //}
                    }
                }
            }
        }

        private void LimparForm()
        {
            foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
            {
                control.Visible = false;
            }
        }
    }
}
