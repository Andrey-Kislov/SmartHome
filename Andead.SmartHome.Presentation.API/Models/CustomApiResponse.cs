using System;
using System.Runtime.Serialization;

namespace Andead.SmartHome.Presentation.API.Models
{
    [DataContract]
    public class CustomApiResponse
    {
        [DataMember]
        public Guid RequestId { get; } = Guid.NewGuid();

        [DataMember]
        public bool Success { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }
        
        public CustomApiResponse(object result = null)
        {
            Success = true;
            Result = result;
        }

        public CustomApiResponse(Exception exception)
        {
            Success = false;
            ErrorMessage = exception.Message;
        }
    }
}
