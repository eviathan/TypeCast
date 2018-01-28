using System;

namespace TypeCast.TestTarget.TestFramework
{
    public class TestFailureException : Exception
    {
        public TestFailureException(string message)
            : base(message) { }
    }
}