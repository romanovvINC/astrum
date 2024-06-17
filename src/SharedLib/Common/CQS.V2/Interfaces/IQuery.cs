using MediatR;
using IResult = Astrum.Core.Common.Results.V1.IResult;

namespace Astrum.Core.Common.CQS.V2.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : class, IResult
{
}