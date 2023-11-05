using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message) { }
    }
    public sealed class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Parameter ids is null") { }
    }
    public sealed class CollectionByIdsBadREquestException : BadRequestException
    {
        public CollectionByIdsBadREquestException() : base("Collection count mismatch comparing to ids") { }
    }
    public sealed class CompanyCollectionBadRequest : BadRequestException
    {
        public CompanyCollectionBadRequest() : base("Company collection sent from client is nulll") { }
    }
}
