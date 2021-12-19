using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPinyinApi
{
    public   sealed class PinyinInput : IDisposable
    {
        private readonly PinyinContext _context;
        private readonly IntPtr _instance;
        private bool disposedValue;

        private int pinyin_length;
        private int offset;
        private IntPtr[] _candidate;
      
        public string[] CandiateList;
        internal PinyinInput(PinyinContext context)
        {
            _context = context;
            _instance = LibPinyinDll.pinyin_alloc_instance(context.ContextPtr);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    LibPinyinDll.pinyin_free_instance(_instance);
                    _context.Save();
                 
                }

                disposedValue = true;
            }
        }

        public int SetInput(string pinyin, string prefix)
        {
            LibPinyinDll.pinyin_reset(_instance);
            offset = 0;
             pinyin_length = LibPinyinDll.pinyin_parse_more_full_pinyins(_instance, pinyin);
            LibPinyinDll.pinyin_guess_sentence_with_prefix(_instance, prefix);
            UpdateCandidateList();
            return pinyin_length;
        }

        public void SetOffset(int newoffset)
        {
            offset = newoffset;
            UpdateCandidateList();
            
        }
        public string Getauxiliary()
        {
            IntPtr ptr = IntPtr.Zero;
            LibPinyinDll.pinyin_get_full_pinyin_auxiliary_text(_instance, offset, ref ptr);
            var s=Utf8Marshaler.FromNative(ptr);
            LibPinyinDll.g_free(ptr);
            return s;
        }
        public int ChooseCandidate(int num)
        {
            offset=LibPinyinDll.pinyin_choose_candidate(_instance, offset, _candidate[num]);
           
            UpdateCandidateList();
            return offset;
        }

        public void Submit()
        {
            LibPinyinDll.pinyin_train(_instance, 0);

        }
        private void UpdateCandidateList()
        {
            LibPinyinDll.pinyin_guess_candidates(_instance, offset, 1);
            var num = 0;
            LibPinyinDll.pinyin_get_n_candidate(_instance, ref num);
            var result = new string[num];
            _candidate = new IntPtr[num];
            for (int i = 0; i < num; i++) 
            {
                var ptr = IntPtr.Zero;
                LibPinyinDll.pinyin_get_candidate(_instance, i, ref ptr);
                _candidate[i] = ptr;
                var wordptr = IntPtr.Zero;
                LibPinyinDll.pinyin_get_candidate_string(_instance, ptr, ref wordptr);
                var s = Utf8Marshaler.FromNative(wordptr);
                result[i] = s;
            } 
            CandiateList= result; 
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
