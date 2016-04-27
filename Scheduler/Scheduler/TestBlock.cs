using System;
using System.Collections;

namespace Scheduler
{
    class TestBlock
    {
        static void Main (string[] args)
        {
            Calender calender = new Calender();
            //Simulate retrieval of a class instance
            Console.WriteLine("This tester creates a Block instance, and attempts to add different classes via the addNewClass() method");
            Block lectures = new Block("lecture");
            Block disc = new Block("disc");
            Block lab = new Block("lab");
            disc.addNewClass("Discrete", "Disc", 8, 9, 135, "Hoopla", "Tue,Thu", "MTH", "203", 5);
            disc.addNewClass("Discrete", "Disc", 9, 10, 135, "Hoopla", "Tue,Thu", "MTH", "203", 3);
            disc.addNewClass("Discrete", "Disc", 15, 16, 135, "Hoopla", "Tue,Thu", "MTH", "203", 0);
            disc.addNewClass("Discrete", "Disc", 16, 17, 135, "Hoopla", "Tue,Thu", "MTH", "203", 9);
            disc.addNewClass("Discrete", "Disc", 13, 14, 135, "Hoopla", "Tue,Thu", "MTH", "203", 5);
            disc.addNewClass("Discrete", "Disc", 8, 9, 135, "Hoopla", "Mon,Wed", "MTH", "203", 10);
            disc.addNewClass("Discrete", "Disc", 9, 10, 135, "Hoopla", "Mon,Wed", "MTH", "203", 10);
            disc.addNewClass("Discrete", "Disc", 15, 16, 135, "Hoopla", "Mon,Wed", "MTH", "203", 10);
            disc.addNewClass("Discrete", "Disc", 16, 17, 135, "Hoopla", "Mon,Wed", "MTH", "203", 1);
            disc.addNewClass("Discrete", "Disc", 13, 14, 135, "Hoopla", "Mon,Wed", "MTH", "203", 0);
            disc.addNewClass("Discrete", "Disc", 17, 18, 135, "Hoopla", "Mon,Wed", "MTH", "203", 5);
            disc.addNewClass("Discrete", "Disc", 11, 12, 135, "Hoopla", "Tue,Thu", "MTH", "203", 11);
            disc.addNewClass("Matlab", "Disc", 8, 9, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 9, 10, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 10, 11, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 11, 12, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 12, 13, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 13, 14, 127, "Attaway", "Fri", "PHO", "103", 11);
            disc.addNewClass("Matlab", "Disc", 14, 15, 127, "Attaway", "Fri", "PHO", "103", 11);
            lab.addNewClass("Matlab", "Lab", 14.5, 15.5, 127, "Attaway", "Mon,Wed", "PHO", "103", 11);
            lab.addNewClass("Matlab", "Lab", 15.5, 16.6, 127, "Attaway", "Mon,Wed", "PHO", "103", 11);
            lab.addNewClass("Matlab", "Lab", 16.5, 17.5, 127, "Attaway", "Mon,Wed", "PHO", "103", 11);
            lab.addNewClass("Matlab", "Lab", 13.5, 14.5, 127, "Attaway", "Mon,Wed", "PHO", "103", 11);
            lab.addNewClass("Matlab", "Lab", 12.5, 13.5, 127, "Attaway", "Mon,Wed", "PHO", "103", 11);

            lectures.addNewClass("Discrete", "Lecture", 8, 10, 135, "Hoopla", "Tue,Thu", "MTH", "203", 33);
            lectures.addNewClass("Discrete", "Lecture", 16, 18, 135, "Hoopla", "Mon,Wed", "MTH", "203", 1);
            lectures.addNewClass("Matlab", "Lecture", 12, 14, 127, "Attaway", "Mon,Wed", "PHO","103", 14);
            lectures.addNewClass("Matlab", "Lecture", 16, 18, 127, "Attaway", "Mon,Wed", "PHO", "103", 5);
            lectures.addNewClass("Matlab", "Lecture", 14, 16, 127, "Attaway", "Mon,Wed", "PHO", "103", 43);
            lectures.addNewClass("Logic", "Lecture", 8, 10, 311, "Attaway", "Mon,Wed", "PHO", "210", 10);
            lectures.addNewClass("Logic", "Lecture", 14, 16, 311, "Attaway", "Mon,Wed", "PHO", "210", 40);
            lectures.addNewClass("C++", "Lecture", 8, 10, 304, "Carruthers", "Mon,Wed", "PHO", "210", 4);
            lectures.addNewClass("C++", "Lecture", 12, 14, 304, "Carruthers", "Mon,Wed", "PHO", "210", 3);

            ClassList[] arr = lectures.getClassObjects();

            for (int i = 0; i < arr.Length; i++)
            {
                foreach (Class c in arr[i])
                {
                    double[] times = c.getTime();
                    Console.WriteLine(times[0] + " " + times[1]);
                }
            }

            //Code to test ScheduleCompuete's findSchedule() method
            ScheduleCompute algorithm = new ScheduleCompute();
            algorithm.addBlock(lab);
            algorithm.addBlock(lectures);
            algorithm.addBlock(disc);
            algorithm.processBlockList();
            algorithm.getBoolCalender().display();
            ArrayList chosenclasses = algorithm.getSchedule();
            foreach (Class c in chosenclasses)
            {
                double[] times = c.getTime(); 
                Console.WriteLine("Class: " + c.getName() + " StartTime: " + times[0] + " EndTime: " + times[1] + " Days: " + c.getDays() + " Type: " + c.getType());
            }
        }
    }
}