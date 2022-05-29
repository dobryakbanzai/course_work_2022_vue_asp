using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class Students
    {
        public Students()
        {
            StudCh = new HashSet<StudCh>();
            StudTeach = new HashSet<StudTeach>();
        }

        public int Id { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public int TotalScore { get; set; }
        public int ClassId { get; set; }

        public virtual Classes Class { get; set; }
        public virtual ICollection<StudCh> StudCh { get; set; }
        public virtual ICollection<StudTeach> StudTeach { get; set; }
    }
}
