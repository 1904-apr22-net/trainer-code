using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySoapService
{
    [DataContract] // means, this class can/will be serialized in SOAP messages
    public class Temperature
    {
        [DataMember] // means, this member should be serialized
        public TempUnit Unit { get; set; }

        [DataMember]
        public double Value { get; set; }

        public enum TempUnit
        {
            Fahrenheit,
            Celsius
        }
    }
}