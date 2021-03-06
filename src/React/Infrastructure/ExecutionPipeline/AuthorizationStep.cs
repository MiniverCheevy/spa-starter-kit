﻿using System.Threading.Tasks;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline.Models;

namespace Web.Infrastructure.ExecutionPipeline
{
    internal class AuthorizationStep<TRequest, TResponse> : Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task<ExecutionState<TRequest, TResponse>> processAsync()
        {
            return Task.FromResult(state);
        }
    }
}