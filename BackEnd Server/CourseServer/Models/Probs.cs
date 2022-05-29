using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseServer.Models
{
    public class Probs
    {
        public string problem { set; get; }
        public string resolve { set; get; }
        public Probs(string problem, string resolve)
        {
            this.problem = problem;
            this.resolve = resolve;
        }
    }
}
