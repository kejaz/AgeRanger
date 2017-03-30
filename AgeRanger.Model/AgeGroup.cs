using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Model
{
    public class AgeGroup : IEntityBase
    {
        public int Id { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Description { get; set; }

    }
}
