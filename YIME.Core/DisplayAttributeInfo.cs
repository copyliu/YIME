using System;
using TSF.InteropTypes;
using TSF.TypeLib;

namespace YIME.Core
{
    public class DisplayAttributeInfo : ITfDisplayAttributeInfo
    {
        public HRESULT GetGUID(out Guid pguid)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetDescription(out string pbstrDesc)
        {
            throw new NotImplementedException();
        }

        public HRESULT GetAttributeInfo(out TF_DISPLAYATTRIBUTE pda)
        {
            throw new NotImplementedException();
        }

        public HRESULT SetAttributeInfo(ref TF_DISPLAYATTRIBUTE pda)
        {
            throw new NotImplementedException();
        }

        public HRESULT Reset()
        {
            throw new NotImplementedException();
        }
    }
}