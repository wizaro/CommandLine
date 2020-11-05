// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Returns a command instance as a built command.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class InstanceCommandBuilder
        : ICommandBuilder
    {
        /// <summary>
        ///     Command instance.
        /// </summary>
        private readonly Command command;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceCommandBuilder"/> class.
        /// </summary>
        /// <param name="command">Command instance.</param>
        internal InstanceCommandBuilder(Command command)
        {
            this.command = command;
        }

        /// <inheritdoc/>
        public bool IsRoot
            => false;

        /// <inheritdoc/>
        public Command Build()
        {
            return this.command;
        }
    }
}