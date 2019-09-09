using System;

namespace Com.Melon.Core.Infrastructure
{
    /// <summary>
    /// The clock util to get the current datetime
    /// The class will make the unit test easier
    /// </summary>
    public static class Clock
    {
        // todo: use thread local to prevent concurrent issue
        private static Func<DateTime> _now;

        static Clock()
        {
            _now = () => DateTime.Now;
        }

        /// <summary>
        /// get the current datetime 
        /// </summary>
        /// <returns>the current datetime</returns>
        public static DateTime Now
        {
            get => _now();
        }

        /// <summary>
        /// set the current datetime to default 
        /// </summary>
        public static void Resume()
        {
            _now = () => DateTime.Now;
        }

        /// <summary>
        /// fix the current datetime
        /// </summary>
        /// <param name="now">the current datetime to specify</param>
        public static void FixNow(DateTime now)
        {
            _now = () => now;
        }
    }
}
