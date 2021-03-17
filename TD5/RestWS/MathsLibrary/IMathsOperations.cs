using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MathsLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMathsOperations
    {
       
        [OperationContract]
        [WebInvoke(Method ="GET", UriTemplate ="Add?a={a}&b={b}", ResponseFormat =WebMessageFormat.Json, BodyStyle =WebMessageBodyStyle.Wrapped)]
        int Add(int a, int b);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Mult?a={a}&b={b}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int Multiply(int a, int b);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Sub?a={a}&b={b}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int Subtract(int a, int b);

        [OperationContract] // ATTENTION POST !
        [WebInvoke(Method = "POST", UriTemplate = "Div?a={a}&b={b}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        float Divide(int a, int b);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "MathsLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
