using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibPinyinApi
{
  
    internal static class Utf8Marshaler  
    {
        private static readonly Encoding Encoding = new UTF8Encoding(false, false);
 

        public static unsafe string FromNative(char* pNativeData)
        {
            return FromNative(Encoding, (byte*)pNativeData);
        }

        public static string FromNative(IntPtr pNativeData)
        {
            return FromNative(Encoding, pNativeData);
        }

        public static string FromNative(IntPtr pNativeData, int length)
        {
            return FromNative(Encoding, pNativeData, length);
        }

        public static string FromBuffer(byte[] buffer)
        {
            return FromBuffer(Encoding, buffer);
        }

        public static string FromBuffer(byte[] buffer, int length)
        {
            return FromBuffer(Encoding, buffer, length);
        }

        public static unsafe string FromNative(Encoding encoding, IntPtr pNativeData)
        {
            return FromNative(encoding, (byte*)pNativeData);
        }

        public static unsafe string FromNative(Encoding encoding, byte* pNativeData)
        {
            if (pNativeData == null)
            {
                return null;
            }

            var start = (byte*)pNativeData;
            var walk = start;

            // Find the end of the string
            while (*walk != 0)
            {
                walk++;
            }

            if (walk == start)
            {
                return string.Empty;
            }

            return new string((sbyte*)pNativeData, 0, (int)(walk - start), encoding);
        }

        public static unsafe string FromNative(Encoding encoding, IntPtr pNativeData, int length)
        {
            if (pNativeData == IntPtr.Zero)
            {
                return null;
            }

            if (length == 0)
            {
                return string.Empty;
            }

            return new string((sbyte*)pNativeData.ToPointer(), 0, length, encoding);
        }

        public static string FromBuffer(Encoding encoding, byte[] buffer)
        {
          
            var length = 0;
            var stop = buffer.Length;

            while (length < stop &&
                   0 != buffer[length])
            {
                length++;
            }

            return FromBuffer(encoding, buffer, length);
        }

        public static string FromBuffer(Encoding encoding, byte[] buffer, int length)
        {
            Debug.Assert(buffer != null);

            if (length == 0)
            {
                return string.Empty;
            }

            return encoding.GetString(buffer, 0, length);
        }
    }
}
