using System;
using System.Collections.Generic;

namespace Day27_EFCore_FirstTimeSolo_DatabaseFirst.Models
{
    public partial class Super
    {
        public Super()
        {
            Mission = new HashSet<Mission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SuperName { get; set; }
        public string Power { get; set; }
        public int? PowerLevel { get; set; }
        public bool? Cape { get; set; }
        public bool? Good { get; set; }

        public virtual ICollection<Mission> Mission { get; set; }
    }
}
