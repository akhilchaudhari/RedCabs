﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RedCabsWebAPI {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RideMeResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RideMeResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RedCabsWebAPI.RideMeResources", typeof(RideMeResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RideMe.
        /// </summary>
        public static string CompanyName {
            get {
                return ResourceManager.GetString("CompanyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to akhil.chaudhari91@gmail.com.
        /// </summary>
        public static string CompanyRegistrationEmailAddress {
            get {
                return ResourceManager.GetString("CompanyRegistrationEmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Akhil@1991.
        /// </summary>
        public static string CompanyRegistrationEmailPassword {
            get {
                return ResourceManager.GetString("CompanyRegistrationEmailPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 587.
        /// </summary>
        public static string EmailAddressPort {
            get {
                return ResourceManager.GetString("EmailAddressPort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 9.
        /// </summary>
        public static string LowestCabRates {
            get {
                return ResourceManager.GetString("LowestCabRates", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0},
        ///
        ///Welcome to {1} cabs
        ///
        ///With your account, you can now get cabs at your doorstep at ₹{2}/km just using the {3} app.
        ///
        ///For your records, you are registered with us as:
        ///
        ///Username: {4}
        ///Password: {5}
        ///
        ///Please keep this information secure and do not share the password with anyone.
        ///
        ///Wishing you a very happy journey with {6} cabs.
        ///
        ///Thanks and Regards,
        ///{7} cabs.
        /// </summary>
        public static string Registration_Email_Body {
            get {
                return ResourceManager.GetString("Registration_Email_Body", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to {0} cabs.
        /// </summary>
        public static string Registration_Email_Subject {
            get {
                return ResourceManager.GetString("Registration_Email_Subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to smtp.gmail.com.
        /// </summary>
        public static string SmtpClient {
            get {
                return ResourceManager.GetString("SmtpClient", resourceCulture);
            }
        }
    }
}
