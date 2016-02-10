using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Model
{
    public class DistanceBreakup
    {
        public int Position { get; set; }
        public double Value { get; set; }
        public bool IsInBound { get; set; }
    }
}
