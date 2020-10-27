// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Saves all state necessary to build a <see cref="Command"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public sealed class CommandBuilder
        : ICommandBuilder
    {
        /// <summary>
        ///     Name of the command.
        /// </summary>
        private string? name;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBuilder"/> class that builds a root command.
        /// </summary>
        public CommandBuilder()
        {
            this.IsRoot = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBuilder"/> class that builds a non-root command.
        /// </summary>
        /// <param name="name">The name of the command. It cannot be <see langword="null"/>, empty or contain whitespace after removing the prefix.</param>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/>, empty or contains whitespace after removing the prefix.</exception>
        public CommandBuilder(string name)
        {
            this.name = Validations.IsValidCommandName(name) ? name : throw Exceptions.BuildArgumentInvalidCommandName(nameof(name));
            this.IsRoot = false;
        }

        /// <inheritdoc/>
        public bool IsRoot { get; }

        /// <summary>
        ///     Gets or sets the name of the command.
        /// </summary>
        /// <value>The name of the command. It cannot be <see langword="null"/>, empty or contain whitespace after removing the prefix. Root commands can have null names.</value>
        /// <exception cref="ArgumentException">The name of the command is <see langword="null"/>, empty or contains whitespace after removing the prefix.</exception>
        public string? Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = (this.IsRoot && value is null) || Validations.IsValidCommandName(value) ? value : throw Exceptions.BuildArgumentInvalidCommandName(nameof(value));
            }
        }

        /// <summary>
        ///     Gets or sets the command description.
        /// </summary>
        /// <value>The command description.</value>
        public string? Description { get; set; }

        /// <summary>
        ///     Gets the collection of child argument builders.
        /// </summary>
        /// <value>Collection of child argument builders.</value>
        [CLSCompliant(false)]
        public ArgumentBuilderCollection Arguments { get; } = new ArgumentBuilderCollection();

        /// <summary>
        ///     Gets the collection of child option builders.
        /// </summary>
        /// <value>Collection of child option builders.</value>
        [CLSCompliant(false)]
        public OptionBuilderCollection Options { get; } = new OptionBuilderCollection();

        /// <summary>
        ///     Gets the collection of global option builders.
        /// </summary>
        /// <value>Collection of global option builders.</value>
        [CLSCompliant(false)]
        public OptionBuilderCollection GlobalOptions { get; } = new OptionBuilderCollection();

        /// <summary>
        ///     Gets the collection of child command builders.
        /// </summary>
        /// <value>Collection of child command builders.</value>
        [CLSCompliant(false)]
        public CommandBuilderCollection Commands { get; } = new CommandBuilderCollection();

        /// <summary>
        ///     Gets the collection of command validators.
        /// </summary>
        /// <value>Collection of command validators.</value>
        [CLSCompliant(false)]
        public CommandValidatorCollection Validators { get; } = new CommandValidatorCollection();

        /// <summary>
        ///     Gets or sets the handler for the command.
        /// </summary>
        /// <value>The handler for the command.</value>
        [CLSCompliant(false)]
        public ICommandHandler? Handler { get; set; }

        /// <summary>
        ///     Gets the collection of additional command build configurations.
        /// </summary>
        /// <value>Collection of additional command build configurations.</value>
        /// <remarks>The configurations are actions to be made onto the created command before returning it by <see cref="Build"/>.</remarks>
        [CLSCompliant(false)]
        public CommandConfigurationCollection BuildConfigurations { get; } = new CommandConfigurationCollection();

        /// <inheritdoc/>
        [CLSCompliant(false)]
        public Command Build()
        {
            var newCommand = this.IsRoot ? new RootCommand() : new Command(this.Name!);

            newCommand.Description = this.Description;
            newCommand.Handler = this.Handler;

            foreach (var argument in this.Arguments)
            {
                newCommand.AddArgument(argument.Build());
            }

            foreach (var option in this.Options)
            {
                newCommand.AddOption(option.Build());
            }

            foreach (var option in this.GlobalOptions)
            {
                newCommand.AddGlobalOption(option.Build());
            }

            foreach (var command in this.Commands)
            {
                newCommand.AddCommand(command.Build());
            }

            foreach (var validator in this.Validators)
            {
                newCommand.AddValidator(validator);
            }

            foreach (var configuration in this.BuildConfigurations)
            {
                configuration(newCommand);
            }

            return newCommand;
        }
    }
}