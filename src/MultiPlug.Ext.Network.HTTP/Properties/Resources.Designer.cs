﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiPlug.Ext.Network.HTTP.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MultiPlug.Ext.Network.HTTP.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap NetworksIcon {
            get {
                object obj = ResourceManager.GetObject("NetworksIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///    &lt;div class=&quot;row-fluid&quot;&gt;
        ///        &lt;div class=&quot;box&quot;&gt;
        ///            &lt;div class=&quot;span2&quot;&gt;
        ///                &lt;a style=&quot;line-height: 52px;&quot; href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;SMEMA Logo&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/SMEMA-small.png&quot;&gt;&lt;/a&gt;
        ///            &lt;/div&gt;
        ///            &lt;div class=&quot;span8&quot;&gt;
        ///                &lt;p style=&quot;font-size:26px; line-height: 54px; text-align: center; margin: 0px;&quot;&gt;About&lt;/p&gt;
        ///            &lt;/div&gt;
        ///   [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsAbout {
            get {
                return ResourceManager.GetString("SettingsAbout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span4&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                &lt;/div&gt;
        ///                &lt;div class=&quot;span4&quot;&gt;
        ///                    &lt;p style=&quot;fon [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHome {
            get {
                return ResourceManager.GetString("SettingsHome", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///@functions
        ///{
        ///    string isCurrentVerb(string theVerb)
        ///    {
        ///        return (@Model.Extension.Model.Verb == theVerb) ? &quot;selected&quot; : &quot;&quot;;
        ///    }
        ///}
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///             [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClient {
            get {
                return ResourceManager.GetString("SettingsHttpClient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientAuthorisation {
            get {
                return ResourceManager.GetString("SettingsHttpClientAuthorisation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientBody {
            get {
                return ResourceManager.GetString("SettingsHttpClientBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientHeaders {
            get {
                return ResourceManager.GetString("SettingsHttpClientHeaders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///@functions {
        ///    public string NavLocationIsHome()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsHttpClient()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home + &quot;httpclient/&quot; ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsParams()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home + &quot;httpclient [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientNavigation {
            get {
                return ResourceManager.GetString("SettingsHttpClientNavigation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientQuery {
            get {
                return ResourceManager.GetString("SettingsHttpClientQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientResponse {
            get {
                return ResourceManager.GetString("SettingsHttpClientResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientSettings {
            get {
                return ResourceManager.GetString("SettingsHttpClientSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///@functions
        ///{
        ///    string isConnected(bool theValue)
        ///    {
        ///        return (theValue) ? &quot;&lt;i class=\&quot;icon-ok icon-large\&quot;&gt;&lt;/i&gt;&quot; : &quot;&lt;i class=\&quot;icon-remove icon-large\&quot;&gt;&lt;/i&gt;&quot;;
        ///    }
        ///
        ///    string isChecked(bool theValue)
        ///    {
        ///        return (theValue) ? &quot;checked&quot; : string.Empty;
        ///    }
        ///}
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpClientSubscription {
            get {
                return ResourceManager.GetString("SettingsHttpClientSubscription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///@functions
        ///{
        ///    string isCurrentVerb(string theVerb)
        ///    {
        ///        return (@Model.Extension.Model.Verb == theVerb) ? &quot;selected&quot; : &quot;&quot;;
        ///    }
        ///}
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///             [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpEndpoint {
            get {
                return ResourceManager.GetString("SettingsHttpEndpoint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///
        ///&lt;form action=&quot;&quot; method=&quot;post&quot; accept-charset=&quot;utf-8&quot; enctype=&quot;application/x-www-form-urlencoded&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; name=&quot;Guid&quot; value=&quot;@Model.Extension.Model.Guid&quot;&gt;
        ///    &lt;section class=&quot;row-fluid&quot;&gt;
        ///
        ///        &lt;div class=&quot;row-fluid&quot;&gt;
        ///            &lt;div class=&quot;box&quot;&gt;
        ///                &lt;div class=&quot;span2&quot;&gt;
        ///                    &lt;a href=&quot;@Raw(Model.Context.Paths.Home)&quot;&gt;&lt;img alt=&quot;Network Icon&quot; src=&quot;@Raw(Model.Context.Paths.Assets)images/networks.png&quot;&gt;&lt;/a&gt;
        ///                [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpEndpointAuthorisation {
            get {
                return ResourceManager.GetString("SettingsHttpEndpointAuthorisation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @model MultiPlug.Base.Http.EdgeApp
        ///@functions {
        ///    public string NavLocationIsHome()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsHttpEndpoint()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home + &quot;httpendpoint/&quot; ? &quot;active&quot; : string.Empty;
        ///    }
        ///
        ///    public string NavLocationIsAuthorisation()
        ///    {
        ///        return Model.Context.Paths.Current == Model.Context.Paths.Home +  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SettingsHttpEndpointNavigation {
            get {
                return ResourceManager.GetString("SettingsHttpEndpointNavigation", resourceCulture);
            }
        }
    }
}
