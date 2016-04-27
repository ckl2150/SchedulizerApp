using System;
using System.Collections.Generic;

namespace Scheduler
{
	public class ClassList : List<Class> {
        
        private int counter;
        private bool slotfilled;
        private int totalslots;
        
		public ClassList() {
            counter = 0;
            slotfilled = false;
            totalslots = this.Capacity;
        }

        public int getCount()
        {
            return counter;
        }

        public void incCount()
        {
            if (counter >= totalslots)
            {
                counter++;
            }
        }

        public void resetCount()
        {
            counter = 0;
        }

        public bool endOfList()
        {
            return counter >= this.Count;
        }

        public void setFilledSlot(bool condition)
        {
            slotfilled = condition;
        }

        public bool filledSlot()
        {
            return slotfilled;
        }
	}
}