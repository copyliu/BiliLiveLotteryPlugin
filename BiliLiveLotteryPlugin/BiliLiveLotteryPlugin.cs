using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilibiliDM_PluginFramework;

namespace BiliLiveLotteryPlugin
{
    public class BiliLiveLotteryPlugin:DMPlugin
    {
        MainWin mainWin;
        public BiliLiveLotteryPlugin()
        {
            PluginAuth = "CopyLiu";
            PluginCont = "copyliu@gmail.com";
            PluginName = "直播抽奖辅助";
            PluginVer = "0.0.0.3";
            mainWin=new MainWin();
            mainWin.plugin = this;
            ReceivedDanmaku += BiliLiveLotteryPlugin_ReceivedDanmaku;
            mainWin.Closed += MainWin_Closed;

        }

        private void MainWin_Closed(object sender, EventArgs e)
        {
            mainWin = new MainWin();
            mainWin.plugin = this;
        }

        private void BiliLiveLotteryPlugin_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            if (Status)
            {
                if (e.Danmaku.MsgType==MsgTypeEnum.Comment)
                { mainWin.NewDMUser(e.Danmaku.CommentUser);}
                
            }
            
        }

        public override void Admin()
        {
            base.Admin();
            
            if (!mainWin.IsVisible)
            { mainWin.Show();}

        }
    }
}
