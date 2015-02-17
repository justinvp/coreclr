// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

/*============================================================
**
** Class:  FormattableStringFactory
**
**
** Purpose: implementation of the FormattableStringFactory
** class.
**
===========================================================*/
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// A factory type used by compilers to create instances of the type <see cref="FormattableString{TArguments}"/>.
    /// </summary>
    public static class FormattableStringFactory
    {
        /// <summary>
        /// Create a <see cref="FormattableString{TArguments}"/> from a composite format string and object
        /// array containing zero or more objects to format.
        /// </summary>
        public static FormattableString<Arguments> Create(string format, params object[] arguments)
        {
            return new FormattableString<Arguments>(format, new Arguments(arguments));
        }

        public struct Arguments : IEquatable<Arguments>, IFormattable
        {
            private readonly object[] _arguments;

            public Arguments(object[] arguments)
            {
                if (arguments == null)
                    throw new ArgumentNullException("arguments");

                _arguments = arguments;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                return string.Format(formatProvider, format, _arguments);
            }

            public bool Equals(Arguments other)
            {
                return object.ReferenceEquals(_arguments, other._arguments);
            }

            public override bool Equals(object obj)
            {
                return obj is Arguments && Equals((Arguments)obj);
            }

            public override int GetHashCode()
            {
                return null == _arguments ? 0 : _arguments.GetHashCode();
            }
        }
    }
}
