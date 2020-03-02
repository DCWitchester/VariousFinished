using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.TimerEvents
{
    class TimerEvents
    {
        /// <summary>
        /// the system timer used for the data Upload
        /// </summary>
        System.Timers.Timer uploadTimer = new System.Timers.Timer(Settings.Settings.programSettings.UploadInterval);
        /// <summary>
        /// the System timer used for the data Downlaod
        /// </summary>
        System.Timers.Timer downloadTimer = new System.Timers.Timer(Settings.Settings.programSettings.DownloadInterval);
        void TimedEvents()
        {
            #warning TBD Main Timed Events
        }
        #region Upload Timer
        /// <summary>
        /// the initialization of the uploadTimer
        /// </summary>
        void InitializeUploadTimer()
        {
            //then we will set the eventHandler for the function
            uploadTimer.Elapsed += UploadEvent;
            //and start the timer
            uploadTimer.AutoReset = true;
            uploadTimer.Enabled = true;
        }
        /// <summary>
        /// a function to stop the uploadTimer
        /// </summary>
        void StopUploadTimer()
        {
            uploadTimer.Stop();
        }
        /// <summary>
        /// a function used to start the uploadTimer
        /// </summary>
        void StartUploadTimer()
        {
            uploadTimer.Start();
        }
        /// <summary>
        /// the main upload event used by the timer
        /// </summary>
        /// <param name="sender">the timer</param>
        /// <param name="e">the timers elapsed event</param>
        void UploadEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            #warning TBD Upload Event
        }
        #endregion
        #region DownloadTimer
        /// <summary>
        /// the initialization of the downloadTimer
        /// </summary>
        void InitializeDownloadTimer()
        {
            //then we will set the eventHandler for the function
            downloadTimer.Elapsed += DownloadEvent;
            //and start the timer
            downloadTimer.AutoReset = true;
            downloadTimer.Enabled = true;
        }
        /// <summary>
        /// the function for stopping the donwload timer
        /// </summary>
        void StopDownloadTimer()
        {
            downloadTimer.Stop();
        }
        /// <summary>
        /// the function for starting the download timer
        /// </summary>
        void StartDownloadTimer()
        {
            downloadTimer.Start();
        }
        /// <summary>
        /// the main Donwload event
        /// </summary>
        /// <param name="sender">the Download Timer</param>
        /// <param name="e">the timers elapsed events</param>
        void DownloadEvent(object sender, System.Timers.ElapsedEventArgs e) 
        {
            #warning TBD Download Event
        }
        #endregion
    }
}
