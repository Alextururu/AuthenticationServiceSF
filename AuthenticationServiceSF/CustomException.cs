using System;

namespace AuthenticationServiceSF
{
    public class CustomException: Exception
    {
        public CustomException(string message)
        {
            new Exception(message);
        }
    }
}
