using Xunit.Sdk;

namespace Algorithms.Test;

public static class AssertionHelper
{
    public static void WithMessage(Action assertion, Func<string> messageFactory)
    {
        try
        {
            assertion.Invoke();
        }
        catch (XunitException ex)
        {
            throw new XunitException(messageFactory.Invoke(), ex);
        }
    }
}