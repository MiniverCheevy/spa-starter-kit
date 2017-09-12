using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Operations.Members.Extras;
using Core.Reports;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Reports;

namespace Core.Operations.Members
{
    [Rest(Verb.Get, RestResources.MemberReport)]
    public class MemberReportQuery : QueryAsync<MemberListRequest, BinaryResponse>
    {
        public MemberReportQuery(MemberListRequest request) : base(request)
        {
        }

        protected override async Task<BinaryResponse> ProcessRequestAsync()
        {
            var data = await new MemberListQuery(request).ExecuteAsync();
            response.AppendResponse(data);
            if (response.IsOk)
            {
                var report = new MemberListReport();
                report.Build(data.Data);
                response.Data = report.Render(ReportFormat.Pdf);
            }
            return response;
        }
    }
}
