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

        public static FormattableString<Arguments<T>> Create<T>(string format, T argument)
        {
            return new FormattableString<Arguments<T>>(format, new Arguments<T>(argument));
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

        public struct Arguments<T> : IEquatable<Arguments<T>>, IFormattable
        {
            private readonly T _argument;

            public Arguments(T argument)
            {
                _argument = argument;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                // TODO optimize (avoid the array allocation and boxing)
                // Presumably when this is added, it'd be added at the same time
                // that new optimized overloads of string.Format are added, that
                // avoid the array allocation and boxing.
                return string.Format(formatProvider, format, new object[] { _argument });
            }

            public bool Equals(Arguments<T> other)
            {
                var comparer = EqualityComparer<T>.Default;
                return comparer.Equals(_argument, other._argument);
            }

            public override bool Equals(object obj)
            {
                return obj is Arguments && Equals((Arguments)obj);
            }

            public override int GetHashCode()
            {
                var comparer = EqualityComparer<T>.Default;
                return comparer.GetHashCode(_argument);
            }
        }
    }
}
