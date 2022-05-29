using System;
using System.Collections.Generic;

namespace CourseServer
{
    public partial class Challenges
    {
        public Challenges()
        {
            StudCh = new HashSet<StudCh>();
        }

        public int Id { get; set; }
        public string ChName { get; set; }
        public int ChScore { get; set; }

        public virtual ICollection<StudCh> StudCh { get; set; }
    }
}
