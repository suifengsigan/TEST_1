using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    public class TypicalSeparateResourceUseEx : TypicalSeparateResourceUse
    {
        public int MPT_ID { get; set; }
        public string TMPT_PARTID { get; set; }
        public string MPT_PARTID { get; set; }
    }
}
