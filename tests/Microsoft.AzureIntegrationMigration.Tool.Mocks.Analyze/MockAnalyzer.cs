// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.Runner.Core;
using Microsoft.Extensions.Logging;

namespace Microsoft.AzureIntegrationMigration.Tool.Mocks.Analyze
{
    /// <summary>
    /// Defines a mock analyze stage runner.
    /// </summary>
    public class MockAnalyzer : IStageAnalyzer, IStageRunner
    {
        /// <summary>
        /// Defines the priority.
        /// </summary>
        private int _priority;

        /// <summary>
        /// Defines a value indicating whether to skip execution of this runner.
        /// </summary>
        private bool _skip;

        /// <summary>
        /// Defines a logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockAnalyzer"/> class with a generic logger.
        /// </summary>
        /// <param name="logger">A logger.</param>
        public MockAnalyzer(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region IStageAnalyzer Interface Implementation

        /// <summary>
        /// Gets the name of the stage runner.
        /// </summary>
        public string Name => "Mocks.MockAnalyzer";

        /// <summary>
        /// Gets or sets the priority of the stage runner.
        /// </summary>
        public int Priority { get => _priority; set => _priority = value; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip execution of this runner.
        /// </summary>
        public bool Skip { get => _skip; set => _skip = value; }

        /// <summary>
        /// Gets the Discover stage.
        /// </summary>
        public Stages Stages => Stages.Analyze;

        /// <summary>
        /// Runs the stage runner.
        /// </summary>
        /// <param name="state">The execution state.</param>
        /// <param name="token">A cancellation token used to cancel this operation.</param>
        /// <returns>A task used to await the operation.</returns>
        public async Task RunAsync(IRunState state, CancellationToken token)
        {
            _logger.LogInformation("Mock analyze {Type} plugin running.", GetType().FullName);

            // Print args
            foreach (var arg in state.Configuration.Args)
            {
                if (arg.Key.StartsWith(Name))
                {
                    _logger.LogInformation("Arg key: {Key}, value: {Value}", arg.Key, arg.Value);
                }
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
