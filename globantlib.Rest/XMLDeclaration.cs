using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace globantlib.Rest
{
    public class XmlDeclarationMessage : Message
    {
        private Message message;
        public XmlDeclarationMessage(Message message)
        {
            this.message = message;
        }

        public override MessageHeaders Headers
        {
            get { return message.Headers; }
        }

        protected override void OnWriteBodyContents(System.Xml.XmlDictionaryWriter writer)
        {
            // WCF XML serialization doesn't support emitting XML DOCTYPE, you need to roll up your own here.
            writer.WriteStartDocument();
            message.WriteBodyContents(writer);
        }


        public override MessageProperties Properties
        {
            get { return message.Properties; }
        }

        public override MessageVersion Version
        {
            get { return message.Version; }
        }
    }

    public class XmlDeclarationMessageFormatter : IDispatchMessageFormatter
    {
        private IDispatchMessageFormatter formatter;
        public XmlDeclarationMessageFormatter(IDispatchMessageFormatter formatter)
        {
            this.formatter = formatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            formatter.DeserializeRequest(message, parameters);
        }

        public Message SerializeReply(MessageVersion messageVersion, Object[] parameters, Object result)
        {
            var message = formatter.SerializeReply(messageVersion, parameters, result);
            return new XmlDeclarationMessage(message);
        }
    }

    public class IncludeXmlDeclarationAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Formatter = new XmlDeclarationMessageFormatter(dispatchOperation.Formatter);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    } 
}