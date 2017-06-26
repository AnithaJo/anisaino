using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot.Entities
{
    [Serializable]
    public class StoredEvent
    {
        public StoredEvent(string dt, string desc)
        {
            evtDate = dt;
            description = desc;
        }
        public string evtDate { get; set; }
        public string description { get; set; }
    }
}