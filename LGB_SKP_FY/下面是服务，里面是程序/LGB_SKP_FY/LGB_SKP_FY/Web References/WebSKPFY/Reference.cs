﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace LGB_SKP_FY.WebSKPFY {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="FServiceSoap", Namespace="http://tempuri.org/")]
    public partial class FService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ReceiveJYJLOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReceiveHZJLByWebOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReceiveYSJLByWebOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReceiveFY_LGBOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FService() {
            this.Url = global::LGB_SKP_FY.Properties.Settings.Default.LGB_SKP_FY_WebSKPFY_FService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ReceiveJYJLCompletedEventHandler ReceiveJYJLCompleted;
        
        /// <remarks/>
        public event ReceiveHZJLByWebCompletedEventHandler ReceiveHZJLByWebCompleted;
        
        /// <remarks/>
        public event ReceiveYSJLByWebCompletedEventHandler ReceiveYSJLByWebCompleted;
        
        /// <remarks/>
        public event ReceiveFY_LGBCompletedEventHandler ReceiveFY_LGBCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReceiveJYJL", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ReceiveJYJL(string sParamIn) {
            object[] results = this.Invoke("ReceiveJYJL", new object[] {
                        sParamIn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReceiveJYJLAsync(string sParamIn) {
            this.ReceiveJYJLAsync(sParamIn, null);
        }
        
        /// <remarks/>
        public void ReceiveJYJLAsync(string sParamIn, object userState) {
            if ((this.ReceiveJYJLOperationCompleted == null)) {
                this.ReceiveJYJLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReceiveJYJLOperationCompleted);
            }
            this.InvokeAsync("ReceiveJYJL", new object[] {
                        sParamIn}, this.ReceiveJYJLOperationCompleted, userState);
        }
        
        private void OnReceiveJYJLOperationCompleted(object arg) {
            if ((this.ReceiveJYJLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReceiveJYJLCompleted(this, new ReceiveJYJLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReceiveHZJLByWeb", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ReceiveHZJLByWeb(string sParamIn) {
            object[] results = this.Invoke("ReceiveHZJLByWeb", new object[] {
                        sParamIn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReceiveHZJLByWebAsync(string sParamIn) {
            this.ReceiveHZJLByWebAsync(sParamIn, null);
        }
        
        /// <remarks/>
        public void ReceiveHZJLByWebAsync(string sParamIn, object userState) {
            if ((this.ReceiveHZJLByWebOperationCompleted == null)) {
                this.ReceiveHZJLByWebOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReceiveHZJLByWebOperationCompleted);
            }
            this.InvokeAsync("ReceiveHZJLByWeb", new object[] {
                        sParamIn}, this.ReceiveHZJLByWebOperationCompleted, userState);
        }
        
        private void OnReceiveHZJLByWebOperationCompleted(object arg) {
            if ((this.ReceiveHZJLByWebCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReceiveHZJLByWebCompleted(this, new ReceiveHZJLByWebCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReceiveYSJLByWeb", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ReceiveYSJLByWeb(string sParamIn) {
            object[] results = this.Invoke("ReceiveYSJLByWeb", new object[] {
                        sParamIn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReceiveYSJLByWebAsync(string sParamIn) {
            this.ReceiveYSJLByWebAsync(sParamIn, null);
        }
        
        /// <remarks/>
        public void ReceiveYSJLByWebAsync(string sParamIn, object userState) {
            if ((this.ReceiveYSJLByWebOperationCompleted == null)) {
                this.ReceiveYSJLByWebOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReceiveYSJLByWebOperationCompleted);
            }
            this.InvokeAsync("ReceiveYSJLByWeb", new object[] {
                        sParamIn}, this.ReceiveYSJLByWebOperationCompleted, userState);
        }
        
        private void OnReceiveYSJLByWebOperationCompleted(object arg) {
            if ((this.ReceiveYSJLByWebCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReceiveYSJLByWebCompleted(this, new ReceiveYSJLByWebCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReceiveFY_LGB", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ReceiveFY_LGB(string sParamIn) {
            object[] results = this.Invoke("ReceiveFY_LGB", new object[] {
                        sParamIn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReceiveFY_LGBAsync(string sParamIn) {
            this.ReceiveFY_LGBAsync(sParamIn, null);
        }
        
        /// <remarks/>
        public void ReceiveFY_LGBAsync(string sParamIn, object userState) {
            if ((this.ReceiveFY_LGBOperationCompleted == null)) {
                this.ReceiveFY_LGBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReceiveFY_LGBOperationCompleted);
            }
            this.InvokeAsync("ReceiveFY_LGB", new object[] {
                        sParamIn}, this.ReceiveFY_LGBOperationCompleted, userState);
        }
        
        private void OnReceiveFY_LGBOperationCompleted(object arg) {
            if ((this.ReceiveFY_LGBCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReceiveFY_LGBCompleted(this, new ReceiveFY_LGBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ReceiveJYJLCompletedEventHandler(object sender, ReceiveJYJLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReceiveJYJLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReceiveJYJLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ReceiveHZJLByWebCompletedEventHandler(object sender, ReceiveHZJLByWebCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReceiveHZJLByWebCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReceiveHZJLByWebCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ReceiveYSJLByWebCompletedEventHandler(object sender, ReceiveYSJLByWebCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReceiveYSJLByWebCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReceiveYSJLByWebCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ReceiveFY_LGBCompletedEventHandler(object sender, ReceiveFY_LGBCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReceiveFY_LGBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReceiveFY_LGBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591