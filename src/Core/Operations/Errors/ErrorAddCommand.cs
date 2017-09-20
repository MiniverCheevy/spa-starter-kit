using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Core.Operations.Errors.Extras;
using Voodoo;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Core.Operations.Errors
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

        protected override async Task<NewItemResponse> ProcessRequestAsync()
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
                           @"Insert Into Exceptions (MachineName, CreationDate, Type,  Host, Url, HTTPMethod, IPAddress, Source, Message, Detail, StatusCode, FullJson, ErrorHash, [User])
                                            Values (@MachineName, @CreationDate, @Type, @Host, @Url, @HTTPMethod, @IPAddress, @Source, @Message, @Detail, @StatusCode, @FullJson, @ErrorHash, @User);
                                            Select @@Identity;";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MachineName", error.MachineName.Truncate(200));
                        command.Parameters.AddWithValue("@CreationDate", error.CreationDate);
                        command.Parameters.AddWithValue("@Type", error.Type.Truncate(200));
                        command.Parameters.AddWithValue("@Host", error.Host.Truncate(200));
                        command.Parameters.AddWithValue("@Url", error.Url.Truncate(200));
                        command.Parameters.AddWithValue("@HTTPMethod", error.HttpMethod.Truncate(200));
                        command.Parameters.AddWithValue("@IPAddress", error.IpAddress.Truncate(200));
                        command.Parameters.AddWithValue("@Source", error.Source.Truncate(200));
                        command.Parameters.AddWithValue("@Message", error.Message.Truncate(200));
                        command.Parameters.AddWithValue("@Detail", error.Details);
                        command.Parameters.AddWithValue("@StatusCode", error.StatusCode ?? 200);
                        command.Parameters.AddWithValue("@FullJson", error.FullJson);
                        command.Parameters.AddWithValue("@ErrorHash", error.ErrorHash);
                        command.Parameters.AddWithValue("@User", error.User.Truncate(128));
                        var result = await command.ExecuteScalarAsync();
                        response.NewItemId = result.To<int>();
                    }
                }
            }
            return response;
        }
    }
}