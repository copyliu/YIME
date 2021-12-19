using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using LibPinyinApi;

namespace YIME.PlayGround
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibPinyinApi.PinyinContext context;
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += (sender, args) => context?.Dispose();
            context = new PinyinContext();
            this.KeyDown += OnKeyDown;


        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
