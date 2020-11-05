// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Builds an argument from an argument factory.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class FunctionArgumentBuilder
        : IArgumentBuilder
    {
        /// <summary>
        ///     Argument factory.
        /// </summary>
        private readonly Func<Argument> argumentFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FunctionArgumentBuilder"/> class.
        /// </summary>
        /// <param name="argumentFactory">Argument factory.</param>
        internal FunctionArgumentBuilder(Func<Argument> argumentFactory)
        {
            this.argumentFactory = argumentFactory;
        }

        /// <inheritdoc/>
        public Argument Build()
        {
            return this.argumentFactory();
        }
    }
}