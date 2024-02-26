using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleDotnet.DDD
{
    public class ValidationResult
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public bool HasErrors { get; private set; }

        public void AddError(string message)
        {
            HasErrors = true;
            Errors.Add(message);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.ToArray());
        }

        public static ValidationResult operator +(ValidationResult a, ValidationResult b)
        {
            a.Errors.AddRange(b.Errors);

            a.HasErrors = a.Errors.Any();

            return a;
        }
    }
}
