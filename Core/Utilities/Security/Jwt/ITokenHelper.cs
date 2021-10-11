using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    interface ITokenHelper
    {
        AccessToken createToken(User user, List<OperationClaim> operationClaims);
    }
}
