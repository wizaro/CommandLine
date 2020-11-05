// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Builds a command from a command factory.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class FunctionCommandBuilder
        : ICommandBuilder
    {
        /// <summary>
        ///     Command factory.
        /// </summary>
        private readonly Func<Command> commandFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FunctionCommandBuilder"/> class.
        /// </summary>
        /// <param name="commandFactory">Command factory.</param>
        internal FunctionCommandBuilder(Func<Command> commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        /// <inheritdoc/>
        public bool IsRoot
            => false;

        /// <inheritdoc/>
        public Command Build()
        {
            var command = this.commandFactory();
            return command is RootCommand _ ? throw Exceptions.BuildInvalidOperationBuiltRootCommand() : command;
        }
    }
}