using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesignDemo
{
    public abstract class Account
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Town { set; get; }
        public string Mail { set; get; }
        public string Adress { set; get; }
        public string Passport { set; get; }
        public string Phone { set; get; }
        public DateTime Birth { set; get; }
        public Image Photo { set; get; }
    }
}