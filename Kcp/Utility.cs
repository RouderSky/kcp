﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

//[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace System.Net.Sockets.Kcp
{
    public static class KcpExtension_FDF71D0BC31D49C48EEA8FAA51F017D4
    {
        private static readonly DateTime utc_time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        [Obsolete("", true)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ConvertTime(this in DateTime time)
        {
            return (uint)(Convert.ToInt64(time.Subtract(utc_time).TotalMilliseconds) & 0xffffffff);
        }

        private static readonly DateTimeOffset utc1970 = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ConvertTimeOld(this in DateTimeOffset time)
        {
            return (uint)(Convert.ToInt64(time.Subtract(utc1970).TotalMilliseconds) & 0xffffffff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ConvertTime2(this in DateTimeOffset time)
        {
#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
            return (uint)(time.ToUnixTimeMilliseconds() & 0xffffffff);
#else
            return (uint)(Convert.ToInt64(time.Subtract(utc1970).TotalMilliseconds) & 0xffffffff);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ConvertTime(this in DateTimeOffset time)
        {
#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
            return (uint)(time.ToUnixTimeMilliseconds());
#else
            return (uint)(Convert.ToInt64(time.Subtract(utc1970).TotalMilliseconds) & 0xffffffff);
#endif
        }

        internal static string ToLogString<T>(this T segment, bool local = false)
            where T : IKcpSegment
        {
            if (local)
            {
                return $"sn:{segment.sn} una:{segment.una} frg:{segment.frg,2} wnd:{segment.wnd}    [ LocalValue: xmit:{segment.xmit} fastack:{segment.fastack}  rto:{segment.rto} ]";
            }
            else
            {
                return $"sn:{segment.sn} una:{segment.una} frg:{segment.frg,2} wnd:{segment.wnd}";
            }

        }
    }
}