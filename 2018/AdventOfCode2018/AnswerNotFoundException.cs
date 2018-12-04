using System;
using System.Runtime.Serialization;

namespace AdventOfCode2018
{
    [Serializable]
    internal class AnswerNotFoundException : Exception
    {
        public AnswerNotFoundException()
        {
        }

        public AnswerNotFoundException(string message) : base(message)
        {
        }

        public AnswerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AnswerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}