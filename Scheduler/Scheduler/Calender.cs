using System;

namespace Scheduler
{
    public class Calender
    {
        private bool[,] planner;

        public Calender()
        {
            planner = new bool[28, 5];
        }

        public void erase()
        {
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (planner[i, j] == true)
                    {
                        planner[i, j] = false;
                    }
                }
            }
        }

        public void addTimeSlot(double start, double end, string days)
        {
            double timeDiff = (end - start);
            double beginindex = start * 2 - 16;
            double endindex = start * 2 - 16 + timeDiff * 2;
            int[] day = convertDay2Num(days);

            foreach (int j in day)
            {
                for (int i = (int) beginindex; i < (int) endindex; i++)
                {
                    planner[i, j] = true;
                }
            }
        }

        public void removeTimeSlot(double start, double end, string days)
        {
            double timeDiff = (end - start);
            double beginindex = start * 2 - 16;
            double endindex = start * 2 - 16 + timeDiff * 2;
            int[] day = convertDay2Num(days);

            foreach (int j in day)
            {
                for (int i = (int) beginindex; i < (int) endindex; i++)
                {
                    planner[i, j] = false;
                }
            }
        }

        public bool hasConflict(double start, double end, string days)
        {
            bool conflict = false;
            double timeDiff = (end - start);
            double beginindex = start * 2 - 16;
            double endindex = start * 2 - 16 + timeDiff * 2;
            int[] day = convertDay2Num(days);

            foreach (int j in day)
            {
                for (int i = (int) beginindex; i < (int) endindex; i++)
                {
                    if (planner[i, j])
                    {
                        conflict = true;
                        break;
                    }
                }
                if (conflict) { break; }
            }
            return conflict;
        }

        private int[] convertDay2Num(string days)
        {
            int[] dayValues;

            if ((days == "Mon,Wed") || (days == "Tue,Thu"))
            {
                dayValues = new int[2];
                if (days == "Mon,Wed")
                {
                    dayValues[0] = 0;
                    dayValues[1] = 2;
                }
                else
                {
                    dayValues[0] = 1;
                    dayValues[1] = 3;
                }
            }
            else
            {
                dayValues = new int[1];
                if (days == "Mon") { dayValues[0] = 0; }
                else if (days == "Tue") { dayValues[0] = 1; }
                else if (days == "Wed") { dayValues[0] = 2; }
                else if (days == "Thu") { dayValues[0] = 3; }
                else if (days == "Fri") { dayValues[0] = 4; }
            }
            return dayValues;
        }

        public void display()
        {
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(planner[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}