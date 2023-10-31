/*************************************************************************************
   * 创建时间：2020.01.15
   * 作    者：李
   * 功能说明：定时器封装
   * 修改时间：2020.02.11
   * 修 改 人：
   * 备    注：OpenTimer()、StopTimer()
  *************************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Timer
{
    public sealed class MilliTimer : IComponent, IDisposable
    {
        public int _interval = 20;      // 全局时钟间隔(ms)
        public int _count = 0;          // 全局时钟分频值

        public bool _flag = true;       // 全局时钟禁止重复刷新

        /// <summary>
        /// 打开定时器，与关闭定时器配合使用。
        /// </summary>
        /// <param name="milliTimer">定时器实例</param>
        /// <param name="interval">间隔(ms)</param>
        /// <param name="tickEvent">委托的tick事件</param>
        public void OpenTimer(MilliTimer milliTimer, int interval, EventHandler tickEvent)
        {
            if (!milliTimer.isRunning)
            {
                ClearAllEvent();                // 手动注销Tick所有事件
                milliTimer.Tick += tickEvent;   // 事件注册
                milliTimer.Interval = interval; // 定时间隔(ms)
                milliTimer.Start();             // 计时开始
            }
        }

        /// <summary>
        /// 关闭定时器，与打开定时器配合使用。
        /// </summary>
        /// <param name="milliTimer">定时器实例</param>
        public void StopTimer(MilliTimer milliTimer)
        {
            if (milliTimer.isRunning)
            {
                ClearAllEvent();        // 手动注销Tick所有事件
                milliTimer.Stop();      // 定时结束
                milliTimer.Dispose();   // 定时释放
            }
        }

        #region *定时器封装定义*

        private static TimerCaps caps;
        private int interval;
        private bool isRunning;
        private int resolution;
        private TimerCallback timerCallback;
        private int timerID;

        private int Interval
        {
            get
            {
                return this.interval;
            }
            set
            {
                if ((value < caps.periodMin) || (value > caps.periodMax))
                {
                    throw new Exception("Timer: Interval limit exceeded!");
                }
                this.interval = value;
            }
        }

        /// <summary>
        /// 正在运行标志
        /// </summary>
        private bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISite Site
        {
            set;
            get;
        }

        public event EventHandler Disposed;  // 这个事件实现了IComponet接口
        public event EventHandler Tick;

        static MilliTimer()
        {
            timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
        }

        public MilliTimer()
        {
            this.interval = caps.periodMin;    // 
            this.resolution = caps.periodMin;  //

            this.isRunning = false;
            this.timerCallback = new TimerCallback(this.TimerEventCallback);

        }

        public MilliTimer(IContainer container)
            : this()
        {
            container.Add(this);
        }

        ~MilliTimer()
        {
            timeKillEvent(this.timerID);
        }

        public void Start()
        {
            if (!this.isRunning)
            {
                this.timerID = timeSetEvent(this.interval, this.resolution, this.timerCallback, 0, 1); // 间隔性地运行

                if (this.timerID == 0)
                {
                    throw new Exception("Timer: Cannot start the timer!");
                }
                this.isRunning = true;
            }
        }

        public void Stop()
        {
            if (this.isRunning)
            {
                timeKillEvent(this.timerID);
                this.isRunning = false;
            }
        }

        /// <summary>
        /// 实现IDisposable接口
        /// </summary>
        public void Dispose()
        {
            timeKillEvent(this.timerID);
            GC.SuppressFinalize(this);
            EventHandler disposed = this.Disposed;
            if (disposed != null)
            {
                disposed(this, EventArgs.Empty);
            }

        }

        private void ClearAllEvent()   //手动事件注销
        {
            if (Tick == null) return;
            Delegate[] dels = Tick.GetInvocationList();
            foreach (Delegate del in dels)
            {
                object delObj = del.GetType().GetProperty("Method").GetValue(del, null);
                string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);////方法名
                Console.WriteLine(funcName);
                Tick -= del as EventHandler;
            }
        }

        //***************************************************  内部函数  ******************************************************************
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerCallback callback, int user, int mode);

        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);

        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);
        //  The timeGetDevCaps function queries the timer device to determine its resolution. 

        private void TimerEventCallback(int id, int msg, int user, int param1, int param2)
        {
            if (this.Tick != null)
            {
                this.Tick(this, null);  // 引发事件
            }
        }

        //***************************************************  内部类型  ******************************************************************

        private delegate void TimerCallback(int id, int msg, int user, int param1, int param2); // timeSetEvent所对应的回调函数的签名

        /// <summary>
        /// 定时器的分辨率（resolution）。单位是ms，毫秒
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct TimerCaps
        {
            public int periodMin;
            public int periodMax;
        }

        #endregion
    }
}