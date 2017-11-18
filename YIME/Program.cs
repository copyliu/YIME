using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TSF.InteropTypes;
using TSF.TypeLib;

namespace YIME
{
    class Program
    {
        private static Guid CLSID_TEXTSERVICE = new Guid("1B4A4B30-B810-4483-8CE6-A5FF82835999");
        private static Guid GUID_PROFILE = new Guid("661B35E7-4637-47F3-8E13-6CEA1E9C6E7E");
        private static string name = "一个输入法";
        static Guid[] SupportCategories = new[]{
            TSF.TypeLib.Guids.GUID_TFCAT_TIP_KEYBOARD,
            TSF.TypeLib.Guids.GUID_TFCAT_DISPLAYATTRIBUTEPROVIDER,
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_UIELEMENTENABLED,
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_SECUREMODE,
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_COMLESS,
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_INPUTMODECOMPARTMENT,
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_IMMERSIVESUPPORT, // WIN8 ONLY
            TSF.TypeLib.Guids.GUID_TFCAT_TIPCAP_SYSTRAYSUPPORT, // WIN8 ONLY
        };
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower() == "uninstall")
            {
                Unregister();
            }
            else
            {
                Register();
            }
            
        }
        [STAThread]
        public static void Unregister()
        {

            ITfCategoryMgr c = new ITfCategoryMgr();
            foreach (var supportCategory in SupportCategories)
            {
                var r = c.UnregisterCategory(CLSID_TEXTSERVICE, supportCategory, CLSID_TEXTSERVICE);
                Console.WriteLine(r.ToString());
            }
            ITfInputProcessorProfiles p = new ITfInputProcessorProfiles();
            var result = p.Unregister(CLSID_TEXTSERVICE);

            Console.WriteLine(result.ToString());
            var regservice = new RegistrationServices();
            regservice.UnregisterAssembly(Assembly.LoadFrom("YIME.Core.dll"));
        }
        [STAThread]
        public static void Register()
        {
            var regservice = new RegistrationServices();
            
            regservice.RegisterAssembly(Assembly.LoadFrom("YIME.Core.dll"),
                AssemblyRegistrationFlags.SetCodeBase);
            var iconname = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                "YIME.Core.dll");
            Console.WriteLine(iconname);
            Console.WriteLine(CultureInfo.GetCultureInfo("zh-cn").LCID);
            //Insert code here.
            var p = new ITfInputProcessorProfiles();
            var result = p.Register(CLSID_TEXTSERVICE);

            Console.WriteLine(result.ToString());
            result = p.AddLanguageProfile(CLSID_TEXTSERVICE, new LangID((ushort)CultureInfo.GetCultureInfo("zh-cn").LCID), GUID_PROFILE,
                name.ToCharArray(), (uint)name.ToCharArray().Length, iconname.ToCharArray(), (uint)iconname.Length, 0);

            Console.WriteLine(result.ToString());
            var c = new ITfCategoryMgr();
            foreach (var supportCategory in SupportCategories)
            {
                var r = c.RegisterCategory(CLSID_TEXTSERVICE, supportCategory, CLSID_TEXTSERVICE);
                Console.WriteLine(r.ToString());
            }

        }

    }
}
