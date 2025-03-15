using Microsoft.Maui.Platform;
using kyiv.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kyiv.ViewModels
{
    public class TimeAccountant
    {
        Thread thread;
        public static int UserTime = 0;
        public TimeAccountant()
        {                       
            thread = new Thread(Doing);
            thread.Start();
        }
        private void Doing()
        {
            //AboutPageDataHelper.CheckWeek();
            //AboutPageDataHelper.CheckCurrentDay();
            //UserTime = DbProvider.GetDayTime();
            //while (true)
            //{              
            //    UserTime += 1;
            //    DbProvider.SetDayTime(UserTime);
            //    AboutPageDataHelper.WriteDay();
            //    if (AboutPage.reloadAboutPage != null)
            //    {
            //        AboutPage.reloadAboutPage();
            //    }
                
            //    Thread.Sleep(60000);

            //}

        } 

    }
}
