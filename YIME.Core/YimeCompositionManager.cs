using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.InteropTypes;
using TSF.TypeLib;

namespace YIME.Core
{
    public class DisplayAttributeInfoEnum :  IEnumTfDisplayAttributeInfo
    {
        public HRESULT Clone(out IEnumTfDisplayAttributeInfo ppEnum)
        {
            throw new NotImplementedException();
        }

        public HRESULT Next(uint ulCount, ITfDisplayAttributeInfo[] rgInfo, out uint pcFetched)
        {
            throw new NotImplementedException();
        }

        public HRESULT Reset()
        {
            throw new NotImplementedException();
        }

        public HRESULT Skip(uint ulCount)
        {
            throw new NotImplementedException();
        }
    }
    public class YimeCompositionManager: ITfCompositionSink, ITfTextEditSink, ITfKeyEventSink
    {
        public HRESULT OnCompositionTerminated(uint ecWrite, ITfComposition pComposition)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnEndEdit(ITfContext pic, uint ecReadOnly, ITfEditRecord pEditRecord)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnSetFocus(int fForeground)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnTestKeyDown(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnTestKeyUp(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnKeyDown(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnKeyUp(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnPreservedKey(ITfContext pic, ref Guid rguid, out bool pfEaten)
        {
            throw new NotImplementedException();
        }

        public void Switch(ITfDocumentMgr pdimFocus, ITfDocumentMgr tfDocumentMgr)
        {
            throw new NotImplementedException();
        }
    }
}
