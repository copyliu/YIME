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
        private ITfThreadMgr _threadmgr;
        private YimeCandidateManager _candidateManager;
        private YimeCompositionManager _compositionManager;
        private uint _clientid;
        private TF_TMAE _flag;
        public static Guid GUID_DISPLAY_ARRTIBUTE_INFO=new Guid("78180C09-3081-4399-B3E9-B318333003D0");

        private uint _threadmgreventsinkCookie;
        private uint _threadfocussinkCookie;
        private uint _texteditsinkCookie;
        //是否需要状态条? statusWindow
        HRESULT ITfTextInputProcessor.Activate(ITfThreadMgr ptim, uint tid)
        {
            return this.ActivateEx(ptim, tid, 0);
        }

        HRESULT ITfTextInputProcessorEx.Deactivate()
        {
            UnInitKeyEventSink();
            UnInitThreadFocusSink();
            UnInitThreadMgrEventSink();
            if (_threadmgr != null)
            {
                Marshal.ReleaseComObject(_threadmgr);
            }
            return new HRESULT() {Code = 0};
        }

        public HRESULT ActivateEx(ITfThreadMgr ptim, uint tid, TF_TMAE dwFlags)
        {
            _candidateManager=new YimeCandidateManager();
            _compositionManager=new YimeCompositionManager(0);
            _threadmgr = ptim;
            _clientid = tid;
            _flag = dwFlags;
            InitThreadMgrEventSink();
            InitThreadFocusSink();
            InitKeyEventSink(_compositionManager,_clientid);

            return new HRESULT(){Code = 0};
        }

        private bool InitKeyEventSink(YimeCompositionManager compositionManager, uint clientid)
        {
            ITfKeystrokeMgr keystrokemgr = _threadmgr as ITfKeystrokeMgr;
            if (keystrokemgr == null) return false;
            var result = keystrokemgr.AdviseKeyEventSink(_clientid, compositionManager,1);
            Marshal.ReleaseComObject(keystrokemgr);
            return result;
        }

        private void UnInitKeyEventSink()
        {
            ITfKeystrokeMgr source = _threadmgr as ITfKeystrokeMgr;
            if (source == null) return;
            var result = source.UnadviseKeyEventSink(_clientid);
            Marshal.ReleaseComObject(source);
        }

        private bool InitThreadFocusSink()
        {
            ITfSource source = _threadmgr as ITfSource;
            if (source == null) return false;
            var result = source.AdviseSink(typeof(ITfThreadMgrEventSink).GUID, this, out _threadfocussinkCookie);
            Marshal.ReleaseComObject(source);
            return result;
        }
        private void UnInitThreadFocusSink()
        {
            ITfSource source = _threadmgr as ITfSource;
            if (source == null) return ;
            var result = source.UnadviseSink(_threadfocussinkCookie);
            Marshal.ReleaseComObject(source);
           
        }

        private bool InitThreadMgrEventSink()
        {

            ITfSource source = _threadmgr as ITfSource;
            if (source == null) return false;
            var result=source.AdviseSink(typeof(ITfThreadMgrEventSink).GUID, this, out _threadmgreventsinkCookie);
            Marshal.ReleaseComObject(source);
            return result;
            

        }

        private void UnInitThreadMgrEventSink()
        {
            ITfSource source = _threadmgr as ITfSource;
            if (source == null) return ;
            source.UnadviseSink(_threadmgreventsinkCookie);
            Marshal.ReleaseComObject(source);
            

        }
        HRESULT ITfTextInputProcessorEx.Activate(ITfThreadMgr ptim, uint tid)
        {
            return this.ActivateEx(ptim, tid, 0);
        }

        HRESULT ITfTextInputProcessor.Deactivate()
        {
            UnInitKeyEventSink();
            UnInitThreadFocusSink();
            UnInitThreadMgrEventSink();
            if (_threadmgr != null)
            {
                Marshal.ReleaseComObject(_threadmgr);
            }
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnInitDocumentMgr(ITfDocumentMgr pdim)
        {
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnUninitDocumentMgr(ITfDocumentMgr pdim)
        {
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnSetFocus(ITfDocumentMgr pdimFocus, ITfDocumentMgr pdimPrevFocus)
        {

            _compositionManager.Switch(pdimFocus, pdimPrevFocus);
            _candidateManager.SetDocumentMgr(pdimFocus);

            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnPushContext(ITfContext pic)
        {
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnPopContext(ITfContext pic)
        {
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnSetThreadFocus()
        {
            //状态栏显示 好像不需要
            return new HRESULT() { Code = 0 };
        }

        public HRESULT OnKillThreadFocus()
        {
            //状态栏隐藏 好像不需要
            return new HRESULT() { Code = 0 };
        }

        public HRESULT EnumDisplayAttributeInfo(out IEnumTfDisplayAttributeInfo ppEnum)
        {
            ppEnum = new DisplayAttributeInfoEnum();
            return new HRESULT() { Code = 0 };
        }

        public HRESULT GetDisplayAttributeInfo(ref Guid guid, out ITfDisplayAttributeInfo ppInfo)
        {
            if (guid == GUID_DISPLAY_ARRTIBUTE_INFO)
            {
                ppInfo = new DisplayAttributeInfo();
                return new HRESULT() {Code = 0};
            }
            else
            {
                ppInfo = null;
                return new HRESULT(){Code=  unchecked((int) 0x80070057) };
            }
            
        }
    }
}
