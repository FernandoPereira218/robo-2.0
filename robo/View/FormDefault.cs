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
using static robo.View.FormInterface2;

namespace robo.View
{
    public partial class FormDefault : Form
    {
        List<Point> pontosComboBox = new List<Point>() { new Point(20, 140), new Point(20, 224) };
        public FormDefault(TOMenus menuSelecionado, string tipoFies, string tipoAditamento)
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
                        string resultado = Convert.ToString(item.Tag);
                        if (resultado == "ADITAMENTO")
                        {
                            if (tipoAditamento == "FIES LEGADO")
                            {
                                foreach (System.Windows.Forms.Control item2 in flowLayoutPanel1.Controls)
                                {
                                    if (item2.Tag != null)
                                    {
                                        if (item2.Tag.ToString().Contains("A_LEGADO"))
                                        {
                                            item2.Visible = true;
                                        }
                                    }
                                }
                            }
                        }
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
