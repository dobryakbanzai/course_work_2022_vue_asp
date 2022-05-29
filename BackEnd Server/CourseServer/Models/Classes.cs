using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class Classes
    {
        public Classes()
        {
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public int ClassNum { get; set; }

        public virtual ICollection<Students> Students { get; set; }
    }
}
