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

        [MarshalAs(UnmanagedType.CustomMarshaler,MarshalTypeRef = typeof(UTF8Marshaler))]
        public string shared_data_dir;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string user_data_dir;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string distribution_name;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string distribution_code_name;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string distribution_version;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string app_name;
        //! A list of modules to load before initializing
        //const char** modules;
        public IntPtr modules;

        /// <summary>
        /// 必须执行一次初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeTraits))-Marshal.SizeOf(typeof(int));

        }



    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeComposition
    {
        
        public int length;
        public int cursor_pos;
        public int sel_start;
        public int sel_end;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string preedit;

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeCandidate
    {
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string text;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string comment;
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
        [MarshalAs(UnmanagedType.LPStruct)]
        public RimeCandidate candidates;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string select_keys;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RimeCommit
    {
        public int data_size;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string text;
        /// <summary>
        /// 必须执行一次初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeTraits)) - Marshal.SizeOf(typeof(int));

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
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string commit_text_preview;
        //char** select_labels;
        public IntPtr select_labels;
        /// <summary>
        /// 必须执行一次初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeTraits)) - Marshal.SizeOf(typeof(int));

        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeStatus
    {
        public int data_size;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string schema_id;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string schema_name;
        public bool is_disabled;
        public bool is_composing;
        public bool is_ascii_mode;
        public bool is_full_shape;
        public bool is_simplified;
        public bool is_traditional;
        public bool is_ascii_punct;
        /// <summary>
        /// 必须执行一次初始化
        /// </summary>
        public void Init()
        {
            this.data_size = Marshal.SizeOf(typeof(RimeTraits)) - Marshal.SizeOf(typeof(int));

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

        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringConstantMarshaler))]
        public string key;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringConstantMarshaler))]
        public string path;
      

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RimeSchemaListItem
    {
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string schema_id;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))]
        public string schema_name;
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
