// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Fluent extensions for <see cref="CommandBuilder"/>.
    /// </summary>
    /// <threadsafety static="false"/>
    public static class CommandBuilderExtensions
    {
        /// <summary>
        ///     Sets the <see cref="CommandBuilder.Name"/> in the command builder.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="name">Name of the command. It cannot be <see langword="null"/>, empty or contain whitespace after removing the prefix. Root commands can have null names.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException">The name of the command is <see langword="null"/>, empty or contains whitespace after removing the prefix.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder SetName(this CommandBuilder builder, string name)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Name = (builder.IsRoot && name is null) || Validations.IsValidCommandName(name) ? name : throw Exceptions.BuildArgumentInvalidCommandName(nameof(name));
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="CommandBuilder.Description"/> in the command builder.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="description">Description of the command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder SetDescription(this CommandBuilder builder, string description)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Description = description;
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IArgumentBuilder"/> to the command builder using the configured builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="name">Name of the argument. It cannot be <see langword="null"/> or whitespace-only.</param>
        /// <param name="argumentBuilder">Argument builder configuration.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/> or composed of only whitespaces.</exception>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="argumentBuilder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder AddArgument<T>(this CommandBuilder builder, string name, Action<ArgumentBuilder<T>> argumentBuilder)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (argumentBuilder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(argumentBuilder));
            }

            var newArgumentBuilder = new ArgumentBuilder<T>(name);
            argumentBuilder(newArgumentBuilder);
            builder.Arguments.Add(newArgumentBuilder);
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IArgumentBuilder"/> to the command builder using the specified argument type.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        /// <remarks><typeparamref name="TArgument"/> must derive from <see cref="Argument"/>, contain a parameterless constructor and be ready to be used on construction.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddArgumentFrom<TArgument>(this CommandBuilder builder)
            where TArgument : Argument, new()
        {
            return builder.AddArgumentFrom(BuildNewArgument);

            static Argument BuildNewArgument()
            {
                return new TArgument();
            }
        }

        /// <summary>
        ///     Adds an <see cref="IArgumentBuilder"/> to the command builder using the specified argument.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="argument">Argument to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="argument"/> is <see langword="null"/>.</exception>
        /// <remarks><paramref name="argument"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddArgumentFrom(this CommandBuilder builder, Argument argument)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Arguments.Add(new InstanceArgumentBuilder(argument ?? throw Exceptions.BuildArgumentNull(nameof(argument))));
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IArgumentBuilder"/> to the command builder using the specified argument factory.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="argumentFactory">Argument factory to generate the argument to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="argumentFactory"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder AddArgumentFrom(this CommandBuilder builder, Func<Argument> argumentFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Arguments.Add(new FunctionArgumentBuilder(argumentFactory ?? throw Exceptions.BuildArgumentNull(nameof(argumentFactory))));
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IOptionBuilder"/> to the command builder using the configured builder.
        /// </summary>
        /// <typeparam name="T">Type of the option argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="aliases">Aliases of the option. At least one must be defined. They cannot be <see langword="null"/>, empty, or contain whitespaces only, after removing the prefixes.</param>
        /// <param name="optionBuilder">Option builder configuration.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException">Either <paramref name="aliases"/> is empty or contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/>, <paramref name="aliases"/>, or <paramref name="optionBuilder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder AddOption<T>(this CommandBuilder builder, IEnumerable<string> aliases, Action<OptionBuilder<T>> optionBuilder)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (optionBuilder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(optionBuilder));
            }

            var newOptionBuilder = new OptionBuilder<T>(aliases);
            optionBuilder(newOptionBuilder);
            builder.Options.Add(newOptionBuilder);
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IOptionBuilder"/> to the command builder using the specified option type.
        /// </summary>
        /// <typeparam name="TOption">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        /// <remarks><typeparamref name="TOption"/> must derive from <see cref="Option"/>, contain a parameterless constructor and be ready to be used on construction.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddOptionFrom<TOption>(this CommandBuilder builder)
            where TOption : Option, new()
        {
            return builder.AddOptionFrom(BuildNewOption);

            static Option BuildNewOption()
            {
                return new TOption();
            }
        }

        /// <summary>
        ///     Adds an <see cref="IOptionBuilder"/> to the command builder using the specified option.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="option">Option to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="option"/> is <see langword="null"/>.</exception>
        /// <remarks><paramref name="option"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddOptionFrom(this CommandBuilder builder, Option option)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Options.Add(new InstanceOptionBuilder(option ?? throw Exceptions.BuildArgumentNull(nameof(option))));
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="IOptionBuilder"/> to the command builder using the specified option factory.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="optionFactory">Option factory to generate the option to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="optionFactory"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder AddOptionFrom(this CommandBuilder builder, Func<Option> optionFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Options.Add(new FunctionOptionBuilder(optionFactory ?? throw Exceptions.BuildArgumentNull(nameof(optionFactory))));
            return builder;
        }

        /// <summary>
        ///     Adds a global <see cref="IOptionBuilder"/> to the command builder using the configured builder.
        /// </summary>
        /// <typeparam name="T">Type of the global option argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="aliases">Aliases of the global option. At least one must be defined. They cannot be <see langword="null"/>, empty, or contain whitespaces only, after removing the prefixes.</param>
        /// <param name="optionBuilder">Global option builder configuration.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException">Either <paramref name="aliases"/> is empty or contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/>, <paramref name="aliases"/>, or <paramref name="optionBuilder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder AddGlobalOption<T>(this CommandBuilder builder, IEnumerable<string> aliases, Action<OptionBuilder<T>> optionBuilder)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (optionBuilder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(optionBuilder));
            }

            var newOptionBuilder = new OptionBuilder<T>(aliases);
            optionBuilder(newOptionBuilder);
            builder.GlobalOptions.Add(newOptionBuilder);
            return builder;
        }

        /// <summary>
        ///     Adds a global <see cref="IOptionBuilder"/> to the command builder using the specified option type.
        /// </summary>
        /// <typeparam name="TOption">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        /// <remarks><typeparamref name="TOption"/> must derive from <see cref="Option"/>, contain a parameterless constructor and be ready to be used on construction.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddGlobalOptionFrom<TOption>(this CommandBuilder builder)
            where TOption : Option, new()
        {
            return builder.AddGlobalOptionFrom(BuildNewOption);

            static Option BuildNewOption()
            {
                return new TOption();
            }
        }

        /// <summary>
        ///     Adds a global <see cref="IOptionBuilder"/> to the command builder using the specified global option.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="option">Global option to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="option"/> is <see langword="null"/>.</exception>
        /// <remarks><paramref name="option"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddGlobalOptionFrom(this CommandBuilder builder, Option option)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .GlobalOptions.Add(new InstanceOptionBuilder(option ?? throw Exceptions.BuildArgumentNull(nameof(option))));
            return builder;
        }

        /// <summary>
        ///     Adds a global <see cref="IOptionBuilder"/> to the command builder using the specified global option factory.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="optionFactory">Global option factory to generate the global option to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="optionFactory"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder AddGlobalOptionFrom(this CommandBuilder builder, Func<Option> optionFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .GlobalOptions.Add(new FunctionOptionBuilder(optionFactory ?? throw Exceptions.BuildArgumentNull(nameof(optionFactory))));
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="ICommandBuilder"/> to the command builder using the configured builder.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="name">The name of the command. It cannot be <see langword="null"/>, empty or contain whitespace after removing the prefix.</param>
        /// <param name="commandBuilder">Command builder configuration.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/>, empty or contains whitespace after removing the prefix.</exception>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="commandBuilder"/> is <see langword="null"/>.</exception>
        public static CommandBuilder AddCommand(this CommandBuilder builder, string name, Action<CommandBuilder> commandBuilder)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (commandBuilder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(commandBuilder));
            }

            var newCommandBuilder = new CommandBuilder(name);
            commandBuilder(newCommandBuilder);
            builder.Commands.Add(newCommandBuilder);
            return builder;
        }

        /// <summary>
        ///     Adds an <see cref="ICommandBuilder"/> to the command builder using the specified command type.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        /// <remarks><typeparamref name="TCommand"/> must derive from <see cref="Command"/>, contain a parameterless constructor and be ready to be used on construction.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddCommandFrom<TCommand>(this CommandBuilder builder)
            where TCommand : Command, new()
        {
            return builder.AddCommandFrom(BuildNewCommand);

            static Command BuildNewCommand()
            {
                return new TCommand();
            }
        }

        /// <summary>
        ///     Adds an <see cref="ICommandBuilder"/> to the command builder using the specified command.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="command">Command to add to the built command. Cannot be a root command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException"><paramref name="command"/> is a root command.</exception>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="command"/> is <see langword="null"/>.</exception>
        /// <remarks><paramref name="command"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddCommandFrom(this CommandBuilder builder, Command command)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (command is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(command));
            }
            else if (command is RootCommand _)
            {
                throw Exceptions.BuildArgumentCommandIsRoot(nameof(command));
            }
            else
            {
                builder.Commands.Add(new InstanceCommandBuilder(command));
                return builder;
            }
        }

        /// <summary>
        ///     Adds an <see cref="ICommandBuilder"/> to the command builder using the specified command factory.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="commandFactory">Command factory to generate the command to add to the built command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="commandFactory"/> is <see langword="null"/>.</exception>
        /// <remarks>The <paramref name="commandFactory"/> must create a non-root command or an exception will be thrown when the command is built.</remarks>
        [CLSCompliant(false)]
        public static CommandBuilder AddCommandFrom(this CommandBuilder builder, Func<Command> commandFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Commands.Add(new FunctionCommandBuilder(commandFactory ?? throw Exceptions.BuildArgumentNull(nameof(commandFactory))));
            return builder;
        }

        /// <summary>
        ///     Adds a validator to <see cref="CommandBuilder.Validators"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="validator">Validator to use in the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="validator"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder AddValidator(this CommandBuilder builder, ValidateSymbol<CommandResult> validator)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Validators.Add(validator ?? throw Exceptions.BuildArgumentNull(nameof(validator)));
            return builder;
        }

        /// <summary>
        ///     Uses the specified handler for the command.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="handler">The handler for the command.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="handler"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder UseHandler(this CommandBuilder builder, ICommandHandler handler)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Handler = handler ?? throw Exceptions.BuildArgumentNull(nameof(handler));
            return builder;
        }

        /// <summary>
        ///     Adds a build configuration to <see cref="CommandBuilder.BuildConfigurations"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <param name="configuration">Build configuration to add for the command. Configurations are actions to be made onto the created command while building.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="configuration"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandBuilder AddBuildConfiguration(this CommandBuilder builder, Action<Command> configuration)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .BuildConfigurations.Add(configuration ?? throw Exceptions.BuildArgumentNull(nameof(configuration)));
            return builder;
        }
    }
}