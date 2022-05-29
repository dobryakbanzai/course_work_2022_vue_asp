using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class StudCh
    {
        public int Id { get; set; }
        public int ChId { get; set; }
        public int StudId { get; set; }
        public int StudChScore { get; set; }

        public virtual Challenges Ch { get; set; }
        public virtual Students Stud { get; set; }
    }
}
