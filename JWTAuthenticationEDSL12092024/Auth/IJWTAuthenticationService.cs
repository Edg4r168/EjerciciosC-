using System;

namespace JWTAuthenticationEDSL12092024.Auth;

public interface IJWTAuthenticationService
{
    string Authenticate(string userName);
}
