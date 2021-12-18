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
        private uint _clientid;
        private ITfContext actionContext;
        public YimeCompositionManager(uint clientid)
        {
            this._clientid = clientid;
        }
        public HRESULT OnCompositionTerminated(uint ecWrite, ITfComposition pComposition)
        {
            return new HRESULT(){Code = 0};
        }

        public HRESULT OnEndEdit(ITfContext pic, uint ecReadOnly, ITfEditRecord pEditRecord)
        {
            return new HRESULT() { Code =0 };
        }

        public HRESULT OnSetFocus(int fForeground)
        {
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnTestKeyDown(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            pfEaten = false;
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnTestKeyUp(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            pfEaten = false;
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnKeyDown(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            pfEaten = false;
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnKeyUp(ITfContext pic, UIntPtr wParam, IntPtr lParam, out bool pfEaten)
        {
            pfEaten = false;
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnPreservedKey(ITfContext pic, ref Guid rguid, out bool pfEaten)
        {
            pfEaten = false;
            return new HRESULT() { Code = 0 };
        }

        public void Switch(ITfDocumentMgr pdimFocus, ITfDocumentMgr pdimPrevFocus)
        {
            if (pdimPrevFocus != null)
            {
                pdimPrevFocus.GetTop(out actionContext);
                var session=new YimeEditSession();
                actionContext.RequestEditSession(_clientid, session, TF_ES.TF_ES_SYNC | TF_ES.TF_ES_READWRITE, out _);
            }
         

        }
    }
}
