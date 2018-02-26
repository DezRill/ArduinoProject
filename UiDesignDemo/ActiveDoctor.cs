using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesignDemo
{
    public class ActiveDoctor
    {
        int id, diploma_num;
        string name, gender, town, phone, mail, adress, passport, specialty, position, short_char;
        DateTime birth, invite_date;

        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        public int Diploma
        {
            set { diploma_num = value; }
            get { return diploma_num; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
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

        public string Passport
        {
            set { passport = value; }
            get { return passport; }
        }

        public string Specialty
        {
            set { specialty = value; }
            get { return specialty; }
        }

        public string Position
        {
            set { position = value; }
            get { return position; }
        }

        public string Characteristic
        {
            set { short_char = value; }
            get { return short_char; }
        }

        public DateTime Birth
        {
            set { birth = value; }
            get { return birth; }
        }

        public DateTime Invite
        {
            set { invite_date = value; }
            get { return invite_date; }
        }
    }
}
