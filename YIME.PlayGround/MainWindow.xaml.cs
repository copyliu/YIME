using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using RimeApi;


namespace YIME.PlayGround
{
  
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UIntPtr rimesessionid;
        private KeyConverter converter;
        private List<string> sblist=new List<string>();
        private string baiduresult = "";
        Regex r=new Regex("[a-z]*");
        public MainWindow()
        {
            InitializeComponent();
            converter=new KeyConverter();
            RimeTraits traits = new RimeTraits();
            traits.app_name = "YIME.PlayGround";

            Rime.RimeSetup(ref traits);
            Console.WriteLine("初始化1");
            Rime.RimeInitialize(IntPtr.Zero);
            Console.WriteLine("初始化2");
            if (Rime.RimeStartMaintenance(true))
            {

                Rime.RimeJoinMaintenanceThread();

            }

            
            rimesessionid = Rime.RimeCreateSession();
            Console.WriteLine("Session ID: " + rimesessionid);
            this.KeyDown+=OnKeyDown;


        }

        string update()
        {

            var commit = new RimeCommit();
            var status = new RimeStatus();
            var context = new RimeContext();
            commit.Init();
            status.Init();
            context.Init();

            Rime.RimeGetCommit(rimesessionid, ref commit);
            Rime.RimeGetStatus(rimesessionid, ref status);
            Rime.RimeGetContext(rimesessionid, ref context);
            if (!string.IsNullOrEmpty(commit.text))
            {
                this.tb.Text = "";
                this.lb.Content = commit.text;
            }
            if (status.is_composing)
            {
                
                this.lb.Content = context.composition.preedit;
                var candiates =
                    RimeApi.Common.StuctArrayFromIntPtr<RimeCandidate>(context.menu.candidates,
                        context.menu.num_candidates);
                sblist.Clear();
                
                for (int i = 0; i < context.menu.num_candidates; i++)
                {
                    sblist.Add(candiates[i].text);
                    
                }
                updatelistdisplay();

            }
            else
            {
                this.tb.Text = "";
            }

            return context.composition.preedit;
        }

        void updatelistdisplay()
        {
            StringBuilder sb=new StringBuilder();
            int i = 1;
            foreach (var v in sblist)
            {
                sb.Append($"{i}: {v}\n");
                i++;
            }
            if (!string.IsNullOrEmpty(baiduresult))
            {
                sb.Append($"6: {baiduresult} (云输入法)");
                
            }
            this.tb.Text = sb.ToString();

        }


        private  async void updatebaidu(string tmp)
        {
            baiduresult = "";
            var rrr = r.Match(tmp.Replace(" ","").Replace("ü","v"));
            if (rrr.Length > 0)
            {
                var py = rrr.Captures[0].Value;
                var web = new WebClient();
                web.QueryString.Add("py",py);
                web.QueryString.Add("rn","0");
                web.QueryString.Add("pn","1");
                web.QueryString.Add("ol","1");
                try
                {
                    var result = await web.DownloadStringTaskAsync("http://olime.baidu.com/py");
                    var jr = JArray.Parse(result);
                    baiduresult = jr[0][0][0].ToString();
                    updatelistdisplay();
                }
                catch (Exception e)
                {
                    return;
                }
               
            }
           
//            web.QueryString=
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            string press="";
            switch (keyEventArgs.Key)
            {
                case Key.OemPlus:
                    press = "=";
                    break;
                case Key.OemMinus:
                    press = "-";
                    break;
                case Key.Space:
                    press = " ";
                    break;
                default:
                    press = converter.ConvertToString(keyEventArgs.Key).ToLower();
                    break;
            }
             
            if (press.Length == 1)
            {
                char pressedkey = press.ToCharArray()[0];
                if (pressedkey >= 'a' && pressedkey <= 'z')
                {
                    Rime.RimeProcessKey(rimesessionid, (int) pressedkey, 0);
                    var tmp=update();
                    updatebaidu(tmp);
                }
                else if (pressedkey == '-' || pressedkey == '=')
                {
                    Rime.RimeProcessKey(rimesessionid, (int)pressedkey, 0);
                    update();

                }
                else if((pressedkey >= '1' && pressedkey <= '5') || pressedkey==' ')
                {
                    Rime.RimeProcessKey(rimesessionid, (int)pressedkey, 0);
                    update();
                }
                else if (pressedkey == '6')
                {
                    if (!string.IsNullOrEmpty(baiduresult))
                    {
                        this.tb.Text = "";
                        this.lb.Content = baiduresult;
                        Rime.RimeClearComposition(rimesessionid);
                    }
                }
            }


        }

        private void Handler(IntPtr contextObject, UIntPtr sessionId, string messageType, string messageValue)
        {
//            Console.
        }
    }
}
