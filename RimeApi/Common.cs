using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RimeApi
{
    public class Common
    {
        public static IntPtr IntPtrFromStuctArray<T>(T[] InputArray) where T : new()
        {
            int size = InputArray.Length;
            T[] resArray = new T[size];
            //IntPtr[] InPointers = new IntPtr[size];
            int dim = IntPtr.Size * size;
            IntPtr rRoot = Marshal.AllocCoTaskMem(Marshal.SizeOf(InputArray[0]) * size);
            for (int i = 0; i < size; i++)
            {
                Marshal.StructureToPtr(InputArray[i], (IntPtr)(rRoot.ToInt32() +
                                                               i * Marshal.SizeOf(InputArray[i])), false);
            }
            return rRoot;
        }

        public static T[] StuctArrayFromIntPtr<T>(IntPtr outArray, int size) where T : new()
        {
            T[] resArray = new T[size];
            IntPtr current = outArray;
            for (int i = 0; i < size; i++)
            {
                resArray[i] = new T();
                resArray[i]=(T)Marshal.PtrToStructure(current, typeof(T));
                Marshal.DestroyStructure(current, typeof(T));
                int structsize = Marshal.SizeOf(resArray[i]);
                current = (IntPtr)((long)current + structsize);
            }
            Marshal.FreeCoTaskMem(outArray);
            return resArray;
        }
        public static IntPtr NativeUtf8FromString(string managedString)
        {
            int len = Encoding.UTF8.GetByteCount(managedString);
            byte[] buffer = new byte[len + 1];
            Encoding.UTF8.GetBytes(managedString, 0, managedString.Length, buffer, 0);
            IntPtr nativeUtf8 = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, nativeUtf8, buffer.Length);
            return nativeUtf8;
        }

        public static unsafe string StringFromNativeUtf8(IntPtr nativeutf8)
        {
            if (nativeutf8 == IntPtr.Zero) return null;
            byte* bytes = (byte*) nativeutf8.ToPointer();
            int size = 0;
            while (bytes[size] != 0)
            {
                ++size;
            }
            byte[] buffer = new byte[size];
            Marshal.Copy(nativeutf8, buffer, 0, size);
            return Encoding.UTF8.GetString(buffer);
        }
//
//        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
//        {
//            int len = 0;
//            while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
//            byte[] buffer = new byte[len];
//            Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
//            return Encoding.UTF8.GetString(buffer);
//        }
    }
}