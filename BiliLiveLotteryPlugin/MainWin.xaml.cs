using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using BilibiliDM_PluginFramework;
using BiliLiveLotteryPlugin.Annotations;

namespace BiliLiveLotteryPlugin
{
    /// <summary>
    /// MainWin.xaml 的交互逻辑
    /// </summary>
    public partial class MainWin:INotifyPropertyChanged
    {
        public DMPlugin plugin;
        private int _defaultDelay;

        private bool IsDefaultCounting;

        long CountingTarget;
        LimitedQueue<string> defQueue=new LimitedQueue<string>();

        public int default_delay    
        {
            get { return _defaultDelay; }
            set
            {
                if (value == _defaultDelay) return;
                _defaultDelay = value;
                OnPropertyChanged(nameof(default_delay));
            }
        }

        public MainWin()
        {
            InitializeComponent();
            default_num_set.DataContext = this;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            tb1.MouseLeftButtonUp += tb_copy_event;
            tb2.MouseLeftButtonUp += tb_copy_event;
            tb3.MouseLeftButtonUp += tb_copy_event;
            tb4.MouseLeftButtonUp += tb_copy_event;
            tb5.MouseLeftButtonUp += tb_copy_event;
        }

        private void tb_copy_event(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
            {
                var obj = (TextBlock) sender;
                try
                {
                    Clipboard.SetData(DataFormats.Text, obj.Text);
                    MessageBox.Show(Application.Current.MainWindow, "名字已复制到剪贴板");
                }
                catch (Exception)
                {


                }

            }
        }

        public void NewDMUser(string user)
        {
            defQueue.Enqueue(user);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                var tmp = defQueue.ToArray();
                tb5.Text = "";
                tb4.Text = "";
                tb3.Text = "";
                tb2.Text = "";
                tb1.Text = "";

                switch (tmp.Length)
                {
                    case 5:
                        tb5.Text = tmp[4];
                        tb4.Text = tmp[3];
                        tb3.Text = tmp[2];
                        tb2.Text = tmp[1];
                        tb1.Text = tmp[0];
                        break;
                    case 4:
                        tb4.Text = tmp[3];
                        tb3.Text = tmp[2];
                        tb2.Text = tmp[1];
                        tb1.Text = tmp[0];
                        break;
                    case 3:
                        tb3.Text = tmp[2];
                        tb2.Text = tmp[1];
                        tb1.Text = tmp[0];
                        break;
                    case 2:
                        tb2.Text = tmp[1];
                        tb1.Text = tmp[0];
                        break;
                    case 1:
                        tb1.Text = tmp[0];
                        break;
                    case 0:
                        break;

                }
            }));
        }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //主倒計時
            if (IsDefaultCounting)
            {
                if (DateTime.Now.Ticks > CountingTarget)
                {
                    plugin.Stop();
                    default_start.IsEnabled = true;
                    tb3.Background = Brushes.Red;

                    
                }
            }
            Console.WriteLine(1);
        }

        private void default_start_Click(object sender, RoutedEventArgs e)
        {
            tb3.Background = null;
            if (default_delay == 0)
            {
                tb3.Background = Brushes.Red;
                plugin.Stop();
            }
            else
            {
                IsDefaultCounting = true;
                CountingTarget = DateTime.Now.AddSeconds(default_delay).Ticks;
                default_start.IsEnabled = false;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
