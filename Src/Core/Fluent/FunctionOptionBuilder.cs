// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Builds an option from an option factory.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class FunctionOptionBuilder
        : IOptionBuilder
    {
        /// <summary>
        ///     Option factory.
        /// </summary>
        private readonly Func<Option> optionFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FunctionOptionBuilder"/> class.
        /// </summary>
        /// <param name="optionFactory">Option factory.</param>
        internal FunctionOptionBuilder(Func<Option> optionFactory)
        {
            this.optionFactory = optionFactory;
        }

        /// <inheritdoc/>
        public Option Build()
        {
            return this.optionFactory();
        }
    }
}