using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot
{



    public class Rootobject
    {
        public string query { get; set; }
        public Topscoringintent topScoringIntent { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Topscoringintent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public class Rootobject
        {
            public string id { get; set; }
            public DateTime timestamp { get; set; }
            public string lang { get; set; }
            public Result result { get; set; }
            public Status status { get; set; }
            public string sessionId { get; set; }
        }

        public class Result
        {
            public string source { get; set; }
            public string resolvedQuery { get; set; }
            public string action { get; set; }
            public bool actionIncomplete { get; set; }
            public Parameters parameters { get; set; }
            public object[] contexts { get; set; }
            public Metadata metadata { get; set; }
            public Fulfillment fulfillment { get; set; }
            public int score { get; set; }
        }

        public class Parameters
        {
        }

        public class Metadata
        {
            public string intentId { get; set; }
            public string webhookUsed { get; set; }
            public string webhookForSlotFillingUsed { get; set; }
            public string intentName { get; set; }
        }

        public class Fulfillment
        {
            public string speech { get; set; }
            public Message[] messages { get; set; }
        }

        public class Message
        {
            public int type { get; set; }
            public string speech { get; set; }
        }

        public class Status
        {
            public int code { get; set; }
            public string errorType { get; set; }
        }

        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }




}