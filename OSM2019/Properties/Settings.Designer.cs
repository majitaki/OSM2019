﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OSM2019.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\majit\\Anaconda3\\Scripts\\activate.bat ")]
        public string AnacondaPath {
            get {
                return ((string)(this["AnacondaPath"]));
            }
            set {
                this["AnacondaPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AnacondaEnv {
            get {
                return ((string)(this["AnacondaEnv"]));
            }
            set {
                this["AnacondaEnv"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("./Resources/PythonScript/GraphGenerator/")]
        public string GraphGeneratorFolderPath {
            get {
                return ((string)(this["GraphGeneratorFolderPath"]));
            }
            set {
                this["GraphGeneratorFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("./Resources/PythonScript/LayoutGenerator/")]
        public string LayoutGeneratorFolderPath {
            get {
                return ((string)(this["LayoutGeneratorFolderPath"]));
            }
            set {
                this["LayoutGeneratorFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\Anaconda3\\Scripts\\activate.bat")]
        public string lab_path {
            get {
                return ((string)(this["lab_path"]));
            }
            set {
                this["lab_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G:\\ProgramData\\Anaconda3\\Scripts\\activate.bat")]
        public string home_path {
            get {
                return ((string)(this["home_path"]));
            }
            set {
                this["home_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\ProgramData\\Anaconda3\\Scripts\\activate.bat")]
        public string kaigi_path {
            get {
                return ((string)(this["kaigi_path"]));
            }
            set {
                this["kaigi_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\majit\\Anaconda3\\Scripts\\activate.bat ")]
        public string note_path {
            get {
                return ((string)(this["note_path"]));
            }
            set {
                this["note_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("graph.json")]
        public string RawGraphFile {
            get {
                return ((string)(this["RawGraphFile"]));
            }
            set {
                this["RawGraphFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("./Working/")]
        public string WorkingFolderPath {
            get {
                return ((string)(this["WorkingFolderPath"]));
            }
            set {
                this["WorkingFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("graph_flag")]
        public string RawGraphFlag {
            get {
                return ((string)(this["RawGraphFlag"]));
            }
            set {
                this["RawGraphFlag"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("position.csv")]
        public string LayoutFile {
            get {
                return ((string)(this["LayoutFile"]));
            }
            set {
                this["LayoutFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("layout_flag")]
        public string LayoutFlag {
            get {
                return ((string)(this["LayoutFlag"]));
            }
            set {
                this["LayoutFlag"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tmp_graph.json")]
        public string TmpGraphFile {
            get {
                return ((string)(this["TmpGraphFile"]));
            }
            set {
                this["TmpGraphFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("./OutputLog")]
        public string OutputLogPath {
            get {
                return ((string)(this["OutputLogPath"]));
            }
            set {
                this["OutputLogPath"] = value;
            }
        }
    }
}
