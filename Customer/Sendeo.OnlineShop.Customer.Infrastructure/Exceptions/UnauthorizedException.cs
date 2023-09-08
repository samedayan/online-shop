using System.Runtime.Serialization;

namespace Sendeo.OnlineShop.Customer.Infrastructure.Exceptions
{
	[Serializable]
	public class UnauthorizedException : Exception
	{
		public string ExceptionCode { get; set; }

		public UnauthorizedException()
		{
		}

		public UnauthorizedException(string message) : base(message)
		{
		}

		public UnauthorizedException(string message, string exceptionCode) : base(message)
		{
			this.ExceptionCode = exceptionCode;
		}

		public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
