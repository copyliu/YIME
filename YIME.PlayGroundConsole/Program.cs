using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RimeApi;

namespace YIME.PlayGroundConsole
{


    class Program
    {
        static void Main(string[] args)
        {
//            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            RimeTraits traits = new RimeTraits();
            traits.app_name = "YIME.PlayGround";
            
            Rime.RimeSetup(ref traits);
            Console.WriteLine("初始化1");
            Rime.RimeInitialize(IntPtr.Zero);
            Console.WriteLine("初始化2");
            Rime.RimeSetNotificationHandler(Handler, IntPtr.Zero);
            if (Rime.RimeStartMaintenance(true))
            {

                Rime.RimeJoinMaintenanceThread();

            }


            var rimesessionid = Rime.RimeCreateSession();
            Console.WriteLine("Session ID: " + rimesessionid);
            Rime.RimeSimulateKeySequence(rimesessionid, "maomishurufazhunbeijiuxu");
            var commit = new RimeCommit();
            var status = new RimeStatus();
            var context = new RimeContext();
            commit.Init();
            status.Init();
            context.Init();

            Rime.RimeGetCommit(rimesessionid, ref commit);
            Rime.RimeGetStatus(rimesessionid, ref status);
            Rime.RimeGetContext(rimesessionid, ref context);
            Console.WriteLine("Commit:" + commit.text);
            Console.WriteLine($"Status:" + status.schema_name);
            Console.WriteLine("Context:");
            var candiates =
                RimeApi.Common.StuctArrayFromIntPtr<RimeCandidate>(context.menu.candidates,
                    context.menu.num_candidates);
            for (int i = 0; i < context.menu.num_candidates; i++)
            {
                Console.WriteLine($"{i}: {candiates[i].text}");
            }



        }

        private static void Handler(IntPtr contextObject, UIntPtr sessionId, IntPtr messageType, IntPtr messageValue)
        {
            string t = Common.StringFromNativeUtf8(messageType);
            string v = Common.StringFromNativeUtf8(messageValue);
            Console.WriteLine($"Call Handler: {t}: {v}");
        }
    }
}
