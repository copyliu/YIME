using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RimeApi
{

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeTraits
    {
        public int data_size;
//        [MarshalAs(UnmanagedType.LPUTF8Str)]  <-坑貨!
        public IntPtr shared_data_dir_p;
        public string shared_data_dir
        {
            get => Common.StringFromNativeUtf8(this.shared_data_dir_p);
            set => this.shared_data_dir_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr user_data_dir_p;
        public string user_data_dir
        {
            get => Common.StringFromNativeUtf8(this.user_data_dir_p);
            set => this.user_data_dir_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr distribution_name_p;
        public string distribution_name
        {
            get => Common.StringFromNativeUtf8(this.distribution_name_p);
            set => this.distribution_name_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr distribution_code_name_p;
        public string distribution_code_name
        {
            get => Common.StringFromNativeUtf8(this.distribution_code_name_p);
            set => this.distribution_code_name_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr distribution_version_p;
        public string distribution_version
        {
            get => Common.StringFromNativeUtf8(this.distribution_version_p);
            set => this.distribution_version_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        //public string app_name;
        public IntPtr app_name_p;
        public string app_name
        {
            get => Common.StringFromNativeUtf8(this.app_name_p);
            set => this.app_name_p = Common.NativeUtf8FromString(value);
        }
        //! A list of modules to load before initializing
        //const char** modules;
        public IntPtr modules;
        /// <summary>
        /// 必须先调用此方法
        /// </summary>
        public void Init ()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeTraits)) -Marshal.SizeOf(typeof(int));
        }
       



    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeComposition
    {
        
        public int length;
        public int cursor_pos;
        public int sel_start;
        public int sel_end;
        
        public IntPtr preedit_p;
        public string preedit
        {
            get => Common.StringFromNativeUtf8(this.preedit_p);
            set => this.preedit_p = Common.NativeUtf8FromString(value);
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeCandidate
    {
        
        public IntPtr text_p;

        public string text
        {
            get => Common.StringFromNativeUtf8(this.text_p);
            set => this.text_p = Common.NativeUtf8FromString(value);
        }

        public IntPtr comment_p;
        public string comment
        {
            get { return Common.StringFromNativeUtf8(this.comment_p); }
            set { this.comment_p = Common.NativeUtf8FromString(value); }
        }
        public IntPtr reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeMenu
    {
        public int page_size;
        public int page_no;
        public bool is_last_page;
        public int highlighted_candidate_index;
        public int num_candidates;
        
        public IntPtr candidates;
        public IntPtr select_keys_p;
        public string select_keys
        {
            get => Common.StringFromNativeUtf8(this.select_keys_p);
            set => this.select_keys_p = Common.NativeUtf8FromString(value);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeCommit
    {
        public int data_size;

        public IntPtr text_p;
        public string text
        {
            get => Common.StringFromNativeUtf8(this.text_p);
            set => this.text_p = Common.NativeUtf8FromString(value);
        }
        /// <summary>
        /// 必须执行一次初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeCommit)) - Marshal.SizeOf(typeof(int));

        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeContext
    {
        public int data_size;
        [MarshalAs(UnmanagedType.Struct)]
        public RimeComposition composition;
        [MarshalAs(UnmanagedType.Struct)]
        public RimeMenu menu;
        
        public IntPtr commit_text_preview_p;
        public string commit_text_preview
        {
            get => Common.StringFromNativeUtf8(this.commit_text_preview_p);
            set => this.commit_text_preview_p = Common.NativeUtf8FromString(value);
        }
        //char** select_labels;
        public IntPtr select_labels_p;
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeContext)) - Marshal.SizeOf(typeof(int));

        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeStatus
    {
        public int data_size;
        public IntPtr schema_id_p;
        public string schema_id
        {
            get => Common.StringFromNativeUtf8(this.schema_id_p);
            set => this.schema_id_p = Common.NativeUtf8FromString(value);
        }
//                [MarshalAs(UnmanagedType.LPUTF8Str)]
//                public string schema_name;
        public IntPtr schema_name_p;
        public string schema_name
        {
            get => Common.StringFromNativeUtf8(this.schema_name_p);
            set => this.schema_name_p = Common.NativeUtf8FromString(value);
        }
        public bool is_disabled;
        public bool is_composing;
        public bool is_ascii_mode;
        public bool is_full_shape;
        public bool is_simplified;
        public bool is_traditional;
        public bool is_ascii_punct;
        /// <summary>
        /// 必须执行初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeStatus)) - Marshal.SizeOf(typeof(int));

        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeCandidateListIterator
    {
        public IntPtr ptr;
        public int index;
        [MarshalAs(UnmanagedType.Struct)]
        RimeCandidate candidate;
       
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeConfig
    {
        public IntPtr ptr;
      

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeConfigIterator
    {
        public IntPtr list;
        public IntPtr map;
        public int index;

//        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr key_p;

        public string key
        {
            get => Common.StringFromNativeUtf8(this.key_p);
            set => this.key_p = Common.NativeUtf8FromString(value);
        }
        //        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public IntPtr path_p;
        public string path
        {
            get => Common.StringFromNativeUtf8(this.path_p);
            set => this.path_p = Common.NativeUtf8FromString(value);
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeSchemaListItem
    {
        public IntPtr schema_id_p;
        public string schema_id
        {
            get => Common.StringFromNativeUtf8(this.schema_id_p);
            set => this.schema_id_p = Common.NativeUtf8FromString(value);
        }
        public IntPtr schema_name_p;
        public string schema_name
        {
            get => Common.StringFromNativeUtf8(this.schema_name_p);
            set => this.schema_name_p = Common.NativeUtf8FromString(value);
        }
        public IntPtr reserved;
       

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeSchemaList
    {
        //FIXME: size_t size;
        public int size;

        [MarshalAs(UnmanagedType.LPArray)]
        public RimeSchemaListItem[] list;
       


    }
}
