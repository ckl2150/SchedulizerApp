using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Scheduler
{
    class ScheduleCompute
    {
        private Block[] blocklist;
        private Calender calender;
        private int blockcount;
        bool validanchor;

        public ScheduleCompute()
        {
            blocklist = new Block[3];
            calender = new Calender();
            blockcount = 0;
            validanchor = true;
        }

        public void processBlockList()
        {
            while ((!((blockcount == blocklist.Length) || (blocklist[blockcount] == null))) && validanchor)
            {
                findSchedule();
            }
            if (!validanchor) { Console.WriteLine("Invalid anchor"); } //Console error message, no matches with current anchor
        }
        
        //Schedules one block per method call
        public void findSchedule()
        {


            ClassList[] classlistarr = blocklist[blockcount].getClassObjects(); //array of values of ClassLists
            int numofclasses = blocklist[blockcount].getNumOfClasses(); //Come back--see if you really need this...im thinking no, since line 41 we use LENGTH

            //The below block of code uses the first classlist in classlistarr as an anchor; all calculations first begin with this classlist
            ClassList anchorclasslist = classlistarr[0]; //anchor
            int anchorind = anchorclasslist.getCount();
            double[] anchortimes = anchorclasslist[anchorind].getTime();

            //When finding the schedule with a new block, before using an anchor class, must check that its time slot isn't already filled; else we go to the next index
            while (!(!calender.hasConflict(anchortimes[0], anchortimes[1], anchorclasslist[anchorind].getDays()) || (anchorind == anchorclasslist.Count)))
            {
                anchorclasslist.incCount();
                anchorind = anchorclasslist.getCount();
                anchortimes = anchorclasslist[anchorind].getTime();

                //If we are at the end of the anchorclasslist with no match, then for the current block, there is no possible class combination (without changing classes from previous blocks)
                //IMPLEMENT AFOREMENTIONED FUNCTOINANLITY LATER?
                if (anchorind == anchorclasslist.Count)
                {
                    validanchor = false;
                    break;
                }
            }



            while ((!((anchorind == anchorclasslist.Count) || blocklist[blockcount].isValid())) && validanchor) //IF anchor index reaches max index OR the current block is valid, while loop exits
            {

                foreach (Class c in anchorclasslist)
                {
                    if (c.getValid())
                    {
                        double[] ctime = c.getTime();
                        calender.removeTimeSlot(ctime[0], ctime[1], c.getDays());
                        c.setValid(false);
                    }
                }
                Console.WriteLine("anchorindex at: " + anchorind);

                calender.addTimeSlot(anchortimes[0], anchortimes[1], anchorclasslist[anchorind].getDays());
                anchorclasslist.setFilledSlot(true); //class from anchor classlist is the first class to be added, so is always valid
                anchorclasslist[anchorind].setValid(true);
                for (int i = 1; i < classlistarr.Length; i++) //Loops through the remaining non-anchor class lists
                {
                    //Must reset bool vals when descending down new level
                    bool nextanchor = false;
                    Console.WriteLine("iterator i is at: " + i);
                    ClassList newclasslist = classlistarr[i];
                    newclasslist.setFilledSlot(false);

                    foreach (Class c in newclasslist) //Whenever we descend to a new level, must set all class valids to false, to present with a clean slate
                    {
                        if (c.getValid())
                        {
                            double[] ctime = c.getTime();
                            calender.removeTimeSlot(ctime[0], ctime[1], c.getDays());
                            c.setValid(false);
                        }
                    }

                    Console.Write("Class: " + newclasslist[newclasslist.getCount()].getName() + " at index " + newclasslist.getCount() + ". End of list = " + newclasslist.endOfList());


                    while (!((newclasslist.filledSlot()) || (newclasslist.getCount() == newclasslist.Count))) //While loop exists IF classlist is valid, or if max indexing is reached
                    {
                        double[] times = newclasslist[newclasslist.getCount()].getTime();
                        Console.WriteLine(". Times: " + times[0] + " " + times[1]);
                        Console.WriteLine();
                        Console.WriteLine("While loop executed: total count of newclasslist is " + newclasslist.Count + " and current count is " + newclasslist.getCount());
                        if ((!calender.hasConflict(times[0], times[1], newclasslist[newclasslist.getCount()].getDays())) && (newclasslist[newclasslist.getCount()].getSeats() != 0)) //Body runs if there is no conflict
                        {
                            Console.WriteLine("inner if executed");
                            calender.addTimeSlot(times[0], times[1], newclasslist[newclasslist.getCount()].getDays());
                            newclasslist[newclasslist.getCount()].setValid(true); //Sets individual class in classlist to true
                            newclasslist.setFilledSlot(true); //Sets this classlist to true
                            newclasslist.incCount();
                        }
                        else
                        {
                            Console.WriteLine("inner else executed");
                            newclasslist.incCount();
                            newclasslist.setFilledSlot(false);
                            Console.WriteLine(newclasslist.getCount());
                            if (newclasslist.getCount() == newclasslist.Count) //If true, means that current classlist has reached end of list, and no combination can be found
                            {
                                if (i != 1) //If true, means that we can go up one classlist level and try with a new index
                                {
                                    newclasslist.resetCount();
                                    i = i-2;
                                    break;
                                }
                                else //If true, then we are already at the classlist before the anchor. This means we must increment the anchorlist index
                                {
                                    if (anchorind == anchorclasslist.Count) //If true, have reached the end of the anchorlist--no viable schedule can be found
                                    {
                                        //First while loop might already take care of this tho
                                        break;
                                    }
                                    else //Otherwise, we can increment anchorlist's index to set a new anchor point--this breaks to the first while loop
                                    {
                                        nextanchor = true;
                                        anchorclasslist.incCount();
                                        i--;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (nextanchor)
                    {
                        break;
                    }
                }

                foreach (ClassList c in classlistarr) //Checking if all Classlists in this block have a filled time slot
                {
                    if (!c.filledSlot())
                    {
                        blocklist[blockcount].setValid(false);
                        break;
                    }
                    blocklist[blockcount].setValid(true);
                }

                if (blocklist[blockcount].isValid())
                {
                    blockcount++;
                    break;
                }
                else //Means that block is not valid, and no class combination can be found from it...CHANGE THIS
                {
                    if (anchorind != anchorclasslist.Count) //If we have not reached the end of the anchorclasslist, will increase index and set new anchor
                    {
                        anchorclasslist.incCount();
                        foreach (ClassList c in classlistarr)
                        {
                            c.setFilledSlot(false); //Need to decide whether to also set blockcount back to 0...
                            foreach (Class k in c)
                            {
                                k.setValid(false);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public Calender getBoolCalender()
        {
            return calender;
        }

        public void addBlock(Block blockobj)
        {
            if (blockobj.getBlockType() == "lecture")
            {
                blocklist[0] = blockobj;
            }
            else if (blockobj.getBlockType() == "lab")
            {
                if (blocklist[1] == null)
                {
                    blocklist[1] = blockobj;
                }
                else
                {
                    blocklist[2] = blockobj;
                }

            }
            else if (blockobj.getBlockType() == "disc")
            {
                if (blocklist[1] == null)
                {
                    blocklist[1] = blockobj;
                }
                else
                {
                    blocklist[2] = blockobj;
                }
            }
        }

        public ArrayList getSchedule() //This algorithm should only run after all three blocks are confirmed to be valid. Goes through one block per method call
        {
            foreach (Block b in blocklist)
            {
                if (!b.isValid())
                {
                    return null;
                }
            }

            ArrayList returnsched = new ArrayList();


            for (int i = 0; i < 3; i++)
            {
                if (blocklist[i] != null)
                {
                    Block block = blocklist[i];
                    ClassList[] classlistarr = block.getClassObjects(); //array of values of ClassLists
                    for (int j = 0; j < classlistarr.Length; j++)
                    {
                        foreach (Class c in classlistarr[j])
                        {
                            if (c.getValid())
                            {
                                returnsched.Add(c);
                                break;
                            }
                        }
                    }
                }
            }
            return returnsched;
        }


        ////WHENEVER we add a class
        ////function to make a get request to API
        //public ??? request()
        //{

        //}

        ////Function which takes JSON file and parses into a usable format
        //public ??? parseFile()
        //{

        //}

    }
}