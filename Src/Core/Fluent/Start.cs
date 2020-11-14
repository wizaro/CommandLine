// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.CommandLine.Builder;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Methods to help build a command-line parser in a fluent language.
    /// </summary>
    /// <threadsafety static="true"/>
    public static class Start
    {
        /// <summary>
        ///     Defines the command-line interface using the configured root command.
        /// </summary>
        /// <param name="rootBuilder">Root command configuration.</param>
        /// <returns>Command-line interface builder for configuration.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="rootBuilder"/> is <see langword="null"/>.</exception>
        /// <example>
        ///     <para>The following example shows how to define a command line using the fluent API:</para>
        ///     <code source="..\..\..\Xmpl\Core\Fluent\FluentExample.cs" lang="C#" />
        /// </example>
        [CLSCompliant(false)]
        public static CommandLineBuilder DefineCommandLine(Action<CommandBuilder> rootBuilder)
        {
            if (rootBuilder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(rootBuilder));
            }

            var newRootCommandBuilder = new CommandBuilder();
            rootBuilder(newRootCommandBuilder);
            return DefineCommandLineFrom((RootCommand)newRootCommandBuilder.Build());
        }

        /// <summary>
        ///     Defines the command-line interface using the specified root command type.
        /// </summary>
        /// <typeparam name="TRoot">Root command type.</typeparam>
        /// <returns>Command-line interface builder for configuration.</returns>
        /// <remarks><typeparamref name="TRoot"/> must derive from <see cref="RootCommand"/>, contain a parameterless constructor and be ready to be used on construction.</remarks>
        [CLSCompliant(false)]
        public static CommandLineBuilder DefineCommandLineFrom<TRoot>()
            where TRoot : RootCommand, new()
        {
            return DefineCommandLineFrom(new TRoot());
        }

        /// <summary>
        ///     Defines the command line interface using the specified root command.
        /// </summary>
        /// <param name="rootCommand">Root command of the interface.</param>
        /// <returns>Command-line interface builder for configuration.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="rootCommand"/> is <see langword="null"/>.</exception>
        /// <remarks><paramref name="rootCommand"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandLineBuilder DefineCommandLineFrom(RootCommand rootCommand)
        {
            return rootCommand is null ? throw Exceptions.BuildArgumentNull(nameof(rootCommand)) : new CommandLineBuilder(rootCommand);
        }

        /// <summary>
        ///     Defines the command line interface using the specified root command factory.
        /// </summary>
        /// <param name="rootFactory">Root command factory for the interface.</param>
        /// <returns>Command-line interface builder for configuration.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="rootFactory"/> is <see langword="null"/>.</exception>
        /// <remarks>The command built by <paramref name="rootFactory"/> must be ready to be used.</remarks>
        [CLSCompliant(false)]
        public static CommandLineBuilder DefineCommandLineFrom(Func<RootCommand> rootFactory)
        {
            return DefineCommandLineFrom((rootFactory ?? throw Exceptions.BuildArgumentNull(nameof(rootFactory))).Invoke());
        }
    }
}