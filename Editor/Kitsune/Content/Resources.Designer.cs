//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kitsune.Content {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Kitsune.Content.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] FontSmall {
            get {
                object obj = ResourceManager.GetObject("FontSmall", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] FontUpper {
            get {
                object obj = ResourceManager.GetObject("FontUpper", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to exit without saving the current changes?.
        /// </summary>
        public static string MB_ExitMsg {
            get {
                return ResourceManager.GetString("MB_ExitMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exit without saving.
        /// </summary>
        public static string MB_ExitTitle {
            get {
                return ResourceManager.GetString("MB_ExitTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _File.
        /// </summary>
        public static string MI_File {
            get {
                return ResourceManager.GetString("MI_File", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _Exit.
        /// </summary>
        public static string MI_FileExit {
            get {
                return ResourceManager.GetString("MI_FileExit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _New.
        /// </summary>
        public static string MI_FileNew {
            get {
                return ResourceManager.GetString("MI_FileNew", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _Open.
        /// </summary>
        public static string MI_FileOpen {
            get {
                return ResourceManager.GetString("MI_FileOpen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _Save.
        /// </summary>
        public static string MI_FileSave {
            get {
                return ResourceManager.GetString("MI_FileSave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creates a new game project.
        /// </summary>
        public static string TT_New {
            get {
                return ResourceManager.GetString("TT_New", resourceCulture);
            }
        }
    }
}
