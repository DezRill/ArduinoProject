using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiDesignDemo
{
    public partial class Form9 : Form
    {
        public Form9()
        {
        }

        public Form9(Form4_2 form4_2)
        {
            InitializeComponent();

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            //Начальное положение формы задаётся вручную
            this.StartPosition = FormStartPosition.Manual;
            //Верхний левый угол экрана
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            //Перенос в нижний правый угол экрана без панели задач
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //Перенос в местоположение верхнего левого угла формы, чтобы её правый нижний угол попал в правый нижний угол экрана
            pt.Offset(-this.Width, -this.Height);
            //Новое положение формы
            this.Location = pt;
        }
    }
}
