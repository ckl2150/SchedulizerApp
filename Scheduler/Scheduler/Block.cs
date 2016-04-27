using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Block
    {
        //Instance variables
        private string type;
        private Dictionary<string, ClassList> courses;
        private bool valid;
        private int numofclasses;

        //Constructor takes a type as a parameter; this specifies whether the objects within are of type lecture, discussion, or lab
        public Block(string coursetype)
        {
            type = coursetype;
            courses = new Dictionary<string, ClassList>();
            valid = false;
            numofclasses = 0;
        }

        //Method which adds new class to dictionary. Makes new key/value pairing if it does not already exist, and adds the class to the existing list.
        public void addNewClass(string name, string classtype, double start, double end, int ccode, string profname, string schedday, string classroom, string roomno, int seats)
        {
            if (courses.ContainsKey(name))
            {
                courses[name].Add(new Class(name, classtype, start, end, ccode, profname, schedday, classroom, roomno, seats));
            }
            else
            {
                courses.Add(name, new ClassList());
                courses[name].Add(new Class(name, classtype, start, end, ccode, profname, schedday, classroom, roomno, seats));
                numofclasses++;
            }
        }

        ///LEAVE ALONE FOR NOW
        ////Retrieves the keys which exist in the dictionary, and returns them as an array
        //public string[] getCourseNames()
        //{
        //    string[] keys = courses.Keys.ToArray();
        //    return keys;
        //}

        public string getBlockType()
        {
            return type;
        }
        
        public ClassList[] getClassObjects()
        {
            ClassList[] values = courses.Values.ToArray();
            return values;
        }

        //Retrieves number of unique classes in this block to the caller
        //Might not need this function anymore...
        public int getNumOfClasses()
        {
            return numofclasses;
        }

        //Returns either the start or end times of a class as an integer array, given the proper string modifier
        public double[] getTimeClass(string name)
        {
            //int[] classTime = 
            ClassList classlist = courses[name];
            double[] classTime = classlist[classlist.getCount()].getTime();
            if (!classlist.endOfList()) //Might be unnecessary if I'm calling this outside
            {
                classlist.incCount();
            }

            return classTime;
            //int[] classTimes = new int[courses[name].Count()];
            //for (int i = 0; i < courses[name].Count(); i++)
            //{
            //    if (modifier == "start")
            //    {
            //        classTimes[i] = courses[name][i].getStart();
            //    }
            //    else
            //    {
            //        classTimes[i] = courses[name][i].getEnd();
            //    }
            //}
            //return classTimes;
        }

        public string getDays(string name)
        {
            ClassList classlist = courses[name];
            return classlist[classlist.getCount()].getDays();
        }

        public bool isValid()
        {
            return valid;
        }

        public void setValid(bool condition)
        {
            valid = condition;
        }
    }
}
