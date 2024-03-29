﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtmCashTest.ConsoleTestClient.AtmCashService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Banknote", Namespace="http://schemas.datacontract.org/2004/07/AtmCashTest.WcfService.Models")]
    [System.SerializableAttribute()]
    public partial class Banknote : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NominalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Nominal {
            get {
                return this.NominalField;
            }
            set {
                if ((this.NominalField.Equals(value) != true)) {
                    this.NominalField = value;
                    this.RaisePropertyChanged("Nominal");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ImpossibleCashOutFault", Namespace="http://schemas.datacontract.org/2004/07/AtmCashTest.WcfService.Models")]
    [System.SerializableAttribute()]
    public partial class ImpossibleCashOutFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AtmCashService.IAtmCashService")]
    public interface IAtmCashService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/PutCash", ReplyAction="http://tempuri.org/IAtmCashService/PutCashResponse")]
        AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] PutCash(AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] banknotes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/PutCash", ReplyAction="http://tempuri.org/IAtmCashService/PutCashResponse")]
        System.Threading.Tasks.Task<AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[]> PutCashAsync(AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] banknotes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetCash", ReplyAction="http://tempuri.org/IAtmCashService/GetCashResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(AtmCashTest.ConsoleTestClient.AtmCashService.ImpossibleCashOutFault), Action="http://tempuri.org/IAtmCashService/GetCashImpossibleCashOutFaultFault", Name="ImpossibleCashOutFault", Namespace="http://schemas.datacontract.org/2004/07/AtmCashTest.WcfService.Models")]
        AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] GetCash(int sum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetCash", ReplyAction="http://tempuri.org/IAtmCashService/GetCashResponse")]
        System.Threading.Tasks.Task<AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[]> GetCashAsync(int sum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetAccountSum", ReplyAction="http://tempuri.org/IAtmCashService/GetAccountSumResponse")]
        int GetAccountSum();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetAccountSum", ReplyAction="http://tempuri.org/IAtmCashService/GetAccountSumResponse")]
        System.Threading.Tasks.Task<int> GetAccountSumAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetMessages", ReplyAction="http://tempuri.org/IAtmCashService/GetMessagesResponse")]
        string GetMessages(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAtmCashService/GetMessages", ReplyAction="http://tempuri.org/IAtmCashService/GetMessagesResponse")]
        System.Threading.Tasks.Task<string> GetMessagesAsync(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAtmCashServiceChannel : AtmCashTest.ConsoleTestClient.AtmCashService.IAtmCashService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AtmCashServiceClient : System.ServiceModel.ClientBase<AtmCashTest.ConsoleTestClient.AtmCashService.IAtmCashService>, AtmCashTest.ConsoleTestClient.AtmCashService.IAtmCashService {
        
        public AtmCashServiceClient() {
        }
        
        public AtmCashServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AtmCashServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AtmCashServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AtmCashServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] PutCash(AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] banknotes) {
            return base.Channel.PutCash(banknotes);
        }
        
        public System.Threading.Tasks.Task<AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[]> PutCashAsync(AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] banknotes) {
            return base.Channel.PutCashAsync(banknotes);
        }
        
        public AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[] GetCash(int sum) {
            return base.Channel.GetCash(sum);
        }
        
        public System.Threading.Tasks.Task<AtmCashTest.ConsoleTestClient.AtmCashService.Banknote[]> GetCashAsync(int sum) {
            return base.Channel.GetCashAsync(sum);
        }
        
        public int GetAccountSum() {
            return base.Channel.GetAccountSum();
        }
        
        public System.Threading.Tasks.Task<int> GetAccountSumAsync() {
            return base.Channel.GetAccountSumAsync();
        }
        
        public string GetMessages(string msg) {
            return base.Channel.GetMessages(msg);
        }
        
        public System.Threading.Tasks.Task<string> GetMessagesAsync(string msg) {
            return base.Channel.GetMessagesAsync(msg);
        }
    }
}
