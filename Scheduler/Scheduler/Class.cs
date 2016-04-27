using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Class
    {
        private string className;
        private string type;
        private double[] time;
        private int code;
        private string prof;
        private string days;
        private string room;
        private int seats;
        private bool valid;

        public Class()
        {
            //Default constructor...need anything? Won't ever be calling default constructor...maybe throw error?
        }

        public Class(string name, string classtype, double start, double end, int ccode, string profname, string schedday, string classroom,
            string roomno, int seatsleft)
        {
            className = name;
            type = classtype;
            time = new double[2] {start, end};
            code = ccode;
            prof = profname;
            days = schedday;
            room = classroom;
            seats = seatsleft;
            valid = false;
        }

        public string getName()
        {
            return className;
        }

        public string getType()
        {
            return type;
        }

        public double[] getTime()
        {
            return time;
        }

        public string getDays()
        {
            return days;
        }

        public int getSeats()
        {
            return seats;
        }

        public bool getValid()
        {
            return valid;
        }

        public void setValid(bool condition)
        {
            valid = condition;
        }

        //do we really need get functions? Or do we want to set these values to public?

    }
}