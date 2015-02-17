// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

/*============================================================
**
** Class:  FormattableString
**
**
** Purpose: implementation of the FormattableString
** class.
**
===========================================================*/
namespace System
{
    public static class FormattableString
    {
        /// <summary>
        /// Format the given object in the invariant culture. This static method may be
        /// imported in C# by
        /// <code>
        /// using static System.FormattableString;
        /// </code>.
        /// Within the scope
        /// of that import directive an interpolated string may be formatted in the
        /// invariant culture by writing, for example,
        /// <code>
        /// Invariant($"{{ lat = {latitude}; lon = {longitude} }}")
        /// </code>
        /// </summary>
        public static string Invariant<TArguments>(FormattableString<TArguments> formattable)
            where TArguments : IFormattable
        {
            return formattable.ToString(Globalization.CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// A composite format string along with the arguments to be formatted. An instance of this
    /// type may result from the use of the C# or VB language primitive "interpolated string".
    /// </summary>
    public struct FormattableString<TArguments> : IEquatable<FormattableString<TArguments>>, IFormattable
        where TArguments : IFormattable
    {
        private readonly string _format;
        private readonly TArguments _arguments;

        public FormattableString(string format, TArguments arguments)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _format = format;
            _arguments = arguments;
        }

        /// <summary>
        /// The composite format string.
        /// </summary>
        public string Format
        {
            get { return _format; }
        }

        /// <summary>
        /// The arguments containing the objects to format.
        /// </summary>
        public TArguments Arguments
        {
            get { return _arguments; }
        }

        /// <summary>
        /// Format to a string using the given culture.
        /// </summary>
        public string ToString(IFormatProvider formatProvider)
        {
            return _arguments.ToString(_format, formatProvider);
        }

        string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
        {
            return ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(Globalization.CultureInfo.CurrentCulture);
        }

        public override bool Equals(object obj)
        {
            return obj is FormattableString<TArguments> && Equals((FormattableString<TArguments>)obj);
        }

        public bool Equals(FormattableString<TArguments> other)
        {
            var comparer = EqualityComparer<TArguments>.Default;
            return _format == other._format && comparer.Equals(_arguments, other._arguments);
        }

        public override int GetHashCode()
        {
            var comparer = EqualityComparer<TArguments>.Default;
            return (null == _format ? 0 : _format.GetHashCode()) ^ comparer.GetHashCode(_arguments);
        }
    }
}
