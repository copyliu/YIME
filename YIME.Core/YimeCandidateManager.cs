using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.InteropTypes;
using TSF.TypeLib;

namespace YIME.Core
{
    public class YimeCandidateManager: ITfCandidateListUIElement
    {
        HRESULT ITfUIElement.GetDescription(out string pbstrDescription)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfCandidateListUIElement.GetGUID(out Guid pguid)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfCandidateListUIElement.Show(bool bShow)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfCandidateListUIElement.IsShown(out bool pbShow)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetUpdatedFlags(out TF_CLUIE pdwFlags)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetDocumentMgr(out ITfDocumentMgr ppdim)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetCount(out uint puCount)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetSelection(out uint puIndex)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetString(uint uIndex, out string pstr)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetPageIndex(uint[] pIndex, uint uSize, out uint puPageCnt)
        {
            throw new NotImplementedException();
        }

        public HRESULT SetPageIndex(ref uint pIndex, uint uPageCnt)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetCurrentPage(out uint puPage)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfCandidateListUIElement.GetDescription(out string pbstrDescription)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfUIElement.GetGUID(out Guid pguid)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfUIElement.Show(bool bShow)
        {
            throw new NotImplementedException();
        }

        HRESULT ITfUIElement.IsShown(out bool pbShow)
        {
            throw new NotImplementedException();
        }

        public void SetDocumentMgr(ITfDocumentMgr pdimFocus)
        {
            throw new NotImplementedException();
        }
    }
}
