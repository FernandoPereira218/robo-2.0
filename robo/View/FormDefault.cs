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
        public FormDefault(TOMenus menuSelecionado, string tipoFies)
        {
            InitializeComponent();
            LimparForm();


            foreach (var item in menuSelecionado.Paineis)
            {
                flowLayoutPanel1.Controls.Find(item, false)[0].Visible = true;
            }



            if (tipoFies.ToUpper() == "FIES LEGADO")
            {
                panelCampus.Visible = true;
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
