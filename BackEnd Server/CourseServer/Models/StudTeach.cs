using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class StudTeach
    {
        public int Id { get; set; }
        public int? StudId { get; set; }
        public int? TeachId { get; set; }

        public virtual Students Stud { get; set; }
        public virtual Teachers Teach { get; set; }
    }
}
