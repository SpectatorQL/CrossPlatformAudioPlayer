namespace CPAP
{
    public static class Time
    {
        public static string Format(double time)
        {
            string timeString;
            if (time >= 60)
            {
                int minutes = (int)time / 60;
                double seconds = time - (60 * minutes);
                timeString = minutes.ToString() + ":" + seconds.ToString("00");
            }
            else
            {
                timeString = time.ToString("0:00");
            }

            return timeString;
        }
    }
}
