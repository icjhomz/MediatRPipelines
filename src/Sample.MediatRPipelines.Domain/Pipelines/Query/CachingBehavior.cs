﻿using MediatR;
using Microsoft.Extensions.Logging;
using Sample.MediatRPipelines.Domain.Primitives;
using ZiggyCreatures.Caching.Fusion;

namespace Sample.MediatRPipelines.Domain.Pipelines.Query;


public sealed class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
{
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
    private readonly IFusionCache _cache;

    public CachingBehavior(ILogger<CachingBehavior<TRequest, TResponse>> logger,
                            IFusionCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {

        var inputRequest = (request as IQueryRequest<TResponse>);

        return await _cache.GetOrSetAsync(inputRequest.CacheKey,
          await next(),
         options => options
         .SetDuration(TimeSpan.FromMinutes(2))
         .SetFailSafe(true)
         .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100))
         );

    }
}
