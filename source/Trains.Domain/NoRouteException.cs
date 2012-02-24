using System;

namespace Trains.Domain
{
    public class NoRouteException : Exception
    {
        public override string Message
        {
            get { return "NO SUCH ROUTE"; }
        }
    }
}