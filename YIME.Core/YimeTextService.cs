using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TSF.InteropTypes;
using TSF.TypeLib;

namespace YIME.Core
{
    [ComVisible(true)]
    [Guid("1B4A4B30-B810-4483-8CE6-A5FF82835999")]
    [ClassInterface(ClassInterfaceType.None)]
    public class YimeTextService : ITfTextInputProcessorEx, ITfThreadMgrEventSink, ITfThreadFocusSink, ITfDisplayAttributeProvider
    {
        HRESULT ITfTextInputProcessor.Activate(ITfThreadMgr ptim, uint tid)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfTextInputProcessorEx.Deactivate()
        {
            throw new NotImplementedException();
        }

        public HRESULT ActivateEx(ITfThreadMgr ptim, uint tid, TF_TMAE dwFlags)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfTextInputProcessorEx.Activate(ITfThreadMgr ptim, uint tid)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfTextInputProcessor.Deactivate()
        {
            throw new NotImplementedException();
        }

        public HRESULT OnInitDocumentMgr(ITfDocumentMgr pdim)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnUninitDocumentMgr(ITfDocumentMgr pdim)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnSetFocus(ITfDocumentMgr pdimFocus, ITfDocumentMgr pdimPrevFocus)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnPushContext(ITfContext pic)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnPopContext(ITfContext pic)
        {
            throw new NotImplementedException();
        }

        public HRESULT OnSetThreadFocus()
        {
            throw new NotImplementedException();
        }

        public HRESULT OnKillThreadFocus()
        {
            throw new NotImplementedException();
        }

        public HRESULT EnumDisplayAttributeInfo(out IEnumTfDisplayAttributeInfo ppEnum)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetDisplayAttributeInfo(ref Guid guid, out ITfDisplayAttributeInfo ppInfo)
        {
            throw new NotImplementedException();
        }
    }
}
