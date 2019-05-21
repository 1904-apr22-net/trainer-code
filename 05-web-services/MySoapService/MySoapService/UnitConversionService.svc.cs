using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MySoapService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UnitConversionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UnitConversionService.svc or UnitConversionService.svc.cs at the Solution Explorer and start debugging.
    public class UnitConversionService : IUnitConversionService
    {
        public double FeetToMeters(double feet)
        {
            return 0.3048 * feet;
        }

        public Temperature ConvertTemperature(Temperature temperature)
        {
            switch (temperature.Unit)
            {
                case Temperature.TempUnit.Fahrenheit:
                    return new Temperature
                    {
                        Unit = Temperature.TempUnit.Celsius,
                        Value = (temperature.Value - 32) * 5 / 9
                    };
                case Temperature.TempUnit.Celsius:
                    return new Temperature
                    {
                        Unit = Temperature.TempUnit.Fahrenheit,
                        Value = temperature.Value * 9 / 5 + 32
                    };
                default:
                    throw new InvalidOperationException("invalid enum value");
            };
        }
    }
}