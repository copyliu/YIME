using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RimeApi
{
    public class UTF8Marshaler : ICustomMarshaler
    {
        static UTF8Marshaler static_instance;

        public IntPtr MarshalManagedToNative(object managedObj)
        {
            if (managedObj == null)
                return IntPtr.Zero;
            if (!(managedObj is string))
                throw new MarshalDirectiveException(
                    "UTF8Marshaler must be used on a string.");

            // not null terminated
            byte[] strbuf = Encoding.UTF8.GetBytes((string)managedObj);
            IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length + 1);
            Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

            // write the terminating null
            Marshal.WriteByte(buffer + strbuf.Length, 0);
            return buffer;
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            byte* walk = (byte*)pNativeData;

            // find the end of the string
            while (*walk != 0)
            {
                walk++;
            }
            int length = (int)(walk - (byte*)pNativeData);

            // should not be null terminated
            byte[] strbuf = new byte[length];
            // skip the trailing null
            Marshal.Copy((IntPtr)pNativeData, strbuf, 0, length);
            string data = Encoding.UTF8.GetString(strbuf);
            return data;
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public void CleanUpManagedData(object managedObj)
        {
        }

        public int GetNativeDataSize()
        {
            return -1;
        }

        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (static_instance == null)
            {
                return static_instance = new UTF8Marshaler();
            }
            return static_instance;
        }
    }
//    class UTF8StringConstantMarshaler : ICustomMarshaler
//    {
//        static UTF8StringConstantMarshaler _instance = new UTF8StringConstantMarshaler();
//
//        public static ICustomMarshaler GetInstance(string cookie = null) => _instance;
//
//        public IntPtr MarshalManagedToNative(object managedObject) => IntPtr.Zero;
//
//        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
//        {
//
//            byte* walk = (byte*)pNativeData;
//
//            // find the end of the string
//            while (*walk != 0)
//            {
//                walk++;
//            }
//            int length = (int)(walk - (byte*)pNativeData);
//
//            // should not be null terminated
//            byte[] strbuf = new byte[length];
//            // skip the trailing null
//            Marshal.Copy((IntPtr)pNativeData, strbuf, 0, length);
//            string data = Encoding.UTF8.GetString(strbuf);
//            return data;
//
//        }
//
//        public void CleanUpManagedData(object managedObject) { }
//        public void CleanUpNativeData(IntPtr nativeData) { }
//
//        public int GetNativeDataSize() => -1;
//    }
}
