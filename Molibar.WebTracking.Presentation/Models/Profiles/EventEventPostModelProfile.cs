using System;
using AutoMapper;

namespace Molibar.WebTracking.Presentation.Models.Profiles
{
    public class EventEventPostModelProfile : Profile
    {
        private static DateTime BaseDateTime = new DateTime(1970, 1, 1);
        private const long MILLISECONDS_TO_TICKS_FACTOR = 10000;

        internal static DateTime JavascriptTicksToDate(long millisSince1970)
        {
            return BaseDateTime.Add(new TimeSpan(millisSince1970 * MILLISECONDS_TO_TICKS_FACTOR));
        }

        internal static long DateToJavascriptTicks(DateTime dateTime)
        {
            return dateTime.Subtract(BaseDateTime).Ticks / MILLISECONDS_TO_TICKS_FACTOR;
        }
    }
}