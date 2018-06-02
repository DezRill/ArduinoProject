using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesignDemo
{
    public class ActiveDoctor: Account
    {
        public string Specialty { set; get; }
        public string Position { set; get; }
        public string Characteristic { set; get; }
        public string Diploma { set; get; }
        public DateTime Invite { set; get; }
    }
}