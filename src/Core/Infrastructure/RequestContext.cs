
using System;
using Fernweh.Core.Identity;
using Fernweh.Core.Security;
using Voodoo.Infrastructure.Notations;

namespace Fernweh.Core.Infrastructure
{
    public class RequestContext
    {
        public AppPrincipal AppPrincipal { get; set; }
        public ClientInfo ClientInfo { get; set; }

        public DateTime ConvertClientLocalTimeToUtc(DateTime clientDateTime)
        {
            var utc = DateTime.SpecifyKind(clientDateTime, DateTimeKind.Unspecified);
            var dateTimeOffset = getDateTimeOffset(utc);
            return dateTimeOffset.UtcDateTime;
        }

        public DateTime ConvertUtcToClientLocalDateTime(DateTime utcDateTime)
        {
            var result = utcDateTime.AddHours(getDateTimeOffsetInHours());
            return result;
        }

        private int getDateTimeOffsetInHours()
        {
            var offset = 0;
            var clientOffset = ClientInfo?.TimeZoneOffsetInMinutes;
            offset = clientOffset ?? TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours;
            return offset;
        }

        private DateTimeOffset getDateTimeOffset(DateTime utc)
        {
            var offset = new TimeSpan(getDateTimeOffsetInHours(), 0, 0);
            var dateTimeOffset = new DateTimeOffset(utc, offset);
            return dateTimeOffset;
        }
    }
}