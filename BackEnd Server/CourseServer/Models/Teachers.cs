using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class Teachers
    {
        public Teachers()
        {
            StudTeach = new HashSet<StudTeach>();
        }

        public int Id { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }

        public virtual ICollection<StudTeach> StudTeach { get; set; }
    }
}
