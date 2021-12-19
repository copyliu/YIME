using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPinyinApi
{
    public class PinyinContext:IDisposable
    {
      
        internal readonly IntPtr ContextPtr;
        
        private bool disposedValue;


        public PinyinContext():this("data","user")
        {
          
        }
        public PinyinContext(string datadir,string userdir)
        {

            ContextPtr = LibPinyinDll.pinyin_init(datadir, userdir);
            LibPinyinDll.pinyin_set_options(ContextPtr, PinyinOption.PINYIN_INCOMPLETE |
                                                              PinyinOption.PINYIN_CORRECT_ALL | PinyinOption.USE_DIVIDED_TABLE | PinyinOption.USE_RESPLIT_TABLE |
                                                              PinyinOption.DYNAMIC_ADJUST);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public PinyinInput CreateNewInput()
        {
            return new PinyinInput(this);
        }

      
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LibPinyinDll.pinyin_mask_out(ContextPtr, 0, 0);
                    LibPinyinDll.pinyin_save(ContextPtr);
                    LibPinyinDll.pinyin_fini(ContextPtr);
                }
                 
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Pinyin()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            LibPinyinDll.pinyin_save(ContextPtr);
        }
    }
}
