using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Fernweh.Core.Operations.Errors.Extras;
using Voodoo;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Fernweh.Core.Operations.Errors
{
    public static class ErrorExtentions
    {
        public static string Truncate(this string value, int max)
        {
            value = value.To<string>();
            if (value.Length > max)
                value = value.Substring(0, max);
            return value;
        }
    }

    public class ErrorAddCommand : ExecutorAsync<ErrorRequest, NewItemResponse>
    {
        public ErrorAddCommand(ErrorRequest request) : base(request)
        {
        }

        protected override void CustomErrorBehavior(Exception ex)
        {
            var fallbackLogger = new FallbackLogger();
            fallbackLogger.Log(ex);
        }

        protected override Task<NewItemResponse> ProcessRequestAsync()
        {
            var error = request.Error;
            using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                //bypass any entity framework issues (model has changed, etc.) by using sql commands
                using (var connection = new SqlConnection(IOC.Settings.DefaultConnectionString))
                {
                    //TODO:consider moving to dapper
                    connection.Open();
                    var sql =
                        @"Insert Into Exceptions (GUID, ApplicationName, MachineName, CreationDate, Type, IsProtected, Host, Url, HTTPMethod, IPAddress, Source, Message, Detail, StatusCode, SQL, FullJson, ErrorHash, DuplicateCount, [User])
                                            Values (@GUID, @ApplicationName, @MachineName, @CreationDate, @Type, @IsProtected, @Host, @Url, @HTTPMethod, @IPAddress, @Source, @Message, @Detail, @StatusCode, @SQL, @FullJson, @ErrorHash, @DuplicateCount, @User)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@GUID", error.GUID);
                        command.Parameters.AddWithValue("@ApplicationName", error.ApplicationName.Truncate(200));
                        command.Parameters.AddWithValue("@MachineName", error.MachineName.Truncate(200));
                        command.Parameters.AddWithValue("@CreationDate", error.CreationDate);
                        command.Parameters.AddWithValue("@Type", error.Type.Truncate(200));
                        command.Parameters.AddWithValue("@IsProtected", error.IsProtected);
                        command.Parameters.AddWithValue("@Host", error.Host.Truncate(200));
                        command.Parameters.AddWithValue("@Url", error.Url.Truncate(200));
                        command.Parameters.AddWithValue("@HTTPMethod", error.HTTPMethod.Truncate(200));
                        command.Parameters.AddWithValue("@IPAddress", error.IPAddress.Truncate(200));
                        command.Parameters.AddWithValue("@Source", error.Source.Truncate(200));
                        command.Parameters.AddWithValue("@Message", error.Message.Truncate(200));
                        command.Parameters.AddWithValue("@Detail", error.Details);
                        command.Parameters.AddWithValue("@StatusCode", error.StatusCode);
                        command.Parameters.AddWithValue("@SQL", error.SQL.To<string>());
                        command.Parameters.AddWithValue("@FullJson", error.FullJson);
                        command.Parameters.AddWithValue("@ErrorHash", error.ErrorHash);
                        command.Parameters.AddWithValue("@DuplicateCount", error.DuplicateCount);
                        command.Parameters.AddWithValue("@User", error.User.Truncate(128));
                        command.ExecuteNonQuery();
                    }
                }
            }
            return Task.FromResult(response);
        }
    }
}