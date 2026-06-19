using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerExercise3.Model
{
    class MainModel
    {
        public string ElapsedTime { get; set; }
        public int TickCount { get; set; }
        public int CurrentThread { get; set; }
        public int ThreadLocalCount { get; set; } 
    }
}
