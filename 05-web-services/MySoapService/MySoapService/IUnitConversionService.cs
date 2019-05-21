using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MySoapService
{
    // in WCF, we have attributes which mark classes, interfaces, methods, etc.
    // to be exposed via endpoints with SOAP.

    [ServiceContract] // means, this interface will be a service having some operations.
    public interface IUnitConversionService
    {
        [OperationContract] // means, this method will be accessible as a SOAP operation
        //[OperationBehavior] // can configure transaction semantics, etc
        double FeetToMeters(double feet);

        [OperationContract]
        Temperature ConvertTemperature(Temperature temperature);
    }
}
