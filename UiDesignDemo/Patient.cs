using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesignDemo
{
    public class Patient
    {
        int id;
        string passport, name, gender, town, phone, mail, adress;
        DateTime birth, reg_date;
        Image photo;

        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        public string Passport
        {
            set { passport = value; }
            get { return passport; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public DateTime Birth
        {
            set { birth = value; }
            get { return birth; }
        }

        public string Gender
        {
            set { gender = value; }
            get { return gender; }
        }

        public string Town
        {
            set { town = value; }
            get { return town; }
        }

        public string Phone
        {
            set { phone = value; }
            get { return phone; }
        }

        public string Mail
        {
            set { mail = value; }
            get { return mail; }
        }

        public string Adress
        {
            set { adress = value; }
            get { return adress; }
        }

        public Image Photo
        {
            set { photo = value; }
            get { return photo; }
        }

        public DateTime Reg_Date
        {
            set { reg_date = value; }
            get { return reg_date; }
        }
    }
}
