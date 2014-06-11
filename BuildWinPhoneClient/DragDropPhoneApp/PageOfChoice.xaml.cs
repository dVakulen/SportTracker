﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DragDropPhoneApp
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    using DragDropPhoneApp.ApiConsumer;

    public partial class PageOfChoice : PhoneApplicationPage
    {
        public PageOfChoice()
        {
            InitializeComponent();
        }

        private void Share_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        // TwitterApi.PostMessageToTwitter(App.DataContext.CurrentActivity.ActivityType.ToString());
            var activite = App.DataContext.CurrentActivity;
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap
                    (activite.Image);

                Extensions.SaveJpeg(btmMap, ms,
                   activite.Image.PixelWidth, activite.Image.PixelHeight, 0, 100);
                imageBytes = ms.ToArray();

            }
            TaskFactory s =new TaskFactory();
            s.StartNew(
                () =>
                    {
                        TwitterApi.PostMessageWithImageToTwitter(
                            App.DataContext.CurrentActivity.ActivityType.ToString() + " "
                            + App.DataContext.CurrentActivity.TimeStamp,
                          imageBytes);
                    });

        }

        private void Not_to_share_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/RealtyList.xaml", UriKind.Relative));
        }
    }
}