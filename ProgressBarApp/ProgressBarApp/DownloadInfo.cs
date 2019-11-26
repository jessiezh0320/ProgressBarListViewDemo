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
     public class DownloadInfo
    {
        private  static string TAG = "DownloadInfo";
        public enum DownloadState
        {
            NOT_STARTED,
            QUEUED,
            DOWNLOADING,
            COMPLETE
        }
        private volatile DownloadState mDownloadState = DownloadState.NOT_STARTED;
        private  string mFilename;
        private volatile int mProgress;
        private  int mFileSize =0;
        private volatile ProgressBar mProgressBar;

        public DownloadInfo(string filename, int size)
        {
            mFilename = filename;
            mProgress = 0;
            mFileSize = size;
            mProgressBar = null;
        }

        public ProgressBar getProgressBar()
        {
            return mProgressBar;
        }
        public void setProgressBar(ProgressBar progressBar)
        {
            Log.Info(TAG, "setProgressBar " + mFilename + " to " + progressBar);
            mProgressBar = progressBar;
        }

        public void setDownloadState(DownloadState state)
        {
            mDownloadState = state;
        }
        public DownloadState getDownloadState()
        {
            return mDownloadState;
        }

        public int getProgress()
        {
            return mProgress;
        }

        public void setProgress(int progress)
        {
            this.mProgress = progress;
        }

        public int getFileSize()
        {
            return mFileSize;
        }

        public string getFilename()
        {
            return mFilename;
        }
    }
}