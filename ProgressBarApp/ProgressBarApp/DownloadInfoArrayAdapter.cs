using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProgressBarApp
{
    public class DownloadInfoArrayAdapter : ArrayAdapter<DownloadInfo>
    {
        private Context context;
        public DownloadInfoArrayAdapter(Context context, int textViewResourceId,
                                    List<DownloadInfo> objects) : base(context, textViewResourceId, objects)
        {
            this.context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //return base.GetView(position, convertView, parent);

            View row = convertView;
            DownloadInfo info = GetItem(position);

            ViewHolder holder = null;
            if (null == row)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                row = inflater.Inflate(Resource.Layout.file_download_row, parent, false);

                holder = new ViewHolder();
                holder.textView = (TextView)row.FindViewById(Resource.Id.downloadFileName);
                holder.progressBar = (ProgressBar)row.FindViewById(Resource.Id.downloadProgressBar);
                holder.button = (Button)row.FindViewById(Resource.Id.downloadButton);
                holder.info = info;

                row.Tag = holder;
            }
            else
            {
                holder =  row.Tag as ViewHolder;

                holder.info.setProgressBar(null);
                holder.info = info;
                holder.info.setProgressBar(holder.progressBar);
            }

            holder.textView.Text = info.getFilename();

            holder.progressBar.Progress = info.getProgress();
            holder.progressBar.Max = info.getFileSize();

            info.setProgressBar(holder.progressBar);


            holder.button.Enabled=  info.getDownloadState() == DownloadInfo.DownloadState.NOT_STARTED;
            Button button = holder.button;

            holder.button.Click += delegate {
                info.setDownloadState(DownloadInfo.DownloadState.QUEUED);
                button.Enabled = false;
                button.Invalidate();
                FileDownloadTask task = new FileDownloadTask(info);
                task.ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            };

            return row;
        }
    }
}