using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ProgressBarApp
{
    public class FileDownloadTask : AsyncTask<Java.Lang.Void, int, Java.Lang.Void>
    {
        private static string TAG = "FileDownloadTask";
        private  DownloadInfo  mInfo;

        public FileDownloadTask(DownloadInfo mInfo)
        {
            this.mInfo = mInfo;
        }

        protected override void OnProgressUpdate(params int[] values)
        {
            //base.OnProgressUpdate(values);

            mInfo.setProgress(values[0]);

            ProgressBar bar = mInfo.getProgressBar();

            if (bar != null) {
                bar.Progress = mInfo.getProgress();
                bar.Invalidate();
            } 
        }

        protected override Java.Lang.Void RunInBackground(params Java.Lang.Void[] @params)
        {
            Log.Info(TAG, "Starting download for " + mInfo.getFilename());
            mInfo.setDownloadState(DownloadInfo.DownloadState.DOWNLOADING);
            for (int i = 0; i <= mInfo.getFileSize(); ++i)
            {
                try
                {
                    Thread.Sleep(16);
                }
                catch (InterruptedException e)
                {
                    e.PrintStackTrace();
                }

                PublishProgress(i);
            }
            mInfo.setDownloadState(DownloadInfo.DownloadState.COMPLETE);
            return null;
        }

        protected override void OnPostExecute(Java.Lang.Object result)
        {
            mInfo.setDownloadState(DownloadInfo.DownloadState.COMPLETE);
        }

        protected override void OnPreExecute()
        {
            mInfo.setDownloadState(DownloadInfo.DownloadState.DOWNLOADING);
        }
    }
}