﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ServiceModel;

namespace GrattaEVinciReference
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://ws.lottomatica.rivenditore.it", ConfigurationName = "GrattaEVinciReference.Desk")]
    public interface Desk
    {

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
       SaldoContoGiocoResponse checkToken(string String_1, string String_2, string String_3);

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
       AddebitoResponse commitAddebito(string String_1, string String_2, string String_3, string String_4,
            string String_5, string String_6, string String_7, string String_8, string String_9, string String_10,
            string String_11, string String_12, string String_13);

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
       AddebitoResponse reserveAddebito(string String_1, string String_2, string String_3, string String_4,
            string String_5, string String_6, string String_7, string String_8, string String_9, string String_10);

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
        AddebitoResponse rollbackAddebito(string String_1, string String_2, string String_3, string String_4,
            string String_5, string String_6, string String_7, string String_8, string String_9, string String_10);

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
        SaldoContoGiocoResponse saldoContoGioco(string String_1, string String_2, string String_3);

        [OperationContract]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style = System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseData))]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "result")]
        ChiaveContoGiocoResponse chiaveContoGioco(string String_1, string String_2, string String_3,
            string String_4);
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AddebitoResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ws.lottomatica.rivenditore.it/types")]
    public partial class SaldoContoGiocoResponse : ResponseData
    {

        private string saldoContoGiocoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string saldoContoGioco
        {
            get
            {
                return this.saldoContoGiocoField;
            }
            set
            {
                this.saldoContoGiocoField = value;
                this.RaisePropertyChanged("saldoContoGioco");
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ChiaveContoGiocoResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SaldoContoGiocoResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AddebitoResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ws.lottomatica.rivenditore.it/types")]
    public partial class ResponseData : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string codiceRisultatoField;

        private string descrizioneRisultatoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string codiceRisultato
        {
            get
            {
                return this.codiceRisultatoField;
            }
            set
            {
                this.codiceRisultatoField = value;
                this.RaisePropertyChanged("codiceRisultato");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string descrizioneRisultato
        {
            get
            {
                return this.descrizioneRisultatoField;
            }
            set
            {
                this.descrizioneRisultatoField = value;
                this.RaisePropertyChanged("descrizioneRisultato");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ws.lottomatica.rivenditore.it/types")]
    public partial class ChiaveContoGiocoResponse : ResponseData
    {

        private string chiaveContoGiocoField;

        private string datiFirmatiField;

        private string firmaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string chiaveContoGioco
        {
            get
            {
                return this.chiaveContoGiocoField;
            }
            set
            {
                this.chiaveContoGiocoField = value;
                this.RaisePropertyChanged("chiaveContoGioco");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string datiFirmati
        {
            get
            {
                return this.datiFirmatiField;
            }
            set
            {
                this.datiFirmatiField = value;
                this.RaisePropertyChanged("datiFirmati");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 2)]
        public string firma
        {
            get
            {
                return this.firmaField;
            }
            set
            {
                this.firmaField = value;
                this.RaisePropertyChanged("firma");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ws.lottomatica.rivenditore.it/types")]
    public partial class AddebitoResponse : SaldoContoGiocoResponse
    {

        private string datiFirmatiField;

        private string firmaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string datiFirmati
        {
            get
            {
                return this.datiFirmatiField;
            }
            set
            {
                this.datiFirmatiField = value;
                this.RaisePropertyChanged("datiFirmati");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string firma
        {
            get
            {
                return this.firmaField;
            }
            set
            {
                this.firmaField = value;
                this.RaisePropertyChanged("firma");
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Desk_v2Channel : GrattaEVinciReference.Desk, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Desk_v2Client : System.ServiceModel.ClientBase<GrattaEVinciReference.Desk>, GrattaEVinciReference.Desk
    {

        public Desk_v2Client()
        {
        }

        public Desk_v2Client(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public Desk_v2Client(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public Desk_v2Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public Desk_v2Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public SaldoContoGiocoResponse checkToken(string String_1, string String_2, string String_3)
        {
            return base.Channel.checkToken(String_1, String_2, String_3);
        }

        public AddebitoResponse commitAddebito(string String_1, string String_2, string String_3, string String_4,
            string String_5, string String_6, string String_7, string String_8, string String_9, string String_10,
            string String_11, string String_12, string String_13)
        {
            return base.Channel.commitAddebito(String_1, String_2, String_3, String_4, String_5, String_6, String_7, String_8, String_9, String_10, String_11, String_12, String_13);
        }

        public AddebitoResponse reserveAddebito(string String_1, string String_2, string String_3,
            string String_4, string String_5, string String_6, string String_7, string String_8, string String_9,
            string String_10)
        {
            return base.Channel.reserveAddebito(String_1, String_2, String_3, String_4, String_5, String_6, String_7, String_8, String_9, String_10);
        }

        public AddebitoResponse rollbackAddebito(string String_1, string String_2, string String_3,
            string String_4, string String_5, string String_6, string String_7, string String_8, string String_9,
            string String_10)
        {
            return base.Channel.rollbackAddebito(String_1, String_2, String_3, String_4, String_5, String_6, String_7, String_8, String_9, String_10);
        }

        public SaldoContoGiocoResponse saldoContoGioco(string String_1, string String_2, string String_3)
        {
            return base.Channel.saldoContoGioco(String_1, String_2, String_3);
        }

        public ChiaveContoGiocoResponse chiaveContoGioco(string String_1, string String_2, string String_3,
            string String_4)
        {
            return base.Channel.chiaveContoGioco(String_1, String_2, String_3, String_4);
        }
    }
}
